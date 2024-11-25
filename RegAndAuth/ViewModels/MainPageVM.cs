using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using RegAndAuth.Models;
using RegAndAuth.Views;

namespace RegAndAuth.ViewModels
{
    internal class MainPageVM: ViewModelBase
    {
        List<User> _usersList = MainWindowViewModel.myConnection.Users.Include(x => x.RoleNavigation).ToList();

        public List<User> UsersList { get => _usersList; set => this.RaiseAndSetIfChanged(ref _usersList, value); }

        public bool isVisibleRegBTAdmin = true;
        public bool isVisibleRegBT = false;
        public bool isVisibleAuthBT = false;

        public bool IsVisibleRegBTAdmin { get => isVisibleRegBTAdmin; set => this.RaiseAndSetIfChanged(ref isVisibleRegBTAdmin, value); }
        public bool IsVisibleRegBT { get => isVisibleRegBT; set => this.RaiseAndSetIfChanged(ref isVisibleRegBT, value); }
        public bool IsVisibleAuthBT { get => isVisibleAuthBT; set => this.RaiseAndSetIfChanged(ref isVisibleAuthBT, value); }


        public MainPageVM()
        {
            if (UsersList.Any(x => x.Role == 1))
            {
                isVisibleRegBTAdmin = false;
                isVisibleRegBT = true;
                isVisibleAuthBT = true;
            }
        }
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
