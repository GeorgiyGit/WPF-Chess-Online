using Library.HelpElems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFTestChess
{
    internal class IconSource
    {
        private ImageSource whiteSource;
        private ImageSource blackSource;
        public PieceType PieceType { get; set; }
        public IconSource() : this(null, null, PieceType.None) { }
        public IconSource(string whiteSource, string blackSource) : this(whiteSource, blackSource, PieceType.None) { }
        public IconSource(string whiteSource, string blackSource,PieceType type)
        {
            PieceType = type;

            SetSource(whiteSource, blackSource);
        }
        public ImageSource GetSource(PieceColors color)
        {
            if (color == PieceColors.White) return whiteSource;
            return blackSource;
        }
        public void SetSource(string whiteSource,string blackSource)
        {

            BitmapImage bitTexture = new BitmapImage();

            bitTexture.BeginInit();
            bitTexture.UriSource = new Uri(whiteSource, UriKind.Relative);
            bitTexture.EndInit();

            this.whiteSource = bitTexture;

            BitmapImage bitTexture2 = new BitmapImage();

            bitTexture2.BeginInit();
            bitTexture2.UriSource = new Uri(blackSource, UriKind.Relative);
            bitTexture2.EndInit();

            this.blackSource = bitTexture2;
        }
    }
}
