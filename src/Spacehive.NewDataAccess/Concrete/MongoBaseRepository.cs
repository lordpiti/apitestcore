using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Spacehive.NewCrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spacehive.NewDataAccess.Concrete
{
    public class MongoBaseRepository
    {
        protected readonly IMongoDatabase _mongoDb;

        public MongoBaseRepository(IOptions<AppSettings> settings)
        {
            var _client = new MongoClient(settings.Value.MongoConnection);
            var db = _client.GetDatabase(settings.Value.Database);

            _mongoDb = db;
        }
    }
}
