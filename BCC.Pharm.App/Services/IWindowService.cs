using System.Windows;
using BCC.Pharm.App.ViewModels;

namespace BCC.Pharm.App.Services
{
    public interface IWindowDialogService
    {
        bool? ShowDialog<TViewModel>(string title, TViewModel dataContext) where TViewModel : BaseViewModel;
    }

    public class  DefaultWindowDialogService: IWindowDialogService
    {
        public bool? ShowDialog<TViewModel>(string title, TViewModel dataContext) where TViewModel : BaseViewModel
        {
            DialogWindow dialogWindow = new DialogWindow
            {
                Title = title,
                DataContext = dataContext,
                Owner = Application.Current.MainWindow
            };
            return dialogWindow.ShowDialog();
        }
    }
}