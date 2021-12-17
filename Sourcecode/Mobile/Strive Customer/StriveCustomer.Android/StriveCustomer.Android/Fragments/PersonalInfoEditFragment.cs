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
using String = System.String;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class PersonalInfoEditFragment : MvxFragment<PersonalInfoEditViewModel>, ITextWatcher
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

        //we need to know if the user is erasing or inputing some new character
        private Boolean backspacingFlag = false;
        //we need to block the :afterTextChanges method to be called again after we just replaced the EditText text
        private Boolean editedFlag = false;
        //we need to mark the cursor position and restore it after the edition
        private int cursorComplement;

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

            contactEditText.AddTextChangedListener(this);

            saveButton.Click += SaveButton_Click;
            backButton.Click += BackButton_Click;


            return rootview;
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
        }
        public void AfterTextChanged(IEditable s)
        {
            String strRegex = s.ToString();
            String phone = new Regex(@"\D").Replace(strRegex, string.Empty);
            if (!editedFlag)
            {
                //if (phone.Length == 6 && !backspacingFlag)
                if (phone.Length >= 6 && !backspacingFlag)
                {
                    editedFlag = true;
                    String ans = "(" + phone.Substring(0, 3) + ") " + phone.Substring(3, 3) + "-" + phone.Substring(6);
                    contactEditText.Text = ans;
                    contactEditText.SetSelection(contactEditText.Text.Length - cursorComplement);
                }
                else if (phone.Length >= 3 && !backspacingFlag)
                {
                    editedFlag = true;
                    String ans = "(" + phone.Substring(0, 3) + ") " + phone.Substring(3);
                    contactEditText.Text = ans;
                    contactEditText.SetSelection(contactEditText.Text.Length - cursorComplement);
                }
            }
            else
            {
                editedFlag = false;
            }
        }
        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            //we store the cursor local relative to the end of the string in the EditText before the edition
            cursorComplement = s.Length() - contactEditText.SelectionStart;
            //we check if the user is inputing or erasing a character
            if (count > after)
            {
                backspacingFlag = true;
            }
            else
            {
                backspacingFlag = false;
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
            var result = await ViewModel.saveClientInfoCommand();
            if (result)
            {
                AppCompatActivity activity = (AppCompatActivity)Context;
                MyProfileInfoNeeds.selectedTab = 0;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, myProfileInfoFragment).Commit();
            }

        }


    }


}
