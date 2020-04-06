using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp
{
    public class FontSizeController
    {
  
        public void SetFontSize(Button button)
        {

            System.Diagnostics.Debug.Write("button name = " + button.ClassId.ToString());
            System.Diagnostics.Debug.Write("Fontsize start = " + button.FontSize.ToString());
            
            // If android or ios, 1.2 else 1.3 (windows)?
            double lineHeight = Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android ? 1.2 : 1.3;
            //lineHeight = lineHeight * button.FontSize;
            System.Diagnostics.Debug.Write(Device.RuntimePlatform.ToString());
            System.Diagnostics.Debug.Write("Lineheight = " + lineHeight.ToString());

            double charWidth = 0.5;

            button.Text = String.Format(button.Text, lineHeight, charWidth, button.Width, button.Height);
            System.Diagnostics.Debug.Write("view.Width = " + button.Width.ToString());
            System.Diagnostics.Debug.Write("view.Height = " + button.Height.ToString());

            int charCount = button.Text.Length;
            System.Diagnostics.Debug.Write("charCount = " + button.Text.Length.ToString());


            int fontSize = (int)Math.Sqrt
                            ((button.Width * button.Height) /
                            (charCount * lineHeight * charWidth));

            System.Diagnostics.Debug.Write("Fontsize end = " + fontSize.ToString());

            int lineCount = (int)(button.Height / (lineHeight * fontSize));
            System.Diagnostics.Debug.Write("lineCount = " + lineCount.ToString());

            int charsPerLine = (int)(button.Width / (charWidth * fontSize));
            System.Diagnostics.Debug.Write("charsPerLine = " + charsPerLine.ToString());

            button.FontSize = fontSize;   


        }

        public void SetFontSize(Label button)
        {

            System.Diagnostics.Debug.Write("button name = " + button.ClassId.ToString());
            System.Diagnostics.Debug.Write("Fontsize start = " + button.FontSize.ToString());

            // If android or ios, 1.2 else 1.3 (windows)?
            double lineHeight = Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android ? 1.2 : 1.3;
            //lineHeight = lineHeight * button.FontSize;
            System.Diagnostics.Debug.Write(Device.RuntimePlatform.ToString());
            System.Diagnostics.Debug.Write("Lineheight = " + lineHeight.ToString());

            double charWidth = 0.5;

            button.Text = String.Format(button.Text, lineHeight, charWidth, button.Width, button.Height);
            System.Diagnostics.Debug.Write("view.Width = " + button.Width.ToString());
            System.Diagnostics.Debug.Write("view.Height = " + button.Height.ToString());

            int charCount = button.Text.Length * 2;
            System.Diagnostics.Debug.Write("charCount = " + button.Text.Length.ToString());


            int fontSize = (int)Math.Sqrt
                            ((button.Width * button.Height) /
                            (charCount * lineHeight * charWidth));

            System.Diagnostics.Debug.Write("Fontsize end = " + fontSize.ToString());

            int lineCount = (int)(button.Height / (lineHeight * fontSize));
            System.Diagnostics.Debug.Write("lineCount = " + lineCount.ToString());

            int charsPerLine = (int)(button.Width / (charWidth * fontSize));
            System.Diagnostics.Debug.Write("charsPerLine = " + charsPerLine.ToString());

            button.FontSize = fontSize;


        }

        public int GetFontSize(Label label)
        {
            
            // If android or ios, 1.2 else 1.3 (windows)?
            double lineHeight = Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android ? 1.2 : 1.3;
      
            double charWidth = 0.5;

            label.Text = String.Format(label.Text, lineHeight, charWidth, label.Width, label.Height);

            int charCount = label.Text.Length; 

            int fontSize = (int)(Math.Sqrt ((label.Width * label.Height) / (charCount * lineHeight * charWidth))) / 2;
       
            return fontSize;
        }
    }
}
