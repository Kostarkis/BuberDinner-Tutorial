using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepo _userRepo;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepo userRepo)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepo = userRepo;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            if (_userRepo.GetUserByEmail(query.Email) is not User _user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            if (_user.Password != query.Password)
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
