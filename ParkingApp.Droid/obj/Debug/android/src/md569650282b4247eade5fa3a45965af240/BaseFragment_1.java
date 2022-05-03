package md569650282b4247eade5fa3a45965af240;


public abstract class BaseFragment_1
	extends md569650282b4247eade5fa3a45965af240.BaseFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ParkingApp.Droid.Views.BaseFragment`1, ParkingApp.Droid", BaseFragment_1.class, __md_methods);
	}


	public BaseFragment_1 ()
	{
		super ();
		if (getClass () == BaseFragment_1.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Views.BaseFragment`1, ParkingApp.Droid", "", this, new java.lang.Object[] {  });
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
