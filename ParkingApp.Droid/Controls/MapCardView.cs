using Android.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;

namespace ParkingApp.Droid.Controls
{
    /// <summary> 
    ///    Custom CardView class that intercepts touch events that would otherwise be passed to its
    ///    children. Useful when one of the children is a MapView, which independently receives touch
    ///    event to markers and info windows.
    /// </summary>
    public class MapCardView : CardView
    {
        public MapCardView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        // Delegate click events to children
        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            //return base.OnInterceptTouchEvent(ev);
            return false;
        }
    }
}