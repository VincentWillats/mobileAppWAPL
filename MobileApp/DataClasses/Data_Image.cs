using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp
{
    public class Data_Image
    {
		string server = "http://www.wapokerleague.com.au/";

		public Uri FullImagePath
		{
			get { return new Uri(server + ImagePath); }
		}

		public Uri ThumbnailImagePath
		{
			get { return new Uri(server + MakeThumbNailPath(ImagePath)); }

		}

		public string ImagePath { get; set;}

		private string MakeThumbNailPath(string imgpath)
		{
			int index = imgpath.IndexOf('/', 12);
			string thumbnailPath = imgpath.Insert(index + 1, "thumbnails/");
			return thumbnailPath;
		}

	}
}
