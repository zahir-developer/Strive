using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace StriveOwner.Android.Adapter
{
    class ViewPagerAdapter : FragmentPagerAdapter
    {

        Context context;

        private List<Fragment> FragmentList = new List<Fragment>();
        private List<string> FragmentTitleList = new List<string>();
        public ViewPagerAdapter(FragmentManager manager) : base(manager)
        {
            //;
        }
        public override int Count
        {
            get
            {
                return FragmentList.Count;
            }
        }
        public override Fragment GetItem(int postion)
        {
            return FragmentList[postion];
        }
        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(FragmentTitleList[position].ToLower());
        }

        public void AddFragment(Fragment fragment, string title)
        {
            FragmentList.Add(fragment);
            FragmentTitleList.Add(title);
        }

    }
}