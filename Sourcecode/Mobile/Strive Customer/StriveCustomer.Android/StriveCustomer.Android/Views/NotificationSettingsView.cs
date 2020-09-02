using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Strive.Core.Models.Customer;

namespace StriveCustomer.Android.Views
{
    public class NotificationSettingsView : Dialog
    {
        private ISharedPreferences sharedPreferences;
        private ISharedPreferencesEditor preferenceEditor;
        private RadioGroup milesGroup;
        private Button okButton;
        public NotificationSettingsView(Context context) : base(context)
        {

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature((int)WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.NotificationSettingsDialog);
            sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this.Context);
            preferenceEditor = sharedPreferences.Edit();
            okButton = FindViewById<Button>(Resource.Id.okButton);
            milesGroup = FindViewById<RadioGroup>(Resource.Id.milesNotifyGroup);
            milesGroup.CheckedChange += MilesGroup_CheckedChange;
            okButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            preferenceEditor.PutString("milesoption",CustomerInfo.selectedMilesOption);
            preferenceEditor.Apply();
        }

        private void MilesGroup_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            switch(e.CheckedId)
            {
                case Resource.Id.optionA:
                    CustomerInfo.selectedMilesOption = "A";
                    break;

                case Resource.Id.optionB:
                    CustomerInfo.selectedMilesOption = "B";
                    break;

                case Resource.Id.optionC:
                    CustomerInfo.selectedMilesOption = "C";
                    break;
            }   
        }

    }
}