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

        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await this.userService.ListAsync();
            return users;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateUser resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var user = mapper.Map<CreateUser, User>(resource);
            var result = await this.userService.CreateAsync(user);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(user);
        }

    }
}
