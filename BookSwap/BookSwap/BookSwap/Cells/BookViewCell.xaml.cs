using BookSwap.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookSwap.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookViewCell : ViewCell
    {
        SKColor _accentColor;
        SKColor _accentDarkColor;
        SKColor _accentExtraDarkColor;

        SKPaint _accentPaint;
        SKPaint _accentDarkPaint;
        SKPaint _accentExtraDarkPaint;
        public BookViewCell()
        {
            InitializeComponent();
        }
        //Each cell will/might have different accent color
        //So need to get it from from BindingContext
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            
            if(this.BindingContext != null) //check if we have a BindingContext
            {
                Color color = Color.FromHex( ((Book)BindingContext).AccentColor);  //BindingContext should be a Book ojbect, since this is inside a Listview

                _accentColor = color.ToSKColor();
                _accentDarkColor = color.WithLuminosity(color.Luminosity - .2).ToSKColor();    //Darken the color (Luminosity goes from 0 to 1, default is 1)
                _accentExtraDarkColor = color.WithLuminosity(color.Luminosity - .25).ToSKColor(); //Even darker 

                _accentPaint = new SKPaint() { Color = _accentColor };
                _accentDarkPaint = new SKPaint() { Color = _accentDarkColor };
                _accentExtraDarkPaint = new SKPaint() { Color = _accentExtraDarkColor };
            }
        }

        private void CellBackGroundCanvas_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs args)
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