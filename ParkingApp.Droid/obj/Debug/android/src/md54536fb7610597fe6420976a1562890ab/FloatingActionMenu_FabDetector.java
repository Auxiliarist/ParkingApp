package md54536fb7610597fe6420976a1562890ab;


public class FloatingActionMenu_FabDetector
	extends android.view.GestureDetector
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ParkingApp.Droid.Controls.FloatingActionMenu+FabDetector, ParkingApp.Droid", FloatingActionMenu_FabDetector.class, __md_methods);
	}


	public FloatingActionMenu_FabDetector (android.content.Context p0, android.view.GestureDetector.OnGestureListener p1)
	{
		super (p0, p1);
		if (getClass () == FloatingActionMenu_FabDetector.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Controls.FloatingActionMenu+FabDetector, ParkingApp.Droid", "Android.Content.Context, Mono.Android:Android.Views.GestureDetector+IOnGestureListener, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public FloatingActionMenu_FabDetector (android.content.Context p0, android.view.GestureDetector.OnGestureListener p1, android.os.Handler p2)
	{
		super (p0, p1, p2);
		if (getClass () == FloatingActionMenu_FabDetector.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Controls.FloatingActionMenu+FabDetector, ParkingApp.Droid", "Android.Content.Context, Mono.Android:Android.Views.GestureDetector+IOnGestureListener, Mono.Android:Android.OS.Handler, Mono.Android", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public FloatingActionMenu_FabDetector (android.content.Context p0, android.view.GestureDetector.OnGestureListener p1, android.os.Handler p2, boolean p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == FloatingActionMenu_FabDetector.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Controls.FloatingActionMenu+FabDetector, ParkingApp.Droid", "Android.Content.Context, Mono.Android:Android.Views.GestureDetector+IOnGestureListener, Mono.Android:Android.OS.Handler, Mono.Android:System.Boolean, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public FloatingActionMenu_FabDetector (android.view.GestureDetector.OnGestureListener p0)
	{
		super (p0);
		if (getClass () == FloatingActionMenu_FabDetector.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Controls.FloatingActionMenu+FabDetector, ParkingApp.Droid", "Android.Views.GestureDetector+IOnGestureListener, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public FloatingActionMenu_FabDetector (android.view.GestureDetector.OnGestureListener p0, android.os.Handler p1)
	{
		super (p0, p1);
		if (getClass () == FloatingActionMenu_FabDetector.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Controls.FloatingActionMenu+FabDetector, ParkingApp.Droid", "Android.Views.GestureDetector+IOnGestureListener, Mono.Android:Android.OS.Handler, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}

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
