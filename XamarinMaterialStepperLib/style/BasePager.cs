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
using Android.Support.V4.View;
using XamarinMaterialStepperLib.interfaces;
using System.Diagnostics;
using XamarinMaterialStepperLib.adapter;

namespace XamarinMaterialStepperLib.style
{
    public class BasePager : BaseStyle
    {
        // view
        protected ViewPager mPager;

        // adapters
        protected Pageable mPagerAdapter;

        protected override void init()
        {
            base.init();
            this.mPager = (ViewPager)FindViewById(Resource.Id.stepPager);

            System.Diagnostics.Debug.Assert(mPager != null);
            mPager.Adapter = ((PagerAdapter)mPagerAdapter);
            mSteps.get(0).onStepVisible();
            mPager.AddOnPageChangeListener(new PageChangeAdapter());
        }

        void initAdapter()
        {
            new PageStateAdapter(SupportFragmentManager);
            if (mPagerAdapter == null)
                if (getStateAdapter())
                    mPagerAdapter = new PageStateAdapter(SupportFragmentManager);
                else
                    mPagerAdapter = new PageAdapter(SupportFragmentManager);
        }

        protected override void addStep(AbstractStep step)
        {
            base.addStep(step);
            initAdapter();
            mPagerAdapter.add(step);
        }

        protected override void addSteps(List<AbstractStep> steps)
        {
            base.addSteps(steps);
            initAdapter();
            mPagerAdapter.set(mSteps.getSteps());
        }

        public override void onUpdate()
        {
            base.onUpdate();
            mPager.CurrentItem=(mSteps.current());
        }
    }
}