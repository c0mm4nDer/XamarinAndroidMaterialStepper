using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinMaterialStepperLib.style
{
   public  class TextStepper: BaseNavigation
    {
        // views
        protected TextView mCounter;
        private String mText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            mText = GetString(Resource.String.ms_text_step);

            applyTheme();

            if (SupportActionBar != null)
                SupportActionBar.Elevation=(0);

            SetContentView(Resource.Layout.style_text);
            init();

            mSwitch.SetInAnimation(this, Resource.Animation.in_from_rigth);
            mSwitch.SetOutAnimation(this, Resource.Animation.out_to_left);
            mCounter = (TextView)FindViewById(Resource.Id.stepCounter);

            onUpdate();
        }

        public override void onUpdate()
        {
            base.onUpdate();
            int next = mSteps.current() < mSteps.total() ? mSteps.current() + 1 : mSteps.current();
            mCounter.Text=(mText.Replace("$current", next.ToString()).Replace("$total", mSteps.total().ToString()));
        }
    }
}