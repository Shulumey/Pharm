using System.Threading.Tasks;
using BCC.Pharm.Business.Queries;
using ReactiveUI;

namespace BCC.Pharm.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        protected override async Task OnLoaded()
        {
            var result = await Mediator.Send(new GetAllMedications.Query());
        }
    }
}