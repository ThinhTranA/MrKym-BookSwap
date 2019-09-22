using System;
using System.Collections.Generic;
using System.Text;

namespace BookSwap
{
    // Taken from : https://stackoverflow.com/questions/49263876/how-to-get-coordinates-of-the-selected-item-in-a-list-view-in-xamarin-forms
    public interface IViewLocationFetcher
    {
        System.Drawing.PointF GetCoordinates(global::Xamarin.Forms.VisualElement view);
    }
}
