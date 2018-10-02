namespace App.ViewModel
{
    using BaseViewModels.Utility;
    using global::App.Model.Entities;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class LandViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<Border> _listBorders;

        private ObservableCollection<Currency> _listCurrencies;

        private ObservableCollection<Language> _listLanguages;
        #endregion

        #region Propperties
        public Land Land { get; set; }

        public ObservableCollection<Border> ListBorders
        {
            get { return this._listBorders; }
            set { this.UpdateValueProperty(ref this._listBorders, value); }
        }

        public ObservableCollection<Currency> ListCurrencies
        {
            get { return this._listCurrencies; }
            set { this.UpdateValueProperty(ref this._listCurrencies, value); }
        }

        public ObservableCollection<Language> ListLanguages
        {
            get { return this._listLanguages; }
            set { this.UpdateValueProperty(ref this._listLanguages, value); }
        }
        #endregion

        #region Constructor
        public LandViewModel(Land landSelected)
        {
            this.Land = landSelected;
            this.LoadBorders();
            this.ListCurrencies = new ObservableCollection<Currency>(this.Land.Currencies);
            this.ListLanguages = new ObservableCollection<Language>(this.Land.Languages);
        }
        #endregion

        #region Methods
        private void LoadBorders()
        {
            this.ListBorders = new ObservableCollection<Border>();

            if (this.Land.Borders != null && this.Land.Borders.Count > 0)
            {
                foreach (var border in this.Land.Borders)
                {
                    var land = MainViewModel.GetInstance().ListLands
                        .Where(l => l.Alpha3Code == border).FirstOrDefault();

                    if (land != null)
                    {
                        this.ListBorders.Add(new Border
                        { 
                            Code = land.Alpha3Code,
                            Name = land.Name
                        });
                    }
                }
            }
           
        }
        #endregion

    }
}
