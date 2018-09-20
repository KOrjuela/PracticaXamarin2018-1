namespace App.Infrastructure
{
    using ViewModel;

    /// <summary>
    /// Implementation locator design pattern
    /// </summary>
    public class InstanceLocator
    {
        #region Properties
        public MainViweModel ViweModelMain { get; set; }
        #endregion

        #region Contructors
        public InstanceLocator()
        {
            this.ViweModelMain = new MainViweModel();
        }
        #endregion
    }
}
