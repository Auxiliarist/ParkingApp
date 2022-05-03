package parkingapp.droid.views;


public class SettingsView
	extends md5392bf011e77833c9c5c2a41f05eb5ac8.MvxPreferenceFragmentCompat_1
	implements
		mono.android.IGCUserPeer,
		android.support.v7.preference.Preference.OnPreferenceChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreatePreferences:(Landroid/os/Bundle;Ljava/lang/String;)V:GetOnCreatePreferences_Landroid_os_Bundle_Ljava_lang_String_Handler\n" +
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onPause:()V:GetOnPauseHandler\n" +
			"n_onPreferenceChange:(Landroid/support/v7/preference/Preference;Ljava/lang/Object;)Z:GetOnPreferenceChange_Landroid_support_v7_preference_Preference_Ljava_lang_Object_Handler:Android.Support.V7.Preferences.Preference/IOnPreferenceChangeListenerInvoker, Xamarin.Android.Support.v7.Preference\n" +
			"";
		mono.android.Runtime.register ("ParkingApp.Droid.Views.SettingsView, ParkingApp.Droid", SettingsView.class, __md_methods);
	}


	public SettingsView ()
	{
		super ();
		if (getClass () == SettingsView.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Views.SettingsView, ParkingApp.Droid", "", this, new java.lang.Object[] {  });
	}


	public void onCreatePreferences (android.os.Bundle p0, java.lang.String p1)
	{
		n_onCreatePreferences (p0, p1);
	}

	private native void n_onCreatePreferences (android.os.Bundle p0, java.lang.String p1);


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public void onPause ()
	{
		n_onPause ();
	}

	private native void n_onPause ();


	public boolean onPreferenceChange (android.support.v7.preference.Preference p0, java.lang.Object p1)
	{
		return n_onPreferenceChange (p0, p1);
	}

	private native boolean n_onPreferenceChange (android.support.v7.preference.Preference p0, java.lang.Object p1);

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
