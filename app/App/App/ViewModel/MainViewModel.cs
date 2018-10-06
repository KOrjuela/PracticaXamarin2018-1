namespace App.ViewModel
{
    using global::App.Model.Entities;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class MainViewModel
    {
        #region Properties
        public List<Land> ListLands { get; set; }

        public TokenResponse Token { get; set; }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
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
            this.LoadMenuItems();
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

        #region Methods

        private void LoadMenuItems()
        {
            this.MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new MenuItemViewModel
                {
                    Icon = "ic_settings",
                    PageName = "MyProfilePage",
                    Title = "My Profile"
                },

                new MenuItemViewModel
                {
                    Icon = "ic_insert_chart",
                    PageName = "StaticsPage",
                    Title = "Statics"
                },

                new MenuItemViewModel
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Log Out"
                }
            };

        }
        #endregion
    }
}
