using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp
{
    public class FontSizeController
    {
  
        public int GetMaxFontSize(Label label)
        {
            double lineHeight = Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android ? 1.2 : 1.3;
      
            double charWidth = 0.5;

            label.Text = String.Format(label.Text, lineHeight, charWidth, label.Width, label.Height);

            int charCount = label.Text.Length; 

            int fontSize = (int)Math.Sqrt (label.Width * label.Height / (charCount * lineHeight * charWidth));
       
            return fontSize;
        }
    }
}
