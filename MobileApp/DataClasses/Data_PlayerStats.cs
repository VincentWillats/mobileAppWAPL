using System;
using System.Collections.Generic;
using System.Text;

/* Data class for PlayerStats, holds a Data_Player
 *   
 * Todo:
 * add more fields as needed
 * 
 * 
 * Creator:         Vincent Willats
 * Date Created:    02/04/20200
 * Contact Email:   Vincentwillats.software@gmail.com
 * 
 * Changelog:
 * 03/04/2020 -- Addedd In top % stats
 * 02/04/2020 -- File made
 */

namespace MobileApp
{
    public class Data_PlayerStats
    {
        public Data_Player Player = new Data_Player();

        public int Total_Games { get; set; }
        public int Total_Games_RankNo { get; set; }
        public decimal Total_Games_In_TopPCT { get; set; }
        public string Total_Games_In_TopPCT_Str
        {
            get { return Total_Games_In_TopPCT.ToString("P"); }
        }


        public int Wins_Amount { get; set; }
        public int Wins_Amount_RankNo { get; set; }
        public decimal Wins_Amount_In_TopPCT { get; set; }
        public string Wins_Amount_In_TopPCT_Str
        {
            get { return Wins_Amount_In_TopPCT.ToString("P"); }
        }


        public decimal Overall_Win_PCT { get; set; }
        public int Overall_Win_PCT_RankNo { get; set; }
        public decimal Overall_Win_PCT_In_TopPCT { get; set; }

        public string Overall_Win_PCT_Str
        {
            get { return Overall_Win_PCT.ToString("P"); }
        }
        public string Overall_Win_PCT_In_TopPCT_Str
        {
            get { return Overall_Win_PCT_In_TopPCT.ToString("P"); }
        }

                
        public int HeadsUp_Amount { get; set; }
        public int HeadsUp_Amount_RankNo { get; set; }
        public decimal HeadsUp_Amount_In_TopPCT { get; set; }
        public string HeadsUp_Amount_In_TopPCT_Str
        {
            get { return HeadsUp_Amount_In_TopPCT.ToString("P"); }
        }


        public decimal HeadsUp_Win_PCT { get; set; }
        public int HeadsUp_Win_PCT_RankNo { get; set; }
        public decimal HeadsUp_Win_PCT_In_TopPCT { get; set; }
        public string HeadsUp_Win_PCT_Str
        {
            get { return HeadsUp_Win_PCT.ToString("P"); }
        }
        public string HeadsUp_Win_PCT_In_TopPCT_Str
        {
            get { return HeadsUp_Win_PCT_In_TopPCT.ToString("P"); }
        }





        public int FinalTable_Amount { get; set; }
        public int FinalTable_Amount_RankNo { get; set; }
        public decimal FinalTable_Amount_In_TopPCT { get; set; }
        public string FinalTable_Amount_In_TopPCT_Str
        {
            get { return FinalTable_Amount_In_TopPCT.ToString("P"); }
        }



        public decimal FinalTable_Win_PCT { get; set; }
        public int FinalTable_Win_PCT_RankNo { get; set; }
        public decimal FinalTable_Win_PCT_In_TopPCT { get; set; }
        public string FinalTable_Win_PCT_Str
        {
            get { return FinalTable_Win_PCT.ToString("P"); }
        }
        public string FinalTable_Win_PCT_In_TopPCT_Str
        {
            get { return FinalTable_Win_PCT_In_TopPCT.ToString("P"); }
        }


        public decimal Avg_Pct_Field_Beaten { get; set; }

        public string Avg_Pct_Field_Beaten_Str
        {
            get { return Avg_Pct_Field_Beaten.ToString("P"); }
        }


        public string FullName
        {
            get { return Player.FullName; }
        }

    }
}
