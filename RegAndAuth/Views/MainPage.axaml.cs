using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegAndAuth.ViewModels;

namespace RegAndAuth;

public partial class MainPage : UserControl
{
    public MainPage()
    {
        InitializeComponent();
        DataContext = new MainPageVM();
    }
}