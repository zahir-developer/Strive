using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Plugin.FilePicker.Abstractions;
using Strive.Core.Models.Employee.Documents;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;
using StriveEmployee.Android.Adapter.MyProfile.Documents;
using Xamarin.Essentials;
using OperationCanceledException = System.OperationCanceledException;

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
        private ObservableCollection<employeeDocument> fileName = new ObservableCollection<employeeDocument>();
        public static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init((Activity)this.Context, savedInstanceState);
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
            if (addDocuments_Adapter != null && addDocuments_Adapter?.GetFile()!=null && addDocuments_Adapter?.GetFile().Count >0)
            {
                save_Button.Enabled = false;
                _userDialog.ShowLoading("Loading");
                foreach (var data in addDocuments_Adapter?.GetFile())
                {
                    ViewModel.employeeDocumentList.Add(data);
                }
                try
                {
                    Task t = Task.Run(async () => await ViewModel.SaveDocuments());
                    t.ContinueWith((t1) =>
                    {
                        AppCompatActivity activity = (AppCompatActivity)this.Context;
                        MyProfileInfoNeeds.selectedTab = 2;
                        activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, MyProfFragment).Commit();
                    });
                }
                catch (Exception ex)
                {
                    if (ex is OperationCanceledException)
                    {
                        return;
                    }
                }
                

                AddDocumentsAdapter.fileList.Clear();
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

        private void Browse_Button_Click(object sender, EventArgs e)
        {
            if (ContextCompat.CheckSelfPermission(Context, Manifest.Permission.ReadExternalStorage) == Permission.Granted)
            {
                OpenFileAsync();
            }
            else
            {
                requestpermission();
            }

        }
        private async void OpenFileAsync()
        {
            try
            {
                var result = await FilePicker.PickMultipleAsync();
                if (result != null)
                {
                    addDocuments_Adapter = new AddDocumentsAdapter(Context, result);
                    var LayoutManager = new LinearLayoutManager(Context);
                    addDoc_RecyclerView.SetLayoutManager(LayoutManager);
                    addDoc_RecyclerView.SetAdapter(addDocuments_Adapter);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void requestpermission()
        {
            if (ContextCompat.CheckSelfPermission(this.Activity, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            {
              RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage }, 1);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == 1)
            {
                OpenFileAsync();
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            }
        }
    }
}