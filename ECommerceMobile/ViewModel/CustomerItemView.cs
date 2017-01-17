using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ECommerceMobile.Models;
using ECommerceMobile.Service;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Xamarin.Forms;

namespace ECommerceMobile.ViewModel
{
    public class CustomerItemView : Customer, INotifyPropertyChanged
    {

        #region Attributes

        private NavigationService navigationService;
        private NetService netService;
        private ApiService apiService;
        private DataService dataService;
        private DialogService dialogService;
        private ImageSource imageSource;

        #endregion


        #region Properties

        public ObservableCollection<DepartmentItemViewModel> Departments { get; set; }
        public ObservableCollection<City> Cities { get; set; }

        public ImageSource ImageSource {

            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
                }
            }

            get { return imageSource; }
        }

        #endregion

        #region Constructor

        public CustomerItemView()
        {
            //servicios
            navigationService = new NavigationService();
            netService = new NetService();
            dataService = new DataService();
            apiService = new ApiService();
            dialogService = new DialogService();

            //Observable collection:
            Departments = new ObservableCollection<DepartmentItemViewModel>();
            Cities = new ObservableCollection<City>();




            //LoadData:
            LoadDepartments();
            LoadCities();
        }



        #endregion

        #region Commands

        public ICommand TakePictureCommand
        {
            get { return new RelayCommand(TakePicture);}
        }

        private async void TakePicture()
        {

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await dialogService.ShowMessage("Error", "So se puede acceder a la camara.");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Photos",
                Name = "newCustomer.jpg"
            });

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            }





        }

        public ICommand CustomerDetailCommand
        {
            get
            {
                return new RelayCommand(customerDetail);
            }
        }




        private async void customerDetail()
        {
            var customerItemViewModel = new CustomerItemView()
            {
                Photo = Photo,
                FirstName = FirstName,
                LastName = LastName,
                Phone = Phone,
                Department = Department,
                City = City,
                UserName = UserName,
                Address = Address,
                CityId = CityId,
                CompanyCustomers = CompanyCustomers,
                CustomerId = CustomerId,
                DepartmentId = DepartmentId,
                IsUpdated = IsUpdated,
                Latitude = Latitude,
                Longitude = Longitude,
                Orders = Orders,
                Sales = Sales
            };

            //Singleton instancia del mismo objeto>
            var mainViewModel = MainViewModel.GetInstance();

            mainViewModel.SetCurrentCustomer(customerItemViewModel);

            await navigationService.Navigate("CustomerDetailPage");

        }

        #endregion

        #region Methods

        private async void LoadCities()
        {
            var cities = new List<City>();

            if (netService.IsConnected())
            {
                cities = await apiService.Get<City>("Cities");

                dataService.Save(cities);
            }
            else
            {
                cities = dataService.Get<City>(true);
            }

            ReloadCities(cities);
        }

        private void ReloadCities(List<City> cities)
        {
            Cities.Clear();

            foreach (var city in cities.OrderBy(c => c.Name))
            {
                Cities.Add(new CityItemViewModel()
                {
                    CityId = city.CityId,
                    Customers = city.Customers,
                    Name = city.Name,
                    DepartmentId = city.DepartmentId,
                    Department = city.Department


                });
            }
        }

        private async void LoadDepartments()
        {
            var departments = new List<Department>();

            if (netService.IsConnected())
            {
                departments = await apiService.Get<Department>("Departments");

                dataService.Save(departments);
            }
            else
            {
                departments = dataService.Get<Department>(true);
            }

            ReloadDeparments(departments);
        }

        private void ReloadDeparments(List<Department> departments)
        {
            Departments.Clear();

            foreach (var department in departments.OrderBy(d => d.Name))
            {
                Departments.Add(new DepartmentItemViewModel()
                {
                    Cities = department.Cities,
                    Customers = department.Customers,
                    DepartmentId = department.DepartmentId,
                    Name = department.Name
                });
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;



        #endregion

    }
  }
