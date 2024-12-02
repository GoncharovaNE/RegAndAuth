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
    }
    public PersonPage(int id)
    {
        InitializeComponent();
        DataContext = new PersonPageVM(id);
    }
}