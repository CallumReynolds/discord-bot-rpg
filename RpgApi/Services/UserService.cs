using RpgApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace RpgApi.Services
{
    public class RPGService
    {
        private readonly IMongoCollection<User> _users;

        public RPGService(IRPGDatabaseSettings settings)
        {
            // Creates a new MongoClient using the ConnectionString value from appsettings.json
            var client = new MongoClient(settings.ConnectionString);
            // Gets the database name from Mongo
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.RPGCollectionName);
        }

        // CRUD - Create, Read, Update, Delete
        // Uses the MongoDB Driver to perform operation against the database
        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);
    }
}