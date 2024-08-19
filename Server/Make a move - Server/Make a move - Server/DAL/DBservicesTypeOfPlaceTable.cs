using System.Data;
using System.Data.SqlClient;
using Make_a_move___Server.BL;

namespace Make_a_move___Server.DAL
{
    public class DBservicesTypeOfPlace
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
        // This method Inserts a typeOfPlace to the TypeOfPlace table 
        // --------------------------------------------------------------------------------------------------

        public int InsertTypeOfPlace(TypeOfPlace typeOfPlace)
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

            cmd = CreateTypeOfPlaceInsertCommandWithStoredProcedure("SP_InsertNewTypeOfPlace", con, typeOfPlace);  // create the command

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
        // Create the SqlCommand for insrting new typeOfPlace using a stored procedure
        //---------------------------------------------------------------------------------

        private SqlCommand CreateTypeOfPlaceInsertCommandWithStoredProcedure(String spName, SqlConnection con, TypeOfPlace typeOfPlace)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@typeOfPlaceCode", typeOfPlace.TypeOfPlaceCode);

            cmd.Parameters.AddWithValue("@typeOfPlaceDescription", typeOfPlace.TypeOfPlaceDescription);


            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method reads typeOfPlace from the database 
        //--------------------------------------------------------------------------------------------------
        public List<TypeOfPlace> ReadTypeOfPlace()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<TypeOfPlace> typeOfPlaceList = new List<TypeOfPlace>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectTypeOfPlaceWithStoredProcedure("SP_ReadTypeOfPlace", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    TypeOfPlace t = new TypeOfPlace();
                    t.TypeOfPlaceCode = Convert.ToInt32(dataReader["typeOfPlaceCode"]);
                    t.TypeOfPlaceDescription = dataReader["typeOfPlaceDescription"].ToString();


                    typeOfPlaceList.Add(t);
                }
                return typeOfPlaceList;
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
        private SqlCommand CreateSelectTypeOfPlaceWithStoredProcedure(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method Updates a typeOfPlace at TypeOfPlace table 
        //--------------------------------------------------------------------------------------------------

        public TypeOfPlace UpdateTypeOfPlace(TypeOfPlace typeOfPlace)
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

            cmd = CreateTypeOfPlaceUpdateCommandWithStoredProcedure("SP_UpdateTypeOfPlace", con, typeOfPlace);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                TypeOfPlace t = null; // Initialize the TypeOfPlace object

                while (dataReader.Read())
                {
                    t = new TypeOfPlace
                    {
                        TypeOfPlaceCode = Convert.ToInt32(dataReader["typeOfPlaceCode"]),
                        TypeOfPlaceDescription = dataReader["typeOfPlaceDescription"].ToString(),

                    };
                }

                if (t != null)
                {
                    // Login successful
                    return t;
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
        private SqlCommand CreateTypeOfPlaceUpdateCommandWithStoredProcedure(String spName, SqlConnection con, TypeOfPlace typeOfPlace)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@typeOfPlaceCode", typeOfPlace.TypeOfPlaceCode);

            cmd.Parameters.AddWithValue("@typeOfPlaceDescription", typeOfPlace.TypeOfPlaceDescription);



            return cmd;
        }







    }
}
