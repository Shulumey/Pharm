using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using BCC.Pharm.Business.Commands;
using BCC.Pharm.Business.Queries;
using BCC.Pharm.Shared.Dtos;
using Microsoft.Win32;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BCC.Pharm.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            ImportCommand = ReactiveCommand.CreateFromTask(() =>
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Xml files (.xml)|*.xml";
                if (dialog.ShowDialog() == true)
                {
                    return Mediator.Send(new ImportDataFromXml.Command()
                    {
                        FilePath = dialog.FileName
                    });
                }

                return Task.CompletedTask;
            });
        }
        
        protected override async Task OnLoaded()
        {
            var result = await Mediator.Send(new GetAllMedications.Query());
            Medications = new ObservableCollection<MedicationDto>(result);
        }

        [Reactive]
        public ObservableCollection<MedicationDto> Medications { get; set; }

        public ReactiveCommand<Unit, Unit> ImportCommand { get; set; }
    }
}