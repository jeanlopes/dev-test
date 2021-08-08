using MongoDB.Driver;
using MyOpenBanking.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;

namespace MyOpenBanking.Application.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly string _key;

        public UserService(IConfiguration configuration)
        {
            var databaseName = configuration.GetValue<string>("MyOpenBankingDatabaseSettings:DatabaseName");
            var client = new MongoClient(configuration.GetConnectionString("DefaultConnection"));
            var database = client.GetDatabase(databaseName);

            _users = database.GetCollection<User>("Users");
            _key = configuration.GetSection("JwtKey").ToString();
        }

        public List<User> GetUsers() => _users.Find(user => true).ToList();

        public User GetUser(string id) => _users.Find(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public string Authenticate(string username, string password)
        {
            var user = _users.Find(x => x.UserName == username && x.Password == password).FirstOrDefault();

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

    }
}
