using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Strive.Core.ViewModels.Owner;

namespace StriveOwner.Android.Adapter
{
    public class SpinnerAdapter<T> : ArrayAdapter<string>
    {
        //    private int count;

        //    public SpinnerAdapter(Context context, int textViewResourceId, List<string> listItems)
        //        : base(context, textViewResourceId)
        //    {
        //      // count = listItems.Count - 1;
        //    }

        //    //public override View GetView(int position, View convertView, ViewGroup parent)
        //    //{
        //    //    // View view = base.GetView(position, convertView, parent);
        //    //    LayoutInflater inflater = LayoutInflater.From(cxt);
        //    //    View view = inflater.Inflate(Resource.Layout.SpinnerDefault_Text, parent, false);
        //    //    TextView text = (TextView)view.FindViewById(Resource.Id.text);
        //    //    if(position < 0)
        //    //    text.Text = "Item Type";
        //    //    return view;
        //    //}
        //    public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        //    {

        //        View view = base.GetDropDownView(position, convertView, parent);
        //        return view;
        //    }
        //    public override int Count =>  Count;
        //}
        private Context mContext;
        private int mTextResourceId;
        private List<string> listItems;
        bool hideDefaultText;
        public SpinnerAdapter(Context context, int textViewResourceId, List<string> listItem, bool isDefaultText) : base(context, textViewResourceId, listItem)
        {
            mContext = Context;
            mTextResourceId = textViewResourceId;
            listItems = listItem;
            hideDefaultText = isDefaultText;
        }
        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            View v;
            if (position == 0 && hideDefaultText)
            {
                TextView tv = new TextView(mContext);
                tv.Visibility = ViewStates.Gone;
                tv.SetHeight(0);
                v = tv;
                v.Visibility = ViewStates.Gone; 
            }
            else
                v = base.GetDropDownView(position, null, parent);
            return v;
            
        }
       
        public override int Count => (base.Count);
    }
}
