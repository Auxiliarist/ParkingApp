package md5392bf011e77833c9c5c2a41f05eb5ac8;


public abstract class MvxPreferenceFragmentCompat_1
	extends mvvmcross.droid.support.v7.preference.MvxPreferenceFragmentCompat
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MvvmCross.Droid.Support.V7.Preference.MvxPreferenceFragmentCompat`1, MvvmCross.Droid.Support.V7.Preference", MvxPreferenceFragmentCompat_1.class, __md_methods);
	}


	public MvxPreferenceFragmentCompat_1 ()
	{
		super ();
		if (getClass () == MvxPreferenceFragmentCompat_1.class)
			mono.android.TypeManager.Activate ("MvvmCross.Droid.Support.V7.Preference.MvxPreferenceFragmentCompat`1, MvvmCross.Droid.Support.V7.Preference", "", this, new java.lang.Object[] {  });
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
