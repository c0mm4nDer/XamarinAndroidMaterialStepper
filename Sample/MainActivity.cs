using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using Android.Runtime;
using Android.Views;

using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Sample
{
    [Activity(Label = "Sample", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, View.IOnClickListener
    {
        private Button tabs, progress, text, dots, tabsNL, tabClassic;
        private Toolbar toolbar;

        public void OnClick(View view)
        {
            Intent intent = null;

            if (view == tabsNL)
                intent = new Intent(this, typeof(TabSample));
            else if (view == progress)
                intent = new Intent(this, typeof(ProgressSample));
            else if (view == text)
                intent = new Intent(this, typeof(TextSample));
            else if (view == dots)
                intent = new Intent(this, typeof(DotsSample));
            else if (view == tabClassic)
                intent = new Intent(this, typeof(TabClassicSample));
            else if (view == tabs) {
                intent = new Intent(this, typeof(TabSample));
                intent.PutExtra("linear", true);
            }
            StartActivityForResult(intent, 1);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (data != null && data.Extras != null)
                foreach (string key in data.Extras.KeySet())
                    Toast.MakeText(this, key + " : " + data.Extras.Get(key).ToString(), ToastLength.Short).Show();
            base.OnActivityResult(requestCode, resultCode, data);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            tabs = (Button)FindViewById(Resource.Id.tabsL);
            tabsNL = (Button)FindViewById(Resource.Id.tabs);
            tabClassic = (Button)FindViewById(Resource.Id.tabClassic);
            text = (Button)FindViewById(Resource.Id.text);
            progress = (Button)FindViewById(Resource.Id.progress);
            dots = (Button)FindViewById(Resource.Id.dots);
            toolbar = (Toolbar)FindViewById(Resource.Id.toolbar);

            tabs.SetOnClickListener(this);
            tabsNL.SetOnClickListener(this);
            tabClassic.SetOnClickListener(this);
            text.SetOnClickListener(this);
            progress.SetOnClickListener(this);
            dots.SetOnClickListener(this);

            SetSupportActionBar(toolbar);
        }
    }
}

