using System;
using System.Reactive.Linq;
using BCC.Pharm.Business.Commands;
using BCC.Pharm.Shared.Contracts.Data;
using BCC.Pharm.Shared.Dtos;
using DynamicData.Binding;
using ReactiveUI;

namespace BCC.Pharm.App.ViewModels
{
    public class MedicationItemViewModel : BaseViewModel
    {
        public MedicationDto Model { get; }

        public MedicationItemViewModel(MedicationDto model)
        {
            Model = model;

            this.WhenAnyPropertyChanged(nameof(Price), nameof(Quantity))
                .Subscribe(async vm =>
                {
                     await Mediator.Send(new UpdateMedication.Command(vm.Model));
                });
        }

        public string ActiveSubstance => Model.ActiveSubstance;

        public string Name => Model.Name;

        public decimal Price
        {
            get => Model.Price;
            set
            {
                Model.Price = value;
                this.RaisePropertyChanged(nameof(Price));
            }
        }

        public int Quantity
        {
            get => Model.Quantity;
            set
            {
                Model.Quantity = value;
                this.RaisePropertyChanged(nameof(Quantity));
            }
        }
    }
}