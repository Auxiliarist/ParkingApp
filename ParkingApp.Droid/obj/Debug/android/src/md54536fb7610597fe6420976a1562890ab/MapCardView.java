package md54536fb7610597fe6420976a1562890ab;


public class MapCardView
	extends android.support.v7.widget.CardView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInterceptTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnInterceptTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("ParkingApp.Droid.Controls.MapCardView, ParkingApp.Droid", MapCardView.class, __md_methods);
	}


	public MapCardView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == MapCardView.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Controls.MapCardView, ParkingApp.Droid", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public MapCardView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == MapCardView.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Controls.MapCardView, ParkingApp.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public MapCardView (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == MapCardView.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Controls.MapCardView, ParkingApp.Droid", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public boolean onInterceptTouchEvent (android.view.MotionEvent p0)
	{
		return n_onInterceptTouchEvent (p0);
	}

	private native boolean n_onInterceptTouchEvent (android.view.MotionEvent p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
