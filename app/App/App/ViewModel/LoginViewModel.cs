namespace App.ViewModel
{
    using BaseViewModels.Utility;
    using GalaSoft.MvvmLight.Command;
    using global::App.View;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Attributes
        private string _password;
        private string _email;
        private bool _isRunning;
        private bool _isEnable;
        #endregion

        #region Properties
        public string Email
        {
            get { return this._email; }
            set { UpdateValueProperty(ref this._email, value); }
        }

        public string Password
        {
            get { return this._password; }
            set { UpdateValueProperty(ref this._password, value); }
        }

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

        #region ActionTap
        public ICommand TapLogin
        {
            get { return new RelayCommand(EventLogin); }
        }
        #endregion

        #region Contructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnable = true;  
        }
        #endregion

        #region ActionEvent

        private async void EventLogin()
        {
            this.Email = "corjuela@gmail.com";
            this.Password = "1234";

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
                this.Password = string.Empty;
                return;
            }

            this.Email = string.Empty;
            this.Password = string.Empty;

            MainViweModel.GetInstance().ViewModelLands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
        }

        #endregion
    }


}
