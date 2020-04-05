using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp
{
    public class Data_LeaderboardEntry
    {
        public Data_Player player = new Data_Player();
        public int SeasonPoints { get; set; }
        public int SeasonID { get; set; }
    }
}
