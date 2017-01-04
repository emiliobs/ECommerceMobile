using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ECommerceMobile.Models;
using ECommerceMobile.Service;
using GalaSoft.MvvmLight.Command;

namespace ECommerceMobile.ViewModel
{
   public class CustomerItemView: Customer
    {

        #region Attributes

        private NavigationService navigationService;

        #endregion

        #region Constructor

        public CustomerItemView()
        {
            navigationService = new NavigationService();
        }

        #endregion

        #region Commands
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

    }
}
