using Make_a_move___Server.DAL;
using System;
namespace Make_a_move___Server.BL
{
    public class PersonalInterests
    {
        private int interestCode;
        private string interestDesc;
        private static List<PersonalInterests> personalInterestsList = new List<PersonalInterests>();

        public PersonalInterests() {  }
        public PersonalInterests(int interestCode, string interestDesc)
        {
            this.interestCode = interestCode;
            this.interestDesc = interestDesc;
        }

        public int InterestCode { get => interestCode; set => interestCode = value; }
        public string InterestDesc { get => interestDesc; set => interestDesc = value; }

        public int InsertPersonalInterests()
        {
            try
            {
                DBservicesPersonalInterests dbs = new DBservicesPersonalInterests();
                personalInterestsList.Add(this);
                return dbs.InsertPersonalInterests(this);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error inserting personalInterests", ex);
            }
        }

        public List<PersonalInterests> ReadPersonalInterests()
        {
            try
            {
                DBservicesPersonalInterests dbs = new DBservicesPersonalInterests();
                return dbs.ReadPersonalInterests();
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading personalInterests", ex);
            }
        }


        public PersonalInterests UpdatePersonalInterests(PersonalInterests newPersonalInterests)
        {
            try
            {
                // Find the personalInterests in the UsersList by InterestCode
                DBservicesPersonalInterests dbs1 = new DBservicesPersonalInterests();
                List<PersonalInterests> list = dbs1.ReadPersonalInterests();
                PersonalInterests personalInterestsToUpdate = list.Find(p => p.InterestCode.Equals(newPersonalInterests.InterestCode));


                if (personalInterestsToUpdate != null)
                {
                    // Update user information
                    personalInterestsToUpdate.InterestCode = newPersonalInterests.InterestCode;
                    personalInterestsToUpdate.InterestDesc = newPersonalInterests.InterestDesc;

                    // Update in the database (assuming DBservices has an UpdateInterst method)
                    DBservicesPersonalInterests dbs = new DBservicesPersonalInterests();
                    return dbs.UpdatePersonalInterests(personalInterestsToUpdate);
                }
                else
                {
                    // Interst not found, handle the case appropriately (return null, throw an exception, etc.)
                    return null; // Or throw new Exception("Interst not found");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error updating Interst", ex);
            }
        }



    }
}
