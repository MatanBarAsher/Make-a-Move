using System.Data;
using System.Data.SqlClient;
using Make_a_move___Server.BL;

namespace Make_a_move___Server.DAL
{
    public class DBservicesPersonalInterests
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
        // This method Inserts a personalInterests to the PersonalInterests table 
        // --------------------------------------------------------------------------------------------------

        public int InsertPersonalInterests(PersonalInterests personalInterests)
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

            cmd = CreatePersonalInterestsInsertCommandWithStoredProcedure("SP_InsertNewPersonalInterest", con, personalInterests);  // create the command

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
        // Create the SqlCommand for insrting new personalInterests using a stored procedure
        //---------------------------------------------------------------------------------

        private SqlCommand CreatePersonalInterestsInsertCommandWithStoredProcedure(String spName, SqlConnection con, PersonalInterests personalInterests)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@interestCode", personalInterests.InterestCode);

            cmd.Parameters.AddWithValue("@interestDesc", personalInterests.InterestDesc);


            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method reads personalInterests from the database 
        //--------------------------------------------------------------------------------------------------
        public List<PersonalInterests> ReadPersonalInterests()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<PersonalInterests> personalInterestsList = new List<PersonalInterests>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectPersonalInterestsWithStoredProcedure("SP_ReadPersonalInterests", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    PersonalInterests pi = new PersonalInterests();
                    pi.InterestCode = Convert.ToInt32(dataReader["interestCode"]);
                    pi.InterestDesc = dataReader["interestDesc"].ToString();


                    personalInterestsList.Add(pi);
                }
                return personalInterestsList;
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
        private SqlCommand CreateSelectPersonalInterestsWithStoredProcedure(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method Updates a personalInterests at Cities table 
        //--------------------------------------------------------------------------------------------------

        public PersonalInterests UpdatePersonalInterests(PersonalInterests personalInterests)
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

            cmd = CreatepersonalInterestsUpdateCommandWithStoredProcedure("SP_UpdatePersonalInterests", con, personalInterests);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                PersonalInterests pi = null; // Initialize the PersonalInterests object

                while (dataReader.Read())
                {
                    pi = new PersonalInterests
                    {
                        InterestCode = Convert.ToInt32(dataReader["interestCode"]),
                        InterestDesc = dataReader["interestDesc"].ToString(),

                    };
                }

                if (pi != null)
                {
                    // Login successful
                    return pi;
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
        private SqlCommand CreatepersonalInterestsUpdateCommandWithStoredProcedure(String spName, SqlConnection con, PersonalInterests personalInterests)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@interestCode", personalInterests.InterestCode);

            cmd.Parameters.AddWithValue("@interestDesc", personalInterests.InterestDesc);



            return cmd;
        }







    }
}
