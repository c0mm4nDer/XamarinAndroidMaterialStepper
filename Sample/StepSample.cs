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
using XamarinMaterialStepperLib;
using Android.Text;
using XamarinMaterialStepperLib.interfaces;

namespace Sample
{
    class StepSample : AbstractStep
    {
        private int i = 1;
        private Button button;
        private readonly static String CLICK = "click";

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View v = inflater.Inflate(Resource.Layout.step, container, false);
            button = (Button)v.FindViewById(Resource.Id.button);

            if (savedInstanceState != null)
                i = savedInstanceState.GetInt(CLICK, 0);

            button.Text = (Html.FromHtml("Tap <b>" + i + "</b>").ToString());

            button.Click += (view, e) =>
            {
                ((Button)view).Text = (Html.FromHtml("Tap <b>" + (++i) + "</b>").ToString());
                mStepper.getExtras().PutInt(CLICK, i);
            };

            return v;
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt(CLICK, i);
        }

        public override string name()
        {
            return "Tab " + Arguments.GetInt("position", 0);
        }

        public override bool isOptional()
        {
            return true;
        }


        public override void onStepVisible()
        {
        }

        public override void onNext()
        {
            Console.WriteLine("onNext");
        }

        public override void onPrevious()
        {
            Console.WriteLine("onPrevious");
        }

        public override String optional()
        {
            return "You can skip";
        }

        public override bool nextIf()
        {
            return i > 1;
        }

        public override String error()
        {
            return "<b>You must click!</b> <small>this is the condition!</small>";
        }
    }
}