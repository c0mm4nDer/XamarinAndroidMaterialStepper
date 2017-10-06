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
using Android.Support.V4.App;
using XamarinMaterialStepperLib.interfaces;

namespace XamarinMaterialStepperLib.adapter
{
    class PageStateAdapter : FragmentStatePagerAdapter, Pageable
    {
        private List<AbstractStep> fragments = new List<AbstractStep>();

        public PageStateAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
           
        }
        public void add(AbstractStep fragment)
        {
            fragments.Add(fragment);
            NotifyDataSetChanged();
        }

        public void set(List<AbstractStep> fragments)
        {
            this.fragments.Clear();
            this.fragments=(fragments);
            NotifyDataSetChanged();
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return fragments[position];
        }

        public override int Count => fragments.Count();
    }
}