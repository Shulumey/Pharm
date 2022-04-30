using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BCC.Pharm.Business.Queries;
using BCC.Pharm.Shared.Dtos;
using ReactiveUI.Fody.Helpers;

namespace BCC.Pharm.App.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        private readonly MedicationDto _medication;

        [Reactive]
        public ObservableCollection<MedicationHistoryDto> HistoryItems { get; set; }

        public string MedicationName => _medication.Name;
        public HistoryViewModel(MedicationDto medication)
        {
            _medication = medication;
        }
        protected override async Task OnLoadedAsync()
        {
            IReadOnlyCollection<MedicationHistoryDto> medicationHistory = await Mediator.Send(new GetMedicationHistory.Query(_medication.Id));
            HistoryItems = new ObservableCollection<MedicationHistoryDto>(medicationHistory);
        }
    }
}