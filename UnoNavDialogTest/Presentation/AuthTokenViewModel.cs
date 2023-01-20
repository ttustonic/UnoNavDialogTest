using Uno.Extensions.Reactive;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace UnoNavDialogTest.Presentation;

[ReactiveBindable(false)]
public partial class AuthTokenViewModel : ObservableObject
{
    INavigator Navigator { get; }

    [ObservableProperty]
    string? _authToken;

    public AuthTokenViewModel(INavigator navigator)
    {
        Navigator = navigator;
        AuthToken = "Token CT 123";
    }

    [RelayCommand]
    public async Task Save()
    {
        await Navigator.NavigateBackWithResultAsync(this, data: AuthToken);
    }
}
