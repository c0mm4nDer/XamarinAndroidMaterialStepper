using Android.OS;
using Android.Support.V4.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamarinMaterialStepperLib.interfaces;
using XamarinMaterialStepperLib.style;

namespace XamarinMaterialStepperLib
{
    public abstract class AbstractStep: Fragment,Nextable
    {
        protected BaseStyle mStepper;

        public AbstractStep stepper(BaseStyle mStepper)
        {
            this.mStepper = mStepper;
            return this;
        }

        protected Bundle getStepData()
        {
            return mStepper.getStepData();
        }

        protected Bundle getStepDataFor(int step)
        {
            return mStepper.getStepDataFor(step);
        }

        protected Bundle getLastStepData()
        {
            return mStepper.getStepDataFor(mStepper.steps() - 1);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance=(true);
        }
        public virtual string optional()
        {
            return IsAdded ? GetString(Resource.String.ms_optional) : "";
        }

        public abstract String name();

        public virtual bool isOptional()
        {
            return false;
        }

        public virtual string error()
        {
            return "No error";
        }

        

        public virtual bool nextIf()
        {
            return true;
        }

        public virtual void onNext()
        {
        }

        public virtual void onPrevious()
        {
        }

        public virtual void onStepVisible()
        {
        }

        
    }
}
