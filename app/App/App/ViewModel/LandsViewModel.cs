namespace App.ViewModel
{
    using BaseViewModels.Utility;
    using COrjuela.Utility.BaseServices;
    using global::App.Model.Entities;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel
    {
        #region Services
        private ApiService _apiService;
        #endregion

        #region Attributes
        private ObservableCollection<Land> _listLands;
        #endregion

        #region Properties
        public ObservableCollection<Land> ListLands
        {
            get { return this._listLands; }
            set { UpdateValueProperty(ref this._listLands, value); }
        }
        #endregion

        #region Contructors

        public LandsViewModel()
        {
            this._apiService = new ApiService();
            this.LoadLands();
        }
        #endregion

        #region Methods
        private async void LoadLands()
        {
            var connection = await this._apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                /// return to the previous Page
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this._apiService.GetList<Land>("http://restcountries.eu", "/rest", "/v2/all");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var listLands = (List<Land>)response.Result;
            this.ListLands = new ObservableCollection<Land>(listLands);
        }
        #endregion
    }
}
