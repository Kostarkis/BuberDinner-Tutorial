using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepo _userRepo;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepo userRepo)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepo = userRepo;
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            if (_userRepo.GetUserByEmail(email) is not null)
            {
                throw new Exception("User with given email already exists");
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
        
        public AuthenticationResult Login(string email, string password)
        {
            //Check if user already exists
            if (_userRepo.GetUserByEmail(email) is not User _user)
            {
                throw new Exception("User with given email does not exists.");
            }
            
            if(_user.Password != password)
            {
                throw new Exception("Invalid password");
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
