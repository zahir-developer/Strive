using System;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.Schedule;
using UIKit;
namespace StriveEmployee.iOS.Views.Schedule
{
    public class RolesPicker : MvxPickerViewModel
    {
        ScheduleViewModel ViewModel;
        public RolesPicker(ScheduleViewModel viewModel, UIPickerView pickerView) : base(pickerView)
        {
            ViewModel = viewModel;
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {
            ViewModel.Roleid = EmployeeTempData.EmployeeRoles[(int)row].Roleid;
            ViewModel.RoleName = EmployeeTempData.EmployeeRoles[(int)row].RoleName;
        }

        public override nint GetRowsInComponent(UIPickerView picker, nint component)
        {
            return EmployeeTempData.EmployeeRoles.Count;
        }

        public override string GetTitle(UIPickerView picker, nint row, nint component)
        {
            return EmployeeTempData.EmployeeRoles[(int)row].RoleName;
        }
    }
}
