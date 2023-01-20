using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Uno.Extensions.Reactive;

namespace UnoNavDialogTest.Presentation;

[ReactiveBindable(false)]
public partial class MainViewModel: ObservableObject
{
    readonly INavigator _navigator;
    [ObservableProperty]
    string? _result = "123";
    public string? Title { get; }

    public ICommand GoToSecondCommand { get; }
    public ICommand AuthCommand { get; }
    public ICommand AuthForResultCommand { get; }

    public MainViewModel(
		INavigator navigator,
		IOptions<AppConfig> appInfo)
	{ 
	
		_navigator = navigator;
		Title = $"Main - {appInfo?.Value?.Title}";
        GoToSecondCommand = new AsyncRelayCommand(GoToSecond);
        AuthCommand = new AsyncRelayCommand(Auth);
        AuthForResultCommand = new AsyncRelayCommand(AuthForResult);
    }

    public async Task GoToSecond(CancellationToken cancellation)
	{
		await _navigator.NavigateViewModelAsync<SecondViewModel>(this, cancellation: cancellation);
	}

    async Task Auth(CancellationToken cancellation)
    {
        await _navigator.NavigateViewModelAsync<AuthTokenViewModel>(this, qualifier: Qualifiers.Dialog, cancellation: cancellation);
        Result = "NOAUTH";
    }

    async Task AuthForResult(CancellationToken cancellation)
    {
        var response = await _navigator.NavigateViewModelForResultAsync<AuthTokenViewModel, string>(this, qualifier: Qualifiers.Dialog, cancellation: cancellation).AsResult();
        Result = response.SomeOrDefault();
    }
}
