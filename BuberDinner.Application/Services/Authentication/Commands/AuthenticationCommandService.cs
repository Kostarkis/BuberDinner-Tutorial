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

namespace BuberDinner.Application.Services.Authentication.Commands
{
    public class AuthenticationCommandService : IAuthenticationCommandService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepo _userRepo;

        public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepo userRepo)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepo = userRepo;
        }

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            if (_userRepo.GetUserByEmail(email) is not null)
            {
                return Errors.User.DupicateEmail;
            }

            var _user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepo.Add(_user);

            var _token = _jwtTokenGenerator.GenerateToken(_user);
            return new AuthenticationResult(
                _user,
                _token);
        }
    }
}
