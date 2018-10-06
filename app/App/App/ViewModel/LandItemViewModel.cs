namespace App.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using global::App.Model.Entities;
    using global::App.View;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LandItemViewModel : Land
    {
        #region Action
        public ICommand ActionTapSelectItem
        {
            get { return new RelayCommand(EventSelectItemLand); }
        }
        #endregion

        #region ActionEvent
        private async void EventSelectItemLand()
        {
            MainViewModel.GetInstance().ViewModelLand = new LandViewModel(this);
            await App.Navegator.PushAsync(new LandTabbedPage());
        }
        #endregion
    }
}
