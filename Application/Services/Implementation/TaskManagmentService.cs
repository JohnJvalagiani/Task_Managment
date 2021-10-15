using Application.Services.Abstraction;
using AutoMapper;
using Core.Services.Abstraction;
using Domain.Entities;
using Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Managment_System.Core.Models;

namespace Application.Services.Implementation
{
    public class TaskManagmentService : ITaskManagementService
    {
        private readonly IMapper _mapper;
        private readonly IRepo<task> _repo;
        private readonly UserManager<AppUser> _userManager;

        public TaskManagmentService(UserManager<AppUser> userManager, IMapper mapper, IRepo<task> repo)
        {
            _userManager = userManager;
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<TaskRead> Create(TaskWrite task)
        {

            var theuser = await _userManager.FindByEmailAsync(task.AssignedUserName);

            if (theuser == null)
                throw new Exception($"User with email {task.AssignedUserName} not found");

          var newTask =  _mapper.Map<task>(task);

          newTask.AssignedUser = theuser.Email;

          var theTask= await  _repo.Add(newTask);

         return   _mapper.Map<TaskRead>(theTask);

        }

        public async Task<IEnumerable<TaskRead>> GetAll()
        {
            var allTasks = await _repo.GetAll();

           return  _mapper.Map<IEnumerable<TaskRead>>(allTasks);

        }

        public async Task<TaskRead> GetById(string Id)
        {
            var theTask = await _repo.GetById(Id);

            return _mapper.Map<TaskRead>(theTask);
        }

        public async Task<TaskRead> Update(TaskRead task)
        {

             await _repo.Update(_mapper.Map<task>(task));

            return task;

        }
        public async Task<bool> Detelete(string Id)
        {

            return await _repo.Remove(Id);

        }
    }
}
