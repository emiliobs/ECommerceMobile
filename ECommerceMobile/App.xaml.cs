using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECommerceMobile.Pages;
using Xamarin.Forms;

namespace ECommerceMobile
{
    public partial class App : Application
    {

        #region Properties
        public static NavigationPage Navigator { get; set; }
        public static MasterPage Master { get; set; }

        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
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
