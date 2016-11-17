using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spacehive.NewServices.Interfaces
{
    public interface INewSurveyService
    {
        List<string> MongoDbAccessTest();

        void MongoDbInsert();
    }
}
