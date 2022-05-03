package md545e957b5a3d0a5b36eb3871fd1a1c207;


public class SplashScreen
	extends md5716162e2cd7f7ce01364d7c5d961f40b.MvxSplashScreenAppCompatActivity_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_supportRequestWindowFeature:(I)Z:GetSupportRequestWindowFeature_IHandler\n" +
			"";
		mono.android.Runtime.register ("ParkingApp.Droid.SplashScreen, ParkingApp.Droid", SplashScreen.class, __md_methods);
	}


	public SplashScreen ()
	{
		super ();
		if (getClass () == SplashScreen.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.SplashScreen, ParkingApp.Droid", "", this, new java.lang.Object[] {  });
	}


	public boolean supportRequestWindowFeature (int p0)
	{
		return n_supportRequestWindowFeature (p0);
	}

	private native boolean n_supportRequestWindowFeature (int p0);

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
