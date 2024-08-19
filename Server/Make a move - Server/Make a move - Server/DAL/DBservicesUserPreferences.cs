using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;
using Make_a_move___Server.BL;

namespace Make_a_move___Server.DAL
{
    public class DBservicesUserPreferences
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
        // This method Inserts a preferences to the Preferences table 
        // --------------------------------------------------------------------------------------------------

        public int InsertUserPreference(UserPreferences userpreference)
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

            cmd = CreateUserPreferenceInsertCommandWithStoredProcedure("SP_InsertNewUserPreferences", con, userpreference);  // create the command

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
        // Create the SqlCommand for insrting new preferences using a stored procedure
        //---------------------------------------------------------------------------------

        private SqlCommand CreateUserPreferenceInsertCommandWithStoredProcedure(String spName, SqlConnection con, UserPreferences userpreference)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@email", userpreference.Email);
            cmd.Parameters.AddWithValue("@preferenceGender", userpreference.PreferenceGender);
            cmd.Parameters.AddWithValue("@minAge", userpreference.MinAge);
            cmd.Parameters.AddWithValue("@maxAge", userpreference.MaxAge);
            cmd.Parameters.AddWithValue("@minHeight", userpreference.MinHeight);
            cmd.Parameters.AddWithValue("@maxHeight", userpreference.MaxHeight);
            cmd.Parameters.AddWithValue("@maxDistance", userpreference.MaxDistance);


            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method reads preference from the database 
        //--------------------------------------------------------------------------------------------------
        public List<UserPreferences> ReadUserPreference()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<UserPreferences> userspreferenceList = new List<UserPreferences>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectUserPreferenceListWithStoredProcedure("SP_UReadUserPreferences", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    UserPreferences up = new UserPreferences();

                    up.Email = dataReader["email"].ToString();
                    up.PreferenceGender = Convert.ToInt32(dataReader["preferenceGender"]);
                    up.MinAge = Convert.ToInt32(dataReader["minAge"]);
                    up.MaxAge = Convert.ToInt32(dataReader["maxAge"]);
                    up.MinHeight = Convert.ToInt32(dataReader["minHeight"]);
                    up.MaxHeight = Convert.ToInt32(dataReader["maxHeight"]);
                    up.MaxDistance = Convert.ToInt32(dataReader["maxDistance"]);

                    userspreferenceList.Add(up);
                }
                return userspreferenceList;
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
        private SqlCommand CreateSelectUserPreferenceListWithStoredProcedure(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }


        //--------------------------------------------------------------------------------------------------
        // This method reads preference from the database by email
        //--------------------------------------------------------------------------------------------------
        public UserPreferences ReadUserPreferencesByEmail(string email)
        {

            SqlConnection con;
            SqlCommand cmd;
            UserPreferences usersPreferences = new UserPreferences();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectUserPreferenceListByEmailWithStoredProcedure("SP_ReadUserPreferencesByEmail", con, email);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    UserPreferences up = new UserPreferences();

                    up.Email = dataReader["email"].ToString();
                    up.PreferenceGender = Convert.ToInt32(dataReader["preferenceGender"]);
                    up.MinAge = Convert.ToInt32(dataReader["minAge"]);
                    up.MaxAge = Convert.ToInt32(dataReader["maxAge"]);
                    up.MinHeight = Convert.ToInt32(dataReader["minHeight"]);
                    up.MaxHeight = Convert.ToInt32(dataReader["maxHeight"]);
                    up.MaxDistance = Convert.ToInt32(dataReader["maxDistance"]);

                    usersPreferences = up;
                }
                return usersPreferences;
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
        private SqlCommand CreateSelectUserPreferenceListByEmailWithStoredProcedure(String spName, SqlConnection con, string email)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@email", email);

            return cmd;
        }



        //--------------------------------------------------------------------------------------------------
        // This method Updates a preference at Preference table 
        //--------------------------------------------------------------------------------------------------

        public UserPreferences UpdateUserPreference(UserPreferences userpreference)
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

            cmd = CreatepreUserfrefernceUpdateCommandWithStoredProcedure("SP_UpdateUserPreferences", con, userpreference);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                UserPreferences up = null; // Initialize the Feedback object

                while (dataReader.Read())
                {
                    up = new UserPreferences
                    {
                        Email = dataReader["email"].ToString(),
                        PreferenceGender = Convert.ToInt32(dataReader["preferenceGender"]),
                        MinAge = Convert.ToInt32(dataReader["minAge"]),
                        MaxAge = Convert.ToInt32(dataReader["maxAge"]),
                        MinHeight = Convert.ToInt32(dataReader["minHeight"]),
                        MaxHeight = Convert.ToInt32(dataReader["maxHeight"]),
                        MaxDistance = Convert.ToInt32(dataReader["maxDistance"])

                    };
                }

                if (up != null)
                {
                    // Login successful
                    return up;
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
        private SqlCommand CreatepreUserfrefernceUpdateCommandWithStoredProcedure(String spName, SqlConnection con, UserPreferences userpreference)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@email", userpreference.Email);
            cmd.Parameters.AddWithValue("@preferenceGender", userpreference.PreferenceGender);
            cmd.Parameters.AddWithValue("@minAge", userpreference.MinAge);
            cmd.Parameters.AddWithValue("@maxAge", userpreference.MaxAge);
            cmd.Parameters.AddWithValue("@minHeight", userpreference.MinHeight);
            cmd.Parameters.AddWithValue("@maxHeight", userpreference.MaxHeight);
            cmd.Parameters.AddWithValue("@maxDistance", userpreference.MaxDistance);

            return cmd;
        }





    }
}
