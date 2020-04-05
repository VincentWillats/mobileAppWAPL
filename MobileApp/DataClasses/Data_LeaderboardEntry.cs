using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp
{
    public class Data_LeaderboardEntry
    {
        public Data_Player player = new Data_Player();

        public string FullName { get { return player.FullName; } }
        public int SeasonPoints { get; set; }
        public int SeasonID { get; set; }

        public int Position { get; set; }
        public string PositionStr { get { return Position.ToString() + ". "; } }
    }
}
