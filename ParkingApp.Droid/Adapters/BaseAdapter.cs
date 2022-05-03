using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.ViewModels;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Threading;

namespace ParkingApp.Droid.Adapters
{
    public abstract class BaseAdapter<TViewModel> : RecyclerView.Adapter where TViewModel : class, IMvxViewModel
    {
        private readonly IReadOnlyReactiveList<TViewModel> _list;
        private IDisposable _subscription;

        protected IMvxAndroidBindingContext BindingContext { get; }

        protected BaseAdapter(IReadOnlyReactiveList<TViewModel> list, IMvxAndroidBindingContext bindingContext)
        {
            _list = list;
            BindingContext = bindingContext;

            _subscription = _list.Changed.Subscribe(e => NotifyDataSetChanged());

            HasStableIds = true;
        }

        public abstract override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType);
        public abstract override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position);
        public abstract override long GetItemId(int position);

        public override int ItemCount => _list.Count;

        public TViewModel GetItem(int position) => _list[position];

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            Interlocked.Exchange(ref _subscription, Disposable.Empty).Dispose();
        }
    }
}