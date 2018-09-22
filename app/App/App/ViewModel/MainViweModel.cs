namespace App.ViewModel
{
    public class MainViweModel
    {
        #region Properties
        public LoginViewModel ViewModelLogin { get; set; }

        public LandsViewModel ViewModelLands { get; set; }
        #endregion

        #region Singleton
        private static MainViweModel _intance;

        public  static MainViweModel GetInstance()
        {
            if (_intance == null)
            {
                return new MainViweModel();
            }

            return _intance;
        }
        #endregion

        #region Contructors
        public MainViweModel()
        {
            _intance = this;
            this.ViewModelLogin = new LoginViewModel();
        }
        #endregion
    }
}
