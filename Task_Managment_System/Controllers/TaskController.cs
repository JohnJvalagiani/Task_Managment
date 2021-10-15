using Application.Services.Abstraction;
using Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Managment_System.Server.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskManagementService _service;


        public TaskController(ITaskManagementService service)
        {

            this._service = service;

        }


        [HttpPost("Create task")]
        public async Task<IActionResult> CreateTask([FromBody] TaskWrite task)
        {

            var result = await _service.Create(task);

            return Ok(result);
        }


        [HttpGet("Get all tasks")]
        public async Task<IActionResult> GetAllTasks()
        {

            var allTasks = await _service.GetAll();

            return Ok(allTasks);

        }


        [HttpDelete("Delete task")]
        public async Task<IActionResult> DeleteTask(string Id)
        {

            var result = await _service.Detelete(Id);

            return Ok(result);

        }

        [HttpGet("Get Task By Id")]
        public async Task<IActionResult> GetTaskById(string taskId)
        {

            var result = await _service.GetById(taskId);

            return Ok(result);
        }

        [HttpPost("Update Task")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskRead task)
        {

            var result = await _service.Update(task);

            return Ok(result);
        }


    }
}
