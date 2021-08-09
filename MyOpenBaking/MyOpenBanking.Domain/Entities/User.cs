using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MyOpenBanking.Domain.Entities.Base;
using System;

namespace MyOpenBanking.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(ObjectId id, string userName, string email, string password)
        {
            Id = id.ToString();
            UserName = userName;
            Email = email;
            Password = password;
        }

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }
    }
}
