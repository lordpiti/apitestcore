using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Spacehive.NewCrossCutting;
using Spacehive.NewDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Spacehive.NewDataAccess.Concrete
{
    public class MongoDbRepository : MongoBaseRepository, IMongoDbRepository
    {
        public MongoDbRepository(IOptions<AppSettings> settings) : base(settings)
        {

        }

        public List<object> MongoDbAccessTest()
        {

            IMongoCollection<BsonDocument> collection = _mongoDb.GetCollection<BsonDocument>("books"); // initialize to the collection to read from

            var lista = new List<object>();


            var cursor = collection.Find(new BsonDocument());//.ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                var bu = BsonSerializer.Deserialize<object>(document);

                lista.Add(bu);

                //using (var stringWriter = new StringWriter())
                //using (var jsonWriter = new JsonWriter(stringWriter))
                //{
                //    var context = BsonSerializationContext.CreateRoot(jsonWriter);
                //    collection.DocumentSerializer.Serialize(context, document);
                //    var line = stringWriter.ToString();

                //    lista.Add(line);
                //}
            }
            return lista;
        }

        public void MongoDbInsert()
        {

            IMongoCollection<BsonDocument> collection = _mongoDb.GetCollection<BsonDocument>("books"); // initialize to the collection to read from

            //string json = "{ 'foo' : 'bar' }";
            //MongoDB.Bson.BsonDocument document
            //    = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);

            var document = new BsonDocument
            {
                { "address" , new BsonDocument
                    {
                        { "street", "2 Avenue" },
                        { "zipcode", "10075" },
                        { "building", "1480" },
                        { "coord", new BsonArray { 73.9557413, 40.7720266 } }
                    }
                },
                { "borough", "Manhattan" },
                { "cuisine", "Italian" },
                { "grades", new BsonArray
                    {
                        new BsonDocument
                        {
                            { "date", new DateTime(2014, 10, 1, 0, 0, 0, DateTimeKind.Utc) },
                            { "grade", "A" },
                            { "score", 11 }
                        },
                        new BsonDocument
                        {
                            { "date", new DateTime(2014, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
                            { "grade", "B" },
                            { "score", 17 }
                        }
                    }
                },
                { "name", "Vella" },
                { "restaurant_id", "41704620" }
            };

            collection.InsertOne(document);
        }

    }
}
