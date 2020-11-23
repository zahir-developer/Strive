﻿using System;
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

namespace StriveEmployee.Android.Fragments.MyProfile.Documents
{
    public class AddDocumentsFragment : MvxFragment<AddDocumentsViewModel>
    {
        private RecyclerView addDoc_RecyclerView;
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

            addDoc_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.editDoc_RecyclerView);
            return rootView;
        }
    }
}