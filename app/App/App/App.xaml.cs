

[assembly: Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
namespace App
{
    using global::App.Helpers;
    using global::App.View;
    using global::App.ViewModel;
    using Xamarin.Forms;

    public partial class App : Application
    {
        #region Properties
        public static NavigationPage Navegator { get; internal set; }
        #endregion


        #region Constructor
        public App()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Settings.Token))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                var viewModel = MainViewModel.GetInstance();
                viewModel.Token = Settings.Token;
                viewModel.TokenType = Settings.TokenType;
                viewModel.ViewModelLands = new LandsViewModel();
                MainPage = new MasterPage();
            }
           
        }

        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion

    }
}
