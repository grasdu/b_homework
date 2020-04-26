namespace Users.API.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Users.API.Domain.Models;
    using Users.API.Domain.Models.Enums;
    using Users.API.Domain.Services;
    using Users.API.Extensions;
    using Users.API.Resources;

    [Produces("application/json")]
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

        /// <summary>
        /// Gets a list of the Users.
        /// </summary> 
        /// <returns>A list of Users</returns>
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await this.userService.ListAsync();
            return users;
        }

        /// <summary>
        /// Creates a new User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST
        ///     {
        ///        "name": "Test User",
        ///        "dateOfBirth": "1991.08.11",
        ///        "accessLevel": Full
        ///     }
        ///    
        /// accessLevel can be Full, Limited or None. It is case insensitive.
        /// </remarks> 
        /// <param name="resource">ProcessUser</param>
        /// <returns>A newly created User</returns>
        /// <response code="200">Returns the newly created User</response>
        /// <response code="400">Returns an error message</response> 
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

        /// <summary>
        /// Updates a User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST
        ///     {
        ///        "name": "Test User",
        ///        "dateOfBirth": "1991.08.11",
        ///        "accessLevel": Full
        ///     }
        ///    
        /// accessLevel can be Full, Limited or None. It is case insensitive.
        /// </remarks> 
        /// <param name="id">Id of the User</param>
        /// <param name="resource">ProcessUser</param>
        /// <returns>A newly created User</returns>
        /// <response code="200">Returns the updated User</response>
        /// <response code="400">Returns an error message</response> 
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

        /// <summary>
        /// Deletes a User.
        /// </summary>
        /// <param name="id">Id of the User</param>
        /// <returns>A newly created User</returns>
        /// <response code="200">Returns the deleted User</response>
        /// <response code="400">Returns an error message</response> 
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
