using Make_a_move___Server.DAL;

namespace Make_a_move___Server.BL
{
    public class SecondFeedback
    {
  
            private string email;
            private int matchId;
            private int Q1;
            private int Q2;
            private int Q3;
            private static List<SecondFeedback> SecondfeedbacksList = new List<SecondFeedback>();

        public int MatchId { get => matchId; set => matchId = value; }
            public int Q11 { get => Q1; set => Q1 = value; }
            public int Q21 { get => Q2; set => Q2 = value; }
            public int Q31 { get => Q3; set => Q3 = value; }
            public string Email { get => email; set => email = value; }

            public SecondFeedback() { }

            public SecondFeedback(string email, int matchId, int q1, int q2, int q3)
            {
                this.email = email;
                this.matchId = matchId;
                this.Q1 = q1;
                this.Q2 = q2;
                this.Q3 = q3;

            }


            public int InsertFeedback()
            {
                try
                {
                DBservicesSecondFeedback dbs = new DBservicesSecondFeedback();
                    SecondfeedbacksList.Add(this);
                    return dbs.InsertSecondFeedback(this);
                }
                catch (Exception ex)
                {
                    // Log or handle the exception appropriately
                    throw new Exception("Error inserting feedback", ex);
                }
            }

            public List<SecondFeedback> ReadFeedback()
            {
                try
                {
                DBservicesSecondFeedback dbs = new DBservicesSecondFeedback();
                    return dbs.ReadFeedback();
                }
                catch (Exception ex)
                {
                    // Log or handle the exception appropriately
                    throw new Exception("Error reading feedback", ex);
                }
            }


        public List<SecondFeedback> ReadFeedbackByEmail(string email)
        {
            try
            {
                DBservicesSecondFeedback dbs = new DBservicesSecondFeedback();
                return dbs.ReadSecondFeedbackByEmail(email);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                throw new Exception("Error reading feedback", ex);
            }
        }




    }
}
