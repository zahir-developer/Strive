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
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;

namespace StriveEmployee.Android.Fragments.MyProfile.Documents
{
    public class AddDocumentsFragment : MvxFragment<AddDocumentsViewModel>
    {
        private Button browse_Button;
        private RecyclerView addDoc_RecyclerView;
        private FileData fileData;


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
            browse_Button.Click += Browse_Button_Click;
            addDoc_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.editDoc_RecyclerView);
            return rootView;
        }

        private async void Browse_Button_Click(object sender, EventArgs e)
        {
            try
            {
                fileData = await CrossFilePicker.Current.PickFile();
                if(fileData == null)
                {
                    return;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}