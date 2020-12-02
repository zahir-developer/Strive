using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace StriveEmployee.Android.Adapter.MyProfile.Documents
{
    public class AddDocumentsViewHolder : RecyclerView.ViewHolder
    {
        public TextView editDocumentName_TextView;
        public ImageButton editViewDoc_ImageButton;
        public ImageButton editDeleteDoc_ImageButton;
        public AddDocumentsViewHolder(View itemView) : base(itemView)
        {
            itemView.FindViewById<TextView>(Resource.Id.editDocumentName_TextView);
            itemView.FindViewById<ImageButton>(Resource.Id.editViewDoc_ImageButton);
            itemView.FindViewById<ImageButton>(Resource.Id.editDeleteDoc_ImageButton);
        }
    }
    public class AddDocumentsAdapter : RecyclerView.Adapter
    {

        Context context;

        public AddDocumentsAdapter(Context context)
        {
            this.context = context;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View view = inflater.Inflate(Resource.Layout.AddDocumentsItemView, parent, false);
            return new AddDocumentsViewHolder(view);
        }
        public override int ItemCount
        {
            get
            {
                return 0;
            }
                
         }
    }

   
}