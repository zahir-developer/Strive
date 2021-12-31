using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;

namespace StriveEmployee.Android.Fragments.MyProfile.Documents
{
    public class AddDocumentsFragment : MvxFragment<AddDocumentsViewModel>
    {
        private Button browse_Button;
        private Button back_Button;
        private Button save_Button;
        private RecyclerView addDoc_RecyclerView;
        private FileData fileData;
        MyProfileFragment MyProfFragment;
        DocumentsFragment documentsFragment;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.AddDocuments_Fragment, null);
            this.ViewModel = new AddDocumentsViewModel();
            browse_Button = rootView.FindViewById<Button>(Resource.Id.browse_Button);
            back_Button = rootView.FindViewById<Button>(Resource.Id.editDocumentsBack_Button);
            save_Button = rootView.FindViewById<Button>(Resource.Id.editDocumentsSave_Button);
            browse_Button.Click += Browse_Button_Click;
            save_Button.Click += Save_Button_ClickAsync;
            back_Button.Click += Back_Button_Click;
            addDoc_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.editDoc_RecyclerView);
            MyProfFragment = new MyProfileFragment();
            documentsFragment = new DocumentsFragment();
            return rootView;
        }

        private async void Save_Button_ClickAsync(object sender, EventArgs e)
        {
            await this.ViewModel.SaveDocuments();
        }

        private void Back_Button_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)this.Context;    
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, documentsFragment).Commit();
        }

        private async void Browse_Button_Click(object sender, EventArgs e)
        {
            try
            {
                fileData = await CrossFilePicker.Current.PickFile();
                if(fileData != null)
                {
                    this.ViewModel.filedata = Convert.ToBase64String(fileData.DataArray);
                   this.ViewModel.filepath = fileData.FilePath;
                   this.ViewModel.filename = fileData.FileName;
                   var fileType = fileData.FileName.Split(".");
                   this.ViewModel.filetype = fileType[1];
                  // await this.ViewModel.SaveDocuments();
                    AppCompatActivity activity = (AppCompatActivity)this.Context;
                    activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, MyProfFragment).Commit();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}