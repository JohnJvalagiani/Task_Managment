using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Managment_System.Core.Models;

namespace Domain.Entities
{
    public class task:BaseEntity
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedUser { get; set; }
    }
}
