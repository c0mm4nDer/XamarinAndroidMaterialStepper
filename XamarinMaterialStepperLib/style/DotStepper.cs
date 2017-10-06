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
using Android.Util;
using Android.Views.Animations;
using Android.Graphics.Drawables;

namespace XamarinMaterialStepperLib.style
{
    public class DotStepper : BaseNavigation
    {
        protected LinearLayout mDots;

        // attributes
        private Color unselected = Color.ParseColor("#bdbdbd");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            applyTheme();

            SetContentView(Resource.Layout.style_dots);
            mDots = (LinearLayout)FindViewById(Resource.Id.dots);

            init();
            onUpdate();

            if (mSteps.total() > 7)
                Log.Info("MaterialStepper", "You should use progress bar with so many steps!");
        }

        public override void onUpdate()
        {

            Animation scale_in = AnimationUtils.LoadAnimation(this, Resource.Animation.scale_in);
            Animation scale_out = AnimationUtils.LoadAnimation(this, Resource.Animation.scale_out);

            int i = 0;

            if (mDots.ChildCount == 0)
            {

                while (i++ < mSteps.total())
                {
                    View view = LayoutInflater.Inflate(Resource.Layout.dot, mDots, false);
                    view.StartAnimation(scale_out);
                    mDots.AddView(view);
                }

                // prevent see animation at start
                new Handler().PostDelayed(delegate
                {
                    mDots.Visibility = (ViewStates.Visible);
                }, 500);
            }


            for (i = 0; i < mDots.ChildCount; i++)

                if (i == mSteps.current() && !mSteps.isActive(i))
                {
                    mDots.GetChildAt(i).StartAnimation(scale_in);
                    mSteps.setActive(i, true);
                    color(i, true);
                }
                else if (i != mSteps.current() && mSteps.isActive(i))
                {
                    mDots.GetChildAt(i).StartAnimation(scale_out);
                    mSteps.setActive(i, false);
                    color(i, false);
                }

            base.onUpdate();

        }

        private void color(int i, bool selected)
        {
            Drawable d = mDots.GetChildAt(i).Background;
            d.SetColorFilter(new PorterDuffColorFilter(selected ? primaryColor : unselected, PorterDuff.Mode.SrcAtop));
        }
    }
}