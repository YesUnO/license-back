using license_back.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license_back.Services.ServiceInterfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<AuthenticateResponse> CreateUser(CreateUserRequest model);
        Task<UserExistsResponse> UserExists(string name);
    }
}
