using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookSwap
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwapDetails : ContentPage
    {
        SKPaint _accentToPaint;
        SKPaint _accentFromPaint;
        private double _colorAngleAnim;

        public SwapDetails()
        {
            InitializeComponent();
            this.BindingContext = App.MainViewModel;

            _accentFromPaint = new SKPaint() { Color = App.MainViewModel.SwapFromBook.Colors.Accent.ToSKColor() };
            _accentToPaint = new SKPaint() { Color = App.MainViewModel.SelectedBook.Colors.Accent.ToSKColor() };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var parentAnim = new Animation();

            parentAnim.Add(0, 1, new Animation(t => 
            {
                _colorAngleAnim = t;
                PageBackground.InvalidateSurface(); //update canvas eachtime _colorAngleAmin change
            },0, 100, Easing.SinInOut));  //Go between 0 and 100 height (the middle row), _colorAngleAmin stop changing till 100 height is reached
            parentAnim.Commit(this, "PageAnimations", 16, 1000);

        }

        private void PageBackground_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            // fill the background
            canvas.DrawRect(info.Rect, _accentToPaint); // draw the lower color, whole page.

            // draw the top half
            using (SKPath path = new SKPath())
            {
                path.MoveTo(0, 0);
                path.LineTo(info.Width, 0);
                path.LineTo(info.Width,(info.Height / 2) - (float)_colorAngleAnim);   //move up on the right side
                path.LineTo(0, (info.Height / 2) + (float)_colorAngleAnim);  //move down on the left side
                path.Close();

                canvas.DrawPath(path, _accentFromPaint);
            }

        }
    }
}