using MvvmCross.ViewModels;

namespace ParkingApp.ViewModels
{
    public abstract class BaseViewModel<TParameter, TReturn> : MvxViewModel<TParameter, TReturn> where TParameter : class
    {
        protected abstract string TAG { get; }

        protected BaseViewModel()
        {
        }
    }
}