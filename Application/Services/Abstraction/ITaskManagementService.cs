using Application.Services.Implementation;
using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Abstraction
{
   public  interface ITaskManagementService
    {
        
        Task<TaskRead> Create(TaskWrite task);
        Task<TaskRead> Update(TaskRead task);
        Task<TaskRead> GetById(string Id);
        Task<IEnumerable<TaskRead>> GetAll();
        Task<IEnumerable<TaskRead>> Search(DetailedSearchParametrs detailedSearchParametrs);
        Task<bool> Detelete(string Id);

    }
}
