using Make_a_move___Server.BL;
using Microsoft.AspNetCore.Mvc;

namespace Make_a_move___Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class SecondFeedbackController : Controller
    {

        
        // GET: api/<SecondFeedbacksController>
        [HttpGet]
        public List<SecondFeedback> ReadFeedback()
        {
            SecondFeedback secondFeedback = new SecondFeedback();
            return secondFeedback.ReadFeedback();
        }


        // POST api/<SecondFeedbacksController>
        [HttpPost]
        public int Post([FromBody] SecondFeedback secondFeedback)
        {
            return secondFeedback.InsertFeedback();
        }



        // GET: api/SecondFeedbacksController/email/{email}
        [HttpGet("email/{email}")]
        public IActionResult GetFeedbackByEmail(string email)
        {
            SecondFeedback secondFeedback = new SecondFeedback();
            List<SecondFeedback> feedbackList = secondFeedback.ReadFeedbackByEmail(email);

            if (feedbackList == null || feedbackList.Count == 0)
            {
                return NotFound("No feedback found for the given email.");
            }

            return Ok(feedbackList);
        }




    }
}
