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
using Java.Util;

namespace XamarinMaterialStepperLib.util
{
    public class StepUtils
    {
        private List<AbstractStep> mSteps = new List<AbstractStep>();
        private List<Boolean> mActiveDots = new List<Boolean>();
        private int mCurrent;


        public List<AbstractStep> getSteps()
        {
            return mSteps;
        }

        public AbstractStep get(int position)
        {
            return mSteps[(position)];
        }

        public bool isActive(int i)
        {
            return mActiveDots[(i)];
        }

        public bool setActive(int i, bool set)
        {
            return mActiveDots[(i)]= set;
        }

        public AbstractStep getCurrent()
        {
            return get(mCurrent);
        }

        public int total()
        {
            return mSteps.Count();
        }

        public void add(AbstractStep step)
        {
            mSteps.Add(step);
            mActiveDots.Add(false);
        }

        public void addAll(List<AbstractStep> mSteps)
        {
            this.mSteps=(mSteps);
            Collections.Fill(mActiveDots, false);
        }

        public void current(int mCurrent)
        {
            this.mCurrent = mCurrent;
        }

        public int current()
        {
            return mCurrent;
        }
    }
}