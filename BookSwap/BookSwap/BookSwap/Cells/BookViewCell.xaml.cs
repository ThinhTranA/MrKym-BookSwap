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

        double scrollValue;
        IViewLocationFetcher viewLocatorFetcher;
        public BookViewCell()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<ScrollMessage, double>(this, ScrollMessage.ScrollChanged,
                (sender, scrollInfo) =>
                {
                    // store away the scroll value (here is Y position)
                    scrollValue = scrollInfo;

                    //tell the cell to redraw
                    if(CellBackGroundCanvas != null)
                        CellBackGroundCanvas.InvalidateSurface();
                });

            viewLocatorFetcher = DependencyService.Get<IViewLocationFetcher>();
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
                _accentDarkColor = color.WithLuminosity(color.Luminosity - .07).ToSKColor();    //Darken the color (Luminosity goes from 0 to 1, default is 1)
                _accentExtraDarkColor = color.WithLuminosity(color.Luminosity - .15).ToSKColor(); //Even darker 

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

            //work out where the cell is on the page, using this cell postion, we can adjust the angle
            var thisCellPosition = viewLocatorFetcher.GetCoordinates(this.View);

            //Fill in the whole grid space
            canvas.DrawRect(info.Rect, _accentPaint);

            // create path for dark color  AccentDarkPaint, dispose path when done.
            using (SKPath path = new SKPath())
            {
                path.MoveTo(0, 0);                          
                path.LineTo(info.Width - thisCellPosition.Y, 0);     //As scroll move up and down, adjust the angle of this, canvas being redrawn each time scroll through messingcenter      
                path.LineTo(0, info.Height *.8f);                
                path.Close();                               
                canvas.DrawPath(path, _accentDarkPaint);    
            }
            //Similarly, create path for Extra Dark color  AccentExtraDarkPaint 
            using (SKPath path = new SKPath())
            {
                path.MoveTo(0, 0);
                path.LineTo(info.Width - thisCellPosition.Y * 2f, 0);
                path.LineTo(0, info.Height * .6f);
                path.Close();
                canvas.DrawPath(path, _accentExtraDarkPaint);
            }
        }
    }
}