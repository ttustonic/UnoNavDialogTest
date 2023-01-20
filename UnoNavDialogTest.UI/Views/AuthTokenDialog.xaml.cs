using Microsoft.Extensions.DependencyInjection;

namespace UnoNavDialogTest.Views
{
    public sealed partial class AuthTokenDialog : ContentDialog
    {
        public AuthTokenViewModel? ViewModel { get; private set; }

        public AuthTokenDialog()
        {
            this.InitializeComponent();
            this.DataContextChanged += AuthTokenDialog_DataContextChanged;

            DataContext = (Application.Current as App)!.Host.Services.GetRequiredService<AuthTokenViewModel>();
            ViewModel = DataContext as AuthTokenViewModel;
        }

        void AuthTokenDialog_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is AuthTokenViewModel vm)
            {
                ViewModel = vm;
                //Bindings.Update();
            }
        }

        async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var deferral = args.GetDeferral();
            await ViewModel!.Save();
            deferral.Complete();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
