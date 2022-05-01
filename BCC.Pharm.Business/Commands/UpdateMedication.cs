using System.Threading;
using System.Threading.Tasks;
using BCC.Pharm.Shared;
using BCC.Pharm.Shared.Contracts;
using BCC.Pharm.Shared.Contracts.Data;
using BCC.Pharm.Shared.Dtos;
using MediatR;

namespace BCC.Pharm.Business.Commands
{
    /// <summary>
    /// Обновление данных о препарате.
    /// </summary>
    public class UpdateMedication : IRequest
    {
        /// <inheritdoc />
        public sealed class Command : IRequest
        {
            /// <summary>
            /// Препарат.
            /// </summary>
            public MedicationDto Medication { get; }

            public Command(MedicationDto medication)
            {
                Medication = medication;
            }
        }

        ///<inheritdoc/>
        public sealed class Handler: IRequestHandler<Command>
        {
            /// <inheritdoc cref="IMedicationsDataProvider"/>
            private readonly IMedicationsDataProvider _dataProvider;
            
            /// <inheritdoc cref="IObjectsComparer{MedicationDto}"/>
            private readonly IObjectsComparer<MedicationDto> _medicationsComparer;

            public Handler(IMedicationsDataProvider dataProvider, IObjectsComparer<MedicationDto> medicationsComparer)
            {
                _dataProvider = dataProvider;
                _medicationsComparer = medicationsComparer;
            }
            
            ///<inheritdoc/>
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                MedicationDto originalMedication = await _dataProvider.GetMedicationAsync(request.Medication.Id);
 
                MedicationDto[] beforeUpdated = {originalMedication};
                MedicationDto[] afterUpdated = {request.Medication};
                
                ChangeSet<MedicationDto> changeSet = _medicationsComparer.GetChangeSet(beforeUpdated, afterUpdated);
                
                await _dataProvider.UpdateAsync(changeSet.Updated, SourceChange.Manual, cancellationToken);
               
                return Unit.Value;
            }
        }
    }
}