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
using Android.Graphics;

namespace XamarinMaterialStepperLib.style
{
    public class ProgressStepper: BaseNavigation
    {
        // views
        protected ProgressBar mProgress;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            applyTheme();

            SetContentView(Resource.Layout.style_progress);

            init();

            mProgress = (ProgressBar)FindViewById(Resource.Id.stepProgress);
            System.Diagnostics.Debug.Assert(mProgress != null);
            mProgress.ProgressDrawable.SetColorFilter(primaryColor, PorterDuff.Mode.SrcAtop);

            onUpdate();
        }

        public override void onUpdate()
        {
            base.onUpdate();
            mProgress.Max=(mSteps.total());
            mProgress.Progress=(mSteps.current() + 1);
        }

        public override void onError()
        {
            base.onError();
            new Handler().PostDelayed(delegate() {
                mSwitch.DisplayedChild=(0);
            }, 1500);
    }
}
}