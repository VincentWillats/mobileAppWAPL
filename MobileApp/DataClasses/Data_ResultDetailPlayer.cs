using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp
{
    public class Data_ResultDetailPlayer
    {
        public Data_Player player = new Data_Player();

        public int Position { get; set; }

        public string PlayerName { get { return player.FullName; } }

        public string ResultStr
        {
            get { return Position.ToString() + ". " + PlayerName; }
        }
    }
}

