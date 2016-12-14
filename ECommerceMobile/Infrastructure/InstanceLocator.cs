using ECommerceMobile.ViewModel;

namespace ECommerceMobile.Infrastructure
{
    public class InstanceLocator
    {
        #region Properties

        public MainViewModel Main { get; set; }


        #endregion


        #region Constructor

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
        #endregion
    }
}
