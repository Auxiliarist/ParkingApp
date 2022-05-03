package md5388640b5d5db4f7b28b73484b08181af;


public class SpotListViewHolder
	extends mvvmcross.droid.support.v7.recyclerview.MvxRecyclerViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ParkingApp.Droid.Adapters.SpotListViewHolder, ParkingApp.Droid", SpotListViewHolder.class, __md_methods);
	}


	public SpotListViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == SpotListViewHolder.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Adapters.SpotListViewHolder, ParkingApp.Droid", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
