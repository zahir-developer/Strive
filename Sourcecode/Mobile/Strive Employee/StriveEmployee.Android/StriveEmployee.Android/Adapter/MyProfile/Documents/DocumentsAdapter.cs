using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;
using File = System.IO.File;
using Uri = Android.Net.Uri;
using Android.Webkit;
using Java.IO;
using Android.Util;

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
                try
                {
                    if(employeeDocuments.Count > 0 && employeeDocuments[position].EmployeeDocumentId != null)
                    {
                        var delete = await docsViewModel.DeleteDocument(employeeDocuments[position].EmployeeDocumentId);
                        if (delete)
                        {
                            employeeDocuments.RemoveAt(position);
                            NotifyItemRemoved(position);
                            NotifyItemRangeChanged(position, employeeDocuments.Count);

                        }
                    }
                }catch(Exception ex)
                {
                    return;
                }  

            }
            else if(ClickInfo.ClickAction == ClickInfo.DownloadClickAction)
            {
                MyProfileTempData.EmployeeDocumentID = employeeDocuments[position].EmployeeDocumentId;
                MyProfileTempData.DocumentPassword = "string";
                DocumentsViewModel docsViewModel = new DocumentsViewModel();
                var fileBase64 = await docsViewModel.DownloadDocument(employeeDocuments[position].EmployeeDocumentId, "string");
                //Intent intent = new Intent(context, typeof(DocumentView));
                MyProfileTempData.DocumentString = fileBase64.Document.Base64Url.ToString();
                //context.StartActivity(intent);
                var data = MyProfileTempData.DocumentString;
                byte[] dataconverted = System.Convert.FromBase64String(data);
                try
                {

                   string base64 = Base64.EncodeToString(dataconverted,
                           Base64Flags.Default);
                    byte[] bfile = Base64.Decode(base64, Base64Flags.Default);
                    var file = SaveBinary(fileBase64.Document.FileName, bfile);
                    var bytes = File.ReadAllBytes(file);
                    Java.IO.File file1 = new Java.IO.File(file);
                    OpenFile(context, file1);
                }catch(UnsupportedEncodingException ex)
                {

                }
                }
           
        }
        public void OpenFile(Context context, Java.IO.File url)
        {
            // Create URI
            Uri uri = FileProvider.GetUriForFile(context, context.ApplicationContext.PackageName + ".fileprovider", url);


            Intent intent = new Intent(Intent.ActionView);
            // Check what kind of file you are trying to open, by comparing the url with extensions.
            // When the if condition is matched, plugin sets the correct intent (mime) type, 
            // so Android knew what application to use to open the file
            if (url.ToString().Contains(".doc") || url.ToString().Contains(".docx"))
            {
                // Word document
                intent.SetDataAndType(uri, "application/msword");
            }
            else if (url.ToString().Contains(".pdf"))
            {
                // PDF file
                intent.SetDataAndType(uri, "application/pdf");
            }
            else if (url.ToString().Contains(".ppt") || url.ToString().Contains(".pptx"))
            {
                // Powerpoint file
                intent.SetDataAndType(uri, "application/vnd.ms-powerpoint");
            }
            else if (url.ToString().Contains(".xls") || url.ToString().Contains(".xlsx"))
            {
                // Excel file
                intent.SetDataAndType(uri, "application/vnd.ms-excel");
            }
            else if (url.ToString().Contains(".zip") || url.ToString().Contains(".rar"))
            {
                // WAV audio file
                intent.SetDataAndType(uri, "application/x-wav");
            }
            else if (url.ToString().Contains(".rtf"))
            {
                // RTF file
                intent.SetDataAndType(uri, "application/rtf");
            }
            else if (url.ToString().Contains(".wav") || url.ToString().Contains(".mp3"))
            {
                // WAV audio file
                intent.SetDataAndType(uri, "audio/x-wav");
            }
            else if (url.ToString().Contains(".gif"))
            {
                // GIF file
                intent.SetDataAndType(uri, "image/gif");
            }
            else if (url.ToString().Contains(".jpg") || url.ToString().Contains(".jpeg") || url.ToString().Contains(".png"))
            {
                // JPG file
                intent.SetDataAndType(uri, "image/jpeg");
            }
            else if (url.ToString().Contains(".txt"))
            {
                // Text file
                intent.SetDataAndType(uri, "text/plain");
            }
            else if (url.ToString().Contains(".3gp") || url.ToString().Contains(".mpg") || url.ToString().Contains(".mpeg") || url.ToString().Contains(".mpe") || url.ToString().Contains(".mp4") || url.ToString().Contains(".avi"))
            {
                // Video files
                intent.SetDataAndType(uri, "video/*");
            }
            else
            {
                //if you want you can also define the intent type for any other file

                //additionally use else clause below, to manage other unknown extensions
                //in this case, Android will show all applications installed on the device
                //so you can choose which application to use
                intent.SetDataAndType(uri, "*/*");
            }

            
            intent = new Intent(Intent.ActionView);
            intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);
            var extension = MimeTypeMap.GetFileExtensionFromUrl(url.ToString());
            var mimeType = MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension.ToLower());
            intent.AddFlags(ActivityFlags.GrantReadUriPermission);
            intent.SetDataAndType(uri, mimeType);
            context.StartActivity(intent);
        }
        private string SaveBinary(string filename, byte[] bytes)
        {
            string filepath = GetFilePath(filename);
            if (File.Exists((filepath)))
            {
                File.Delete(filepath);
            }
            File.WriteAllBytes(filepath, bytes);

            return filepath;
        }



        string GetFilePath(string filename)
        {
            return System.IO.Path.Combine(GetPath(), filename);
        }

        string GetPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        }
    }
    
    public static class ClickInfo
    {
        public static int DeleteClickAction = 1;
        public static int DownloadClickAction = 2;
        public static int ClickAction = -1;
    }
   
}