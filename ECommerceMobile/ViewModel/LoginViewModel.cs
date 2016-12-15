using System.ComponentModel;
using System.Windows.Input;
using ECommerceMobile.Service;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace ECommerceMobile.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        #region Atttributes

        private NavigationService navigationService;

        private DialogService dialogService;

        private ApiService apiService;

        private bool isRunning;

        #endregion

        #region Properties
        public string User { get; set; }

        public string Password { get; set; }


        public bool IsRemembered { get; set; }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }


            get { return isRunning; }


        }

        #endregion

        #region Constructor

        public LoginViewModel()
        {
            navigationService = new NavigationService();

            dialogService = new Service.DialogService();

            apiService = new ApiService();

            IsRemembered = true;
        }

        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get { return  new RelayCommand(Login);}
        }



        #endregion

        #region Methods



        private async void Login()
        {


            if (string.IsNullOrEmpty(User))
            {

                //Aqui ointo los mensajes:
                await dialogService.ShowMessage("Error", "Debes ingresar un Usuario.");

                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar una Contraseña.");

                return;
            }


            //Aqui ya consumo el servicio:
            IsRunning = true;
            var response = await apiService.Login(User, Password);
            IsRunning = false;


            //pregusnto si fuanciona, y si no funciona lo dejo en el loninPage, y si funciona pasa al masterpage:
            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);

                return;
            }

            //Metodo setMainPage para cambiar al pagina principa despues del login:
            navigationService.SetMainPage();
        }

        #endregion

        #region Event
        //se utiliza para decirle al activateIndicator si es false o true:(cuando cambia la propiedad)
         public event PropertyChangedEventHandler PropertyChanged;
        #endregion


    }
}
