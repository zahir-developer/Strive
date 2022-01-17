using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Strive.Core.Models.Employee.Documents;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;
using StriveEmployee.Android.Adapter.MyProfile.Documents;

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
        private AddDocumentsAdapter addDocuments_Adapter;
        private List<employeeDocument> fileName = new List<employeeDocument>();
        public static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();

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
            return rootView;
        }

        private  void Save_Button_ClickAsync(object sender, EventArgs e)
        {
            if (fileData != null && (addDocuments_Adapter != null && addDocuments_Adapter?.GetFile()!=null))
            {
                save_Button.Enabled = false;
                _userDialog.ShowLoading("Loading");
                ViewModel.filepath = addDocuments_Adapter.GetFile().filePath;
                ViewModel.filename = addDocuments_Adapter.GetFile().fileName;
                ViewModel.filetype = addDocuments_Adapter.GetFile().fileType;
                Task t = Task.Run(async () => await ViewModel.SaveDocuments());
                t.ContinueWith((t1) =>
                {
                    AppCompatActivity activity = (AppCompatActivity)this.Context;
                    MyProfileInfoNeeds.selectedTab = 2;
                    activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, MyProfFragment).Commit();
                });
               
               
            }
            else
            {
                save_Button.Enabled = true;
                AppCompatActivity activity = (AppCompatActivity)this.Context;
                MyProfileInfoNeeds.selectedTab = 2;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, MyProfFragment).Commit();
            }
        }

        private void Back_Button_Click(object sender, EventArgs e)
        {
            save_Button.Enabled = true;
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            MyProfileInfoNeeds.selectedTab = 2;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, MyProfFragment).Commit();
        }

        private async void Browse_Button_Click(object sender, EventArgs e)
        {
            try
            {
                fileData = await CrossFilePicker.Current.PickFile();
                if (fileData != null)
                {
                    this.ViewModel.filedata = Convert.ToBase64String(fileData.DataArray);
                    //this.ViewModel.filepath = fileData.FilePath;
                    //this.ViewModel.filename = fileData.FileName;
                    //var fileType = fileData.FileName.Split(".");
                    //this.ViewModel.filetype = fileType[1];
                    var fileType = fileData.FileName.Split(".");
                    var employeeDocuments = new employeeDocument();
                    employeeDocuments.fileName = fileData.FileName;
                    employeeDocuments.filePath = fileData.FilePath;
                    employeeDocuments.base64 = Convert.ToBase64String(fileData.DataArray);
                    employeeDocuments.fileType = fileType[1];
                    fileName.Add(employeeDocuments);
                    // await this.ViewModel.SaveDocuments();
                    //AppCompatActivity activity = (AppCompatActivity)this.Context;
                    //activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, MyProfFragment).Commit();
                    if (fileName!= null && fileName.Count > 0)
                    {
                        addDocuments_Adapter = new AddDocumentsAdapter(Context, fileName);
                        var LayoutManager = new LinearLayoutManager(Context);
                        addDoc_RecyclerView.SetLayoutManager(LayoutManager);
                        addDoc_RecyclerView.SetAdapter(addDocuments_Adapter);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}