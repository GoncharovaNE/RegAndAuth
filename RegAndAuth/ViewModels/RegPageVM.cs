using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegAndAuth.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Avalonia;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System.Text.RegularExpressions;
using Tmds.DBus.Protocol;

namespace RegAndAuth.ViewModels
{
    internal class RegPageVM: ViewModelBase
    {

        List<User> _usersList = MainWindowViewModel.myConnection.Users.Include(x => x.RoleNavigation).ToList();
        public List<User> UsersList { get => _usersList; set => this.RaiseAndSetIfChanged(ref _usersList, value); }

        User _newUser = new User();

        public User NewUser { get => _newUser; set => this.RaiseAndSetIfChanged(ref _newUser, value); }

        string _newPassword;

        public string NewPassword { get => _newPassword; set => _newPassword = value; }       

        public async void Adduser()
        {
            if (NewUser.Fio != null && NewUser.Login != null)
            {
                if (UsersList.Any(x => x.Login == NewUser.Login))
                {
                    string Messege = "Пользователь с таким логином уже существует!";
                    ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение с уведомлением!", Messege, ButtonEnum.Ok).ShowAsync();
                }
                else
                {
                    List<Role> roles = MainWindowViewModel.myConnection.Roles.ToList();
                    if (!UsersList.Any(x => x.Role == 1))
                    {
                        NewUser.Role = 1;
                        NewUser.RoleNavigation = roles[0];
                    }
                    else
                    {
                        NewUser.Role = 2;
                        NewUser.RoleNavigation = roles[1];
                    }
                    if (_newPassword != string.Empty)
                    {                        
                        if (checkPassword(_newPassword) == "Пароль корректен.")
                        {
                            string Messege = "Пароль корректен!";
                            ButtonResult result1 = await MessageBoxManager.GetMessageBoxStandard("Сообщение с уведомлением!", Messege, ButtonEnum.Ok).ShowAsync();
                            NewUser.Password = MD5.HashData(Encoding.ASCII.GetBytes(NewPassword));
                            MainWindowViewModel.myConnection.Users.Add(NewUser);
                            MainWindowViewModel.myConnection.SaveChanges();
                            Messege = "Регистрация прошла успешно!";
                            ButtonResult result2 = await MessageBoxManager.GetMessageBoxStandard("Сообщение с уведомлением!", Messege, ButtonEnum.Ok).ShowAsync();
                            MainWindowViewModel.Instance.Uc = new MainPage();
                        }
                        else
                        {
                            string Messege = checkPassword(_newPassword);
                            ButtonResult result1 = await MessageBoxManager.GetMessageBoxStandard("Сообщение с уведомлением!", Messege, ButtonEnum.Ok).ShowAsync();
                        }                        
                    }
                    else
                    {
                        string Messege = "Поле с паролем является обязательным!";
                        ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение с уведомлением!", Messege, ButtonEnum.Ok).ShowAsync();
                    }                    
                }                
            }
            else
            {
                string Messege = "Необходимо заполнить все поля!";
                ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение с уведомлением!", Messege, ButtonEnum.Ok).ShowAsync();
            }            
        }

        public string checkPassword(string pass)
        {
            Regex reg = new Regex(@"^(?=(.*[A-Z]){2,})(?=(.*[a-z]){3,})(?=(.*\d){2,})(?!^\d)[A-Za-z\d]{8,}$");
            if (pass.Length < 8)
                return "Пароль должен быть не менее 8 символов.";

            if (!Regex.IsMatch(pass, @"[A-Z].*[A-Z]"))
                return "Пароль должен содержать не менее 2 заглавных латинских символов.";

            if (!Regex.IsMatch(pass, @"[a-z].*[a-z].*[a-z]"))
                return "Пароль должен содержать не менее 3 строчных латинских символов.";

            if (!Regex.IsMatch(pass, @"\d.*\d"))
                return "Пароль должен содержать не менее 2 цифр.";

            if (Regex.IsMatch(pass, @"^\d"))
                return "Пароль не должен начинаться с цифры.";

            if (!reg.IsMatch(pass))
                return "Пароль содержит некорректные символы.";
            return "Пароль корректен.";
        }

        public async Task AddOnePhoto()
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desctop || desctop.MainWindow?.StorageProvider is not { } provider) throw new NullReferenceException("провайдер отсутствует");

            var file = await provider.OpenFilePickerAsync(
                new FilePickerOpenOptions()
                {
                    Title = "Выберите изображение",
                    AllowMultiple = false,
                    FileTypeFilter = [FilePickerFileTypes.All, FilePickerFileTypes.ImageAll]
                }
                );
            if (file != null)
            {
                await using var readStream = await file[0].OpenReadAsync();
                byte[] buffer = new byte[readStream.Length];
                readStream.ReadAtLeast(buffer, 1);
                NewUser.Image = buffer;

                MainWindowViewModel.myConnection.SaveChanges();
                string Messege = "Изображение успешно добавлено!";
                ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение с уведомлением!", Messege, ButtonEnum.Ok).ShowAsync();
            }
        }

        public void ToMainPage()
        {
            MainWindowViewModel.Instance.Uc = new MainPage();
        }
    }
}
