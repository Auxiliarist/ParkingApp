package md533364895e4d16c5470c7a4244d5b1d00;


public class LocationService_LocationListener
	extends md57dae306e9c511046bb3e5da82eb8f47a.LocationCallback
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLocationResult:(Lcom/google/android/gms/location/LocationResult;)V:GetOnLocationResult_Lcom_google_android_gms_location_LocationResult_Handler\n" +
			"";
		mono.android.Runtime.register ("ParkingApp.Droid.Services.LocationService+LocationListener, ParkingApp.Droid", LocationService_LocationListener.class, __md_methods);
	}


	public LocationService_LocationListener ()
	{
		super ();
		if (getClass () == LocationService_LocationListener.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Services.LocationService+LocationListener, ParkingApp.Droid", "", this, new java.lang.Object[] {  });
	}


	public void onLocationResult (com.google.android.gms.location.LocationResult p0)
	{
		n_onLocationResult (p0);
	}

	private native void n_onLocationResult (com.google.android.gms.location.LocationResult p0);

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
