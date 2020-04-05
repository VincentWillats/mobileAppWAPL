using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp
{
    public class Data_Stat
    {

        public Data_Stat(string statName, string statValue)
        {
            StatName = statName;
            StatValue = statValue;
        }
        public string StatName { get; private set; }

        public string StatValue { get; private set; }
    }
}
