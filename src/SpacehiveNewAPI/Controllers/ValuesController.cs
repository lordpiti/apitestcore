using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spacehive.NewServices.Concrete;
using Spacehive.NewServices.Interfaces;

namespace SpacehiveNewAPI.Controllers
{


    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly INewSurveyService _newSurveyService;

        public ValuesController(INewSurveyService newSurveyService)
        {
            _newSurveyService = newSurveyService;
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _newSurveyService.MongoDbAccessTest();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
