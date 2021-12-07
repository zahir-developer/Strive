using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;
using Environment = System.Environment;
using StriveEmployee.Android.Views;
using Java.Util.Zip;
using Android.Support.V7.App;

namespace StriveEmployee.Android.Adapter.MyProfile.Documents
{
    public class DocumentsViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
    {
        public TextView documentName_TextView;
        public TextView documentDate_TextView;
        public IItemClickListener itemClickListener;
        public ImageButton DownloadButton;
        public ImageButton DeleteButton;

        public DocumentsViewHolder(View itemView) : base(itemView)
        {
            documentName_TextView = itemView.FindViewById<TextView>(Resource.Id.documentName_TextView);
            documentDate_TextView = itemView.FindViewById<TextView>(Resource.Id.documentDate_TextView);
            DeleteButton = itemView.FindViewById<ImageButton>(Resource.Id.deleteButton);
            DownloadButton = itemView.FindViewById<ImageButton>(Resource.Id.downloadButton);
            DeleteButton.Click += DeleteButton_Click;
            DownloadButton.Click += DownloadButton_Click;
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            ClickInfo.ClickAction = 2;
            itemClickListener.OnClick(null, AdapterPosition, false);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            ClickInfo.ClickAction = 1;
            itemClickListener.OnClick(null, AdapterPosition, false);
        }

        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.itemClickListener = itemClickListener;
        }
        public void OnClick(View view)
        {
            
        }
    }
    public class DocumentsAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        private List<EmployeeDocument> employeeDocuments = new List<EmployeeDocument>();
        private DocumentsViewHolder documents_ViewHolder;
        public DocumentsAdapter(Context context, List<EmployeeDocument> employeeDocuments)
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
            if (!String.IsNullOrEmpty(employeeDocuments[position].CreatedDate))
            {
                var date = employeeDocuments[position].CreatedDate.Split("T");
                documents_ViewHolder.documentDate_TextView.Text = date[0];
            }
            documents_ViewHolder.SetItemClickListener(this);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.DocumentsItemView, parent, false);
            return new DocumentsViewHolder(itemView);
        }
        
        public async void OnClick(View itemView, int position, bool isLongClick)
        {
            if(ClickInfo.ClickAction == ClickInfo.DeleteClickAction)
            {
                DocumentsViewModel docsViewModel = new DocumentsViewModel();
                var delete = await docsViewModel.DeleteDocument(employeeDocuments[position].EmployeeDocumentId);
                if(delete)
                {
                    employeeDocuments.RemoveAt(position);
                    NotifyItemRemoved(position);
                    NotifyItemRangeChanged(position, employeeDocuments.Count);
                    
                }

            }
            else if(ClickInfo.ClickAction == ClickInfo.DownloadClickAction)
            {
                MyProfileTempData.EmployeeDocumentID = employeeDocuments[position].EmployeeDocumentId;
                MyProfileTempData.DocumentPassword = "string";
                DocumentsViewModel docsViewModel = new DocumentsViewModel();
                var fileBase64 = await docsViewModel.DownloadDocument(employeeDocuments[position].EmployeeDocumentId, "string");
                Intent intent = new Intent(context, typeof(DocumentView));
                MyProfileTempData.DocumentString = fileBase64.Document.Base64Url.ToString();
                context.StartActivity(intent);

            }
            else
            {

            }
        }
    }
    
    public static class ClickInfo
    {
        public static int DeleteClickAction = 1;
        public static int DownloadClickAction = 2;
        public static int ClickAction = -1;
    }
   
}