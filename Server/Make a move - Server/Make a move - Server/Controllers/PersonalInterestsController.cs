using Make_a_move___Server.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Make_a_move___Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalInterestsController : ControllerBase
    {
        // GET: api/<PersonalInterestsController>
        [HttpGet]
        public List<PersonalInterests> ReadPersonalInterests()
        {
            PersonalInterests personalInterests = new PersonalInterests();
            return personalInterests.ReadPersonalInterests();
        }

        

        // POST api/<PersonalInterestsController>
        [HttpPost]
        public int Post([FromBody] PersonalInterests personalInterests)
        {
            return personalInterests.InsertPersonalInterests();
        }

        [HttpPut("Update")]
        public PersonalInterests Update([FromBody] PersonalInterests personalInterest)
        {
            return personalInterest.UpdatePersonalInterests(personalInterest);
        }
    }
}
