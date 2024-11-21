using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RegAndAuth.ViewModels;

namespace RegAndAuth;

public partial class PersonPage : UserControl
{
    public PersonPage()
    {
        InitializeComponent();
        DataContext = new PersonPageVM();
    }
}