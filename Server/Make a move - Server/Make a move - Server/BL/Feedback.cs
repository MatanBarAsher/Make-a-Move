using Make_a_move___Server.DAL;
using System;
namespace Make_a_move___Server.BL
{
    public class Feedback
    {
        private string email;
        private int matchId;
        private int Q1;
        private int Q2;
        private int Q3; 
        private int Q4;
        private string name;
        private static List<Feedback> feedbacksList = new List<Feedback>();

        
        public int MatchId { get => matchId; set => matchId = value; }
        public int Q11 { get => Q1; set => Q1 = value; }
        public int Q21 { get => Q2; set => Q2 = value; }
        public int Q31 { get => Q3; set => Q3 = value; }
        public int Q41 { get => Q4; set => Q4 = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }

        public Feedback() { }

        public Feedback(string email, int matchId, int q1, int q2, int q3, int q4, string name)
        {
            this.email = email;
            this.matchId = matchId;
            this.Q1 = q1;
            this.Q2 = q2;
            this.Q3 = q3;
            this.Q4 = q4;
            this.name = name;
        }


        public int InsertFeedback()
        {
            try
            {
                DBservicesFeedback dbs = new DBservicesFeedback();
                feedbacksList.Add(this);
                return dbs.InsertFeedback(this);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error inserting feedback", ex);
            }
        }

        public List<Feedback> ReadFeedback()
        {
            try
            {
                DBservicesFeedback dbs = new DBservicesFeedback();
                return dbs.ReadFeedback();
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading feedback", ex);
            }
        }

        public List<Feedback> ReadFeedbackByEmail(string email)
        {
            try
            {
                DBservicesFeedback dbs = new DBservicesFeedback();
                return dbs.ReadFeedbackByEmail(email);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading feedback", ex);
            }
        }




    }
}
