using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using license_back.Services.ServiceInterfaces;
using license_back.APIModels;

namespace license_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticateResponse>> PostUser(CreateUserRequest model)
        {
            var authenticateResponse = await _userService.CreateUser(model);

            return authenticateResponse;
        }

        [Route("Authenticate")]
        [HttpPost]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            var authenticateResponse = await _userService.Authenticate(model);
            if (authenticateResponse == null)
            {
                return Unauthorized();
            }
            return authenticateResponse;
        }

        [HttpGet]
        public async Task<ActionResult<UserExistsResponse>> Exists(string name)
        {
            return await _userService.UserExists(name);
        }
    }
}
