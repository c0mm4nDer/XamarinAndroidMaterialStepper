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
using Android.Graphics.Drawables;
using Android.Support.V4.Graphics.Drawable;
using Android.Graphics;

namespace XamarinMaterialStepperLib.util
{
    public class TintUtils
    {
        public static void tintTextView(TextView textview, int color)
        {
            Drawable[] drawables = textview.GetCompoundDrawables();

            for (int i = 0; i < drawables.Length; i++)
            {

                if (drawables[i] == null)
                    continue;

                Drawable wrapDrawable = DrawableCompat.Wrap(drawables[i]);
                DrawableCompat.SetTintMode(wrapDrawable, PorterDuff.Mode.SrcAtop);
                DrawableCompat.SetTint(wrapDrawable, color);

                if (i == 0)
                    textview.SetCompoundDrawables(wrapDrawable, drawables[1], drawables[2], drawables[3]);
                else if (i == 1)
                    textview.SetCompoundDrawables(drawables[0], wrapDrawable, drawables[2], drawables[3]);
                else if (i == 2)
                    textview.SetCompoundDrawables(drawables[0], drawables[1], wrapDrawable, drawables[3]);
                else
                    textview.SetCompoundDrawables(drawables[0], drawables[1], drawables[2], wrapDrawable);
            }


        }
    }
}