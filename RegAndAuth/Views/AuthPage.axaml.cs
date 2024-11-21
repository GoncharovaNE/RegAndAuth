using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegAndAuth.ViewModels;

namespace RegAndAuth;

public partial class AuthPage : UserControl
{
    public AuthPage()
    {
        InitializeComponent();
        DataContext = new AuthPageVM();
    }
}