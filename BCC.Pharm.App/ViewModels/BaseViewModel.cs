using System.Threading.Tasks;
using Autofac;
using MediatR;
using ReactiveUI;
using Unit = System.Reactive.Unit;

namespace BCC.Pharm.App.ViewModels
{
    public class BaseViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, Unit> LoadedCommand { get; }

        protected IMediator Mediator => App.DependencyContainer.Resolve<IMediator>();
        
        public BaseViewModel()
        {
            LoadedCommand = ReactiveCommand.CreateFromTask(OnLoaded);
        }

        protected virtual Task OnLoaded() => Task.CompletedTask;
    }
}