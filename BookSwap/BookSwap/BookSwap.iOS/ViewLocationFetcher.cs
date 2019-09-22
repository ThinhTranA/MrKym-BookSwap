using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using BookSwap.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:Dependency(typeof(ViewLocationFetcher))]
namespace BookSwap.iOS
{
    public class ViewLocationFetcher : IViewLocationFetcher
    {
        public PointF GetCoordinates(VisualElement view)
        {
            var renderer = Platform.GetRenderer(view);
            var nativeView = renderer.NativeView;
            var rect = nativeView.Superview.ConvertPointToView(nativeView.Frame.Location, null);
            return new System.Drawing.PointF((int)Math.Round(rect.X), (int)Math.Round(rect.Y));
        }
    }
}