using Make_a_move___Server.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Make_a_move___Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // GET: api/<AdminController>
        [HttpGet]
        public List<Admin> ReadUsers()
        {
            Admin admin = new Admin();
            return admin.ReadAdmins();
        }


        // POST api/<AdminController>
        [HttpPost]
        public int Post([FromBody] Admin admin)
        {
            return admin.InsertAdmin();
        }

        [HttpPost("Login")]
        public Admin CheckLogin([FromBody] Admin admin)
        {
            return admin.CheckLogin();
        }

        [HttpPut("Update")]
        public Admin Update([FromBody] Admin admin)
        {
            return admin.UpdateAdmin(admin);
        }

    }
}
