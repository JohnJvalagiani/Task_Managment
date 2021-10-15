using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Managment_System.Server.Models
{
    public class DisplayRolesModel
    {
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<UsersModel> Users { get; set; }
    }

}
