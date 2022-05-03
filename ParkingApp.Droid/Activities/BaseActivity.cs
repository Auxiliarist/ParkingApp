using Android.Annotation;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.ViewModels;

namespace ParkingApp.Droid.Activities
{
    public abstract class BaseActivity : MvxAppCompatActivity 
    {
        protected abstract string TAG { get; }
        protected abstract int LayoutResource { get; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(LayoutResource);

            //Change status bar color from default black to primarydark
            SetStatusBarColor();
        }

        [TargetApi(Value = 23)]
        private void SetStatusBarColor()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                int color = ContextCompat.GetColor(ApplicationContext, Resource.Color.primaryDark);
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                Window.SetStatusBarColor(new Color(color));
            }
        }  
    }

    public abstract class BaseActivity<TViewModel> : BaseActivity where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
}