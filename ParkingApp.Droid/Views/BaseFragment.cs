using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace ParkingApp.Droid.Views
{
    public abstract class BaseFragment : MvxFragment
    {
        protected abstract int FragmentLayoutResource { get; }
        protected virtual int FragmentToolbarResource { get; }

        public MvxAppCompatActivity ParentActivity => (MvxAppCompatActivity)Activity;
        public Toolbar Toolbar { get; set; }

        protected BaseFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(FragmentLayoutResource, null);

            //Toolbar = view.FindViewById<Toolbar>(FragmentToolbarResource);

            //if (Toolbar != null)
            //{
            //    ParentActivity.SetSupportActionBar(Toolbar);
            //}

            return view;
        }
    }

    public abstract class BaseFragment<TViewModel> : BaseFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
}