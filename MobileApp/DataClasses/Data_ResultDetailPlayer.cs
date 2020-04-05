using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp
{
    public class Data_ResultDetailPlayer
    {
        public int Position { get; set; }

        public string PlayerName { get; set; }

        public string ResultStr
        {
            get { return Position.ToString() + ". " + PlayerName; }
        }
    }
}

