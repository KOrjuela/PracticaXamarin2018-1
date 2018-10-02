namespace App.ViewModel
{
    using global::App.Model.Entities;
    using System.Collections.Generic;

    public class MainViewModel
    {
        #region Properties
        public List<Land> ListLands { get; set; }
        #endregion

        #region ViewModels
        public LoginViewModel ViewModelLogin { get; set; }

        public LandsViewModel ViewModelLands { get; set; }

        public LandViewModel ViewModelLand { get; set; }
        #endregion
            
        #region Contructors
        public MainViewModel()
        {
            _intance = this;
            this.ViewModelLogin = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel _intance;

        public static MainViewModel GetInstance()
        {
            if (_intance == null)
            {
                return new MainViewModel();
            }

            return _intance;
        }
        #endregion
    }
}
