package md54536fb7610597fe6420976a1562890ab;


public class FloatingActionMenu_ChildAnimator
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.animation.Animator.AnimatorListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAnimationCancel:(Landroid/animation/Animator;)V:GetOnAnimationCancel_Landroid_animation_Animator_Handler:Android.Animation.Animator/IAnimatorListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onAnimationEnd:(Landroid/animation/Animator;)V:GetOnAnimationEnd_Landroid_animation_Animator_Handler:Android.Animation.Animator/IAnimatorListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onAnimationRepeat:(Landroid/animation/Animator;)V:GetOnAnimationRepeat_Landroid_animation_Animator_Handler:Android.Animation.Animator/IAnimatorListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onAnimationStart:(Landroid/animation/Animator;)V:GetOnAnimationStart_Landroid_animation_Animator_Handler:Android.Animation.Animator/IAnimatorListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("ParkingApp.Droid.Controls.FloatingActionMenu+ChildAnimator, ParkingApp.Droid", FloatingActionMenu_ChildAnimator.class, __md_methods);
	}


	public FloatingActionMenu_ChildAnimator ()
	{
		super ();
		if (getClass () == FloatingActionMenu_ChildAnimator.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Controls.FloatingActionMenu+ChildAnimator, ParkingApp.Droid", "", this, new java.lang.Object[] {  });
	}

	public FloatingActionMenu_ChildAnimator (md54536fb7610597fe6420976a1562890ab.FloatingActionMenu p0, android.view.View p1)
	{
		super ();
		if (getClass () == FloatingActionMenu_ChildAnimator.class)
			mono.android.TypeManager.Activate ("ParkingApp.Droid.Controls.FloatingActionMenu+ChildAnimator, ParkingApp.Droid", "ParkingApp.Droid.Controls.FloatingActionMenu, ParkingApp.Droid:Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public void onAnimationCancel (android.animation.Animator p0)
	{
		n_onAnimationCancel (p0);
	}

	private native void n_onAnimationCancel (android.animation.Animator p0);


	public void onAnimationEnd (android.animation.Animator p0)
	{
		n_onAnimationEnd (p0);
	}

	private native void n_onAnimationEnd (android.animation.Animator p0);


	public void onAnimationRepeat (android.animation.Animator p0)
	{
		n_onAnimationRepeat (p0);
	}

	private native void n_onAnimationRepeat (android.animation.Animator p0);


	public void onAnimationStart (android.animation.Animator p0)
	{
		n_onAnimationStart (p0);
	}

	private native void n_onAnimationStart (android.animation.Animator p0);

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
