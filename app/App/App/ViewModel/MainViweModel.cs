namespace App.ViewModel
{
    public class MainViweModel
    {
        #region ViewModels
        public LoginViewModel ViewModelLogin { get; set; }
        #endregion

        #region Contructors
        public MainViweModel()
        {
            this.ViewModelLogin = new LoginViewModel();
        }
        #endregion
    }
}
