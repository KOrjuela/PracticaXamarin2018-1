namespace App.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using global::App.Helpers;
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
                    Settings.Token = string.Empty;
                    Settings.TokenType = string.Empty;

                    var viewModel = MainViewModel.GetInstance();
                    viewModel.Token = string.Empty;
                    viewModel.TokenType = string.Empty;

                    Application.Current.MainPage = new NavigationPage(new LoginPage());
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
