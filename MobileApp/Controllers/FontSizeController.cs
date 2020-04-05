using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp
{
    public class FontSizeController
    {
        public void SetFontSize(ref Button button)
        {
            double lineHeight = Device.RuntimePlatform == Device.iOS ||
                                 Device.RuntimePlatform == Device.Android ? 1.2 : 1.3;

            double charWidth = 0.5;

            button.Text = String.Format(button.Text, lineHeight, charWidth, button.Width, button.Height);
            int charCount = button.Text.Length;

            int fontSize = (int)Math.Sqrt(button.Width * button.Height /
                            (charCount * lineHeight * charWidth));


            int lineCount = (int)(button.Height / (lineHeight * fontSize));
            int charsPerLine = (int)(button.Width / (charWidth * fontSize));
            button.FontSize = fontSize;
        }

        public void SetFontSize(ref Label label)
        {
            double lineHeight = Device.RuntimePlatform == Device.iOS ||
                                 Device.RuntimePlatform == Device.Android ? 1.2 : 1.3;
            double charWidth = 0.5;

            label.Text = String.Format(label.Text, lineHeight, charWidth, label.Width, label.Height);
            int charCount = label.Text.Length;

            int fontSize = (int)Math.Sqrt(label.Width * label.Height /
                            (charCount * lineHeight * charWidth));

            int lineCount = (int)(label.Height / (lineHeight * fontSize));
            int charsPerLine = (int)(label.Width / (charWidth * fontSize));

            label.FontSize = fontSize;
        }

    }
}
