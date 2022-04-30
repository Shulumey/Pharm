using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows;
using BCC.Pharm.Business.Commands;
using BCC.Pharm.Business.Queries;
using BCC.Pharm.Shared;
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
                }

                await Task.CompletedTask;
            });

            ExportToJsonCommand = ReactiveCommand.CreateFromTask(() => SaveTextFileAsync(ExportFormat.Json, "Json files (.json)|*.json"));

            ExportToXmlCommand = ReactiveCommand.CreateFromTask(() => SaveTextFileAsync(ExportFormat.Xml, "Xml files (.xml)|*.xml"));

            RefreshCommand = ReactiveCommand.CreateFromTask(OnLoadedAsync);
        }
        
        protected override async Task OnLoadedAsync()
        {
            var result = await Mediator.Send(new GetAllMedications.Query());
            Medications = new ObservableCollection<MedicationDto>(result);
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
                string serializedData = await Mediator.Send(new ExportMedicationsQuery.Query(data, format));
                {
                    File.WriteAllText(saveFileDialog.FileName, serializedData);
                    MessageBox.Show("Файл успешно выгружен", "Выгрузка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        [Reactive]
        public ObservableCollection<MedicationDto> Medications { get; set; }

        public ReactiveCommand<Unit, Unit> ImportCommand { get; }
        public ReactiveCommand<Unit, Unit> RefreshCommand { get; }
        public ReactiveCommand<Unit, Unit> ExportToJsonCommand { get; }
        public ReactiveCommand<Unit, Unit> ExportToXmlCommand { get; }
    }
}