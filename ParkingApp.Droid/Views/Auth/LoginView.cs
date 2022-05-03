using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.Credentials;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Support.Constraints;
using Android.Views;
using Android.Widget;
using MvvmCross.Logging;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using ParkingApp.Constants;
using ParkingApp.ViewModels;

// Make floating action button Appear after all 10 digits inputted

namespace ParkingApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(AuthViewModel), fragmentContentId: Resource.Id.content_auth, addToBackStack: true, isCacheableFragment: true)]
    [Register(ViewTags.LOGIN)]
    public class LoginView : BaseFragment<LoginViewModel>
    {
        protected override int FragmentLayoutResource => Resource.Layout.LoginView;

        ImageButton phoneButton;

        ConstraintSet set;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            phoneButton = view.FindViewById<ImageButton>(Resource.Id.imageButton2);

            CredentialPickerConfig credentialPickerConfig = new CredentialPickerConfig.Builder()
                .SetShowCancelButton(true)
                .SetShowAddAccountButton(true)
                .Build();

            // Construct a request for phone numbers and show the picker
            HintRequest hint = new HintRequest.Builder()
            .SetPhoneNumberIdentifierSupported(true)
            //.SetHintPickerConfig(credentialPickerConfig)
            .Build();

            GoogleApiClient client = new GoogleApiClient.Builder(Context)
            .AddApi(Auth.CREDENTIALS_API)
            .Build();

            client.Connect();

            var intent = Auth.CredentialsApi.GetHintPickerIntent(client, hint);

            phoneButton.Click += (o, e) =>
            {
                if(client.IsConnected)
                StartIntentSenderForResult(intent.IntentSender, 42, null, 0, 0, 0, null);       
            };

            return view;
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            switch (requestCode)
            {
                case 42:
                    switch (resultCode)
                    {
                        case (int)Result.Ok:
                            Logs.Instance.Debug("OK");

                            Credential credential = (Credential)data.GetParcelableExtra(Credential.ExtraKey);
                            // Phone number string in E.164 format
                            var phoneNumber = credential.Id;

                            break;
                        case CredentialsApi.ActivityResultNoHintsAvailable:
                            Logs.Instance.Debug("No Accounts Available");
                            break;
                        case CredentialsApi.ActivityResultOtherAccount:
                            Logs.Instance.Debug("Canceled");
                            break;
                        default:
                            Logs.Instance.Debug($"Result code: {resultCode}");
                            break;
                    }
                    break;
            }
        }

        // Push buttons down, and fade in the edit text field in middle
        void ShowPhoneNumberEntry()
        {

        }
    }
}