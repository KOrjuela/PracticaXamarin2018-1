namespace App.ViewModel
{
    using BaseViewModels.Utility;
    using COrjuela.Utility.BaseServices;
    using GalaSoft.MvvmLight.Command;
    using global::App.View;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private ApiService _apiService;
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
        #endregion

        #region Contructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;
            this._apiService = new ApiService();
        }
        #endregion

        #region ActionEvent

        private async void EventLogin()
        {           
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.
                    DisplayAlert(
                        "Error",
                        "You must enter an Email",
                        "Accept"
                    );

                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.
                    DisplayAlert(
                        "Error",
                        "You must enter an Password",
                        "Accept"
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
                        "Error", 
                        connection.Message, 
                        "Accept"
                    );

                return;
            }

            var token = await this._apiService.
                GetToken(
                    "https://apppruebalanapi.azurewebsites.net", 
                    this.Email, 
                    this.Password
                );

            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = false;

                await Application.Current.MainPage.
                    DisplayAlert(
                        "Error",
                        "Something was wrong, please try later.",
                        "Accept"
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
                        "Error",
                        token.ErrorDescription,
                        "Accept"
                    );

                this.Password = string.Empty;

                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;

            mainViewModel.ViewModelLands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
        }

        #endregion
    }


}
