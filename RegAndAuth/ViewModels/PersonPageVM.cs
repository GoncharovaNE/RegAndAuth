using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;
using RegAndAuth.Models;
using RegAndAuth.Views;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Avalonia;

namespace RegAndAuth.ViewModels
{
    internal class PersonPageVM: ViewModelBase
    {
        public void ToMainPage()
        {
            MainWindowViewModel.Instance.Uc = new MainPage();
        }

        User _user;
        public User User { get => _user; set => this.RaiseAndSetIfChanged(ref _user, value); }
        public PersonPageVM(int id)
        {
            User = MainWindowViewModel.myConnection.Users.Include(x => x.RoleNavigation).FirstOrDefault(x => x.Iduser == id);
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
                User.Image = buffer;

                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.Uc = new PersonPage(User.Iduser);
                string Messege = "Изображение успешно изменено!";
                ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение с уведомлением!", Messege, ButtonEnum.Ok).ShowAsync();
            }
        }
    }
}