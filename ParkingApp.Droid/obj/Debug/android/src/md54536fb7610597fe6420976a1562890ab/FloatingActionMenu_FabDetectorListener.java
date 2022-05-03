package md54536fb7610597fe6420976a1562890ab;


public class FloatingActionMenu_FabDetectorListener
	extends android.view.GestureDetector.SimpleOnGestureListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDown:(Landroid/view/MotionEvent;)Z:GetOnDown_Landroid_view_MotionEvent_Handler\n" +
			"n_onSingleTapUp:(Landroid/view/MotionEvent;)Z:GetOnSingleTapUp_Landroid_view_MotionEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("ParkingApp.Droid.Controls.FloatingActionMenu+FabDetectorListener, ParkingApp.Droid", FloatingActionMenu_FabDetectorListener.class, __md_methods);
	}


	public FloatingActionMenu_FabDetectorListener ()
	{
		super ();
		if (getClass () == FloatingActionMenu_FabDetectorListener.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Controls.FloatingActionMenu+FabDetectorListener, ParkingApp.Droid", "", this, new java.lang.Object[] {  });
	}

	public FloatingActionMenu_FabDetectorListener (md54536fb7610597fe6420976a1562890ab.FloatingActionMenu p0)
	{
		super ();
		if (getClass () == FloatingActionMenu_FabDetectorListener.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Controls.FloatingActionMenu+FabDetectorListener, ParkingApp.Droid", "ParkingApp.Droid.Controls.FloatingActionMenu, ParkingApp.Droid", this, new java.lang.Object[] { p0 });
	}


	public boolean onDown (android.view.MotionEvent p0)
	{
		return n_onDown (p0);
	}

	private native boolean n_onDown (android.view.MotionEvent p0);


	public boolean onSingleTapUp (android.view.MotionEvent p0)
	{
		return n_onSingleTapUp (p0);
	}

	private native boolean n_onSingleTapUp (android.view.MotionEvent p0);

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
