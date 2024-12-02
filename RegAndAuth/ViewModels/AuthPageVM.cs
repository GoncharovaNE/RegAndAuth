using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RegAndAuth.Models;
using RegAndAuth.Views;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace RegAndAuth.ViewModels
{
    internal class AuthPageVM: ViewModelBase
    {
        string _login = "";
        string _password = "";

        public string Login { get => _login; set => _login = value; }
        public string Password { get => _password; set => _password = value; }

        public async void AuthUser()
        {
            byte[] _hashPassword = MD5.HashData(Encoding.ASCII.GetBytes(_password));
            User user = MainWindowViewModel.myConnection.Users.Include(x => x.RoleNavigation).FirstOrDefault(x => x.Login == _login && x.Password == _hashPassword);
            if (user != null)
            {
                string Messege = "Авторизация прошла успешно!";
                ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение с уведомлением!", Messege, ButtonEnum.Ok).ShowAsync();
                MainWindowViewModel.Instance.Uc = new PersonPage(user.Iduser);
            }
            else
            {
                string Messege = "Пользователь не найден!";
                ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение с уведомлением!", Messege, ButtonEnum.Ok).ShowAsync();
            }
        }

        public void ToMainPage()
        {
            MainWindowViewModel.Instance.Uc = new MainPage();
        }
    }
}
