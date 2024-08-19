using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using Make_a_move___Server.BL;

namespace Make_a_move___Server.DAL
{
    public class DBservicesUser
    {
        public SqlConnection connect(String conString)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

     
       //--------------------------------------------------------------------------------------------------
       // This method Inserts a User to the Users table 
       // --------------------------------------------------------------------------------------------------

        public int InsertUser(User user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                // create the connection
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateUserInsertCommandWithStoredProcedure("SP_InsertNewUser", con, user);  // create the command

            try
            {
                // execute the command
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //---------------------------------------------------------------------------------
        // Create the SqlCommand for insrting new user using a stored procedure
        //---------------------------------------------------------------------------------

        private SqlCommand CreateUserInsertCommandWithStoredProcedure(String spName, SqlConnection con, User user)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@email", user.Email);

            cmd.Parameters.AddWithValue("@firstName", user.FirstName);

            cmd.Parameters.AddWithValue("@lastName", user.LastName);

            cmd.Parameters.AddWithValue("@password", user.Password);

            cmd.Parameters.AddWithValue("@gender", user.Gender);

            string images = JsonSerializer.Serialize(user.Image);
            cmd.Parameters.AddWithValue("@image", images);

            cmd.Parameters.AddWithValue("@height", user.Height);

            cmd.Parameters.AddWithValue("@birthday", user.Birthday);

            cmd.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);

            cmd.Parameters.AddWithValue("@city", user.City);
        
            cmd.Parameters.AddWithValue("@currentPlace", user.CurrentPlace);

            cmd.Parameters.AddWithValue("@persoalText", user.PersoalText);
            


            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method reads users from the database 
        //--------------------------------------------------------------------------------------------------
        public List<User> ReadUsers()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<User> usersList = new List<User>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectUserWithStoredProcedure("SP_ReadUsers", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    User u = new User();
                    u.Email = dataReader["email"].ToString();
                    u.FirstName = dataReader["firstName"].ToString();
                    u.LastName = dataReader["lastName"].ToString();
                    u.Password = dataReader["password"].ToString();
                    u.Image = JsonSerializer.Deserialize<string[]>(dataReader["image"].ToString());
                    u.Gender = Convert.ToInt32(dataReader["gender"]);
                    u.Height = Convert.ToInt32(dataReader["height"]);
                    u.Birthday = Convert.ToDateTime(dataReader["birthday"]);
                    u.PhoneNumber = dataReader["phoneNumber"].ToString();
                    u.IsActive = Convert.ToBoolean(dataReader["isActive"]);
                    u.TimeStamp = Convert.ToDateTime(dataReader["timeStamp"]);
                    u.City = dataReader["city"].ToString();
                    u.CurrentPlace = Convert.ToInt32(dataReader["currentPlace"]);
                    u.PersoalText = dataReader["persoalText"].ToString();
                     

                    usersList.Add(u);
                }
                return usersList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateSelectUserWithStoredProcedure(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method Updates a user at user table 
        //--------------------------------------------------------------------------------------------------

        public User UpdateUser(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateUserUpdateCommandWithStoredProcedure("SP_UpdateUser", con, user);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                User u = null; // Initialize the User object

                while (dataReader.Read())
                {
                    u = new User
                    {
                        Email = dataReader["email"].ToString(),
                        FirstName = dataReader["firstName"].ToString(),
                        LastName = dataReader["familyName"].ToString(),
                        Password = dataReader["password"].ToString(),
                        Image = JsonSerializer.Deserialize<string[]>(dataReader["image"].ToString()),
                        Gender = Convert.ToInt32(dataReader["gender"]),
                        Height = Convert.ToInt32(dataReader["height"]),
                        Birthday = Convert.ToDateTime(dataReader["birthday"]),
                        PhoneNumber = dataReader["phoneNumber"].ToString(),
                        IsActive = Convert.ToBoolean(dataReader["isActive"]),
                        TimeStamp = Convert.ToDateTime(dataReader["timeStamp"]),
                        City = dataReader["city"].ToString(),
                        CurrentPlace = Convert.ToInt32(dataReader["currentPlace"]),
                        PersoalText= dataReader["persoalText"].ToString(),
                        
                    };
                }

                if (u != null)
                {
                    // Login successful
                    return u;
                }
                else
                {
                    // Login failed, return null or throw an exception as needed
                    return null;
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------
        private SqlCommand CreateUserUpdateCommandWithStoredProcedure(String spName, SqlConnection con, User user)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@email", user.Email);

            cmd.Parameters.AddWithValue("@firstName", user.FirstName);

            cmd.Parameters.AddWithValue("@lastName", user.LastName);

            cmd.Parameters.AddWithValue("@password", user.Password);

            cmd.Parameters.AddWithValue("@gender", user.Gender);

            string images = JsonSerializer.Serialize(user.Image);
            cmd.Parameters.AddWithValue("@image", images);

            cmd.Parameters.AddWithValue("@height", user.Height);

            cmd.Parameters.AddWithValue("@birthday", user.Birthday);

            cmd.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);

            cmd.Parameters.AddWithValue("isActive", user.IsActive);

            cmd.Parameters.AddWithValue("@timeStamp", user.TimeStamp);

            cmd.Parameters.AddWithValue("@city", user.City);

            cmd.Parameters.AddWithValue("@currentPlace", user.CurrentPlace);

            cmd.Parameters.AddWithValue("@persoalText", user.PersoalText);

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method checks a user login at user table 
        //--------------------------------------------------------------------------------------------------

        public User CheckLogin(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateLoginCommandWithStoredProcedure("SP_CheckLogin", con, user); // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                User u = null; // Initialize the User object

                while (dataReader.Read())
                {
                    u = new User
                    {
                        Email = dataReader["email"].ToString(),
                        FirstName = dataReader["firstName"].ToString(),
                        LastName = dataReader["lastName"].ToString(),
                        Password = dataReader["password"].ToString(),
                        Image = JsonSerializer.Deserialize<string[]>(dataReader["image"].ToString()),
                        Gender = Convert.ToInt32(dataReader["gender"]),
                        Height = Convert.ToInt32(dataReader["height"]),
                        Birthday = Convert.ToDateTime(dataReader["birthday"]),
                        PhoneNumber = dataReader["phoneNumber"].ToString(),
                        IsActive = Convert.ToBoolean(dataReader["isActive"]),
                        TimeStamp = Convert.ToDateTime(dataReader["timeStamp"]),
                        City = dataReader["city"].ToString(),
                        CurrentPlace = Convert.ToInt32(dataReader["currentPlace"]),
                        PersoalText = dataReader["persoalText"].ToString(),

                    };
                }

                if (u != null && u.IsActive)
                {
                    // Login successful
                    return u;
                }
                else
                {
                    // Login failed, return null or throw an exception as needed
                    return null;
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private SqlCommand CreateLoginCommandWithStoredProcedure(String spName, SqlConnection con, User user)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@inputEmail", user.Email);

            cmd.Parameters.AddWithValue("@inputPassword", user.Password);
            return cmd;
        }

        public void UpdateUserCurrentPlace(User user)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateUpdateUserCurrentPlaceCommand("SP_UpdateUserCurrentPlace", con, user); // create the command

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //---------------------------------------------------------------------------------
        // Create the SqlCommand for updating current place using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateUpdateUserCurrentPlaceCommand(string spName, SqlConnection con, User user)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // stored procedure name

            cmd.CommandTimeout = 10;           // Time to wait for the execution, default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text
            cmd.Parameters.AddWithValue("@email", user.Email);

            cmd.Parameters.AddWithValue("@firstName", user.FirstName);

            cmd.Parameters.AddWithValue("@lastName", user.LastName);

            cmd.Parameters.AddWithValue("@password", user.Password);

            cmd.Parameters.AddWithValue("@gender", user.Gender);

            string images = JsonSerializer.Serialize(user.Image);
            cmd.Parameters.AddWithValue("@image", images);

            cmd.Parameters.AddWithValue("@height", user.Height);

            cmd.Parameters.AddWithValue("@birthday", user.Birthday);

            cmd.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);

            cmd.Parameters.AddWithValue("isActive", user.IsActive);

            cmd.Parameters.AddWithValue("@timeStamp", user.TimeStamp);

            cmd.Parameters.AddWithValue("@city", user.City);

            cmd.Parameters.AddWithValue("@currentPlace", user.CurrentPlace);

            cmd.Parameters.AddWithValue("@persoalText", user.PersoalText);


            return cmd;
        }


        // Reading users by place
           
        public List<User> ReadUsersByPlace(int placeToLook)
        {

            SqlConnection con;
            SqlCommand cmd;
            List<User> usersList = new List<User>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectUserByPlaceWithStoredProcedure("SP_ReadUsersWithPreferencesByPlace", con, placeToLook);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    User u = new User();
                    u.Email = dataReader["email"].ToString();
                    u.FirstName = dataReader["firstName"].ToString();
                    u.LastName = dataReader["lastName"].ToString();
                    u.Password = dataReader["password"].ToString();
                    u.Image = JsonSerializer.Deserialize<string[]>(dataReader["image"].ToString());
                    u.Gender = Convert.ToInt32(dataReader["gender"]);
                    u.Height = Convert.ToInt32(dataReader["height"]);
                    u.Birthday = Convert.ToDateTime(dataReader["birthday"]);
                    u.TimeStamp = Convert.ToDateTime(dataReader["timeStamp"]);
                    u.City = dataReader["city"].ToString();
                    u.PreferencesDictionary = new Dictionary<string, string>
                    {
                        { "preferenceGender", dataReader["preferenceGender"].ToString() },
                        { "minAge", dataReader["minAge"].ToString() },
                        { "maxAge", dataReader["maxAge"].ToString() },
                        { "minHeight", dataReader["minHeight"].ToString() },
                        { "maxHeight", dataReader["maxHeight"].ToString() },
                        { "maxDistance", dataReader["maxDistance"].ToString() }
                    };
                    u.CurrentPlace = Convert.ToInt32(dataReader["currentPlace"]); ;
                    u.PersoalText = dataReader["persoalText"].ToString();

                    usersList.Add(u);
                    
                }
                return usersList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateSelectUserByPlaceWithStoredProcedure(String spName, SqlConnection con, int placeCode)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@currentPlace", placeCode);

            return cmd;
        }




        public User GetUserByEmail(string email)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectUserByEmailCommand("SP_GetUserByEmail", con, email); // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                User u = null; // Initialize the User object

                while (dataReader.Read())
                {
                    u = new User
                    {
                        Email = dataReader["email"].ToString(),
                        FirstName = dataReader["firstName"].ToString(),
                        LastName = dataReader["lastName"].ToString(),
                        Password = dataReader["password"].ToString(),
                        Image = JsonSerializer.Deserialize<string[]>(dataReader["image"].ToString()),
                        Gender = Convert.ToInt32(dataReader["gender"]),
                        Height = Convert.ToInt32(dataReader["height"]),
                        Birthday = Convert.ToDateTime(dataReader["birthday"]),
                        PhoneNumber = dataReader["phoneNumber"].ToString(),
                        IsActive = Convert.ToBoolean(dataReader["isActive"]),
                        City = dataReader["city"].ToString(),
                        TimeStamp = Convert.ToDateTime(dataReader["timeStamp"]),
                        CurrentPlace = Convert.ToInt32(dataReader["currentPlace"]),
                        PersoalText = dataReader["persoalText"].ToString(),
                    };
                }

                return u;
            }
            catch (SqlException ex)
            {
                // Log the SQL exception
                Console.WriteLine("SQL Exception:");
                Console.WriteLine($"Error Number: {ex.Number}");
                Console.WriteLine($"Message: {ex.Message}");
                // Additional error handling logic...

                // Rethrow the exception or return null
                throw; // Rethrow the exception to propagate it to the caller
            }
            
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }


        }

        public User GetUserPreferencesByEmail(string email)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectUserByEmailCommand("SP_GetUserWithPrefrencesByEmail", con, email); // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                User u = null; // Initialize the User object

                while (dataReader.Read())
                {
                    u = new User
                    {
                        Email = dataReader["email"].ToString(),
                        FirstName = dataReader["firstName"].ToString(),
                        LastName = dataReader["lastName"].ToString(),
                        Password = dataReader["password"].ToString(),
                        Image = JsonSerializer.Deserialize<string[]>(dataReader["image"].ToString()),
                        Gender = Convert.ToInt32(dataReader["gender"]),
                        Height = Convert.ToInt32(dataReader["height"]),
                        Birthday = Convert.ToDateTime(dataReader["birthday"]),
                        PhoneNumber = dataReader["phoneNumber"].ToString(),
                        IsActive = Convert.ToBoolean(dataReader["isActive"]),
                        City = dataReader["city"].ToString(),
                        TimeStamp = Convert.ToDateTime(dataReader["timeStamp"]),
                        PreferencesDictionary = new Dictionary<string, string>
                        {
                            { "preferenceGender", dataReader["preferenceGender"].ToString() },
                            { "minAge", dataReader["minAge"].ToString() },
                            { "maxAge", dataReader["maxAge"].ToString() },
                            { "minHeight", dataReader["minHeight"].ToString() },
                            { "maxHeight", dataReader["maxHeight"].ToString() },
                            { "maxDistance", dataReader["maxDistance"].ToString() }
                        },
                        CurrentPlace = Convert.ToInt32(dataReader["currentPlace"]),
                        PersoalText = dataReader["persoalText"].ToString(),

                    };
                }

                return u;
            }
            catch (SqlException ex)
            {
                // Log the SQL exception
                Console.WriteLine("SQL Exception:");
                Console.WriteLine($"Error Number: {ex.Number}");
                Console.WriteLine($"Message: {ex.Message}");
                // Additional error handling logic...

                // Rethrow the exception or return null
                throw; // Rethrow the exception to propagate it to the caller
            }
           
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        private SqlCommand CreateSelectUserByEmailCommand(String spName, SqlConnection con, string email)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@inputEmail", email); // Add parameter for email

            return cmd;
        }



        public int ChangeUserImages(string email, string[] images)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateChangeUserImagesCommandWithStoredProcedure("SP_ChangeUserImages", con, email, images); // Create the command

            try
            {
                //Execute the command
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // Write to log
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

       
            
        //---------------------------------------------------------------------------------
        // Create the SqlCommand for changing user images using a stored procedure
        //---------------------------------------------------------------------------------

        private SqlCommand CreateChangeUserImagesCommandWithStoredProcedure(String spName, SqlConnection con, string email, string[] images)
    {
        SqlCommand cmd = new SqlCommand(); // Create the command object

        cmd.Connection = con; // Assign the connection to the command object

        cmd.CommandText = spName; // Can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10; // Time to wait for the execution. The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // The type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", email);

        string imagesJson = JsonSerializer.Serialize(images);
        cmd.Parameters.AddWithValue("@images", imagesJson);

        return cmd;
    }

    public int AddImage(byte[] imageData, string mimeType)
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlCommand cmd1;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateAddImageCommand("SP_AddImage", con, imageData, mimeType); // create the command
            cmd1 = CreateGetImageIdCommand("SP_GetLastImageId", con); // create the command

            try
            {
                cmd.ExecuteNonQuery();
                int imageId = (int)cmd1.ExecuteScalar();
                return imageId;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateAddImageCommand(string spName, SqlConnection con, byte[] imageData, string mimeType)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // stored procedure name

            cmd.CommandTimeout = 10;           // Time to wait for the execution, default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@imageData", imageData);

            cmd.Parameters.AddWithValue("@mimeType", mimeType);

            return cmd;
        }


        private SqlCommand CreateGetImageIdCommand(string spName, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // stored procedure name

            cmd.CommandTimeout = 10;           // Time to wait for the execution, default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }


        public (byte[] ImageData, string MimeType) GetImage(int imageId)
        {
            SqlConnection con;
            SqlCommand cmd;
            byte[] imageData = null;
            string mimeType = null;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            try
            {
                cmd = CreateGetImageCommand("SP_GetImage", con, imageId); // create the command
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    imageData = reader["ImageData"] as byte[];
                    mimeType = reader["MimeType"] as string;
                }
            }
            catch (Exception ex)
            {
                // write to log or handle exception
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close(); // close the connection
                }
            }

            return (imageData, mimeType);
        }

        private SqlCommand CreateGetImageCommand(string spName, SqlConnection con, int imageId)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // stored procedure name

            cmd.CommandTimeout = 10;           // Time to wait for the execution, default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@ImageId", imageId);

            return cmd;
        }



        public void InsertUserPersonalInterests(string email, List<int> interestCodes)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                con = connect("myProjDB"); // create the connection

                foreach (var interestCode in interestCodes)
                {
                    cmd = CreateAddPersonalInterestCommand("SP_InsertUserPersonalInterest", con, email, interestCode); // create the command

                    // Execute the command
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private SqlCommand CreateAddPersonalInterestCommand(string spName, SqlConnection con, string email, int interestCode)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // stored procedure name

            cmd.CommandTimeout = 10;           // Time to wait for the execution, default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@email", email);

            cmd.Parameters.AddWithValue("@interestCode", interestCode);

            return cmd;
        }


        public void DeleteUserPersonalInterests(string email)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;

            try
            {
                con = connect("myProjDB"); // create the connection

                cmd = CreateDeletePersonalInterestCommand("SP_DeleteUserPersonalInterests", con, email); // create the command

                // Execute the command
                cmd.ExecuteNonQuery();
            
            }
            catch (Exception ex)
            {
                // write to log
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private SqlCommand CreateDeletePersonalInterestCommand(string spName, SqlConnection con, string email)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // stored procedure name

            cmd.CommandTimeout = 10;           // Time to wait for the execution, default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@email", email);

            return cmd;
        }



        public List<int> GetUserInterestCodesByEmail(string email)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            List<int> interestCodes = new List<int>();

            try
            {
                con = connect("myProjDB");
                cmd = CreateSelectUPIByEmailCommand("SP_ReadUserPersonalInterests", con, email);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int interestDesc = Convert.ToInt32(reader["interestCode"]);
                    interestCodes.Add(interestDesc);
                }

                reader.Close();
                return interestCodes;
            }
            catch (Exception ex)
            {
                // Handle exception, log error, etc.
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private SqlCommand CreateSelectUPIByEmailCommand(string spName, SqlConnection con, string email)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@inputEmail", email); // Add parameter for email

            return cmd;
        }

        public int AddLike(string firstEmail, string secondEmail, int currentPlase)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                // create the connection
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateNewLikeCommandWithStoredProcedure("SP_InsertNewLike", con, firstEmail, secondEmail, currentPlase);  // create the command

            try
            {
                // execute the command
                int numEffected = cmd.ExecuteNonQuery();
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //---------------------------------------------------------------------------------
        // Create the SqlCommand for insrting new user using a stored procedure
        //---------------------------------------------------------------------------------

        private SqlCommand CreateNewLikeCommandWithStoredProcedure(String spName, SqlConnection con, string firstEmail, string secondEmail, int currentPlase)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@FirstEmail", firstEmail);
            cmd.Parameters.AddWithValue("@SecondEmail", secondEmail);
            cmd.Parameters.AddWithValue("@placeCode", currentPlase);


            return cmd;
        }


        public List<string> GetFriendsNames(string email)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            List<string> friendsList = new List<string>();

            try
            {
                con = connect("myProjDB");
                cmd = CreateSelectFriendsWithStoredProcedure("SP_getFrinedsList", con, email);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string friendName = reader["עם מי בילית היום"].ToString();
                    friendsList.Add(friendName);
                }

                reader.Close();
                return friendsList;
            }
            catch (Exception ex)
            {
                // Handle exception, log error, etc.
                throw ex;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private SqlCommand CreateSelectFriendsWithStoredProcedure(string spName, SqlConnection con, string email)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@email", email); // Add parameter for email

            return cmd;
        }


        public IEnumerable<dynamic> ReadLikesByEmail(string inputEmail)
            {

            SqlConnection con;
            SqlCommand cmd;
            List<Object> likesList = new List<Object>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectLikesByEmailWithStoredProcedure("SP_ReadLikesByEmail", con, inputEmail);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    var like = new
                    {
                        //FirstEmail = dataReader["FirstEmail"].ToString(),
                        SecondEmail = dataReader["SecondEmail"].ToString(),
                        PlaceCode = Convert.ToInt32(dataReader["PlaceCode"])
                    };

                    likesList.Add(like);
                };
                return likesList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        public IEnumerable<dynamic> GetlikesForMe(string inputEmail)
        {

            SqlConnection con;
            SqlCommand cmd;
            List<Object> likesList = new List<Object>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectLikesByEmailWithStoredProcedure("SP_ReadLikesForMe", con, inputEmail);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    var like = new
                    {
                        FirstEmail = dataReader["FirstEmail"].ToString(),
                        //SecondEmail = dataReader["SecondEmail"].ToString(),
                        PlaceCode = Convert.ToInt32(dataReader["PlaceCode"])
                    };

                    likesList.Add(like);
                };
                return likesList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateSelectLikesByEmailWithStoredProcedure(String spName, SqlConnection con, string inputEmail)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@inputEmail", inputEmail);
            return cmd;
        }


        public IEnumerable<dynamic> ReadAnalysisByEmail(string inputEmail)
        {

            SqlConnection con;
            SqlCommand cmd;
            List<dynamic> results = new List<dynamic>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectDataByEmailWithStoredProcedure("SP_getAnalysisForUser", con, inputEmail);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    var data = new
                    {
                        MostFrequentFriend = dataReader["MostFrequentFriend"].ToString(),
                        FriendPercentage = Convert.ToDouble(dataReader["FriendPercentage"]),
                        MostFrequentPlace = dataReader["MostFrequentPlace"].ToString(),
                        PlacePercentage = Convert.ToDouble(dataReader["PlacePercentage"]),
                        MostFrequentWeekday = dataReader["MostFrequentWeekday"].ToString(),
                        WeekdayPercentage = Convert.ToDouble(dataReader["WeekdayPercentage"])
                    };
                    results.Add(data);

                };
                return results;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        public IEnumerable<dynamic> ReadPreferenceAnalysisByEmail(string inputEmail)
        {
            SqlConnection con;
            SqlCommand cmd;
            List<dynamic> results = new List<dynamic>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectDataByEmailWithStoredProcedure("SP_GetUserPreferenceAnalysis", con, inputEmail); // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    var data = new
                    {
                        UserEmail = dataReader["UserEmail"].ToString(),
                        HeightMatchPercentage = Convert.ToDouble(dataReader["HeightMatchPercentage"]),
                        HeightMismatchPercentage = Convert.ToDouble(dataReader["HeightMismatchPercentage"]),
                        AgeMatchPercentage = Convert.ToDouble(dataReader["AgeMatchPercentage"]),
                        AgeMismatchPercentage = Convert.ToDouble(dataReader["AgeMismatchPercentage"])
                    };
                    results.Add(data);
                }
                return results;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        public IEnumerable<dynamic> ReadPersonalInterestAnalysisByEmail(string inputEmail)
        {
            SqlConnection con;
            SqlCommand cmd;
            List<dynamic> results = new List<dynamic>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectDataByEmailWithStoredProcedure("SP_GetInterestAnalysis", con, inputEmail); // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    var data = new
                    {
                        CommonInterest = dataReader["CommonInterest"].ToString(),
                        HeightMatchPercentage = Convert.ToDouble(dataReader["InterestPercentage"])
                        
                    };
                    results.Add(data);
                }
                return results;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateSelectDataByEmailWithStoredProcedure(String spName, SqlConnection con, string inputEmail)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@Email", inputEmail);
            return cmd;
        }

        


    }

}
