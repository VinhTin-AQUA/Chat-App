﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.IO;
using System.Net.Http;

namespace ChatAppServer.Models
{
    [CollectionName("messages")]
    public class Message
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string Id { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string  GroupId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}