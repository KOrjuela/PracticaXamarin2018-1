namespace App.Infrastructure
{
    using ViewModel;

    /// <summary>
    /// Implementation locator design pattern
    /// </summary>
    public class InstanceLocator
    {
        #region Properties
        public MainViewModel MainViewModel { get; set; }
        #endregion

        #region Contructors
        public InstanceLocator()
        {
            this.MainViewModel = new MainViewModel();
        }
        #endregion
    }
}
