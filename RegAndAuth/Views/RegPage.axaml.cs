using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegAndAuth.ViewModels;

namespace RegAndAuth;

public partial class RegPage : UserControl
{
    public RegPage()
    {
        InitializeComponent();
        DataContext = new RegPageVM();
    }
}