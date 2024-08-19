namespace Make_a_move___Server.BL
{
using Make_a_move___Server.DAL;
using Make_a_move___Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
    using System.Runtime.ConstrainedExecution;

    public class User
    {
        private string email;
        private string firstName;
        private string lastName;
        private string password;
        private int gender;
        private string[] image;
        private int height;
        private DateTime birthday;
        private string phoneNumber;
        private bool isActive;
        private string city;
        private DateTime timeStamp;
        private string[] personalInterestsIds;
        private int currentPlace;
        private string persoalText;
        private static List<User> usersList = new List<User>();
        private static Dictionary<string, List<string>> likedRelationships = new Dictionary<string, List<string>>();
        private Dictionary<string, string> preferencesDictionary = new Dictionary<string, string>();


        public User() { }

        public User(string email, string firstName, string lastName, string password, int gender, string[] image, int height, DateTime birthday, string phoneNumber, bool isActive, string city, DateTime timeStamp, int currentPlace, string persoalText, Dictionary<string, string> preferencesDictionary, string[] personalInterestsIds)
        {
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.password = password;
            this.gender = gender;
            this.image = image;
            this.height = height;
            this.birthday = birthday;
            this.phoneNumber = phoneNumber;
            this.isActive = isActive;
            this.city = city;
            this.timeStamp = timeStamp;
            this.personalInterestsIds = personalInterestsIds;
            this.currentPlace = currentPlace;
            this.persoalText = persoalText;
            this.preferencesDictionary = preferencesDictionary;
        }

        //public User(string email, string firstName, string lastName, string password, int gender, string[] image, int height, DateTime birthday, string phoneNumber, bool isActive, string city, int currentPlace, string persoalText, Dictionary<string, string> preferencesDictionary, string[] personalInterestsIds)
        //{
        //    this.email = email;
        //    this.firstName = firstName;
        //    this.lastName = lastName;
        //    this.password = password;
        //    this.gender = gender;
        //    this.image = image;
        //    this.height = height;
        //    this.birthday = birthday;
        //    this.phoneNumber = phoneNumber;
        //    this.isActive = isActive;
        //    this.city = city;
        //    this.personalInterestsIds = personalInterestsIds;
        //    this.currentPlace = currentPlace;
        //    this.persoalText = persoalText;
        //    this.preferencesDictionary = preferencesDictionary;
        //}

        public string Email { get => email; set => email = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Password { get => password; set => password = value; }
        public int Gender { get => gender; set => gender = value; }
        public string[] Image { get => image; set => image = value; }
        public int Height { get => height; set => height = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public string City { get => city; set => city = value; }

        public DateTime TimeStamp { get => timeStamp; set => timeStamp = value; }

        public int CurrentPlace { get => currentPlace; set => currentPlace = value; }
        public string PersoalText { get => persoalText; set => persoalText = value; }
        public Dictionary<string, string> PreferencesDictionary { get => preferencesDictionary; set => preferencesDictionary = value; }



        public int InsertUser()
        {
            try
            {
                DBservicesUser dbs = new DBservicesUser();
                usersList.Add(this);
                return dbs.InsertUser(this);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error inserting user", ex);
            }
        }

        public List<User> ReadUsers()
        {
            try
            {
                DBservicesUser dbs = new DBservicesUser();
                return dbs.ReadUsers();
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading users", ex);
            }
        }


        public User CheckLogin()
        {
            try
            {
                DBservicesUser dbs = new DBservicesUser();
                User loggedInUser = dbs.CheckLogin(this);

                // If login is successful, return the logged-in user
                if (loggedInUser != null)
                {
                    return loggedInUser;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error checking login", ex);
            }
        }

        public User UpdateUser(User newUser)
        {
            try
            {
                DBservicesUser dbs1 = new DBservicesUser();
                List<User> list = dbs1.ReadUsers();
                // Find the user in the UsersList by email
                User userToUpdate = list.Find(u => string.Equals(u.Email.Trim(), newUser.Email.Trim(), StringComparison.OrdinalIgnoreCase));

                if (userToUpdate != null)
                {
                    // Update user informationYarin@gmail.com
                    userToUpdate.FirstName = newUser.FirstName;
                    userToUpdate.LastName = newUser.LastName;
                    userToUpdate.Password = newUser.Password;
                    userToUpdate.IsActive = newUser.IsActive;
                    userToUpdate.gender = newUser.gender;
                    userToUpdate.image = newUser.image;
                    userToUpdate.height = newUser.height;
                    userToUpdate.birthday = newUser.birthday;
                    userToUpdate.phoneNumber = newUser.phoneNumber;
                    userToUpdate.timeStamp = newUser.timeStamp;
                    userToUpdate.city = newUser.city;
                    userToUpdate.currentPlace = newUser.currentPlace;
                    userToUpdate.persoalText = newUser.persoalText;



                    // Update in the database (assuming DBservices has an UpdateUser method)
                    DBservicesUser dbs = new DBservicesUser();
                    
                    return dbs.UpdateUser(userToUpdate);
                }
                else
                {
                    // User not found, handle the case appropriately (return null, throw an exception, etc.)
                    return null; // Or throw new Exception("User not found");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error updating user", ex);
            }
        }

        public User UpdateUserCurrentPlace(string email, Place place)
        {
            try
            {
                DBservicesUser dbs1 = new DBservicesUser();
                User newUser = dbs1.GetUserByEmail(email);
                List<User> list = dbs1.ReadUsers();
                // Find the user in the UsersList by email
                User userToUpdate = list.Find(u => string.Equals(u.Email.Trim(), newUser.Email.Trim(), StringComparison.OrdinalIgnoreCase));


                if (userToUpdate != null)
                {
                        Place place1 = new Place();
                    // check if this place exists
                    DBservicesPlace dbs2 = new DBservicesPlace();
                    List<Place> places = dbs2.ReadPlaces();
                    Place retrivePlace = new Place();
                    if (places.Find(p => string.Equals(p.Name.Trim()+p.Address.Trim(), place.Name.Trim()+place.Address.Trim())) == null)
                    { 
                        int counter = place.InsertPlace();
                        userToUpdate.CurrentPlace = counter;
                    }
                    else {
                        retrivePlace = places.Find(p => string.Equals(p.Name.Trim() + p.Address.Trim(), place.Name.Trim() + place.Address.Trim()));
                        // Update the currentPlace field
                        userToUpdate.CurrentPlace = retrivePlace.PlaceCode;
                    }


                        // Update in the database
                        DBservicesUser dbs = new DBservicesUser();
                        dbs.UpdateUser(userToUpdate);

                        return userToUpdate;
                    
                }
                else
                {
                    // User not found, handle the case appropriately (return null, throw an exception, etc.)
                    return null; // Or throw new Exception("User not found");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error updating user", ex);
            }
        }

        

        public List<User> ReadUsersByPlace(int placeCode)
        {
            try
            {
                DBservicesUser dbs = new DBservicesUser();
                return dbs.ReadUsersByPlace(placeCode);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading users", ex);
            }
        }

        public int CalculateAge(DateTime birthday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        private readonly HttpClient _httpClient;

        public User(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<double> GetData(int originCode, int destinationCode)
        {
         
            var url = $"https://data.gov.il/api/3/action/datastore_search?resource_id=bc5293d3-1023-4d9e-bdbe-082b58f93b65&filters={{\"קוד מוצא\":{originCode},\"קוד יעד\":{destinationCode}}}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {response.ReasonPhrase}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

            if (apiResponse.success && apiResponse.result != null)
            {
                return apiResponse.result.records[0].DistanceFromCenter;
            }

            throw new Exception("Failed to fetch data");
        }

        public async Task<double> CalculateDistance(string city1, string city2)
        {
            if(city1 == city2)
            {
                return 0;
            }
            var httpClient = new HttpClient();
            var user = new User(httpClient);
            int cityCode1 = Int32.Parse(city1);
            int cityCode2 = Int32.Parse(city2);
            double response = await user.GetData(cityCode1, cityCode2);
            return response;
        }



        public async Task< Dictionary<User, Tuple<double, double>>> CalculateMatchPercentage(User u)
        {
            // Perform the gender check first
            bool genderMatches = this.PreferencesDictionary["preferenceGender"] == u.Gender.ToString();
            bool otherGenderMatches = u.PreferencesDictionary["preferenceGender"] == this.Gender.ToString();

            // If gender check fails, return an empty dictionary
            if (!genderMatches || !otherGenderMatches)
            {
                return new Dictionary<User, Tuple<double, double>>();
            }

            double agePercentage;
            double otherAgePercentage;
            double heightPercentage;
            double otherHeightPercentage;
            double distancePercentage;
            double otherDistancePercentage;
            double distance = 0;

            if(this.City != u.City)
            {
                distance = await CalculateDistance(this.City, u.City);
            }


            int age = CalculateAge(u.Birthday);
            int secAge = CalculateAge(this.Birthday);

            // Age check
            int minAge = Int32.Parse(this.PreferencesDictionary["minAge"]);
            int maxAge = Int32.Parse(this.PreferencesDictionary["maxAge"]);
            if (minAge <= age && maxAge >= age)
            {
                agePercentage = 100;
            }
            else if (age > maxAge)
            {
                agePercentage = 100 - (age - maxAge) * 2;
            }
            else
            {
                agePercentage = 100 - (minAge - age) * 2;
            }
            agePercentage = Math.Max(agePercentage, 0); // Ensure percentage is not negative

            // Other Age check
            int otherMinAge = Int32.Parse(u.PreferencesDictionary["minAge"]);
            int otherMaxAge = Int32.Parse(u.PreferencesDictionary["maxAge"]);
            if (otherMinAge <= secAge && otherMaxAge >= secAge)
            {
                otherAgePercentage = 100;
            }
            else if (secAge > otherMaxAge)
            {
                otherAgePercentage = 100 - (secAge - otherMaxAge) * 2;
            }
            else
            {
                otherAgePercentage = 100 - (otherMinAge - secAge) * 2;
            }
            otherAgePercentage = Math.Max(otherAgePercentage, 0); // Ensure percentage is not negative

            ////////////////////////////////////////////////////////////////////////////


            // height check
            int minHeight = Int32.Parse(this.PreferencesDictionary["minHeight"]);
            int maxHeighte = Int32.Parse(this.PreferencesDictionary["maxHeight"]);
            if (minHeight <= u.height && maxHeighte >= u.height)
            {
                heightPercentage = 100;
            }
            else if (u.height > maxHeighte)
            {
                heightPercentage = 100 - (u.height - maxHeighte) * 2;
            }
            else
            {
                heightPercentage = 100 - (minHeight - u.height) * 2;
            }
            heightPercentage = Math.Max(heightPercentage, 0); // Ensure percentage is not negative

            // Other Age check
            int otherMinHeight = Int32.Parse(u.PreferencesDictionary["minHeight"]);
            int otherMaxHeight = Int32.Parse(u.PreferencesDictionary["maxHeight"]);
            if (otherMinHeight <= this.height && otherMaxHeight >= this.height)
            {
                otherHeightPercentage = 100;
            }
            else if (this.height > otherMaxHeight)
            {
                otherHeightPercentage = 100 - (this.height - otherMaxHeight) * 2;
            }
            else
            {
                otherHeightPercentage = 100 - (otherMinHeight - this.height) * 2;
            }
            otherHeightPercentage = Math.Max(otherHeightPercentage, 0); // Ensure percentage is not negative
            

            // Distance check 
            double maxDistance = Double.Parse(this.PreferencesDictionary["maxDistance"]);
            if (maxDistance >= distance)
            {
                distancePercentage = 100;
            }
            else
            {
                distancePercentage = 100 - Math.Abs(distance - maxDistance) * 2;
            }
            distancePercentage = Math.Max(distancePercentage, 0); // Ensure percentage is not negative

            // Other Distance check
            double otherMaxDistance = Double.Parse(u.PreferencesDictionary["maxDistance"]);
            if (otherMaxDistance >= distance)
            {
                otherDistancePercentage = 100;
            }
            else
            {
                otherDistancePercentage = 100 - Math.Abs(distance - otherMaxDistance) * 2;
            }

            otherDistancePercentage = Math.Max(otherDistancePercentage, 0); // Ensure percentage is not negative

            double calMatchPercentage = (agePercentage + heightPercentage + distancePercentage) / 3;
            double otherCalMatchPercentage = (otherAgePercentage + otherHeightPercentage + otherDistancePercentage) / 3;

                return new Dictionary<User, Tuple<double, double>>
                {
                      { u, new Tuple<double, double>(calMatchPercentage, otherCalMatchPercentage) }
                };
        }

        public async Task< Dictionary<User, Tuple<double, double>>> ReadUsersByPreference()
        {
            List<User> list = this.ReadUsersByPlace(this.CurrentPlace);
            Dictionary<User, Tuple<double, double>> result = new Dictionary<User, Tuple<double, double>>();

            foreach (User u in list)
            {
                if (u == this)
                {
                    continue;
                }
                // Calculate the match percentage
                var match = await this.CalculateMatchPercentage(u);

                // Add matched users to the result
                foreach (var kvp in match)
                {
                    var matchedUser = kvp.Key;
                    var matchPercentages = kvp.Value;

                    // Add only users with a valid match percentage
                    if (matchPercentages.Item1 > 0)
                    {
                        result[matchedUser] = matchPercentages;
                    }
                }
            }

            return result;
        }

        public async Task<Dictionary<User, Tuple<double, double>>> GetMatchPercantegeByEmails(User u1, User u2)
        {
            //User u1 = GetUserByEmail(email1);
            //User u2 = GetUserByEmail(email2);
            //List<User> list = this.ReadUsersByPlace(this.CurrentPlace);

            Dictionary<User, Tuple<double, double>> result = new Dictionary<User, Tuple<double, double>>();

                // Calculate the match percentage
                var match = await u1.CalculateMatchPercentage(u2);

                // Add matched users to the result
                foreach (var kvp in match)
                {
                    var matchedUser = kvp.Key;
                    var matchPercentages = kvp.Value;

                    // Add only users with a valid match percentage
                    if (matchPercentages.Item1 > 0)
                    {
                        result[matchedUser] = matchPercentages;
                    }
                }
            

            return result;
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                // Create an instance of your DAL service
                DBservicesUser dbs = new DBservicesUser();

                // Call the method in your DAL to retrieve the user by email
                User user = dbs.GetUserByEmail(email);
                //User user = dbs.GetUserPreferencesByEmail(email);

                // Return the user fetched from the database
                return user;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error getting user by email", ex);
            }
        }

        public User GetUserDetailsToDisplay(string email)
        {
            try
            {
                User user = GetUserByEmail(email);
                user.password = "";
                return user;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error getting user to show by email", ex);
            }
        }

        public User GetUserPreferencesByEmail(string email)
        {
            try
            {
                // Create an instance of your DAL service
                DBservicesUser dbs = new DBservicesUser();

                // Call the method in your DAL to retrieve the user by email
                User user = dbs.GetUserPreferencesByEmail(email);

                // Return the user fetched from the database
                return user;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error getting user by email", ex);
            }
        }

        

        public string ChangeImages(string email, List<string> images)
        {
            try
            {
                User user = GetUserByEmail(email);
                user.image = user.Image.Concat(images.ToArray()).ToArray();
                DBservicesUser dbs = new DBservicesUser();
                dbs.UpdateUser(user);
                User newUser = GetUserByEmail(email);
                Console.WriteLine(newUser);
                return newUser.image[newUser.image.Length - 1];
            }
            catch (Exception ex)
            {
                //    Log or handle the exception appropriately
                throw new Exception("Error changing images", ex);
            }
        }

       
         
        public int checkExistingUserByKeyAndValue(string key,string value)
        {
            DBservicesUser dbs1 = new DBservicesUser();
            List<User> list = dbs1.ReadUsers();
            // Find if this value exists
            User user1 = list.Find(u => string.Equals(u.Email.Trim(), value.Trim(), StringComparison.OrdinalIgnoreCase));
            User user2 = list.Find(u => string.Equals(u.PhoneNumber.Trim(), value.Trim(), StringComparison.OrdinalIgnoreCase));
            
            if(user1 == null && user2 == null)
            {
                return 0;
            }
            return 1;

        }

        public User EditPreferences(User user)
        {
            try
            {
                List<User> list = ReadUsers();
                foreach (User u in list)
                {
                    if (user.email == u.email)
                    {
                        u.PreferencesDictionary = user.preferencesDictionary;
                        DBservicesUser dbs = new DBservicesUser();
                        return dbs.UpdateUser(u);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading data", ex);
            }
        }  

        public List<string> GetUsersEmails()
        {
            List<User> list = ReadUsers();
            List<string> emailsList = new List<string>();
            foreach (var user in list)
            { 
                emailsList.Add(user.Email);
            }
            return emailsList;
        }

        public int AddImage(byte[] imageData, string mimeType)
        {
            try
            {
                DBservicesUser dbs = new DBservicesUser();
                return dbs.AddImage(imageData, mimeType);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error inserting Image", ex);
            }
        }
        
        public (byte[] ImageData, string MimeType) GetImage(int imageID)
        {
            try
            {
                DBservicesUser dbs = new DBservicesUser();
                return dbs.GetImage(imageID);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error Getting Image", ex);
            }
        }

        public string[] getImagesByEmail(string email)
        {
             try
            {
                DBservicesUser dbs = new DBservicesUser();
                return dbs.GetUserByEmail(email).image;
            } 
            
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error getting Image", ex);
            }
        }


        public int AddLike(string userEmail, string likedUserEmail, int currentplace)
        {
            try
            {
                DBservicesUser dbs = new DBservicesUser();
                return dbs.AddLike(userEmail, likedUserEmail, currentplace);
            }    
             catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error adding like", ex);
            }

        }


        public Dictionary<string, object> GetAllAnalysesByEmail(string inputEmail)
        {
            var result = new Dictionary<string, object>();
            DBservicesUser dbs = new DBservicesUser();

            // Retrieve data from each analysis method
            var analysisData = dbs.ReadAnalysisByEmail(inputEmail).FirstOrDefault();
            var preferenceAnalysisData = dbs.ReadPreferenceAnalysisByEmail(inputEmail).FirstOrDefault();
            

            // Extract specific fields from analysisData
            if (analysisData != null)
            {
                var name = analysisData.MostFrequentFriend;
                var namePrecentage = analysisData.FriendPercentage;
                var place = analysisData.MostFrequentPlace;
                var placePrecentage = analysisData.PlacePercentage;
                var day = analysisData.MostFrequentWeekday;
                var dayPrecentage = analysisData.WeekdayPercentage;

                result["Name"] = name;
                result["NamePrecentage"] = namePrecentage;
                result["Place"] = place;
                result["PlacePrecentage"] = placePrecentage;
                result["Day"] = day;
                result["DayPrecentage"] = dayPrecentage;
            }

            // Extract and compare percentage data from preferenceAnalysisData
            if (preferenceAnalysisData != null)
            {
                var heightMatchPercentage = preferenceAnalysisData.HeightMatchPercentage;
                var nonMatchingHeightPercentage = preferenceAnalysisData.HeightMismatchPercentage;
                var ageMatchPercentage = preferenceAnalysisData.AgeMatchPercentage;
                var nonMatchingAgePercentage = preferenceAnalysisData.AgeMismatchPercentage;

                result["HeightMatchPercentage"] = heightMatchPercentage;
                result["NonMatchingHeightPercentage"] = nonMatchingHeightPercentage;

                result["AgeMatchPercentage"] = ageMatchPercentage;
                result["NonMatchingAgePercentage"] = nonMatchingAgePercentage;
            }

            // Combine personalInterestAnalysisData
            var personalInterestAnalysisData = dbs.ReadPersonalInterestAnalysisByEmail(inputEmail).ToList();
            if (personalInterestAnalysisData.Any())
            {
                // Concatenate all interests and their percentages
                var interestData = personalInterestAnalysisData
                    .Select(d => d.CommonInterest)
                    .Distinct() // Ensure unique interests
                    .ToList();

                var interestsConcatenated = string.Join(", ", interestData);
                result["PersonalInterests"] = interestsConcatenated;

                // Add highest percentage
                var highestInterest = personalInterestAnalysisData
                    .OrderByDescending(d => d.HeightMatchPercentage) // Assuming interest percentage to sort by
                    .FirstOrDefault();

                if (highestInterest != null)
                {
                    result["HighestInterestPercentage"] = highestInterest.HeightMatchPercentage;
                }
            }

            return result;
            }


        


    }



}

