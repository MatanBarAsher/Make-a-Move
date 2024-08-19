using Make_a_move___Server.BL;
using Make_a_move___Server.Controllers;
using Make_a_move___Server.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Net;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using static Make_a_move___Server.BL.User;
using System.Net.Http;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

//For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Make_a_move___Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public List<User> ReadUsers()
        {
            User user = new User();
            return user.ReadUsers();
        }

        // POST api/<UsersController>
        [HttpPost]
        public int Post([FromBody] User user)
        {
            return user.InsertUser();
        }

        [HttpPost("Login")]
        public User CheckLogin([FromBody] User user)
        {
            return user.CheckLogin();
        }

        [HttpPut("Update")]
        public User Update([FromBody] User user)
        {
            return user.UpdateUser(user);
        }

        [HttpGet("Report/{placeCode}")]
        public List<User> ReadUsersByPlace([FromRoute] string placeCode)
        {
            int p = Convert.ToInt32(placeCode);
            User user = new User();
            return user.ReadUsersByPlace(p);
        }

        [HttpPost]
        [Route("UpdatePlace/{email}")]
        public User UpdateUserCurrentPlace([FromRoute] string email, [FromBody] Place place)
        {
            User user = new User();
            return user.UpdateUserCurrentPlace(email, place);
        }


        [HttpPost("AddLike")]
        public IActionResult AddLike([FromQuery] string userEmail, [FromQuery] string likedUserEmail, [FromQuery] int currentPlace)
        {
            try
            {
                DBservicesUser dbs = new DBservicesUser();
                int result = dbs.AddLike(userEmail, likedUserEmail, currentPlace);
                return Ok(result); // Assuming you want to return the result of the operation
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                return StatusCode(500, $"Error inserting Like: {ex.Message}");
            }
        }

        [HttpGet("ReadUsersByPreference")]
        public async Task< Dictionary<string, Tuple<double, double>>> ReadUsersByPreference(string userEmail)
        {

            // Get the current user by email
            User u = new User();
            User currentUser = u.GetUserPreferencesByEmail(userEmail);

            // Check if the user exists
            if (currentUser == null)
            {
                // Return an empty dictionary if the user does not exist
                return new Dictionary<string, Tuple<double, double>>();
            }

            // Call ReadUsersByPreference to get users matching the preferences of the current user
            Dictionary<User, Tuple<double, double>> result = await currentUser.ReadUsersByPreference();

            // Convert User objects to strings (assuming User has a unique identifier)
            Dictionary<string, Tuple<double, double>> serializedResult = result.ToDictionary(
                kvp => kvp.Key.Email, // Assuming Email is a unique identifier
                kvp => kvp.Value);

            return serializedResult;
        }

        [HttpGet("GetMatchPercentage")]

        public async Task<double> GetMatchPercentage(string email1, string email2) {
            User u = new();
            DBservicesUser dbs = new DBservicesUser();
            // Call ReadUsersByPreference to get users matching the preferences of the current user
            User user1 = dbs.GetUserPreferencesByEmail(email1);
            User user2 = dbs.GetUserPreferencesByEmail(email2);

            Dictionary<User, Tuple<double, double>> result = await u.GetMatchPercantegeByEmails(user1, user2);

            double b = result.Values.FirstOrDefault().Item2;
            return result.Values.FirstOrDefault().Item1;

             

        }

        [HttpPost]
        [Route("changeImages/{email}")]

        public async Task<IActionResult> ChangeImages([FromForm] List<IFormFile> files, [FromRoute] string email)
        {
            List<string> imageLinks = new List<string>();

            string path = System.IO.Directory.GetCurrentDirectory();

            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(path, "uploadedFiles/" + formFile.FileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    imageLinks.Add(formFile.FileName);
                }
            }
            User u = new();
            
            if (u.ChangeImages(email, imageLinks) != null)
            {
              // Return status code  
              return Ok(imageLinks);
            }
            else
            {
                return BadRequest("Img");
            }

        }

        [HttpPost("AddImages")]
        public ActionResult UploadImage(IFormFile file)
        {
            User _service = new();
            if (file != null && file.Length > 0)
            {
                try
                {
                    byte[] imageData;
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        imageData = memoryStream.ToArray();
                    }
                    string mimeType = file.ContentType;

                    // Call your service method to add the image to the database
                    int imageId = _service.AddImage(imageData, mimeType);

                    return Ok(imageId);
                }
                catch (Exception ex)
                {
                    return StatusCode(500,"Error uploading image: " + ex.Message);
                }
            }
            else
            {
                return StatusCode(500, "Please select a file.");
            }

        }

        [HttpGet("GetImage")]
        public IActionResult GetImage(int imageId)
        {
            User _service = new();

            var (imageData, mimeType) = _service.GetImage(imageId);

            if (imageData == null)
            {
                return NotFound();
            }

            return File(imageData, mimeType); // Return the image with its MIME type
        }

        [HttpGet]
        [Route("GetUserByEmail/{email}")]
        public User GetUserByEmail([FromRoute] string email)
        {
            User _service = new();
            return _service.GetUserByEmail(email);
        }

        [HttpGet]
        [Route("GetUserDetailsNoPasswordByEmail/{email}")]
        public User GetUserDetailsByEmail([FromRoute] string email)
        {
            User _service = new();  
            return _service.GetUserDetailsToDisplay(email);
        }

        [HttpPost]
        [Route("checkExistingUserByKeyAndValue/{key}")]
        public int checkExistingUserByKeyAndValue([FromRoute] string key , [FromBody] string value)
        {
            User u = new User();
            return u.checkExistingUserByKeyAndValue(key, value);
        }

      

        [HttpGet("getEmails")]
        public List<string> GetUsersEmails()
        {
            User user = new User();
            return user.GetUsersEmails();
        }

        // POST api/user/addpersonalinterests
        [HttpPost]
        [Route("addpersonalinterests/{email}")]
        public IActionResult AddPersonalInterests([FromRoute] string email,[FromBody] List<int> interestCodes)
        {
            DBservicesUser dbs = new DBservicesUser();
            try
            {
                dbs.InsertUserPersonalInterests(email, interestCodes);
                return Ok("Personal interests added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add personal interests: {ex.Message}");
            }
        }



        // GET api/user/interests/{email}
        [HttpGet("interests/{email}")]
        public IActionResult GetUserInterests(string email)
        {
            DBservicesUser dbs = new DBservicesUser();
            try
            {
                List<int> interestCodes = dbs.GetUserInterestCodesByEmail(email);
                return Ok(interestCodes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve user interests: {ex.Message}");
            }
        }

        [HttpPut]
        [Route(("UpdatePersonalInterestsByEmail/{email}"))]
        public IActionResult UpdatePersonalInterestsByEmail([FromRoute] string email, [FromBody] List<int> interestCodes)
        {
            DBservicesUser dbs = new();
            try
            {
                dbs.DeleteUserPersonalInterests(email);
                dbs.InsertUserPersonalInterests(email, interestCodes);
                return Ok("Personal interests updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to Update personal interests: {ex.Message}");
            }
        }

        [HttpGet("GetAllUserData/{email}")]
        public IActionResult GetAllUserData(string email)
        {
            DBservicesUser dbs = new DBservicesUser();
            try
            {
                // Call the function that retrieves the user details
                User user = dbs.GetUserPreferencesByEmail(email);


                // Call the function that retrieves the user's interest codes
                List<int> interestCodes = dbs.GetUserInterestCodesByEmail(email);

                // Create an object that contains the data
                var result = new
                {
                    User = user,
                    InterestCodes = interestCodes
                };

                // Return the result as an OK response
                return Ok(result);
            }
            catch (Exception ex)
            {
                // In case of an error, return a bad request with the error message
                return BadRequest($"Failed to retrieve user data: {ex.Message}");
            }
        }
        [HttpGet("getImagesByEmail/{email}")]

        public string[] getImagesByEmail(string email)
        {
            User user = new();
            return user.getImagesByEmail(email);
        }


        // GET api/user/friends/{email}
        [HttpGet("friends/{email}")]
        public IActionResult GetFriendsNames(string email)
        {
            DBservicesUser dbs = new DBservicesUser();
            try
            {
                List<string> friendsList = dbs.GetFriendsNames(email);
                return Ok(friendsList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to retrieve user friendsList: {ex.Message}");
            }
        }

        [HttpGet("getMylikes")]
        public IActionResult GetLikesByEmail(string email)
        {

            DBservicesUser dbs = new DBservicesUser();
            try
            {
                IEnumerable<dynamic> likes = dbs.ReadLikesByEmail(email);

                // Return the data as JSON (ASP.NET Core)
                return Ok(likes);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging mechanism you prefer)
                // e.g., _logger.LogError(ex, "An error occurred while getting likes.");
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpGet("getWhoLikesMe")]
        public IActionResult GetlikesForMe(string email)
        {

            DBservicesUser dbs = new DBservicesUser();
            try
            {
                IEnumerable<dynamic> likes = dbs.GetlikesForMe(email);

                // Return the data as JSON (ASP.NET Core)
                return Ok(likes);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging mechanism you prefer)
                // e.g., _logger.LogError(ex, "An error occurred while getting likes.");
                return StatusCode(500, "Internal server error.");
            }
        }



        [HttpGet("getAnalysis")]
        public IActionResult GetAnalysis(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email parameter is required.");
            }
            User user = new();
            try
            {
                var analysisData = user.GetAllAnalysesByEmail(email);
                return Ok(analysisData);
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }


        }


