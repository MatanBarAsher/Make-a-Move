using Make_a_move___Server.BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Make_a_move___Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        // GET: api/<PlacesController>
        [HttpGet]
        public List<Place> ReadPlaces()
        {
            Place place = new Place();
            return place.ReadPlaces();
        }


        // POST api/<PlacesController>
        [HttpPost]
        public int Post([FromBody] Place place)
        {
            return place.InsertPlace();
        }



        [HttpPut("Update")]
        public Place Update([FromBody] Place place)
        {
            return place.UpdatePlace(place);
        }

    }
}
