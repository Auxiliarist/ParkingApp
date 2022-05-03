package parkingapp.droid.activities;


public class AuthActivity
	extends md5d3e072e6187f252457dc36a0a8d573ee.BaseActivity_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("ParkingApp.Droid.Activities.AuthActivity, ParkingApp.Droid", AuthActivity.class, __md_methods);
	}


	public AuthActivity ()
	{
		super ();
		if (getClass () == AuthActivity.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Activities.AuthActivity, ParkingApp.Droid", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
