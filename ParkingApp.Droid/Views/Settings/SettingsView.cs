using Android.OS;
using Android.Runtime;
using Android.Support.V7.Preferences;
using Android.Views;
using Java.Lang;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.Preference;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using ParkingApp.Constants;
using ParkingApp.ViewModels;
using MvvmCross.Logging;
using System.Reactive.Linq;
using ReactiveUI;

namespace ParkingApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(SettingsMainViewModel), Resource.Id.content_settings)]
    [Register(ViewTags.SETTINGS)]
    public class SettingsView : MvxPreferenceFragmentCompat<SettingsViewModel>, Preference.IOnPreferenceChangeListener
    {
        SwitchPreferenceCompat switchPreference;

        public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
        {
            AddPreferencesFromResource(Resource.Xml.SettingsView);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            switchPreference = (SwitchPreferenceCompat)FindPreference("perform_sync");

            var set = this.CreateBindingSet<SettingsView, SettingsViewModel>();
            set.Bind(switchPreference).For(v => v.Checked).To(vm => vm.DarkMode);
            set.Apply();

            return view;
        }

        public bool OnPreferenceChange(Preference preference, Object newValue)
        {
            if(preference is SwitchPreferenceCompat switchPreference)
            {
                Logs.Instance.Debug(newValue.ToString());

                ViewModel.DarkMode = System.Boolean.Parse(newValue.ToString());
            }

            return true;
        }

        public override void OnResume()
        {
            base.OnResume();

            // register listener
            switchPreference.OnPreferenceChangeListener = this;
        }

        public override void OnPause()
        {
            // unregister listener
            switchPreference.OnPreferenceChangeListener = null;

            base.OnPause();
        }
    }
}