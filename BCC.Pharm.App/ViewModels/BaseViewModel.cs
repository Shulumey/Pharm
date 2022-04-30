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

        protected IMediator Mediator => Container.Resolve<IMediator>();
        protected IContainer Container => App.DependencyContainer;
        
        public BaseViewModel()
        {
            LoadedCommand = ReactiveCommand.CreateFromTask(OnLoadedAsync);
        }

        protected virtual Task OnLoadedAsync() => Task.CompletedTask;
    }
}