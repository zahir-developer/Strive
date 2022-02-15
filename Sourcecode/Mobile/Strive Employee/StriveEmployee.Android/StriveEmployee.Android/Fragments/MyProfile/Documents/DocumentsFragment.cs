using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;
using StriveEmployee.Android.Adapter.MyProfile.Documents;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveEmployee.Android.Fragments.MyProfile.Documents
{
    [MvxUnconventionalAttribute]
    public class DocumentsFragment : MvxFragment<DocumentsViewModel>
    {
        private ImageButton addDocument_ImageButton;
        private RecyclerView documents_RecyclerView;
        private DocumentsAdapter documents_Adapter;
        private AddDocumentsFragment addDocuments_Fragment;
        private View rootView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            rootView = this.BindingInflate(Resource.Layout.Documents_Fragment, null);
            this.ViewModel = new DocumentsViewModel();

            documents_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.documents_RecyclerView);
            addDocument_ImageButton = rootView.FindViewById<ImageButton>(Resource.Id.addDocuments_ImageButton);
            addDocument_ImageButton.Click += AddDocument_ImageButton_Click;
            GetDocumentDetails(true);
            return rootView;
        }       

        private void AddDocument_ImageButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity acitivity = (AppCompatActivity)this.Context;
            addDocuments_Fragment = new AddDocumentsFragment();
            acitivity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, addDocuments_Fragment).Commit();
        }

        public async void GetDocumentDetails(bool isInitialCall)
        {
            if (this.ViewModel == null) 
            {
                ViewModel = new DocumentsViewModel();
            }
            try
            {
                ViewModel.isAndroidFlag = isInitialCall;
                await this.ViewModel.GetDocumentInfo();
                if (this.ViewModel.DocumentDetails != null && this.ViewModel.DocumentDetails.Employee.EmployeeDocument != null)
                {
                    documents_Adapter = new DocumentsAdapter(Context, this.ViewModel.DocumentDetails.Employee.EmployeeDocument);
                    var LayoutManager = new LinearLayoutManager(Context);
                    documents_RecyclerView.SetLayoutManager(LayoutManager);
                    documents_RecyclerView.SetAdapter(documents_Adapter);
                }
                else
                {
                    documents_RecyclerView.SetAdapter(null);
                    documents_RecyclerView.SetLayoutManager(null);
                }
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }        
        }
        public void NoData()
        {
            if (this.ViewModel.DocumentDetails != null)
            {
                Snackbar snackbar = Snackbar.Make(rootView, "No relatable data!", Snackbar.LengthShort);
                snackbar.Show();
            }

        }
    }
}