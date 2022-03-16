using System;
using System.Linq;
using System.Text.RegularExpressions;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Telephony;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using Boolean = System.Boolean;
using Exception = System.Exception;
using String = System.String;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class PersonalInfoEditFragment : MvxFragment<PersonalInfoEditViewModel>
    {
        private EditText fullNameEditText;
        private EditText contactEditText;
        private EditText addressEditText;
        private EditText zipCodeEditText;
        private EditText secondaryContactEditText;
        private EditText emailEditText;
        private Button saveButton;
        private Button backButton;
        private MyProfileInfoFragment myProfileInfoFragment;
        private string deletedNumber;
        private Boolean backspacingFlag = false;
        private Boolean contactEditedFlag = false;
        private int cursorComplement;
        private Boolean secBackspacingFlag = false;
        private Boolean secEditedFlag = false;
        private int secCursorComplement;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.PersonalInfoEditFragment, null);
            this.ViewModel = new PersonalInfoEditViewModel();
            myProfileInfoFragment = new MyProfileInfoFragment();
            backButton = rootview.FindViewById<Button>(Resource.Id.backPersonal);
            fullNameEditText = rootview.FindViewById<EditText>(Resource.Id.fullName);
            contactEditText = rootview.FindViewById<EditText>(Resource.Id.contactNumber);
            addressEditText = rootview.FindViewById<EditText>(Resource.Id.Address);
            zipCodeEditText = rootview.FindViewById<EditText>(Resource.Id.zipCodes);
            secondaryContactEditText = rootview.FindViewById<EditText>(Resource.Id.secondaryPhone);
            emailEditText = rootview.FindViewById<EditText>(Resource.Id.emails);
            saveButton = rootview.FindViewById<Button>(Resource.Id.personalInfoSave);


            fullNameEditText.Text = MyProfileCustomerInfo.FullName;
            contactEditText.Text = MyProfileCustomerInfo.ContactNumber;
            addressEditText.Text = MyProfileCustomerInfo.Address;
            zipCodeEditText.Text = MyProfileCustomerInfo.ZipCode;
            secondaryContactEditText.Text = MyProfileCustomerInfo.SecondaryContactNumber;
            emailEditText.Text = MyProfileCustomerInfo.Email;

            contactEditText.AfterTextChanged += contactEditText_AfterTextChanged;
            contactEditText.BeforeTextChanged += contactEditText_BeforeTextChanged;
            secondaryContactEditText.AfterTextChanged += secondaryContactEditText_AfterTextChanged;
            secondaryContactEditText.BeforeTextChanged += secondaryContactEditText_BeforeTextChanged;

            saveButton.Click += SaveButton_Click;
            backButton.Click += BackButton_Click;


            return rootview;
        }

        private void secondaryContactEditText_BeforeTextChanged(object sender, TextChangedEventArgs e)
        {
            //we store the cursor local relative to the end of the string in the EditText before the edition
            secCursorComplement = e.Text.ToString().Length - secondaryContactEditText.SelectionStart;
            //we check if the user ir inputing or erasing a character
            if (e.BeforeCount > e.AfterCount)
            {
                secBackspacingFlag = true;
            }
            else
            {
                secBackspacingFlag = false;
            }
        }

        private void secondaryContactEditText_AfterTextChanged(object sender, AfterTextChangedEventArgs e)
        {
            String strRegex = e.Editable.ToString();
            String phone = new Regex(@"\D").Replace(strRegex, string.Empty);
            if (!secEditedFlag)
            {
                //if (phone.Length == 6 && !backspacingFlag)
                if (phone.Length >= 6 && !secBackspacingFlag)
                {
                    secEditedFlag = true;
                    String ans = "(" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + "-" + phone.Substring(6);
                    secondaryContactEditText.Text = ans;
                    secondaryContactEditText.SetSelection(secondaryContactEditText.Text.Length - secCursorComplement);
                }
                else if (phone.Length >= 3 && !secBackspacingFlag)
                {
                    secEditedFlag = true;
                    String ans = "(" + phone.Substring(0, 3) + ") " + phone.Substring(3);
                    secondaryContactEditText.Text = ans;
                    secondaryContactEditText.SetSelection(secondaryContactEditText.Text.Length - secCursorComplement);
                }
            }
            else
            {
                secEditedFlag = false;
            }
        }

        private void contactEditText_BeforeTextChanged(object sender, TextChangedEventArgs e)
        {
            //we store the cursor local relative to the end of the string in the EditText before the edition
            cursorComplement = e.Text.ToString().Length - contactEditText.SelectionStart;
            //we check if the user ir inputing or erasing a character
            if (e.BeforeCount > e.AfterCount)
            {
                backspacingFlag = true;
            }
            else
            {
                backspacingFlag = false;
            }
        }

        private void contactEditText_AfterTextChanged(object sender, AfterTextChangedEventArgs e)
        {
            String strRegex = e.Editable.ToString();
            String phone = new Regex(@"\D").Replace(strRegex, string.Empty);
            if (!contactEditedFlag)
            {
                //if (phone.Length == 6 && !backspacingFlag)
                if (phone.Length >= 6 && !backspacingFlag)
                {
                    contactEditedFlag = true;
                    String ans = "(" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + "-" + phone.Substring(6);
                    contactEditText.Text = ans;
                    contactEditText.SetSelection(contactEditText.Text.Length - cursorComplement);
                }
                else if (phone.Length >= 3 && !backspacingFlag)
                {
                    contactEditedFlag = true;
                    String ans = "(" + phone.Substring(0, 3) + ") " + phone.Substring(3);
                    contactEditText.Text = ans;
                    contactEditText.SetSelection(contactEditText.Text.Length - cursorComplement);
                }
            }
            else
            {
                contactEditedFlag = false;
            }
        }


        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            MyProfileInfoNeeds.selectedTab = 0;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, myProfileInfoFragment).Commit();
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            ViewModel.FullName = fullNameEditText.Text;
            ViewModel.ContactNumber = contactEditText.Text;
            ViewModel.Address = addressEditText.Text;
            ViewModel.ZipCode = zipCodeEditText.Text;
            ViewModel.SecondaryContactNumber = secondaryContactEditText.Text;
            ViewModel.Email = emailEditText.Text;
            try
            {
                var result = await ViewModel.saveClientInfoCommand();
                if (result)
                {
                    AppCompatActivity activity = (AppCompatActivity)Context;
                    MyProfileInfoNeeds.selectedTab = 0;
                    activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, myProfileInfoFragment).Commit();
                }
            }
           catch (Exception ex)
            {
                if (ex is System.OperationCanceledException)
                {
                    return;
                }
            }

        }


        
    }


}
