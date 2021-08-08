using MongoDB.Bson.Serialization.Attributes;
using MyOpenBanking.Domain.Entities.Base;

namespace MyOpenBanking.Domain.Entities
{
    public class User : BaseEntity
    {
        

        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }
    }
}
