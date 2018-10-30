namespace App.ViewModel
{
    using BaseViewModels.Utility;
    using GalaSoft.MvvmLight.Command;
    using global::App.Helpers;
    using global::App.Resources;
    using global::App.Services;
    using global::App.View;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService _apiService;

        private DataService _apiDataService;
        #endregion

        #region Attributes
        private string _password;
        private string _email;
        private bool _isRunning;
        private bool _isEnabled;
        #endregion

        #region Properties
        public string Email
        {
            get { return this._email; }
            set { this.UpdateValueProperty(ref this._email, value); }
        }

        public string Password
        {
            get { return this._password; }
            set { this.UpdateValueProperty(ref this._password, value); }
        }

        public bool IsRunning
        {
            get { return this._isRunning; }
            set { this.UpdateValueProperty(ref this._isRunning, value); }
        }

        public bool IsRemembered { get; set; }

        public bool IsEnabled
        {
            get { return this._isEnabled; }
            set { this.UpdateValueProperty(ref this._isEnabled, value); }
        }
        #endregion

        #region Action
        public ICommand ActionTapLogin
        {
            get { return new RelayCommand(EventLogin); }
        }

        public ICommand ActionRegister
        {
            get { return new RelayCommand(EventRegister); }
        }


        #endregion

        #region Contructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;
            this._apiService = new ApiService();
            this._apiDataService = new DataService();
        }
        #endregion

        #region ActionEvent

        private async void EventRegister()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ViewModelRegister = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        private async void EventLogin()
        {
            this.Email = "ingkrlosorjuela@gmail.com";
            this.Password = "Krlos123*";

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.
                    DisplayAlert(
                        Languages.Error,
                        "You must enter an Email",
                        Languages.Accept
                    );

                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.
                    DisplayAlert(
                       Languages.Error,
                        "You must enter an Password",
                        Languages.Accept
                    );

                this.Password = string.Empty;

                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            /// Validete internet connection

            var connection = await this._apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = false;

                await Application.Current.MainPage.
                        DisplayAlert(
                        Languages.Error,
                        connection.Message,
                        Languages.Accept
                    );

                return;
            }

            var apiLandsAzure = Application.Current.Resources["APILandsAzure"].ToString();

            var token = await this._apiService.
                GetToken(
                    apiLandsAzure,
                    this.Email,
                    this.Password
                );

            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = false;

                await Application.Current.MainPage.
                    DisplayAlert(
                        Languages.Error,
                        "Something was wrong, please try later.",
                        Languages.Accept
                    );

                this.Password = string.Empty;

                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = false;

                await Application.Current.MainPage.
                    DisplayAlert(
                        Languages.Error,
                        token.ErrorDescription,
                        Languages.Accept
                    );

                this.Password = string.Empty;

                return;
            }

            
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token.AccessToken;
            mainViewModel.TokenType = token.TokenType;

            mainViewModel.UserSesion = new Model.Entities.UserLocal
            {
                Email = this.Email,
                FirstName = "Carlos Augusto",
                LastName = "Orjuela",
                UserId = 1,
                UserTypeId = 1,
                Telephone = "3219620027"
            };     

            if (this.IsRemembered)
            {
                /// Guardamos daron en persistencia
                Settings.Token = token.AccessToken;
                Settings.TokenType = token.TokenType;

                /// Eliminamos algun dato existente e insertamos uno nuevo
                this._apiDataService.DeleteAllAndInsert(mainViewModel.UserSesion);
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;

            mainViewModel.ViewModelLands = new LandsViewModel();
            /// await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
            Application.Current.MainPage = new MasterPage();

        }

        #endregion
    }


}
