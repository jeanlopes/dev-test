using MongoDB.Driver;
using MyOpenBanking.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using MyOpenBanking.Application.Services.Interface;
using MyOpenBanking.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MyOpenBanking.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _key;
        private readonly ILogger<UserService> _logger;
        private readonly IClientSessionHandle _clientSessionHandle;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IClientSessionHandle clientSessionHandle, ILogger<UserService> logger)
        {
            _logger = logger;
            _clientSessionHandle = clientSessionHandle;
            _userRepository = userRepository;
            _key = configuration.GetSection("JwtKey").ToString();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                return await _userRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            
         }

        public async Task<User> GetUser(string id)
        {
            try
            {
                return await _userRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<User> Create(User user)
        {
            _clientSessionHandle.StartTransaction();
            try
            {
                await _userRepository.Create(user);
                await _clientSessionHandle.CommitTransactionAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await _clientSessionHandle.AbortTransactionAsync();
                throw;
            }
        }

        public async Task<string> Authenticate(string username, string password)
        {
            try {
                var user = await _userRepository.GetByUserAndPasswordAsync(username, password);

                if (user == null)
                    return null;

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes(_key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

    }
}
