using Make_a_move___Server.BL;
using System;
using System.Data;
using System.Data.SqlClient;


namespace Make_a_move___Server.DAL
{
    public class DBservicesMatch
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
        // This method reads matches from the database 
        //--------------------------------------------------------------------------------------------------
        public List<Match> ReadMatches()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<Match> matchesList = new List<Match>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectMatchWithStoredProcedure("SP_ReadMatches", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                   Match m = new Match();
                    m.Firstemail = dataReader["Firstemail"].ToString();
                    m.Secondemail = dataReader["Secondemail"].ToString();
                    //m.TimeStamp = dataReader["Firstemail"].ToString();
                    m.PlaceCode = Convert.ToInt32(dataReader["placeCode"]);
                    m.MatchNum = Convert.ToInt32(dataReader["MatchNum"]);


                    matchesList.Add(m);
                }
                return matchesList;
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
        private SqlCommand CreateSelectMatchWithStoredProcedure(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }


        public List<Match> ReadMatchesByEmail(string inputEmail)
        {

            SqlConnection con;
            SqlCommand cmd;
            List<Match> matchesList = new List<Match>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectMatchByEmailWithStoredProcedure("SP_ReadMatchesByEmail", con, inputEmail);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Match m = new Match();
                    m.Firstemail = dataReader["Firstemail"].ToString();
                    m.Secondemail = dataReader["Secondemail"].ToString();
                    //m.TimeStamp = dataReader["Firstemail"].ToString();
                    m.PlaceCode = Convert.ToInt32(dataReader["placeCode"]);
                    m.MatchNum = Convert.ToInt32(dataReader["MatchNum"]);


                    matchesList.Add(m);
                }
                return matchesList;
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
        private SqlCommand CreateSelectMatchByEmailWithStoredProcedure(String spName, SqlConnection con, string inputEmail)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@inputEmail", inputEmail);
            return cmd;
        }




   



    }


} 


