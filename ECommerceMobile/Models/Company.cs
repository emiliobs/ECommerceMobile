using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ECommerceMobile.Models
{
     public class Company
    {
        [PrimaryKey]
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public int DepartmentId { get; set; }
        public int CityId { get; set; }

        [OneToMany(CascadeOperations =  CascadeOperation.All)]
        public List<User> Users { get; set; }

        public override int GetHashCode()
        {
            return CompanyId;
        }
    }
}
