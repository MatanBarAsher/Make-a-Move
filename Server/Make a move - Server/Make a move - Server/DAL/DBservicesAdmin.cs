using System.Data;
using System.Data.SqlClient;
using Make_a_move___Server.BL;

namespace Make_a_move___Server.DAL
{
    public class DBservicesAdmin
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
        // This method Inserts a Admin to the Admin table 
        // --------------------------------------------------------------------------------------------------

        public int InsertAdmin(Admin admin)
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

            cmd = CreateAdminInsertCommandWithStoredProcedure("SP_InsertNewAdmin", con, admin);  // create the command

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
        // Create the SqlCommand for insrting new admin using a stored procedure
        //---------------------------------------------------------------------------------

        private SqlCommand CreateAdminInsertCommandWithStoredProcedure(String spName, SqlConnection con, Admin admin)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@adminCode", admin.AdminCode);

            cmd.Parameters.AddWithValue("@adminName", admin.AdminName);

            cmd.Parameters.AddWithValue("@adminPassword", admin.AdminPassword);

            cmd.Parameters.AddWithValue("@isActive", admin.IsActive);


            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method reads admins from the database 
        //--------------------------------------------------------------------------------------------------
        public List<Admin> ReadAdmin()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<Admin> admimList = new List<Admin>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateSelectAdminWithStoredProcedure("SP_ReadAdmin", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    Admin a = new Admin();
                    a.AdminCode = Convert.ToInt32(dataReader["adminCode"]);
                    a.AdminName = dataReader["adminName"].ToString();
                    a.AdminPassword = dataReader["adminPassword"].ToString();
                    a.IsActive = Convert.ToBoolean(dataReader["isActive"]);
                   
                    admimList.Add(a);
                }
                return admimList;
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
        private SqlCommand CreateSelectAdminWithStoredProcedure(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method Updates a admin at admin table 
        //--------------------------------------------------------------------------------------------------

        public Admin UpdateAdmin(Admin admin)
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

            cmd = CreateAdminUpdateCommandWithStoredProcedure("SP_UpdateAdmin", con, admin);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Admin a = null; // Initialize the admin object

                while (dataReader.Read())
                {
                    a = new Admin
                    {
                        AdminCode = Convert.ToInt32(dataReader["adminCode"]),
                        AdminName = dataReader["adminName"].ToString(),
                        AdminPassword = dataReader["adminPassword"].ToString(),
                        IsActive = Convert.ToBoolean(dataReader["isActive"])

                    };
                }

                if (a != null)
                {
                    // Login successful
                    return a;
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
        private SqlCommand CreateAdminUpdateCommandWithStoredProcedure(String spName, SqlConnection con, Admin admin)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@adminCode", admin.AdminCode);

            cmd.Parameters.AddWithValue("@adminName", admin.AdminName);

            cmd.Parameters.AddWithValue("@adminPassword", admin.AdminPassword);

            cmd.Parameters.AddWithValue("@isActive", admin.IsActive);


            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method checks a admin login at admin table 
        //--------------------------------------------------------------------------------------------------

        public Admin CheckLogin(Admin admin)
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

            cmd = CreateadminLoginCommandWithStoredProcedure("SP_CheckAdminLogin", con, admin); // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Admin a = null; // Initialize the Admin object

                while (dataReader.Read())
                {
                    a = new Admin
                    {
                        AdminCode = Convert.ToInt32(dataReader["adminCode"]),
                        AdminName = dataReader["adminName"].ToString(),
                        AdminPassword = dataReader["adminPassword"].ToString(),
                        IsActive = Convert.ToBoolean(dataReader["isActive"])
                        
                     };
                }

                if (a != null)
                {
                    // Login successful
                    return a;
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

        private SqlCommand CreateadminLoginCommandWithStoredProcedure(String spName, SqlConnection con, Admin admin)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@inputCode", admin.AdminCode);
            cmd.Parameters.AddWithValue("@inputPassword", admin.AdminPassword);
            return cmd;
        }





    }
}

