using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BCC.Pharm.Shared;
using BCC.Pharm.Shared.Contracts;
using BCC.Pharm.Shared.Contracts.Data;
using BCC.Pharm.Shared.Dtos;
using MediatR;

namespace BCC.Pharm.Business.Commands
{
    public class UpdateMedication : IRequest
    {
        public sealed class Command : IRequest
        {
            public MedicationDto Medication { get; }

            public Command(MedicationDto medication)
            {
                Medication = medication;
            }
        }

        ///<inheritdoc/>
        public sealed class Handler: IRequestHandler<Command>
        {
            private readonly IMedicationsDataProvider _dataProvider;
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