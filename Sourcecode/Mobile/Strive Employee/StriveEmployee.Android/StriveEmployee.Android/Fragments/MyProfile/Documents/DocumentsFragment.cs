using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;
using StriveEmployee.Android.Adapter.MyProfile.Documents;

namespace StriveEmployee.Android.Fragments.MyProfile.Documents
{
    public class DocumentsFragment : MvxFragment<DocumentsViewModel>
    {
        private RecyclerView documents_RecyclerView;
        private DocumentsAdapter documents_Adapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.Documents_Fragment, null);
            this.ViewModel = new DocumentsViewModel();

            documents_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.documents_RecyclerView);
            GetDocumentDetails();
            return rootView;
        }

        private async void GetDocumentDetails()
        {
           await this.ViewModel.GetDocumentInfo();
            if(this.ViewModel.DocumentDetails != null)
            {
                documents_Adapter = new DocumentsAdapter(Context, this.ViewModel.DocumentDetails.Employee.EmployeeDocument);
                var LayoutManager = new LinearLayoutManager(Context);
                documents_RecyclerView.SetLayoutManager(LayoutManager);
                documents_RecyclerView.SetAdapter(documents_Adapter);
            }
            
        }
    }
}