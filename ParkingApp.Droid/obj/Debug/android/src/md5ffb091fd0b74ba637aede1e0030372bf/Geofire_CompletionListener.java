package md5ffb091fd0b74ba637aede1e0030372bf;


public class Geofire_CompletionListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.google.firebase.database.DatabaseReference.CompletionListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onComplete:(Lcom/google/firebase/database/DatabaseError;Lcom/google/firebase/database/DatabaseReference;)V:GetOnComplete_Lcom_google_firebase_database_DatabaseError_Lcom_google_firebase_database_DatabaseReference_Handler:Firebase.Database.DatabaseReference/ICompletionListenerInvoker, Xamarin.Firebase.Database\n" +
			"";
		mono.android.Runtime.register ("GeoFire.Xamarin.Android.Geofire+CompletionListener, GeoFire.Xamarin.Android", Geofire_CompletionListener.class, __md_methods);
	}


	public Geofire_CompletionListener ()
	{
		super ();
		if (getClass () == Geofire_CompletionListener.class)
			mono.android.TypeManager.Activate ("GeoFire.Xamarin.Android.Geofire+CompletionListener, GeoFire.Xamarin.Android", "", this, new java.lang.Object[] {  });
	}


	public void onComplete (com.google.firebase.database.DatabaseError p0, com.google.firebase.database.DatabaseReference p1)
	{
		n_onComplete (p0, p1);
	}

	private native void n_onComplete (com.google.firebase.database.DatabaseError p0, com.google.firebase.database.DatabaseReference p1);

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
