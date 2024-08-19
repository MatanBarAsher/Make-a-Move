using Make_a_move___Server.DAL;
using Microsoft.Extensions.Primitives;
using System;
namespace Make_a_move___Server.BL
{
    public class Match
    {
        private string firstemail;
        private string secondemail;
        private DateTime timeStamp;
        private int placeCode;
        private int matchNum;
        private static List<Match> matchesList = new List<Match>();

        public string Firstemail { get => firstemail; set => firstemail = value; }
        public string Secondemail { get => secondemail; set => secondemail = value; }
        public DateTime TimeStamp { get => timeStamp; set => timeStamp = value; }
        public int PlaceCode { get => placeCode; set => placeCode = value; }
        public int MatchNum { get => matchNum; set => matchNum = value; }

        public Match() { }

        public Match(string firstemail, string secondemail, int placeCode, int matchNum)
        {
            this.Firstemail = firstemail;
            this.Secondemail = secondemail;
            this.PlaceCode = placeCode;
            this.MatchNum = matchNum;
        }

        //public int InsertMatch()
        //{
        //    try
        //    {
        //        DBservicesMatch dbs = new DBservicesMatch();
        //        matchesList.Add(this);
        //        return dbs.InsertMatch(this);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log or handle the exception appropriately
        //        throw new Exception("Error inserting match", ex);
        //    }
        //}

        public List<Match> ReadMatches()
        {
            try
            {
                DBservicesMatch dbs = new DBservicesMatch();
                return dbs.ReadMatches();
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading matches", ex);
            }
        }



        public List<Match> ReadMatchesByEmail(string inputemail)
        {
            try
            {
                DBservicesMatch dbs = new DBservicesMatch();
                return dbs.ReadMatchesByEmail(inputemail);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading matches", ex);
            }
        }


    }
}
