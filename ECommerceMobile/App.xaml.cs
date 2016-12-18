using ECommerceMobile.Models;
using ECommerceMobile.Pages;
using ECommerceMobile.Service;
using ECommerceMobile.ViewModel;
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
        public static User CurrentUser
        { get; set; }

        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();

            //aqui si esta con recuerdo entra sin logiarse:


            dataService = new DataService();

            //Aqui busco o pregunto si hay un usuario logiado en la bd:
            var user = dataService.GetUser();

            //aqui pregunto si el user esta con recuerdo(en bd)
            if (user != null && user.IsRemembered)
            {

                //aqui consumo el sigleton de MainViewMOdel(lapropiedady el metodo sin instancia la clase)
                //tomo la propiedad
                var mainViewModel = MainViewModel.GetInstance();

                //aqui tomo el método:
                mainViewModel.LoadUser(user);

                //aqui cuando se haga el login con remembered le digo al currentUser el userio:
                //cuando me deslogue, saber cual es usuario a desloguiar:
                App.CurrentUser = user;

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
