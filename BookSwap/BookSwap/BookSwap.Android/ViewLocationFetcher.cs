using System.Drawing;
using BookSwap;
using BookSwap.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//put this in dependency service
[assembly:Dependency(typeof(ViewLocationFetcher))]
namespace BookSwap.Droid
{
    public class ViewLocationFetcher : IViewLocationFetcher
    {
        public PointF GetCoordinates(VisualElement view)
        {
            var renderer = Platform.GetRenderer(view);
            var nativeView = renderer.View;
            var location = new int[2];
            //var density = nativeView.Context.Resources.DisplayMetrics.Density;
            var density =(float) Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;

            nativeView.GetLocationOnScreen(location);
            return new System.Drawing.PointF(location[0] / density, location[1] / density);

            //To calculate screen density, you can use this equation:
            //Screen density = Screen width(or height) in pixels / Screen width(or height) in inches
            //Source: https://material.io/design/layout/pixel-density.html#pixel-density
        }
    }
}