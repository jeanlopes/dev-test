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

namespace MyOpenBanking.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _key;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _key = configuration.GetSection("JwtKey").ToString();
        }

        public IEnumerable<User> GetUsers() 
        {
            try
            {
                return _userRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
         }

        public User GetUser(string id)
        {
            try
            {
                return _userRepository.GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public User Create(User user)
        {
            try
            {
                return _userRepository.Create(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public string Authenticate(string username, string password)
        {
            try {
                var user = _userRepository.GetByUserAndPassword(username, password);

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
                Console.WriteLine(ex.Message);
                throw;
            }

        }

    }
}
