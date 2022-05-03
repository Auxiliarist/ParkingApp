package md5ffb091fd0b74ba637aede1e0030372bf;


public class ThreadEventRaiser
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("GeoFire.Xamarin.Android.ThreadEventRaiser, GeoFire.Xamarin.Android", ThreadEventRaiser.class, __md_methods);
	}


	public ThreadEventRaiser ()
	{
		super ();
		if (getClass () == ThreadEventRaiser.class)
			mono.android.TypeManager.Activate ("GeoFire.Xamarin.Android.ThreadEventRaiser, GeoFire.Xamarin.Android", "", this, new java.lang.Object[] {  });
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
