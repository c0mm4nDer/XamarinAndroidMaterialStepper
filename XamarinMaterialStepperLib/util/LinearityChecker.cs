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

namespace XamarinMaterialStepperLib.util
{
    public class LinearityChecker
    {
        public List<bool> mDone = new List<bool>();

        public LinearityChecker(int steps)
        {
            while ((steps--) > 0)
                mDone.Add(false);
        }

        public bool beforeDone(int i)
        {
            return i > 0 ? mDone[(i - 1)] : mDone[(0)];
        }

        public bool isDone(int i)
        {
            return mDone[(i)];
        }

        public void setDone(int i)
        {
            mDone[(i > 0 ? i - 1 : 0)]=true;
        }
    }
}