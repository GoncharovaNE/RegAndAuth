using Avalonia.Controls;
using ReactiveUI;
using RegAndAuth.Models;

namespace RegAndAuth.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel Instance;
        public static _43pGoncharova2Context myConnection = new _43pGoncharova2Context();
            public MainWindowViewModel()
        {
            Instance = this;
        }

        UserControl _uc = new MainPage();

        public UserControl Uc { get => _uc; set => this.RaiseAndSetIfChanged(ref _uc, value); }
    }
}
