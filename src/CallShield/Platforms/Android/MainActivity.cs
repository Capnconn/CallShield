using Android.App;
using Android.App.Roles;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace CallShield.UI.Platforms.Android
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        private static readonly int REQUEST_ID = 1;

        public MainActivity()
        {
        }

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RoleManager roleManager = (RoleManager)GetSystemService(RoleService)!;
            Intent intent =
            roleManager.CreateRequestRoleIntent(RoleManager.RoleCallScreening);
            StartActivityForResult(intent, REQUEST_ID);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == REQUEST_ID)
            {
                if (resultCode == Result.Ok)
                {
                }
                else
                {
                }
            }
        }
    }
}
