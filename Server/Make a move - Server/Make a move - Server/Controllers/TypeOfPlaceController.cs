using Make_a_move___Server.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Make_a_move___Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfPlaceController : ControllerBase
    {
        // GET: api/<TypeOfPlaceController>
        [HttpGet]
        public List<TypeOfPlace> ReadUsers()
        {
            TypeOfPlace typeOfPlace = new TypeOfPlace();
            return typeOfPlace.ReadTypeOfPlace();
        }


        // POST api/<TypeOfPlaceController>
        [HttpPost]
        public int Post([FromBody] TypeOfPlace typeOfPlace)
        {
            return typeOfPlace.InsertTypeOfPlace();
        }

        [HttpPut("Update")]
        public TypeOfPlace Update([FromBody] TypeOfPlace typeOfPlace)
        {
          return typeOfPlace.UpdateTypeOfPlace(typeOfPlace);
        }




    }
}
