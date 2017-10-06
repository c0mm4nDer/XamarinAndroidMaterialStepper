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
using XamarinMaterialStepperLib.util;
using XamarinMaterialStepperLib.interfaces;
using Android.Text;
using Java.Lang;

namespace XamarinMaterialStepperLib.style
{
    public class BaseNavigation : BasePager, View.IOnClickListener
    {
        // view
        protected TextView mPrev, mNext, mEnd, mError;
        protected ViewSwitcher mSwitch;

        protected override void init()
        {
            base.init();

            mPrev = (TextView)FindViewById(Resource.Id.stepPrev);
            mNext = (TextView)FindViewById(Resource.Id.stepNext);
            mEnd = (TextView)FindViewById(Resource.Id.stepEnd);
            mError = (TextView)FindViewById(Resource.Id.stepError);
            this.mSwitch = (ViewSwitcher)FindViewById(Resource.Id.stepSwitcher);

            System.Diagnostics.Debug.Assert(mSwitch != null);
            mSwitch.DisplayedChild = (0);
            mSwitch.SetInAnimation(this, Resource.Animation.in_from_bottom);
            mSwitch.SetOutAnimation(this, Resource.Animation.out_to_bottom);

            // tint & color
            TintUtils.tintTextView(mPrev, tintColor);
            TintUtils.tintTextView(mNext, tintColor);
            mEnd.SetTextColor(primaryColor);

            // listener
            mPrev.SetOnClickListener(this);
            mNext.SetOnClickListener(this);
            mEnd.SetOnClickListener(this);
        }
        public void OnClick(View view)
        {
            AbstractStep step = mSteps.getCurrent();

            if (view == mPrev)
            {
                step.onPrevious();
                onPrevious();
            }
            else if (view == mNext || view == mEnd)
            {
                step.onNext();
                onNext();
            }
        }


        public override void onError()
        {
            mError.Text = (Html.FromHtml(mErrorString).ToString());
            if (mSwitch.DisplayedChild == 0)
                mSwitch.DisplayedChild = (1);

            new Handler().PostDelayed(new Runnable(delegate
            {
                if (mSwitch.DisplayedChild == 1) mSwitch.DisplayedChild = (0);
            }), getErrorTimeout() + 300);
        }
        

        public override void onUpdate()
        {
            base.onUpdate();
            bool isLast = mSteps.current() == mSteps.total() - 1;
            bool isFirst = mSteps.current() == 0;
            mNext.Visibility=(isLast ? ViewStates.Gone : ViewStates.Visible);
            mEnd.Visibility=(!isLast ? ViewStates.Gone : ViewStates.Visible);
            mPrev.Visibility=(isFirst && !startPreviousButton ? ViewStates.Gone : ViewStates.Visible);
            if (mSwitch.DisplayedChild != 0) mSwitch.DisplayedChild=(0);
        }
    }
}