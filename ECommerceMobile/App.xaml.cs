using ECommerceMobile.Pages;
using ECommerceMobile.Service;
using Xamarin.Forms;

namespace ECommerceMobile
{
    public partial class App : Application
    {

        #region Attributes

        private DataService dataService;
        #endregion

        #region Properties
        public static NavigationPage Navigator { get; set; }
        public static MasterPage Master { get; set; }

        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();

            dataService = new DataService();

            //Aqui busco o pregunto si hay un usuario logiado en la bd:
            var user = dataService.GetUser();

            //aqui pregunto si el user esta con recuerdo(en bd)
            if (user != null && user.IsRemembered)
            {
                MainPage = new MasterPage();
            }
            else
            {
                MainPage = new LoginPage();

            }

        }
        #endregion



        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
