using System.Data;
using System.Data.SqlClient;
using Make_a_move___Server.BL;

namespace Make_a_move___Server.DAL
{
    public class DBservicesSecondFeedback
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

        public int InsertSecondFeedback(SecondFeedback feedback)
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

            cmd = CreateFeedbackInsertCommandWithStoredProcedure("SP_InsertSecondFeedback", con, feedback);  // create the command

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

        private SqlCommand CreateFeedbackInsertCommandWithStoredProcedure(String spName, SqlConnection con, SecondFeedback feedback)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@email", feedback.Email);
            cmd.Parameters.AddWithValue("@matchId", feedback.MatchId);
            cmd.Parameters.AddWithValue("@נפגשנו_שוב", feedback.Q11);
            cmd.Parameters.AddWithValue("@אני_מאמין_ה_שנקבע_להיפגש_שוב", feedback.Q21);
            cmd.Parameters.AddWithValue("@אני_חושב_ת_שההתאמה_הייתה_נכונה_עבורי", feedback.Q31);


            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method reads feedback from the database 
        //--------------------------------------------------------------------------------------------------
        public List<SecondFeedback> ReadFeedback()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<SecondFeedback> feedbackList = new List<SecondFeedback>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectFeedbackWithStoredProcedure("SP_ReadSecondFeedback", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    SecondFeedback f = new SecondFeedback();
                    f.Email = dataReader["Email"].ToString();
                    f.MatchId = Convert.ToInt32(dataReader["matchId"]);
                    f.Q11 = Convert.ToInt32(dataReader["נפגשנו שוב"]);
                    f.Q21 = Convert.ToInt32(dataReader["אני מאמין/ה שנקבע להיפגש שוב"]);
                    f.Q31 = Convert.ToInt32(dataReader["אני חושב/ת שההתאמה הייתה נכונה עבורי"]);
                    
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

        public List<SecondFeedback> ReadSecondFeedbackByEmail(string email)
        {

            SqlConnection con =null;
            SqlCommand cmd;
            List<SecondFeedback> feedbackList = new List<SecondFeedback>();

            try
            {
                con = connect("myProjDB"); // create the connection
                cmd = CreateSelectFeedbackByEmailWithStoredProcedure("SP_ReadSecondFeedbackByEmaill", con, email); // create the command
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    SecondFeedback f = new SecondFeedback
                    {
                        Email = dataReader["Email"].ToString(),
                        MatchId = Convert.ToInt32(dataReader["matchId"]),
                        Q11 = Convert.ToInt32(dataReader["נפגשנו שוב"]),
                        Q21 = Convert.ToInt32(dataReader["אני מאמין/ה שנקבע להיפגש שוב"]),
                        Q31 = Convert.ToInt32(dataReader["אני חושב/ת שההתאמה הייתה נכונה עבורי"])
                    };
                    feedbackList.Add(f);
                }
                return feedbackList;
            }
            catch (Exception ex)
            {
                // write to log
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
