using DynamicMenu.ViewModels;

namespace DynamicMenu.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }
}