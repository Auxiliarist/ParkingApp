using Android.OS;
using Android.Runtime;
using Android.Support.Constraints;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using ParkingApp.Constants;
using ParkingApp.Droid.Adapters;
using ParkingApp.ViewModels;

namespace ParkingApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), fragmentContentId: Resource.Id.content_main, addToBackStack: true, isCacheableFragment: true)]
    [Register(ViewTags.SPOTLIST)]
    public class SpotListView : BaseFragment<SpotListViewModel>
    {
        protected override int FragmentLayoutResource => Resource.Layout.SpotListView;

        private ConstraintLayout emptyLayout;
        private RecyclerView recyclerView;
        private SpotListAdapter adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.SpotListRecyclerView);
            emptyLayout = view.FindViewById<ConstraintLayout>(Resource.Id.SpotListEmptyLayout);

            if(recyclerView != null)
            {
                recyclerView.HasFixedSize = true;
                recyclerView.NestedScrollingEnabled = false;
                recyclerView.SetLayoutManager(new LinearLayoutManager(Context) { ReverseLayout = true, StackFromEnd = true, ItemPrefetchEnabled = true, InitialPrefetchItemCount = 4 });

                adapter = new SpotListAdapter(MainViewModel.Data, (IMvxAndroidBindingContext)BindingContext);
                recyclerView.SetAdapter(adapter);
            }

            return view;
        }
    }
}