﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.Schedule;
using StriveEmployee.Android.Adapter.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveEmployee.Android.Fragments.Schedule
{
    public class ScheduleCheckListFragment : MvxFragment<ScheduleCheckListViewModel>
    {

        private RecyclerView checkList_RecyclerView;
        private ScheduleCheckListAdapter checkListAdapter;
        private ArrayAdapter<string> rolesAdapter;
        private Spinner rolesSpinner;
        public Button finishButton;
        private List<string> roles;
        Context context;
        private static int selectedPosition;
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.Schedule_ChecklistFragment, null);
            rolesSpinner = rootView.FindViewById<Spinner>(Resource.Id.role_Spinner);
            checkList_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.checkList_RecyclerView);
            finishButton = rootView.FindViewById<Button>(Resource.Id.finishButton);
            SetRoleSpinner();
            finishButton.Click += FinishButton_Click;
            rolesSpinner.ItemSelected += RoleSpinner_ItemSelected;
            context = this.Context;
            this.ViewModel = new ScheduleCheckListViewModel();
            //ScheduleCheckListViewModel.SelectedChecklist.Clear();
            


            return rootView;
        }

        private void SetRoleSpinner()
        {
            if (EmployeeTempData.EmployeeRoles.Count != 0)
            {
                roles = new List<string>();
                var roleId = EmployeeTempData.EmployeeRoles[0].Roleid;
                selectedPosition = EmployeeTempData.EmployeeRoles.FindIndex(x => x.Roleid == roleId);
                foreach (var RolesData in EmployeeTempData.EmployeeRoles)
                {
                    roles.Add(RolesData.RoleName);
                }
                rolesAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, roles);
                rolesSpinner.Adapter = rolesAdapter;
                rolesSpinner.SetSelection(selectedPosition);

                //if (selectedPosition != -1)
                //{
                //    rolesSpinner.SetSelection(selectedPosition);
                //}
                //else
                //{
                //    rolesSpinner.SetSelection(0);
                //}


            }
        }

        private void RoleSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ViewModel.Roleid = EmployeeTempData.EmployeeRoles[e.Position].Roleid;
            ViewModel.RoleName = EmployeeTempData.EmployeeRoles[e.Position].RoleName;
            selectedPosition = ViewModel.Roleid;
            GetCheckListData();
        }

        private async void FinishButton_Click(object sender, EventArgs e)
        {
            await ViewModel.FinishTask();
            GetCheckListData();
        }

        private async void GetCheckListData()
        {
            try
            {
                await ViewModel.GetTaskList();
                if (ViewModel.checklist != null && ViewModel.checklist.ChecklistNotification.Count != 0)
                {
                    checkListAdapter = new ScheduleCheckListAdapter(ViewModel.checklist);
                    var layoutManager = new LinearLayoutManager(context);
                    checkList_RecyclerView.SetLayoutManager(layoutManager);
                    checkList_RecyclerView.SetAdapter(checkListAdapter);

                }
                else
                {
                    checkList_RecyclerView.SetLayoutManager(null);
                    checkList_RecyclerView.SetAdapter(null);

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
    }
}