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
using Android.Support.V7.App;
using XamarinMaterialStepperLib.interfaces;
using XamarinMaterialStepperLib.util;
using Android;
using Android.Text;
using Android.Util;
using Android.Support.V4.Content;
using Android.Graphics.Drawables;

using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Graphics;

namespace XamarinMaterialStepperLib.style
{
   public class BaseStyle : AppCompatActivity, Stepable
    {
        protected StepUtils mSteps = new StepUtils();
        Bundle mExtras = new Bundle();
        Dictionary<int, Bundle> mStepData = new Dictionary<int, Bundle>();

        // attributes
        protected String mTitle;
        protected String mErrorString;

        // properties
        protected Color tintColor, primaryColor, primaryColorDark;
        protected bool startPreviousButton = false;
        private int mErrorTimeout = 1500;
        private bool useStateAdapter = false;
        private Toolbar toolbar;

        // getters
        protected int getColor()
        {
            return primaryColor;
        }

        public Bundle getStepData()
        {
            return getStepDataFor(mSteps.current());
        }

        public Bundle getStepDataFor(int step)
        {
            if (mStepData[(step)] == null)
                mStepData.Add(step, new Bundle());
            return mStepData[(step)];
        }

        public int steps()
        {
            return mSteps.total();
        }

        protected int getErrorTimeout()
        {
            return mErrorTimeout;
        }

        protected void setErrorTimeout(int mErrorTimeout)
        {
            this.mErrorTimeout = mErrorTimeout;
        }

        
        protected void UseStateAdapter()
        {
            useStateAdapter = true;
        }

        //[Obsolete("Not used anymore", true)]
        protected void SetStateAdapter()
        {
            this.useStateAdapter = true;
        }

        protected void setStartPreviousButton()
        {
            this.startPreviousButton = true;
        }

        protected bool getStateAdapter()
        {
            return useStateAdapter;
        }

        public Bundle getExtras()
        {
            return mExtras;
        }

        // setters

        protected void setTitle(String mTitle)
        {
            this.mTitle = mTitle;
        }

        protected void setPrimaryColor(Color primaryColor)
        {
            this.primaryColor = primaryColor;
        }

        protected void setDarkPrimaryColor(Color primaryColorDark)
        {
            this.primaryColorDark = primaryColorDark;
        }

        // steps utils

        protected virtual void addStep(AbstractStep step)
        {
            mSteps.add(wrap(step));
        }

        protected virtual void addSteps(List<AbstractStep> steps)
        {
            mSteps.addAll(wrap(steps));
        }

        protected virtual void init()
        {
            toolbar = (Toolbar)FindViewById(Resource.Id.toolbar);
            toolbar.SetBackgroundColor(primaryColor);
            toolbar.Title=(Html.FromHtml(mTitle).ToString());
        }

        public Toolbar getToolbar()
        {
            return toolbar;
        }

        private void findColors()
        {

            if (primaryColor == 0)
            {
                TypedValue typedValue = new TypedValue();
                Theme.ResolveAttribute(Resource.Attribute.colorPrimary, typedValue, true);
                primaryColor = Color.ParseColor("#" + String.Format("{0:X}", typedValue.Data));
            }

            if (primaryColor == 0)
                primaryColor = Color.ParseColor("#" + String.Format("{0:X}", ContextCompat.GetColor(this, Resource.Color.material_stepper_global)));

            if (primaryColorDark == 0)
                primaryColorDark = Color.ParseColor("#" + String.Format("{0:X}", ContextCompat.GetColor(this, Resource.Color.material_stepper_global_dark)));

            if (primaryColor == 0)
            {
                TypedValue typedValue = new TypedValue();
                Theme.ResolveAttribute(Resource.Attribute.colorPrimaryDark, typedValue, true);
                primaryColorDark = Color.ParseColor("#" + String.Format("{0:X}", typedValue.Data) );
            }

        }

       protected  void applyTheme()
        {

            findColors();

            if (SupportActionBar != null)
            {

                SupportActionBar.Title=(Html.FromHtml(mTitle).ToString());
                SupportActionBar.SetBackgroundDrawable(new ColorDrawable(primaryColor));

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    Window.SetStatusBarColor(primaryColorDark);

            }

            tintColor = Color.ParseColor("#" + String.Format("{0:X}", ContextCompat.GetColor(this, Resource.Color.material_stepper_bottom_bar_text)));

        }

        
        public virtual void onComplete()
        {
            // to be redefined
        }

        public virtual void onComplete(Bundle data)
        {
            // to be redefined
        }

        public virtual void onPrevious()
        {
            if (mSteps.current() <= 0)
                return;

            mSteps.current(mSteps.current() - 1);
            onUpdate();
        }
        public virtual void onNext()
        {
            AbstractStep step = mSteps.getCurrent();

            if (!step.isOptional() && !step.nextIf())
            {
                mErrorString = step.error();
                onError();
                return;
            }

            if (mSteps.current() == mSteps.total() - 1)
            {
                Intent intent = new Intent();
                intent.PutExtras(mExtras);
                SetResult(Result.FirstUser, intent);
                onComplete();
                onComplete(getExtras());
                return;
            }

            if (mSteps.current() > mSteps.total() - 1)
                return;

            mSteps.current(mSteps.current() + 1);
            onUpdate();
        }



        public virtual void onError()
        {
        }


        public virtual void onUpdate()
        {
        }

        private AbstractStep wrap(AbstractStep step)
        {
            return step.stepper(this);
        }

        private List<AbstractStep> wrap(List<AbstractStep> steps)
        {
            foreach (AbstractStep step in steps)
                step.stepper(this);

            return steps;
        }

        [Obsolete("Not used anymore", true)]
        protected void setColorPrimary(Color primaryColor)
        {
            this.primaryColor = primaryColor;
        }

        [Obsolete("Not used anymore", true)]
        protected void setColorPrimaryDark(Color primaryColorDark)
        {
            this.primaryColorDark = primaryColorDark;
        }
    }
}