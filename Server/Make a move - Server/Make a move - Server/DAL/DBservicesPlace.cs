using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using Make_a_move___Server.BL;

namespace Make_a_move___Server.DAL
{
    public class DBservicesPlace
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
        // This method Inserts a Place to the Places table 
        // --------------------------------------------------------------------------------------------------

        public int InsertPlace(Place place)
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

            cmd = CreatePlaceInsertCommandWithStoredProcedure("SP_InsertNewPlace", con, place);  // create the command

            try
            {
                // execute the command
                int numEffected = cmd.ExecuteNonQuery();
                return place.PlaceCode;
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
        // Create the SqlCommand for insrting new place using a stored procedure
        //---------------------------------------------------------------------------------

        private SqlCommand CreatePlaceInsertCommandWithStoredProcedure(String spName, SqlConnection con, Place place)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@placeCode", place.PlaceCode);

            cmd.Parameters.AddWithValue("@name", place.Name);

            cmd.Parameters.AddWithValue("@address", place.Address);

            cmd.Parameters.AddWithValue("@typeOfPlace", place.TypeOfPlace);

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method reads places from the database 
        //--------------------------------------------------------------------------------------------------
        public List<Place> ReadPlaces()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<Place> placeList = new List<Place>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectPlaceWithStoredProcedure("SP_ReadPlaces", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Place p = new Place();
                    p.PlaceCode = Convert.ToInt32(dataReader["placeCode"]);
                    p.Name = dataReader["name"].ToString();
                    p.Address = dataReader["address"].ToString();
                    p.TypeOfPlace = dataReader["typeOfPlace"].ToString();

                    placeList.Add(p);
                }
                return placeList;
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
        private SqlCommand CreateSelectPlaceWithStoredProcedure(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method Updates a place at Places table 
        //--------------------------------------------------------------------------------------------------

        public Place UpdatePlace(Place place)
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

            cmd = CreatePlaceUpdateCommandWithStoredProcedure("SP_UpdatePlace", con, place);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Place p = null; // Initialize the Place object

                while (dataReader.Read())
                {
                    p = new Place
                    {
                        PlaceCode = Convert.ToInt32(dataReader["placeCode"]),
                        Name = dataReader["name"].ToString(),
                        Address = dataReader["address"].ToString(),
                        TypeOfPlace = dataReader["typeOfPlace"].ToString()
                };
                }

                if (p != null)
                {
                    // Login successful
                    return p;
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
        private SqlCommand CreatePlaceUpdateCommandWithStoredProcedure(String spName, SqlConnection con, Place place)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@placeCode", place.PlaceCode);

            cmd.Parameters.AddWithValue("@name", place.Name);

            cmd.Parameters.AddWithValue("@address", place.Address);

            cmd.Parameters.AddWithValue("@typeOfPlace", place.TypeOfPlace);


            return cmd;
        }




    }
}
