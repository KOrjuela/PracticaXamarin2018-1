namespace App.ViewModel
{
    using global::App.Model.Entities;

    public class LandViewModel
    {
        #region Propperties
        public Land Land { get; set; }
        #endregion

        #region Constructor
        public LandViewModel(Land landSelected)
        {
            this.Land = landSelected;
        }
        #endregion

    }
}
