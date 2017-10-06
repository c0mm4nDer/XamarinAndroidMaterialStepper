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
using XamarinMaterialStepperLib.style;
using XamarinMaterialStepperLib;

namespace Sample
{
    [Activity(Label = "TabClassicSample")]
    class TabClassicSample: TabStepper
    {
        private int i = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            setErrorTimeout(1500);
            setLinear(false);
            setTitle("Tab Stepper <small>(Classic Tab) </small>");
            setAlternativeTab(false);
            setDisabledTouch();
            setPreviousVisible();

            

            addStep(createFragment(new StepSample()));
            addStep(createFragment(new StepSample()));
            addStep(createFragment(new StepSample()));
            addStep(createFragment(new StepSample()));
            addStep(createFragment(new StepSample()));
            base.OnCreate(savedInstanceState);
        }

        private AbstractStep createFragment(AbstractStep fragment)
        {
            Bundle b = new Bundle();
            b.PutInt("position", i++);
            fragment.Arguments=(b);
            return fragment;
        }

        public override void onComplete()
        {
            base.onComplete();
            Console.WriteLine("completed");
        }
    }
}