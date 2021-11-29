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

namespace StriveOwner.Android.Adapter
{
    public interface IItemClickListener
    {
        void OnClick(View itemView, int position, bool isLongClick);
    }
}