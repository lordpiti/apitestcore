using Spacehive.NewDataAccess.Interfaces;
using Spacehive.NewServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spacehive.NewServices.Concrete
{
    public class NewSurveyService : INewSurveyService
    {
        private readonly IMongoDbRepository _mongoRepository;

        public NewSurveyService(IMongoDbRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public List<object> MongoDbAccessTest()
        {
            return _mongoRepository.MongoDbAccessTest();
        }

        public void MongoDbInsert()
        {
            _mongoRepository.MongoDbInsert();
        }
    }
}
