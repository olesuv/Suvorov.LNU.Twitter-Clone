using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suvorov.LNU.TwitterClone.API.Controllers;
using Suvorov.LNU.TwitterClone.Database.Services;
using Suvorov.LNU.TwitterClone.Models.Database;
using System.Text.Json.Serialization;
using System.Text.Json;
using OpenAI_API.Moderation;

namespace Suvorov.LNU.TwitterClone.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        private readonly FollowService _followService;

        public FollowController(IMapper mapper, ILogger<UserController> logger, FollowService followService)
        {
            _mapper = mapper;
            _logger = logger;
            _followService = followService;
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Follow>> GetById(int id)
        {
            try
            {
                var follow = await _followService.GetById(id);
                if (follow == null)
                {
                    return NotFound();
                }

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    MaxDepth = 32
                };

                var serializedFollow = JsonSerializer.Serialize(follow, options);
                return Content(serializedFollow, "application/json");
                // return Ok(_mapper.Map<Follow>(follow));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving follow by Id");
                return StatusCode(500);
            }
        }
    }
}
