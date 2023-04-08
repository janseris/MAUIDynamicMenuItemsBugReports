using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DynamicMenu.Models;
using DynamicMenu.Services;
using DynamicMenu.Views;

namespace DynamicMenu.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        public static event EventHandler SuccessfullLogin;
        private readonly UserService service;
        private readonly AppState appState;

        public LoginViewModel(UserService service, AppState appState)
        {
            this.service = service;
            this.appState = appState;
        }

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private bool invalidVisible;


        [RelayCommand]
        void OnAppearing()
        {
            Name = "";
            InvalidVisible = false;
        }

        [RelayCommand]
        void OnNameChanged()
        {
            InvalidVisible = false;
        }


        [RelayCommand]
        async void Login()
        {
            var inputText = Name;
            var user = service.GetUserByName(inputText);
            if (user is null)
            {
                InvalidVisible = true;
                return;
            }

            appState.CurrentUser = user;
            SuccessfullLogin?.Invoke(this, EventArgs.Empty);
        }
    }
}
