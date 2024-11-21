using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegAndAuth.Views;

namespace RegAndAuth.ViewModels
{
    internal class MainPageVM: ViewModelBase
    {
        public void ToRegPage()
        {
            MainWindowViewModel.Instance.Uc = new RegPage();
        }
        public void ToAuthPage()
        {
            MainWindowViewModel.Instance.Uc = new AuthPage();
        }
    }
}
