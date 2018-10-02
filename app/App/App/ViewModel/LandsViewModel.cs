namespace App.ViewModel
{
    using BaseViewModels.Utility;
    using COrjuela.Utility.BaseServices;
    using GalaSoft.MvvmLight.Command;
    using global::App.Model.Entities;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LandsViewModel : BaseViewModel
    {
        #region Services 
        private ApiService _apiService;
        #endregion

        #region Attributes
        private bool _isRefreshing;

        private string _textFilter;

        private List<Land> _resultListLands;

        private ObservableCollection<LandItemViewModel> _listLands;
        #endregion

        #region Properties
        public ObservableCollection<LandItemViewModel> ListLands
        {
            get { return this._listLands; }
            set { UpdateValueProperty(ref this._listLands, value); }
        }

        public bool IsRefreshing
        {
            get { return this._isRefreshing; }
            set { UpdateValueProperty(ref this._isRefreshing, value); }
        }

        public string TextFilter
        {
            get { return this._textFilter; }
            set
            {
                UpdateValueProperty(ref this._textFilter, value);
                this.EventSearch();
            }
        }
        #endregion

        #region Action
        public ICommand ActionRefreshList
        {
            get { return new RelayCommand(EventRefreshList); }
        }

        public ICommand ActionSearchInList
        {
            get { return new RelayCommand(EventSearch); }
        }
        #endregion

        #region Contructors
        public LandsViewModel()
        {
            this._apiService = new ApiService();
            this.LoadLands();
        }
        #endregion

        #region ActionEvent
        private void EventRefreshList() => this.LoadLands();

        private void EventSearch()
        {
            if (string.IsNullOrEmpty(this.TextFilter))
            {
                 this.ListLands = new ObservableCollection<LandItemViewModel>(this.ConvertToLandItemViewModel());
            }
            else
            {
                this.ListLands = new ObservableCollection<LandItemViewModel>
                    (this.ConvertToLandItemViewModel().Where(
                        l => l.Name.ToLower().Contains(this.TextFilter.ToLower())
                          || l.Capital.ToLower().Contains(this.TextFilter.ToLower())
                    ));
            }
        }
        #endregion

        #region Methods
        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await this._apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                /// return to the previous Page
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this._apiService.GetList<Land>("http://restcountries.eu", "/rest", "/v2/all");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            this._resultListLands = (List<Land>)response.Result;
            this.ListLands = new ObservableCollection<LandItemViewModel>(this.ConvertToLandItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<LandItemViewModel> ConvertToLandItemViewModel()
        {
            if (this._resultListLands != null && this._resultListLands.Count > 0)
            {
                return this._resultListLands.Select(l => new LandItemViewModel
                {
                    Alpha2Code = l.Alpha2Code,
                    Alpha3Code = l.Alpha3Code,
                    AltSpellings = l.AltSpellings,
                    Area = l.Area,
                    Borders = l.Borders,
                    CallingCodes = l.CallingCodes,
                    Capital = l.Capital,
                    Cioc = l.Cioc,
                    Currencies = l.Currencies,
                    Demonym = l.Demonym,
                    Flag = l.Flag,
                    Gini = l.Gini,
                    Languages = l.Languages,
                    Latlng = l.Latlng,
                    Name = l.Name,
                    NativeName = l.NativeName,
                    NumericCode = l.NumericCode,
                    Population = l.Population,
                    Region = l.Region,
                    RegionalBlocs = l.RegionalBlocs,
                    Subregion = l.Subregion,
                    Timezones = l.Timezones,
                    TopLevelDomain = l.TopLevelDomain,
                    Translations = l.Translations,
                });
            }

            return null;
        }
        #endregion
    }
}
