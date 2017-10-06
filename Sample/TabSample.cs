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
    [Activity(Label = "TabSample")]
    class TabSample:TabStepper
    {
        private int i = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            bool linear = Intent.GetBooleanExtra("linear", false);

            setErrorTimeout(1500);
            setLinear(linear);
            setTitle("Tab Stepper <small>(" + (linear ? "" : "Non ") + "Linear)</small>");
            setAlternativeTab(true);

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
    }
}