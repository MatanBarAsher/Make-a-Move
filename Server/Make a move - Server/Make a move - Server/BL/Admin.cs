using Make_a_move___Server.DAL;
using System;
namespace Make_a_move___Server.BL
{
    public class Admin
    {
        private int adminCode;
        private string adminName;
        private string adminPassword;
        private bool isActive;
        private static List<Admin> adminsList = new List<Admin>();

        public Admin() { }
        public Admin(int adminCode, string adminName, string adminPassword, bool isActive)
        {
            this.adminCode = adminCode;
            this.adminName = adminName;
            this.adminPassword = adminPassword;
            this.isActive = isActive;

        }

        public int AdminCode { get => adminCode; set => adminCode = value; }
        public string AdminName { get => adminName; set => adminName = value; }
        public string AdminPassword { get => adminPassword; set => adminPassword = value; }
        public bool IsActive { get => isActive; set => isActive = value; }


        public int InsertAdmin()
        {
            try
            {
                DBservicesAdmin dbs = new DBservicesAdmin();
                adminsList.Add(this);
                return dbs.InsertAdmin(this);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error inserting admin", ex);
            }
        }

        public List<Admin> ReadAdmins()
        {
            try
            {
                DBservicesAdmin dbs = new DBservicesAdmin();
                return dbs.ReadAdmin();
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading admins", ex);
            }
        }
        public Admin CheckLogin()
        {
            try
            {
                DBservicesAdmin dbs = new DBservicesAdmin();
                return dbs.CheckLogin(this);
            }
            catch (Exception ex)
            {
                //Log or handle the exception appropriately
                throw new Exception("Error checking login", ex);
            }
        }

        public Admin UpdateAdmin(Admin newAdmin)
        {
            try
            {
                // Find the user in the UsersList by AdminCode
                DBservicesAdmin dbs1 = new DBservicesAdmin();
                List<Admin> list = dbs1.ReadAdmin();
                Admin adminToUpdate = list.Find(a => a.AdminCode.Equals(newAdmin.AdminCode));

                if (adminToUpdate != null)
                {
                    // Update user information
                    adminToUpdate.AdminCode = newAdmin.AdminCode;
                    adminToUpdate.AdminName = newAdmin.AdminName;
                    adminToUpdate.AdminPassword = newAdmin.AdminPassword;
                    adminToUpdate.IsActive = newAdmin.IsActive;

                    // Update in the database (assuming DBservices has an UpdateUser method)
                    DBservicesAdmin dbs = new DBservicesAdmin();
                    return dbs.UpdateAdmin(adminToUpdate);
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
    }
}
