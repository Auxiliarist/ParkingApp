using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using ParkingApp.Droid.Controls;
using ParkingApp.ViewModels;
using ReactiveUI;
using System.Collections.Generic;
using System.Linq;

namespace ParkingApp.Droid.Adapters
{
    public class SpotListAdapter : BaseAdapter<SpotViewModel>
    {
        public SpotListAdapter(IReadOnlyReactiveList<SpotViewModel> list, IMvxAndroidBindingContext bindingContext) : base(list, bindingContext)
        {
        }

        public override long GetItemId(int position)
        {
            var key = GetItem(position).Spot.Key;
            return SpotKeyToItemId.GetItemId(key);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflaterHolder);

            var view = BindingContext.BindingInflate(Resource.Layout.item_spot_rv, parent, false);

            var viewHolder = new SpotListViewHolder(view, itemBindingContext);

            return viewHolder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (!(holder is SpotListViewHolder viewHolder))
                return;

            var dataContext = GetItem(position);

            viewHolder.SetDataContext(dataContext);
        }

        private static class SpotKeyToItemId
        {
            private static long IdGenerator = 0;
            private static Dictionary<long, string> IdToKey = new Dictionary<long, string>();
            private static Dictionary<string, long> KeyToId = new Dictionary<string, long>();

            public static long GetItemId(string key)
            {
                if (!KeyToId.Keys.Contains(key))
                {
                    var id = IdGenerator++;
                    KeyToId.Add(key, id);
                    IdToKey.Add(id, key);
                }

                return KeyToId.GetValueOrDefault(key);
            }

            public static string GetKey(long id)
            {
                if (IdToKey.Keys.Contains(id))
                    return IdToKey.GetValueOrDefault(id);

                return null;
            }
        }
    }

    public class SpotListViewHolder : MvxRecyclerViewHolder
    {
        public TextView AddressTextView { get; set; }
        public Button ClaimSpotButton { get; set; }
        public MapCardView MapCardView { get; set; }
        public ImageView SpotImageView { get; set; }
        public TextView TimestampTextView { get; set; }
        public Button ViewSpotButton { get; set; }

        public SpotListViewHolder(View itemView, IMvxAndroidBindingContext context) : base(itemView, context)
        {
            MapCardView = itemView.FindViewById<MapCardView>(Resource.Id.MapCardView);
            SpotImageView = itemView.FindViewById<ImageView>(Resource.Id.SpotImageView);
            ViewSpotButton = itemView.FindViewById<Button>(Resource.Id.SpotButtonView);
            ClaimSpotButton = itemView.FindViewById<Button>(Resource.Id.SpotButtonClaim);
            AddressTextView = itemView.FindViewById<TextView>(Resource.Id.SpotTextAddress);
            TimestampTextView = itemView.FindViewById<TextView>(Resource.Id.SpotTextTimestamp);

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<SpotListViewHolder, SpotViewModel>();
                set.Bind(AddressTextView).To(vm => vm.Spot.Address);

                set.Bind(ViewSpotButton).To(vm => vm.ViewSpotCommand);
                set.Bind(ClaimSpotButton).To(vm => vm.ClaimSpotCommand);
                set.Apply();
            });
        }

        public void SetDataContext(object context)
        {
            this.DataContext = context;
        }
    }
}