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
using Strive.Core.Models.Employee.PersonalDetails;

namespace StriveEmployee.Android.Adapter.MyProfile.Documents
{
    public class DocumentsViewHolder : RecyclerView.ViewHolder
    {
        public TextView documentName_TextView;
        public TextView documentDate_TextView;
        public DocumentsViewHolder(View itemView) : base(itemView)
        {
            documentName_TextView = itemView.FindViewById<TextView>(Resource.Id.documentName_TextView);
            documentDate_TextView = itemView.FindViewById<TextView>(Resource.Id.documentDate_TextView);
        }
    }
    public class DocumentsAdapter : RecyclerView.Adapter
    {

        Context context;
        private List<EmployeeDocument> employeeDocuments = new List<EmployeeDocument>();
        private DocumentsViewHolder documents_ViewHolder;
        public DocumentsAdapter(Context context, List<EmployeeDocument> employeeDocuments )
        {
            this.context = context;
            this.employeeDocuments = employeeDocuments;
        }
        public override int ItemCount
        {
            get
            {
                return employeeDocuments.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            documents_ViewHolder = holder as DocumentsViewHolder;
            documents_ViewHolder.documentName_TextView.Text = employeeDocuments[position].FileName;
            if(!String.IsNullOrEmpty(employeeDocuments[position].CreatedDate))
            {
                var date = employeeDocuments[position].CreatedDate.Split("T");
                documents_ViewHolder.documentDate_TextView.Text = date[0];
            }          
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.DocumentsItemView, parent, false);
            return new DocumentsViewHolder(itemView);
        }
    }
   

}