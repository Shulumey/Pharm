using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows;
using BCC.Pharm.App.Services;
using BCC.Pharm.Business.Commands;
using BCC.Pharm.Business.Queries;
using BCC.Pharm.Shared;
using Microsoft.Win32;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BCC.Pharm.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IWindowDialogService _dialogService;

        public MainViewModel(IWindowDialogService dialogService)
        {
            _dialogService = dialogService;
            
            ImportCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "Xml files (.xml)|*.xml"
                };
                if (dialog.ShowDialog() == true)
                {
                    await Mediator.Send(new ImportDataFromXml.Command()
                    {
                        FilePath = dialog.FileName
                    });
                    await OnLoadedAsync();
                    MessageBox.Show("Данные успешно импортированы", "Ипорт", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                await Task.CompletedTask;
            });

            ExportToJsonCommand = ReactiveCommand.CreateFromTask(() => SaveTextFileAsync(ExportFormat.Json, "Json files (.json)|*.json"));

            ExportToXmlCommand = ReactiveCommand.CreateFromTask(() => SaveTextFileAsync(ExportFormat.Xml, "Xml files (.xml)|*.xml"));

            RefreshCommand = ReactiveCommand.CreateFromTask(OnLoadedAsync);

            ShowMedicationHistoryCommand = ReactiveCommand.Create<MedicationItemViewModel>(selectedItem =>
            {
                HistoryViewModel vm = new HistoryViewModel(selectedItem.Model);
                _dialogService.ShowDialog("История изменений", vm);
            });
        }
        
        protected override async Task OnLoadedAsync()
        {
            var result = await Mediator.Send(new GetAllMedications.Query());
            Medications = new ObservableCollection<MedicationItemViewModel>(result.Select(x => new MedicationItemViewModel(x)));
        }

        private async Task SaveTextFileAsync(ExportFormat format, string fileFilterDialog)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = fileFilterDialog
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                var data = await Mediator.Send(new GetAllMedications.Query());
                string serializedData = await Mediator.Send(new ExportMedications.Query(data, format));
                {
                    File.WriteAllText(saveFileDialog.FileName, serializedData);
                    MessageBox.Show("Файл успешно выгружен", "Выгрузка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        [Reactive]
        public ObservableCollection<MedicationItemViewModel> Medications { get; set; }

        public ReactiveCommand<Unit, Unit> ImportCommand { get; }
        public ReactiveCommand<Unit, Unit> RefreshCommand { get; }
        public ReactiveCommand<Unit, Unit> ExportToJsonCommand { get; }
        public ReactiveCommand<Unit, Unit> ExportToXmlCommand { get; }
        public ReactiveCommand<MedicationItemViewModel, Unit> ShowMedicationHistoryCommand { get; }
    }
}