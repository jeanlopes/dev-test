using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace MyOpenBaking.Models
{
    [BsonIgnoreExtraElements]
    public class ApplicationUser : MongoUser
	{
	}
}
