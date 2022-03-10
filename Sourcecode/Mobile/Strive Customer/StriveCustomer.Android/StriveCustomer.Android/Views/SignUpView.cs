using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Adapter;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveCustomer.Android.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "SignUp View")]
    public class SignUpView : MvxAppCompatActivity<SignUpViewModel>
    {
        private Button signUpButton;
        private TextView signUpTextView;
        private EditText signUpMobileNumber;
        private EditText signUpEmailId;
        private EditText signUpFirstName;
        private EditText signUpLastName;
        private EditText signUpPassword;
        private EditText signUpConfirmPassword;
        private Spinner signUpMakeSpinner;
        private Spinner signUpModelSpinner;
        private Spinner signUpColorSpinner;
        private List<string> makeList, colorList, modelList;        
        private VehicleAdapter<string> makeAdapter, modelAdapter,colorAdapter;
        private Boolean BackspacingFlag = false;
        private Boolean EditedFlag = false;
        private int CursorComplement;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SignUpScreen);
            signUpTextView = FindViewById<TextView>(Resource.Id.signUpTextView);
            signUpButton = FindViewById<Button>(Resource.Id.signUpButton);
            signUpMobileNumber = FindViewById<EditText>(Resource.Id.signUpMobile);
            signUpEmailId = FindViewById<EditText>(Resource.Id.signUpEmail);
            signUpFirstName = FindViewById<EditText>(Resource.Id.signUpFirstName);
            signUpLastName = FindViewById<EditText>(Resource.Id.signUpLastName);
            signUpPassword = FindViewById<EditText>(Resource.Id.signUpPassword);
            signUpConfirmPassword = FindViewById<EditText>(Resource.Id.signUpConfirmPassword);
            signUpMakeSpinner = FindViewById<Spinner>(Resource.Id.signUpMakeOptions);
            signUpModelSpinner = FindViewById<Spinner>(Resource.Id.signUpModelOptions);
            signUpColorSpinner = FindViewById<Spinner>(Resource.Id.signUpColorOptions);
            makeList = new List<string>();
            colorList = new List<string>();
            modelList = new List<string>();
            signUpMobileNumber.BeforeTextChanged += SignUpMobileNumber_BeforeTextChanged;
            signUpMobileNumber.AfterTextChanged += SignUpMobileNumber_AfterTextChanged;
            signUpMakeSpinner.ItemSelected += SignUpMakeSpinner_ItemSelected;
            signUpModelSpinner.ItemSelected += SignUpModelSpinner_ItemSelected;
            signUpColorSpinner.ItemSelected += SignUpColorSpinner_ItemSelected;
            var bindingset = this.CreateBindingSet<SignUpView,SignUpViewModel>();

            bindingset.Bind(signUpTextView).To(svm => svm.SignUp);
            //bindingset.Bind(signUpButton).For(svm => svm.Text).To(svm => svm.SignUp);
            bindingset.Bind(signUpMobileNumber).To(svm => svm.PhoneNumber);
            bindingset.Bind(signUpEmailId).To(svm => svm.EmailAddress);
            bindingset.Bind(signUpFirstName).To(svm => svm.FirstName);
            bindingset.Bind(signUpLastName).To(svm => svm.LastName);
            bindingset.Bind(signUpPassword).To(svm => svm.Password);
            bindingset.Bind(signUpConfirmPassword).To(svm => svm.ConfirmPassword);            
            bindingset.Bind(signUpButton).To(svm => svm.Commands["SignUp"]);
            bindingset.Apply();
            LoadSpinner();
        }

        private void SignUpMobileNumber_AfterTextChanged(object sender, global::Android.Text.AfterTextChangedEventArgs e)
        {
            String strRegex = e.Editable.ToString();
            String phone = new Regex(@"\D").Replace(strRegex, string.Empty);
            if (!EditedFlag)
            {                
                if (phone.Length >= 6 && !BackspacingFlag)
                {
                    EditedFlag = true;
                    String ans = "(" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + "-" + phone.Substring(6);
                    signUpMobileNumber.Text = ans;
                    signUpMobileNumber.SetSelection(signUpMobileNumber.Text.Length - CursorComplement);
                }
                else if (phone.Length >= 3 && !BackspacingFlag)
                {
                    EditedFlag = true;
                    String ans = "(" + phone.Substring(0, 3) + ") " + phone.Substring(3);
                    signUpMobileNumber.Text = ans;
                    signUpMobileNumber.SetSelection(signUpMobileNumber.Text.Length - CursorComplement);
                }
            }
            else
            {
                EditedFlag = false;
            }
        }

        private void SignUpMobileNumber_BeforeTextChanged(object sender, global::Android.Text.TextChangedEventArgs e)
        {
            
            CursorComplement = e.Text.ToString().Length - signUpMobileNumber.SelectionStart;            
            if (e.BeforeCount > e.AfterCount)
            {
                BackspacingFlag = true;
            }
            else
            {
                BackspacingFlag = false;
            }
        }

        private async void LoadSpinner()
        {
            try
            {
               // await ViewModel.getVehicleDetails();
                await ViewModel.GetMakeList();
                var preselectedManufacturer = 0;
                foreach (var makeName in ViewModel.makeList.Make)
                {
                    makeList.Add(makeName.MakeValue);
                    if (MembershipDetails.vehicleMakeNumber == makeName.MakeId)
                    {
                        MembershipDetails.selectedMake = preselectedManufacturer;

                    }
                    preselectedManufacturer++;
                }
                var preselectedColor = 0;
                foreach (var colorName in ViewModel.ColorList)
                {
                    if (colorName.Value.Contains("Unknown"))
                    {
                        colorList.Add("Color");
                    }
                    else
                    {
                        colorList.Add(colorName.Value);
                    }
                    if (MembershipDetails.colorNumber == colorName.Key)
                    {
                        MembershipDetails.selectedColor = preselectedColor;

                    }
                    preselectedColor++;
                }
                makeList.Insert(0, "Vehicle Make*");
                colorList.Insert(0,"Color*");
                makeAdapter = new VehicleAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, makeList);
                makeAdapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                colorAdapter = new VehicleAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, colorList);
                colorAdapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);                
                signUpMakeSpinner.Adapter = makeAdapter;
                signUpColorSpinner.Adapter = colorAdapter;                
                signUpMakeSpinner.SetSelection(MembershipDetails.selectedMake);                
                signUpColorSpinner.SetSelection(MembershipDetails.selectedColor);
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }           

        }
        private async void GetModelList()
        {
            try
            {
                await ViewModel.GetModelList(MembershipDetails.vehicleMakeName);
                if (ViewModel.modelList != null)
                {
                    modelList = new List<string>();
                    var preselectedModel = 0;
                    foreach (var modelName in ViewModel.modelList.Model)
                    {
                        modelList.Add(modelName.ModelValue);
                        if (MembershipDetails.modelNumber == modelName.ModelId)
                        {
                            MembershipDetails.selectedModel = preselectedModel;

                        }
                        preselectedModel++;

                    }

                }

                modelList.Insert(0, "Model*");
                modelAdapter = new VehicleAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, modelList);
                modelAdapter.SetDropDownViewResource(Android.Resource.Layout.support_simple_spinner_dropdown_item);
                signUpModelSpinner.Adapter = modelAdapter;
                signUpModelSpinner.SetSelection(MembershipDetails.selectedModel);
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }


        }

        private void SignUpColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position > 0)
            {
                MembershipDetails.selectedColor = e.Position;
                var selected = this.ViewModel.ColorList.ElementAt(e.Position);
                MembershipDetails.colorNumber = selected.Key;
                MembershipDetails.colorName = selected.Value;
                ViewModel.VehicleColor = selected.Value;
            }
            else 
            {
                MembershipDetails.selectedColor = e.Position;                
            }
            

        }

        private void SignUpModelSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position > 0)
            {
                MembershipDetails.selectedModel = e.Position - 1;
                var selected = this.ViewModel.modelList.Model[e.Position - 1];//.ElementAt(e.Position);
                MembershipDetails.modelNumber = selected.ModelId;
                MembershipDetails.modelName = selected.ModelValue;
                ViewModel.VehicleModel = selected.ModelValue;
            }
            else
            {
                MembershipDetails.selectedModel = e.Position;

            }
        }

        private void SignUpMakeSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position > 0)
            {
                MembershipDetails.selectedMake = e.Position - 1;
                var selected = this.ViewModel.makeList.Make.ElementAt(e.Position - 1);
                ViewModel.VehicleMake = selected.MakeValue;
                MembershipDetails.vehicleMakeNumber = selected.MakeId;
                MembershipDetails.vehicleMfr = selected.MakeId;
                MembershipDetails.vehicleMakeName = selected.MakeValue;
                MembershipDetails.clientVehicleID = 0;
                MembershipDetails.selectedModel = 0;


            }
            else
            {
                MembershipDetails.selectedMake = e.Position;
                var selected = this.ViewModel.makeList.Make.ElementAt(e.Position);
                MembershipDetails.vehicleMakeNumber = selected.MakeId;
                MembershipDetails.vehicleMakeName = selected.MakeValue;

            }
            GetModelList();

        }
    }
}