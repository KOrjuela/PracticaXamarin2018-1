namespace App.ViewModel
{
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
        public ICommand OnClickLogin { get; set; }
        #endregion

        #region Contructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
        }
        #endregion
    }
}
