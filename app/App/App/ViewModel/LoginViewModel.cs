namespace App.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;

    public class LoginViewModel
    {
        #region ViewModels
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsRunning { get; set; }

        public bool IsRemembered { get; set; }
        #endregion

        #region OnClick
        public ICommand EventLogin
        {
            get
            {
                return new RelayCommand(TapEventLogin);
            }
        }

        private void TapEventLogin()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Contructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
        }
        #endregion
    }
}
