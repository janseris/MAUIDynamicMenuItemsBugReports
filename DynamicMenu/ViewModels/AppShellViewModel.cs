using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DynamicMenu.Models;
using DynamicMenu.Views;

namespace DynamicMenu.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        private readonly AppState appState;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private bool archeologieMenuButtonVisible = false;

        [ObservableProperty]
        private bool bozpMenuButtonVisible = false;

        [ObservableProperty]
        private bool monitoringMenuButtonVisible = false;

        public AppShellViewModel(AppState appState)
        {
            this.appState = appState;
            LoginViewModel.SuccessfullLogin += OnSuccessfullLogin;
        }

        private void OnSuccessfullLogin(object sender, EventArgs e)
        {
            var user = appState.CurrentUser;
            if(user is null)
            {
                GoToLogin("Neznámý uživatel");
            }
            UserName = user.Name;
            UpdateMenuItems(user.Prava);
        }

        [RelayCommand]
        async void Logout()
        {
            appState.CurrentUser = null;
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
        
        private async void GoToLogin(string message)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            await Shell.Current.DisplayAlert("Chyba", message, "OK");
        }

        private async void UpdateMenuItems(List<MenuButtonPravo> prava)
        {
            if (prava.Count == 0)
            {
                GoToLogin("Uživatel nemá žádná práva");
            }
            ArcheologieMenuButtonVisible = prava.Contains(MenuButtonPravo.Archeologie);
            BozpMenuButtonVisible = prava.Contains(MenuButtonPravo.BOZP);
            MonitoringMenuButtonVisible = prava.Contains(MenuButtonPravo.Monitoring);
            GoToPage(prava.First());
        }


        private async void GoToPage(MenuButtonPravo pravo)
        {
            switch (pravo)
            {
                case MenuButtonPravo.Archeologie:
                    await Shell.Current.GoToAsync($"//{nameof(ArcheologiePage)}");
                    break;
                case MenuButtonPravo.BOZP:
                    await Shell.Current.GoToAsync($"//{nameof(BozpPage)}");
                    break;
                case MenuButtonPravo.Monitoring:
                    await Shell.Current.GoToAsync($"//{nameof(MonitoringPage)}");
                    break;
            }
        }
    }
}
