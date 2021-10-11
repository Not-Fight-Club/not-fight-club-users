using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{


    public class RewardRepository
    {

        public int GetReward(ViewUser user)
        {
            if(DateTime.Now.Month > user.LastLogin.Month || DateTime.Now.Day > user.LastLogin.Day )
            {
                return CalculateReward(user.LoginStreak);
            }
                
            return -1;
        }

        private int CalculateReward(int? streak)
        {
            if (streak == null)
                streak = 1;
            
            decimal streakD = Convert.ToDecimal(streak);
            decimal tempResult;

            Random rand = new Random();
            //Each caase converts the result of the random function to a decimal so it can be rounded to the nearest 5. Then converts back to an int to return in order to be used by the front end.
            switch(streakD)
            {
                case 1:
                    tempResult = Math.Round(Convert.ToDecimal(rand.Next(10,30)) / 5) * 5;
                    return Convert.ToInt32(tempResult);
                case 2:
                    tempResult = Math.Round(Convert.ToDecimal(rand.Next(40,70))/5)*5;
                    return Convert.ToInt32(tempResult); 
                case 3:
                    tempResult = Math.Round(Convert.ToDecimal(rand.Next(80,110))/5)*5;
                    return Convert.ToInt32(tempResult); 
                case 4:
                    tempResult = Math.Round(Convert.ToDecimal(rand.Next(120,150))/5)*5;
                    return Convert.ToInt32(tempResult); 
                case 5:
                    tempResult = Math.Round(Convert.ToDecimal(rand.Next(160, 200))/5)*5;
                    return Convert.ToInt32(tempResult); 
                case 6:
                    tempResult = Math.Round(Convert.ToDecimal(rand.Next(220, 300))/5)*5;
                    return Convert.ToInt32(tempResult); 
                case 7:
                    tempResult = Math.Round(Convert.ToDecimal(rand.Next(400, 600))/5)*5;
                    return Convert.ToInt32(tempResult);
                default:
                    return -1;
                
            }

        }
    }
}
