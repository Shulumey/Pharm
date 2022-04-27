using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BCC.Pharm.Shared.Contracts.Data;
using BCC.Pharm.Shared.Dtos;
using MediatR;

namespace BCC.Pharm.Business.Queries
{
    /// <summary>
    /// Получение всех препаратов.
    /// </summary>
    public static class GetAllMedications
    {
        /// <inheritdoc />
        public sealed class Query: IRequest<IReadOnlyCollection<MedicationDto>>
        {
        }

        /// <inheritdoc />
        public sealed class Handler: IRequestHandler<Query ,IReadOnlyCollection<MedicationDto>>
        {
            /// <inheritdoc cref="IMedicationsDataProvider"/>
            private readonly IMedicationsDataProvider _medicationsDataProvider;
            
            public Handler(IMedicationsDataProvider medicationsDataProvider)
            {
                _medicationsDataProvider = medicationsDataProvider;
            }
            
            /// <inheritdoc />
            public async Task<IReadOnlyCollection<MedicationDto>> Handle(Query request, CancellationToken cancellationToken) =>
                await _medicationsDataProvider.GetAllMedicationsAsync(cancellationToken);
        }
    }
}