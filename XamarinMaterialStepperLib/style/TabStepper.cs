using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using XamarinMaterialStepperLib.util;
using Android.Graphics.Drawables;
using Android.Text;

namespace XamarinMaterialStepperLib.style
{
    public class TabStepper : BasePager, View.IOnClickListener
    {
        protected TextView mError;

        // attributes
        Color unselected = Color.ParseColor("#9e9e9e");

        // views
        private HorizontalScrollView mTabs;
        private LinearLayout mStepTabs;
        private bool mLinear;
        private bool showPrevButton = false;
        private bool disabledTouch = false;
        private bool mTabAlternative;
        private ViewSwitcher mSwitch;
        private LinearityChecker mLinearity;
        private Button mContinue;
        private TextView mPreviousButton;

        protected void setLinear(bool mLinear)
        {
            this.mLinear = mLinear;
        }

        protected void setDisabledTouch()
        {
            this.disabledTouch = true;
        }

        protected void setPreviousVisible()
        {
            this.showPrevButton = true;
        }

        protected void setAlternativeTab(bool mTabAlternative)
        {
            this.mTabAlternative = mTabAlternative;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            applyTheme();

            if (SupportActionBar != null)
                SupportActionBar.Elevation = (0);

            SetContentView(Resource.Layout.style_horizontal_tabs);

            init();

            mTabs = (HorizontalScrollView)FindViewById(Resource.Id.steps);
            mStepTabs = (LinearLayout)mTabs.FindViewById(Resource.Id.stepTabs);
            mSwitch = (ViewSwitcher)FindViewById(Resource.Id.stepSwitcher);
            mError = (TextView)FindViewById(Resource.Id.stepError);
            mPreviousButton = (TextView)FindViewById(Resource.Id.stepPrev);

            mContinue = (Button)FindViewById(Resource.Id.continueButton);
            mContinue.SetTextColor(primaryColor);
            mContinue.SetOnClickListener(this);

            mSwitch.DisplayedChild = (0);
            mSwitch.SetInAnimation(this, Resource.Animation.in_from_bottom);
            mSwitch.SetOutAnimation(this, Resource.Animation.out_to_bottom);

            mLinearity = new LinearityChecker(mSteps.total());

            if (!showPrevButton)
                mPreviousButton.Visibility = (ViewStates.Gone);

            mPreviousButton.Click += delegate {
                AbstractStep step = mSteps.getCurrent();
                step.onPrevious();
                onPrevious();
            };

            onUpdate();
        }

        public override void onUpdate()
        {
            int i = 0;

            if (mStepTabs.ChildCount == 0)
            {
                while (i < mSteps.total())
                {
                    AbstractStep step = mSteps.get(i);
                    mStepTabs.AddView(createStepTab((i++), step.name(), step.isOptional(), step.optional()));
                }
            }

            int size = mStepTabs.ChildCount;

            for (i = 0; i < size; i++)
            {

                View view = mStepTabs.GetChildAt(i);

                Boolean done = mLinearity.isDone(i);
                View doneIcon = view.FindViewById(Resource.Id.done);
                View stepIcon = view.FindViewById(Resource.Id.step);


                doneIcon.Visibility = (done ? ViewStates.Visible : ViewStates.Gone);
                stepIcon.Visibility = (!done ? ViewStates.Visible : ViewStates.Gone);
                //((TextView)doneIcon).Text = i.ToString();
                

                if(i > mSteps.current())
                {
                    doneIcon.Visibility = ViewStates.Gone;
                    stepIcon.Visibility = ViewStates.Visible;
                    color(stepIcon, false);
                }
                else if (i == mSteps.current())
                {
                    doneIcon.Visibility = ViewStates.Gone;
                    stepIcon.Visibility = ViewStates.Visible;
                    color(stepIcon, true);
                }
                else
                {
                    doneIcon.Visibility = ViewStates.Visible;
                    stepIcon.Visibility = ViewStates.Gone;
                    color(doneIcon, true);
                }

                

                TextView text = (TextView)view.FindViewById(Resource.Id.title);
                //text.SetTypeface(, i == mSteps.current() || done ? TypefaceStyle.Bold : TypefaceStyle.Normal);
                (view.FindViewById(Resource.Id.title)).Alpha = (i == mSteps.current() || done ? 1 : 0.54f);

                mPreviousButton.Visibility = (showPrevButton && mSteps.current() > 0 ? ViewStates.Visible : ViewStates.Gone);

            }


            if (mSteps.current() == mSteps.total() - 1)
                mContinue.Text = GetString(Resource.String.ms_end);
            else
                mContinue.Text = GetString(Resource.String.ms_continue);

        }

        private bool updateDoneCurrent()
        {
            if (mSteps.getCurrent().nextIf())
            {
                mLinearity.setDone(mSteps.current() + 1);
                return true;
            }
            return mSteps.getCurrent().isOptional();
        }

        private View createStepTab(int position, String title, bool isOptional, String optionalStr)
        {
            View view = LayoutInflater.Inflate(mTabAlternative ? Resource.Layout.step_tab_alternative : Resource.Layout.step_tab, mStepTabs, false);
            ((TextView)view.FindViewById(Resource.Id.step)).Text = ((position + 1).ToString());

            if (isOptional)
            {
                view.FindViewById(Resource.Id.optional).Visibility = (ViewStates.Visible);
                ((TextView)view.FindViewById(Resource.Id.optional)).Text = (optionalStr);
            }

            if (position == mSteps.total() - 1)
                view.FindViewById(Resource.Id.divider).Visibility = (ViewStates.Gone);

            ((TextView)view.FindViewById(Resource.Id.title)).Text = (title);


            if (!disabledTouch)
                view.Click += delegate
                {
                    bool optional = mSteps.getCurrent().isOptional();

                    if (position != mSteps.current())
                        updateDoneCurrent();

                    if (!mLinear || optional || mLinearity.beforeDone(position))
                    {
                        mSteps.current(position);
                        updateScrolling(position);
                    }
                    else
                        onError();

                    onUpdate();
                };

            return view;
        }

        private void color(View view, bool selected)
        {
            Drawable d = view.Background;
            d.SetColorFilter(new PorterDuffColorFilter(selected ? primaryColor : unselected, PorterDuff.Mode.SrcAtop));
        }

        private void updateScrolling(int newPosition)
        {
            View tab = mStepTabs.GetChildAt(mSteps.current());
            bool isNear = mSteps.current() == newPosition - 1 || mSteps.current() == newPosition + 1;
            mPager.SetCurrentItem(mSteps.current(), isNear);
            mTabs.SmoothScrollTo(tab.Left - 20, 0);
            onUpdate();
        }

        public override void onError()
        {

            mError.Text = (Html.FromHtml(mSteps.getCurrent().error()).ToString());

            if (mSwitch.DisplayedChild == 0)
                mSwitch.DisplayedChild = (1);

            new Handler().PostDelayed(delegate
            {
                if (mSwitch.DisplayedChild == 1) mSwitch.DisplayedChild = (0);
            }, getErrorTimeout() + 300);

        }

        public override void onPrevious()
        {
            base.onPrevious();
            updateScrolling(mSteps.current() - 1);
        }

        public void OnClick(View v)
        {
            if (updateDoneCurrent())
            {
                AbstractStep step = mSteps.getCurrent();
                step.onNext();
                onNext();
                updateScrolling(mSteps.current() + 1);
            }
            else
                onError();
        }

        [Obsolete("Not used anymore", true)]
        protected void DisabledTouch()
        {
            this.disabledTouch = true;
        }

        [Obsolete("Not used anymore", true)]
        protected void showPreviousButton()
        {
            this.showPrevButton = true;
        }
    }
}