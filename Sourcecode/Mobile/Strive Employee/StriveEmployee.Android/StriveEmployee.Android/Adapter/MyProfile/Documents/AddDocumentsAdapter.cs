using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.Documents;
using Xamarin.Essentials;

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
        private ObservableCollection<employeeDocument> selectedFile = new ObservableCollection<employeeDocument>();
        public static ObservableCollection<FileResult> fileList = new ObservableCollection<FileResult>();
        

        public AddDocumentsAdapter(Context context, IEnumerable<FileResult> result)
        {
            this.context = context;
            //this.fileList = new ObservableCollection<FileResult>(result);
            result.ToList().ForEach(fileList.Add);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            addDocuments_ViewHolder = holder as AddDocumentsViewHolder;
            //if (tempFileList.Count > 0)
            //{
            //    foreach (var data in tempFileList)
            //    {
            //        if (data.FileName != fileList[position].FileName)
            //        {
            //            tempFileList.Add(data);
            //        }
            //        else
            //        {
            //            tempFileList.Remove(data);
            //        }
            //    }
            //}
            //else
            //{
            //    tempFileList.Add(fileList);
            //}
            addDocuments_ViewHolder.documentName_TextView.Text = fileList[position].FileName;
            addDocuments_ViewHolder.checkDoc_ImageButton.Tag = position;
            addDocuments_ViewHolder.checkDoc_ImageButton.Click += SelectDocument;
        }

        private void SelectDocument(object sender, EventArgs e)
        {
            var compoundBtn = (CompoundButton)sender;
            int position = (int)compoundBtn.Tag;
            var employeeDocuments = new employeeDocument();

            byte[] DataArray = File.ReadAllBytes(fileList[position].FullPath);
            var fileType = fileList[position].FileName.Split(".");
            employeeDocuments.fileName = fileList[position].FileName;
            employeeDocuments.filePath = fileList[position].FullPath;
            employeeDocuments.base64 = Convert.ToBase64String(DataArray);
            employeeDocuments.fileType = fileType[1];
            selectedFile.Add(employeeDocuments);
        }

        public ObservableCollection<employeeDocument> GetFile()
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