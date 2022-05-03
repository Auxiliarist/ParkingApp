package md5d3e072e6187f252457dc36a0a8d573ee;


public abstract class BaseActivity_1
	extends md5d3e072e6187f252457dc36a0a8d573ee.BaseActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ParkingApp.Droid.Activities.BaseActivity`1, ParkingApp.Droid", BaseActivity_1.class, __md_methods);
	}


	public BaseActivity_1 ()
	{
		super ();
		if (getClass () == BaseActivity_1.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Activities.BaseActivity`1, ParkingApp.Droid", "", this, new java.lang.Object[] {  });
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
