using Make_a_move___Server.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;
using System.Linq;
namespace Make_a_move___Server.BL
{
    public class UserPreferences
    {
        private string email;
        private int preferenceGender;
        private int minAge;
        private int maxAge;
        private int minHeight;
        private int maxHeight;
        private int maxDistance;
        private static List<UserPreferences> usersPreferencesList = new List<UserPreferences>();

        public UserPreferences() { }

        public UserPreferences(string email, int preferenceGender, int minAge, int maxAge, int minHeight, int maxHeight, int maxDistance)
        {
            this.email = email;
            this.preferenceGender = preferenceGender;
            this.minAge = minAge;
            this.maxAge = maxAge;
            this.minHeight = minHeight;
            this.maxHeight = maxHeight;
            this.maxDistance = maxDistance;
        }

        public string Email { get => email; set => email = value; }
        public int PreferenceGender { get => preferenceGender; set => preferenceGender = value; }
        public int MinAge { get => minAge; set => minAge = value; }
        public int MaxAge { get => maxAge; set => maxAge = value; }
        public int MinHeight { get => minHeight; set => minHeight = value; }
        public int MaxHeight { get => maxHeight; set => maxHeight = value; }
        public int MaxDistance { get => maxDistance; set => maxDistance = value; }


        public int InsertUserPreference()
        {
            try
            {
                DBservicesUserPreferences dbs = new DBservicesUserPreferences();
                usersPreferencesList.Add(this);
                return dbs.InsertUserPreference(this);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error inserting user preference", ex);
            }
        }

        public static List<UserPreferences> ReadUserPreference()
        {
            try
            {
                DBservicesUserPreferences dbs = new DBservicesUserPreferences();
                usersPreferencesList = dbs.ReadUserPreference();
                return usersPreferencesList;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading user preferences", ex);
            }
        }
        public UserPreferences UpdateUserPreferences(UserPreferences newUserPreferences)
        {
            try

            {
                DBservicesUserPreferences dbs1 = new DBservicesUserPreferences();
                List<UserPreferences> list = dbs1.ReadUserPreference();
                // Find the user in the UsersList by email
                UserPreferences userToUpdate = list.Find(u => string.Equals(u.Email.Trim(), newUserPreferences.Email.Trim(), StringComparison.OrdinalIgnoreCase));

                if (userToUpdate != null)
                {
                    // Update user information
                    userToUpdate.PreferenceGender = newUserPreferences.PreferenceGender;
                    userToUpdate.MinAge = newUserPreferences.MinAge;
                    userToUpdate.MaxAge = newUserPreferences.MaxAge;
                    userToUpdate.MinHeight = newUserPreferences.MinHeight;
                    userToUpdate.MaxHeight = newUserPreferences.MaxHeight;
                    userToUpdate.MaxDistance = newUserPreferences.MaxDistance;


                    // Update in the database (assuming DBservices has an UpdateUser method)
                    DBservicesUserPreferences dbs = new DBservicesUserPreferences();
                    return dbs.UpdateUserPreference(userToUpdate);
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

        public static UserPreferences ReadUserPreferencesByEmail(string email)
        {
            try
            {
                DBservicesUserPreferences dbs = new DBservicesUserPreferences();
                UserPreferences up = new UserPreferences();
                up = dbs.ReadUserPreferencesByEmail(email);
                return up;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading user preferences", ex);
            }
        }
    }
}






    

