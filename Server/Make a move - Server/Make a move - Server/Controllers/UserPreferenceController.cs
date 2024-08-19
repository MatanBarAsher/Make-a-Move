using Make_a_move___Server.BL;
using Make_a_move___Server.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Net;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Make_a_move___Server.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
    public class UserPreferenceController : ControllerBase
    {

        // GET:  api/<UserPreferenceController>
        [HttpGet]
        public List<UserPreferences> ReadUserPreference()
        {
            UserPreferences userPreferences = new UserPreferences();
            return UserPreferences.ReadUserPreference();
        }

        // POST :  api/<UserPreferenceController>
        [HttpPost]
        public int Post([FromBody] UserPreferences userPreferences)
        {
            return userPreferences.InsertUserPreference();
        }

        [HttpPut]
        public UserPreferences Update([FromBody] UserPreferences userPreferences)
        {
            return userPreferences.UpdateUserPreferences(userPreferences);
        }

        [HttpGet("ReadUserPreferencesByEmail")]
        public UserPreferences ReadUserPreferencesByEmail([FromQuery] string email)
        {
            UserPreferences userPreferences = new UserPreferences();
            return UserPreferences.ReadUserPreferencesByEmail(email);
        }
    }
}
