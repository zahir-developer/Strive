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
using Strive.Core.Models.Employee.Documents;

namespace StriveEmployee.Android.Adapter.MyProfile.Documents
{
    public class AddDocumentsViewHolder : RecyclerView.ViewHolder
    {
        public TextView documentName_TextView;
        public CheckBox checkDoc_ImageButton;
        public AddDocumentsViewHolder(View itemView) : base(itemView)
        {
            documentName_TextView = itemView.FindViewById<TextView>(Resource.Id.editDocumentName_TextView);
            checkDoc_ImageButton = itemView.FindViewById<CheckBox>(Resource.Id.checkDocButton);
            //itemView.FindViewById<ImageButton>(Resource.Id.editDeleteDoc_ImageButton);
        }
    }
    public class AddDocumentsAdapter : RecyclerView.Adapter
    {

        Context context;
        private AddDocumentsViewHolder addDocuments_ViewHolder;
        private employeeDocument selectedFile;
        private List<employeeDocument> fileList;

       

        public AddDocumentsAdapter(Context context, List<employeeDocument> fileName)
        {
            this.context = context;
            this.fileList = fileName;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            addDocuments_ViewHolder = holder as AddDocumentsViewHolder;
            addDocuments_ViewHolder.documentName_TextView.Text = fileList[position].fileName;
            addDocuments_ViewHolder.checkDoc_ImageButton.Tag = position;
            addDocuments_ViewHolder.checkDoc_ImageButton.Click += SelectDocument;
        }

        private void SelectDocument(object sender, EventArgs e)
        {
            var compoundBtn = (CompoundButton)sender;
            int position = (int)compoundBtn.Tag;
            selectedFile = fileList[position];
            
        }

        public employeeDocument GetFile()
        {
            return selectedFile;
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
                return fileList.Count;
            }
                
         }
    }

   
}