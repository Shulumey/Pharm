using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Windows;
using ReactiveUI;

namespace BCC.Pharm.App
{
    public class AppDefaultExceptionHandler : IObserver<Exception>
    {
        public void OnNext(Exception exc) => Handle(exc);

        public void OnError(Exception exc) => Handle(exc);

        public void OnCompleted()
        {
        }

        private void Handle(Exception exc)
        {
            if (Debugger.IsAttached) Debugger.Break();

            RxApp.MainThreadScheduler.Schedule(() => MessageBox.Show(exc.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error));
        }
    }
}