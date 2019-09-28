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
            FromTitleLabel.Opacity = 0;
            FromTitleLabel.Opacity = 0;
            DescriptionText.Opacity = 0;

            var parentAnim = new Animation();

            // animate the background angle
            parentAnim.Add(0, 1, new Animation(t => 
            {
                _colorAngleAnim = t;
                PageBackground.InvalidateSurface(); //update canvas eachtime _colorAngleAmin change
            },0, 100, Easing.SinInOut));  //Go between 0 and 100 height (the middle row), _colorAngleAmin stop changing till 100 height is reached

            // title animations
            parentAnim.Add(0.1, .25, new Animation(t => FromTitleLabel.TranslationY = t, start: 50, end: 0, easing: Easing.SinInOut));
            parentAnim.Add(0.1, .25, new Animation(t => FromTitleLabel.Opacity = t, start: 0, end: 1, easing: Easing.SinInOut));
            parentAnim.Add(0.1, .3, new Animation(t => FromAuthorLabel.TranslationY = t, start: 50, end: 0, easing: Easing.SinInOut));
            parentAnim.Add(0.1, .3, new Animation(t => FromAuthorLabel.Opacity = t, start: 0, end: 1, easing: Easing.SinInOut));

            // book background
            parentAnim.Add(0, .2, new Animation(t => BookBorderBoxView.Scale = t, 0, 1));

            // description background
            //DesciptionBackGround
            DescriptionBackground.AnchorX = 0;
            parentAnim.Add(beginAt: .1,finishAt: .4, new Animation(t => DescriptionBackground.ScaleX = t, 0, 1, Easing.SinInOut));

            // description text
            parentAnim.Add(.30, .4, new Animation(t => DescriptionText.ScaleX = t, 0, 1, Easing.SinInOut));

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