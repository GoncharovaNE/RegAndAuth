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

        public void Adduser()
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
                NewUser.Password = MD5.HashData(Encoding.ASCII.GetBytes(NewPassword));                
            }
            MainWindowViewModel.myConnection.Users.Add(NewUser);
            MainWindowViewModel.myConnection.SaveChanges();
            MainWindowViewModel.Instance.Uc = new MainPage();
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
                MainWindowViewModel.Instance.Uc = new PersonPage();
            }
        }
    }
}
