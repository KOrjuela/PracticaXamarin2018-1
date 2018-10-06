﻿namespace App.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using global::App.View;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MenuItemViewModel
    {
        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Action
        public ICommand ActionTapItemMenu
        {
            get { return new RelayCommand(EventTapItemMenu); }
        }
        #endregion

        #region ActionEvent
        private void EventTapItemMenu()
        {
            switch (this.PageName)
            {
                case "LoginPage":
                     Application.Current.MainPage = new LoginPage();
                     break;

                default:
                    break;
            }
        }
        #endregion


        #region Method

        #endregion


    }
}
