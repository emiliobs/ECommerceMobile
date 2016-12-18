﻿using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ECommerceMobile.Models
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int CustomerId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Photo { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int DepartmentId { get; set; }

        public int CityId { get; set; }

        [ManyToOne]
        public Department Department { get; set; }

        [ManyToOne]
        public City City { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CompanyCustomer> CompanyCustomers { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Order> Orders { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Sale> Sales { get; set; }

        public bool IsUpdated { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string PhotoFullPath => Photo == null ? string.Empty : $"http://zulu-software.com/ECommerce{Photo.Substring(1)}";

        public override int GetHashCode()
        {
            return CustomerId;
        }

    }
}