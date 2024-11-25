using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RegAndAuth.Models;
using RegAndAuth.Views;

namespace RegAndAuth.ViewModels
{
    internal class AuthPageVM: ViewModelBase
    {
        string _login = "";
        string _password = "";

        public string Login { get => _login; set => _login = value; }
        public string Password { get => _password; set => _password = value; }

        public void AuthUser()
        {
            byte[] _hashPassword = MD5.HashData(Encoding.ASCII.GetBytes(_password));
            User user = MainWindowViewModel.myConnection.Users.Include(x => x.RoleNavigation).FirstOrDefault(x => x.Login == _login && x.Password == _hashPassword);
            if (user != null)
            {
                MainWindowViewModel.Instance.Uc = new PersonPage();
            }
        }
    }
}
