using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenAI_API.Moderation;
using Suvorov.LNU.TwitterClone.Database.Interfaces;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Database;
using Suvorov.LNU.TwitterClone.Models.Frontend;

namespace Suvorov.LNU.TwitterClone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(IMapper mapper, ILogger<UserController> logger, UserService userService)
        {
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> GetById(int id)
        {
            try
            {
                var user = await _userService.GetById(id);
                if (user == null)
                {
                    return NotFound();
                }

                var result = _mapper.Map<User>(user);

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    MaxDepth = 32
                };

                var serializedPost = JsonSerializer.Serialize(result, options);
                return Content(serializedPost, "application/json");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user by Id");
                return StatusCode(500);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<User>> GetAll()
        {
            var result = await _userService.GetAll().ToListAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            var serializedPost = JsonSerializer.Serialize(result, options);
            return Content(serializedPost, "application/json");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Create(CreateUserRequest createUserRequest)
        {
            if (await _userService.UserNameExists(createUserRequest.UserName))
            {
                return BadRequest("Username already exists.");
            }

            if (await _userService.EmailExists(createUserRequest.EmailAddress))
            {
                return BadRequest("Email already exists.");
            }

            var result = _mapper.Map<User>(createUserRequest);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 32
            };

            var serializedPost = JsonSerializer.Serialize(result, options);
            return Content(serializedPost, "application/json");
        }


        [HttpDelete("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _userService.GetById(id);
                if (user == null)
                {
                    return NotFound();
                }

                await _userService.Delete(user);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user");
                return StatusCode(500);
            }
        }
    }
}
