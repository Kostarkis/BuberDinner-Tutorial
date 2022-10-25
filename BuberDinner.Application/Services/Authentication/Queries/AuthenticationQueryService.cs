using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication.Queries
{
    public class AuthenticationQueryService : IAuthenticationQueryService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepo _userRepo;

        public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepo userRepo)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepo = userRepo;
        }

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            //Check if user already exists
            if (_userRepo.GetUserByEmail(email) is not User _user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            if (_user.Password != password)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            //Create  JWT token
            var _token = _jwtTokenGenerator.GenerateToken(_user);

            return new AuthenticationResult(
                _user,
                _token
                );
        }
    }
}
