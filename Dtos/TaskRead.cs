using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
   public class TaskRead
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AssignedUser { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
    }
}
