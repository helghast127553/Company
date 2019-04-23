using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Domain
{
    public class EmployeeDomainModel
    {
        public int EmployeeID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string UserGroupID { get; set; }
        public int? EquipmentID { get; set; }
    }
}
