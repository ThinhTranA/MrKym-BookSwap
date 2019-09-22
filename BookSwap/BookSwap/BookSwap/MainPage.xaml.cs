using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFUtils.Effects;

namespace BookSwap
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        SKColor _accentColor;
        SKColor _accentDarkColor;
        SKColor _accentExtraDarkColor;

        SKPaint _accentPaint;
        SKPaint _accentDarkPaint;
        SKPaint _accentExtraDarkPaint;

        public MainPage()
        {
            InitializeComponent();
            Color color = Color.FromHex("#FFF571");

            _accentColor = color.ToSKColor();
            _accentDarkColor = color.WithLuminosity(color.Luminosity - .2).ToSKColor();    //Darken the color (Luminosity goes from 0 to 1, default is 1)
            _accentExtraDarkColor = color.WithLuminosity(color.Luminosity - .25).ToSKColor(); //Even darker 

            _accentPaint = new SKPaint() { Color = _accentColor };
            _accentDarkPaint = new SKPaint() { Color = _accentDarkColor };
            _accentExtraDarkPaint = new SKPaint() { Color = _accentExtraDarkColor };

            var eff = new XFUtils.Effects.ScrollReporterEffect();
            eff.ScrollChanged += Eff_ScrollChanged;    //attach this effect to listview to get the Y position of the scroll
            BooksListView.Effects.Add(eff);
        }

        private void Eff_ScrollChanged(object sender, ScrollEventArgs args)
        {
            // send a message to the cell that the scroll position has changes
            MessagingCenter.Send<ScrollMessage, double>(new ScrollMessage(), ScrollMessage.ScrollChanged, args.Y);
        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            //Fill in the whole grid space
            canvas.DrawRect(info.Rect, _accentPaint);

            // create path for dark color  AccentDarkPaint, dispose path when done.
            using (SKPath path = new SKPath())
            {
                //Assume drawing in a 10 by 10 coordinate system.
                path.MoveTo(0, 0);                          //Start at point (0,0)
                path.LineTo(info.Width * .7f, 0);           //Line from (0,0) to (0,7) ,Draw to the right to 70 % width of the rectangle(cell)
                path.LineTo(info.Width * .2f, info.Height); //Draw from point (0,7) to (2,10)
                path.LineTo(0, info.Height);                //Draw from point (2,10) to (0,10)
                path.Close();                               //Close the path, it knows to draw from 0,10 to 0,0
                canvas.DrawPath(path, _accentDarkPaint);    //Draw and fill path with accent dark paint color.
            }
            //Similarly, create path for Extra Dark color  AccentExtraDarkPaint 
            using (SKPath path = new SKPath())
            {
                path.MoveTo(0, 0);
                path.LineTo(info.Width * .33f, 0);
                path.LineTo(0, info.Height * .6f);
                path.Close();
                canvas.DrawPath(path, _accentExtraDarkPaint);
            }
        }
    }
}
