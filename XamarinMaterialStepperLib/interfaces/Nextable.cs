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

namespace XamarinMaterialStepperLib.interfaces
{
    public interface Nextable
    {
        bool nextIf();

        bool isOptional();

        void onStepVisible();

        void onNext();

        void onPrevious();

        string optional();

        string error();
    }
}