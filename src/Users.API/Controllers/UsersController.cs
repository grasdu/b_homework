namespace Users.API.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Users.API.Domain.Models;
    using Users.API.Domain.Services;
    using Users.API.Extensions;
    using Users.API.Resources;

    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IRabbitMqService rabbitMqService;

        public UsersController(IUserService userService, IMapper mapper, IRabbitMqService rabbitMqService)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.rabbitMqService = rabbitMqService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await this.userService.ListAsync();
            return users;
        }

        [HttpPost]
        public async Task<IActionResult> PostUserAsync([FromBody] ProcessUser resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var user = mapper.Map<ProcessUser, User>(resource);
            var result = await this.userService.CreateAsync(user);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            rabbitMqService.Send(result);

            return Ok(result.User);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] ProcessUser resource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var user = mapper.Map<ProcessUser, User>(resource);
            var result = await this.userService.UpdateAsync(id, user);
            
            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.User);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var result = await this.userService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.User);
        }

    }
}
