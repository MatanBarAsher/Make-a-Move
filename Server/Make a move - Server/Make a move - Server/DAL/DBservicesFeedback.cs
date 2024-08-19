using System.Data;
using System.Data.SqlClient;
using Make_a_move___Server.BL;

namespace Make_a_move___Server.DAL
{
    public class DBservicesFeedback
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
        // This method Inserts a feedback to the Feedback table 
        // --------------------------------------------------------------------------------------------------

        public int InsertFeedback(Feedback feedback)
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

            cmd = CreateFeedbackInsertCommandWithStoredProcedure("InsertFeedback", con, feedback);  // create the command

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
        // Create the SqlCommand for insrting new feedback using a stored procedure
        //---------------------------------------------------------------------------------

        private SqlCommand CreateFeedbackInsertCommandWithStoredProcedure(String spName, SqlConnection con, Feedback feedback)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@email", feedback.Email);
            cmd.Parameters.AddWithValue("@matchId", feedback.MatchId);
            cmd.Parameters.AddWithValue("@בעל_מאפיינים_דומים_למה_שאני_מחפשת", feedback.Q11);
            cmd.Parameters.AddWithValue("@התמונות_תואמות_למציאות", feedback.Q21);
            cmd.Parameters.AddWithValue("@תחומי_העניין_ששתיפ_ה_עזרו_לי_לפתח_איתו_ה_שיחה", feedback.Q31);
            cmd.Parameters.AddWithValue("@הייתי_רוצה_להיפגש_איתו_שוב", feedback.Q41);
            cmd.Parameters.AddWithValue("@עם_מי_בילית_היום", feedback.Name);

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method reads feedback from the database 
        //--------------------------------------------------------------------------------------------------
        public List<Feedback> ReadFeedback()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<Feedback> feedbackList = new List<Feedback>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectFeedbackWithStoredProcedure("SP_ReadFeedback", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Feedback f = new Feedback();
                    f.Email = dataReader["Email"].ToString();
                    f.MatchId = Convert.ToInt32(dataReader["matchId"]);
                    f.Q11 = Convert.ToInt32(dataReader["בעל מאפיינים דומים למה שאני מחפשת"]);
                    f.Q21 = Convert.ToInt32(dataReader["התמונות תואמות למציאות"]);
                    f.Q31 = Convert.ToInt32(dataReader["תחומי העניין ששתיפ_ה עזרו לי לפתח איתו_ה שיחה"]);
                    f.Q41 = Convert.ToInt32(dataReader["הייתי רוצה להיפגש איתו שוב"]);
                    f.Name = dataReader["עם מי בילית היום"].ToString();




                    feedbackList.Add(f);
                }
                return feedbackList;
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
        private SqlCommand CreateSelectFeedbackWithStoredProcedure(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method Updates a feedback at Feedback table 
        //--------------------------------------------------------------------------------------------------

        public Feedback UpdateFeedback(Feedback feedback)
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

            cmd = CreateFeedbackUpdateCommandWithStoredProcedure("SP_UpdateFeedback", con, feedback);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Feedback f = null; // Initialize the Feedback object

                while (dataReader.Read())
                {
                    f = new Feedback
                    {
                    Email = dataReader["Email"].ToString(),
                    MatchId = Convert.ToInt32(dataReader["matchId"]),
                    Q11 = Convert.ToInt32(dataReader["בעל מאפיינים דומים למה שאני מחפשת"]),
                    Q21 = Convert.ToInt32(dataReader["התמונות תואמות למציאות"]),
                    Q31 = Convert.ToInt32(dataReader["תחומי העניין ששתיפ_ה עזרו לי לפתח איתו_ה שיחה"]),
                    Q41 = Convert.ToInt32(dataReader["הייתי רוצה להיפגש איתו שוב"]),
                    Name = dataReader["עם מי בילית היום"].ToString()

                };
                }

                if (f != null)
                {
                    // Login successful
                    return f;
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
        private SqlCommand CreateFeedbackUpdateCommandWithStoredProcedure(String spName, SqlConnection con, Feedback feedback)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text


            cmd.Parameters.AddWithValue("@email", feedback.Email);
            cmd.Parameters.AddWithValue("@matchId", feedback.MatchId);
            cmd.Parameters.AddWithValue("@בעל_מאפיינים_דומים_למה_שאני_מחפשת", feedback.Q11);
            cmd.Parameters.AddWithValue("@התמונות_תואמות_למציאות", feedback.Q21);
            cmd.Parameters.AddWithValue("@תחומי_העניין_ששתיפ_ה_עזרו_לי_לפתח_איתו_ה_שיחה", feedback.Q31);
            cmd.Parameters.AddWithValue("@הייתי_רוצה_להיפגש_איתו_שוב", feedback.Q41);
            cmd.Parameters.AddWithValue("@עם_מי_בילית_היום", feedback.Name);


            return cmd;
        }


        public List<Feedback> ReadFeedbackByEmail(string email)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            List<Feedback> feedbackList = new List<Feedback>();

            try
            {
                con = connect("myProjDB"); // create the connection
                cmd = CreateSelectFeedbackByEmailWithStoredProcedure("SP_GetFeedbackByEmail", con, email); // create the command
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Feedback f = new Feedback
                    {
                        Email = dataReader["email"].ToString(),
                        MatchId = Convert.ToInt32(dataReader["matchId"]),
                        Q11 = Convert.ToInt32(dataReader["בעל מאפיינים דומים למה שאני מחפשת"]),
                        Q21 = Convert.ToInt32(dataReader["התמונות תואמות למציאות"]),
                        Q31 = Convert.ToInt32(dataReader["תחומי העניין ששתיפ_ה עזרו לי לפתח איתו_ה שיחה"]),
                        Q41 = Convert.ToInt32(dataReader["הייתי רוצה להיפגש איתו שוב"]),
                        Name = dataReader["עם מי בילית היום"].ToString()
                    };

                    feedbackList.Add(f);
                }
                return feedbackList;
            }
            catch (Exception ex)
            {
                // Log the exception (for example, using a logger)
                throw new Exception("Error retrieving feedback data", ex);
            }
            finally
            {
                con?.Close(); // close the db connection
            }
        }

        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure
        //---------------------------------------------------------------------------------
        private SqlCommand CreateSelectFeedbackByEmailWithStoredProcedure(string spName, SqlConnection con, string email)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = con,              // assign the connection to the command object
                CommandText = spName,          // can be Select, Insert, Update, Delete 
                CommandTimeout = 10,           // Time to wait for the execution; the default is 30 seconds
                CommandType = System.Data.CommandType.StoredProcedure // the type of the command, can also be text
            };

            cmd.Parameters.AddWithValue("@Email", email);
            return cmd;
        }







    }
}
