﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyOpenBanking.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; protected set; }
    }
}
