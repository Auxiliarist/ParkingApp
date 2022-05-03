package mono.android.app;

public class ApplicationRegistration {

	public static void registerApplications ()
	{
				// Application and Instrumentation ACWs must be registered first.
		mono.android.Runtime.register ("ParkingApp.Droid.MainApplication, ParkingApp.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", md545e957b5a3d0a5b36eb3871fd1a1c207.MainApplication.class, md545e957b5a3d0a5b36eb3871fd1a1c207.MainApplication.__md_methods);
		mono.android.Runtime.register ("MvvmCross.Platforms.Android.Views.MvxAndroidApplication, MvvmCross, Version=6.1.2.0, Culture=neutral, PublicKeyToken=null", md5231beb04e46a1dc811e36737109a7a02.MvxAndroidApplication.class, md5231beb04e46a1dc811e36737109a7a02.MvxAndroidApplication.__md_methods);
		mono.android.Runtime.register ("MvvmCross.Platforms.Android.Views.MvxAndroidApplication`2, MvvmCross, Version=6.1.2.0, Culture=neutral, PublicKeyToken=null", md5231beb04e46a1dc811e36737109a7a02.MvxAndroidApplication_2.class, md5231beb04e46a1dc811e36737109a7a02.MvxAndroidApplication_2.__md_methods);
		mono.android.Runtime.register ("MvvmCross.Droid.Support.V7.AppCompat.MvxAppCompatApplication`2, MvvmCross.Droid.Support.V7.AppCompat, Version=6.1.2.0, Culture=neutral, PublicKeyToken=null", md5716162e2cd7f7ce01364d7c5d961f40b.MvxAppCompatApplication_2.class, md5716162e2cd7f7ce01364d7c5d961f40b.MvxAppCompatApplication_2.__md_methods);
		
	}
}
