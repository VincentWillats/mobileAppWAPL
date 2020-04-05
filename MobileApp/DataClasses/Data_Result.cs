using System;
using System.Collections.Generic;
using System.Text;


/* Class Wrapper for Results
 *   
 * Todo:
 * 
 * 
 * 
 * Creator:         Vincent Willats
 * Date Created:    29/03/2020
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 29/03/2020 -- File made
 */

namespace MobileApp
{
    public class Data_Result
    {
        public Data_Player winner = new Data_Player();
        public Data_Tournament tourny = new Data_Tournament();

        public decimal FirstPrize { get; set; }

        public string Winner
        {
            get { return winner.FullName; }
        }

        public string Venue
        {
            get { return tourny.VenueName; }
        }
            
        public DateTime StartDate
        {
            get { return tourny.StartDate; }
        }

        private DateTime _regoDatee;
        public DateTime RegoDate
        {
            get { return tourny.RegoDate; }
        }



    }
}
