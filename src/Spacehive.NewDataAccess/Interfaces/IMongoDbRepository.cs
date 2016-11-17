using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spacehive.NewDataAccess.Interfaces
{
    public interface IMongoDbRepository
    {
        List<string> MongoDbAccessTest();

        void MongoDbInsert();
    }
}
