using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BCC.Pharm.Shared;
using BCC.Pharm.Shared.Contracts;
using BCC.Pharm.Shared.Contracts.Business;
using BCC.Pharm.Shared.Contracts.Data;
using BCC.Pharm.Shared.Dtos;
using MediatR;

namespace BCC.Pharm.Business.Commands
{
    /// <summary>
    /// Импорт данных их XML файла.
    /// </summary>
    public static class ImportDataFromXml
    {
        /// <inheritdoc />
        public sealed class Command : IRequest
        {
            /// <summary>
            /// Путь к XML-файлу.
            /// </summary>
            public string FilePath { get; set; }
        }
        
        /// <inheritdoc />
        public sealed class Handler: IRequestHandler<Command>
        {
            /// <inheritdoc cref="IMedicationsImporter"/>
            private readonly IMedicationsImporter _importDataFile;

            /// <inheritdoc cref="IObjectsComparer{MedicationDto}"/>
            private readonly IObjectsComparer<MedicationDto> _medicationsComparer;

            /// <inheritdoc cref="IMedicationsDataProvider"/>
            private readonly IMedicationsDataProvider _medicationsDataProvider;

            public Handler(IMedicationsImporter importDataFile, IObjectsComparer<MedicationDto> medicationsComparer, IMedicationsDataProvider medicationsDataProvider)
            {
                _importDataFile = importDataFile;
                _medicationsComparer = medicationsComparer;
                _medicationsDataProvider = medicationsDataProvider;
            }
            
            /// <inheritdoc />
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                using (TextReader filerReader = new StreamReader(request.FilePath))
                {
                    IReadOnlyCollection<MedicationDto> data = await _importDataFile.ReadAsync(filerReader);
                    IReadOnlyCollection<MedicationDto> originalData = await _medicationsDataProvider.GetAllMedicationsAsync(cancellationToken);

                    ChangeSet<MedicationDto> changeSet = _medicationsComparer.GetChangeSet(originalData, data.Distinct(MedicationDto.DefaultComparer).ToArray());

                    if (changeSet.Added.Any())
                    {
                        await _medicationsDataProvider.AddAsync(changeSet.Added, cancellationToken);
                    }

                    if (changeSet.Updated.Any())
                    {
                        await _medicationsDataProvider.UpdateAsync(changeSet.Updated, SourceChange.Automatically, cancellationToken);
                    }

                    if (changeSet.Removed.Any())
                    {
                        await _medicationsDataProvider.DeleteAsync(changeSet.Removed, cancellationToken);
                    }
                }

                return Unit.Value;
            }
        }
    }
}