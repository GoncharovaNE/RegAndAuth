using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegAndAuth.Models;

namespace RegAndAuth.ViewModels
{
    internal class RegPageVM: ViewModelBase
    {
        User _newUser;

        public User NewUser { get => _newUser; set => this.RaiseAndSetIfChanged(ref _newUser, value); }

        string _newPassword;

        public string NewPassword { get => _newPassword; set => _newPassword = value; }

        public void Adduser()
        {

        }
    }
}
