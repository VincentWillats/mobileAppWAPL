using System;
using System.Collections.Generic;
using System.Text;

/* Data class for Tournaments
 *   
 * Todo:
 * Add more tournament data fields 
 * 
 * 
 * Creator:         Vincent Willats
 * Date Created:    28/03/2020
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 29/03/2020 -- Changed from UpcomingTournaments to Tournaments,
 *                  Added WinnerPlayerID, WinnerPrize, TournamentID
 * 28/03/2020 -- File made
 */

namespace MobileApp
{
    public class Data_Tournament
    {
        public DateTime StartDate { get; set; }
        public DateTime RegoDate { get; set; }

        public string ResultListDescription
        {
            get
            {
                return RegoDate.ToShortTimeString() + " Registration, " + StartDate.ToShortTimeString() +
                      " Start!" + "\n" + Buyin.ToString("c0") + " Freeze-out, " + ToCashPrize().ToString("c0") + " towards prizes.\n" +
                      "Cash tables avaliable from " + RegoDate.ToShortTimeString();
            }
        }
        private decimal ToCashPrize()
        {
            decimal howMany50 = Buyin / 50;
            decimal reggo = 8 * howMany50;
            decimal toCashPrize = Buyin - reggo;
            return toCashPrize;
        }

        public string VenueName { get; set; }
        public string Address01 { get; set; }
        public string Address02 { get; set; }
        public string Address03 { get; set; }
        public string Address04 { get; set; }
        public decimal Buyin { get; set; }
        public int WinnerPlayerID { get; set; }
        public decimal WinnerPrize { get; set; }
        public int TournamentID { get; set; }

        public override string ToString()
        {
            return VenueName;
        }
    }
}
