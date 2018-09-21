namespace App.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using Utility;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Attributes
        private string _password;

        private bool _isRunning;

        private bool _isEnable;
        #endregion

        #region ViewModels
        public string Email
        {
            get { return this._password; }
            set { UpdateValueProperty(ref this._password, value); }
        }

        public string Password { get; set; }

        public bool IsRunning
        {
            get { return this._isRunning; }
            set { UpdateValueProperty(ref this._isRunning, value); }
        }

        public bool IsRemembered { get; set; }

        public bool IsEnable
        {
            get { return this._isEnable; }
            set { UpdateValueProperty(ref this._isEnable, value); }
        }
        #endregion

        #region EventTap
        public ICommand EventLogin
        {
            get
            {
                return new RelayCommand(TapEventLogin);
            }
        }

        private async void TapEventLogin()
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

            if (this.Email != "corjuela@gmail.com" || this.Password != "1234")
            {
                await Application.Current.MainPage.
                    DisplayAlert(
                        "Error",
                        "Email or Password incorret",
                        "Accept"
                    );
                return;
            }


            await Application.Current.MainPage.
                DisplayAlert(
                    "Error",
                    "OK",
                    "Accept"
                );

        }
        #endregion

        #region Contructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnable = true;
        }
        #endregion
    }
}
