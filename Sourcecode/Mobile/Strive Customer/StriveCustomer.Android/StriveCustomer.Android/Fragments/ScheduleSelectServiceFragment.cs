using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer.Schedule;

namespace StriveCustomer.Android.Fragments
{
    public class ScheduleSelectServiceFragment : MvxFragment<ScheduleServicesViewModel>
    {
        private Button ScheduleServices_BackButton;
        private Button ScheduleServices_FrontButton;
        private ScheduleFragment scheduleFragment;
        private ScheduleLocationsFragment locationsFragment;
        private View[] layout;
        private CheckBox[] checkBoxes;
        private TextView[] serviceViewToggle;
        private TextView[] serviceDetailPassage; 
        private LinearLayout ScheduleServices_LinearLayout;
        private int oldSelection = -1;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.ScheduleServicesFragment, null);
            this.ViewModel = new ScheduleServicesViewModel();

            ScheduleServices_BackButton = rootView.FindViewById<Button>(Resource.Id.scheduleServices_BackButton);
            ScheduleServices_FrontButton = rootView.FindViewById<Button>(Resource.Id.scheduleServices_NextButton);
            ScheduleServices_LinearLayout = rootView.FindViewById<LinearLayout>(Resource.Id.ScheduleServices_LinearLayout);

            ScheduleServices_BackButton.Click += ScheduleServices_BackButton_Click;
            ScheduleServices_FrontButton.Click += ScheduleServices_NextButton_Click;

            GetServicesDates();

            return rootView;
        }

        private void ScheduleServices_NextButton_Click(object sender, EventArgs e)
        {
            locationsFragment = new ScheduleLocationsFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, locationsFragment).Commit();
        }

        private void ScheduleServices_BackButton_Click(object sender, EventArgs e)
        {
            scheduleFragment = new ScheduleFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, scheduleFragment).Commit();
        }

        private async void GetServicesDates()
        {
            await this.ViewModel.GetScheduledServices();

            if(this.ViewModel.scheduleServices != null && this.ViewModel.scheduleServices.ServicesWithPrice.Count > 0)
            {
                checkBoxes = new CheckBox[this.ViewModel.scheduleServices.ServicesWithPrice.Count];
                serviceViewToggle = new TextView[this.ViewModel.scheduleServices.ServicesWithPrice.Count];
                layout = new View[this.ViewModel.scheduleServices.ServicesWithPrice.Count];
                serviceDetailPassage = new TextView[this.ViewModel.scheduleServices.ServicesWithPrice.Count];
                for (int service = 0; service < this.ViewModel.scheduleServices.ServicesWithPrice.Count; service++ )
                {
                    layout[service] = LayoutInflater.From(Context).Inflate(Resource.Layout.ScheduleSelectService_ItemView, ScheduleServices_LinearLayout, false);
                    var serviceName = layout[service].FindViewById<TextView>(Resource.Id.scheduleServiceName_TextView);
                    var serviceCost = layout[service].FindViewById<TextView>(Resource.Id.scheduleServiceCost_TextView);
                    checkBoxes[service] = layout[service].FindViewById<CheckBox>(Resource.Id.selectedScheduleService_CheckBox);
                    serviceDetailPassage[service] = layout[service].FindViewById<TextView>(Resource.Id.detailedServiceInfo);
                    serviceDetailPassage[service].Visibility = ViewStates.Gone;

                    serviceViewToggle[service] = layout[service].FindViewById<TextView>(Resource.Id.infoShowChange_TextView);
                    serviceViewToggle[service].Text = "View More";
                    serviceViewToggle[service].PaintFlags = PaintFlags.UnderlineText;
                    serviceName.Text = this.ViewModel.scheduleServices.ServicesWithPrice[service].ServiceName;
                    serviceCost.Text = "$"+this.ViewModel.scheduleServices.ServicesWithPrice[service].Price.ToString();

                    assignListeners(service);


                    ScheduleServices_LinearLayout.AddView(layout[service]);

                }
                
            }
            
        }
        public void assignListeners(int position)
        {
           switch(position)
            {
                case 0:
                    serviceViewToggle[0].Click += ServiceViewToggle_Click0;
                    checkBoxes[0].CheckedChange += ScheduleSelectServiceFragment_CheckedChange0;
                    break;
                case 1:
                    serviceViewToggle[1].Click += ServiceViewToggle_Click1;
                    checkBoxes[1].CheckedChange += ScheduleSelectServiceFragment_CheckedChange1;
                    break;
                case 2:
                    serviceViewToggle[2].Click += ServiceViewToggle_Click2;
                    checkBoxes[2].CheckedChange += ScheduleSelectServiceFragment_CheckedChange2;
                    break;
                case 3:
                    serviceViewToggle[3].Click += ServiceViewToggle_Click3;
                    checkBoxes[3].CheckedChange += ScheduleSelectServiceFragment_CheckedChange3;
                    break;
                case 4:
                    serviceViewToggle[4].Click += ServiceViewToggle_Click4;
                    checkBoxes[4].CheckedChange += ScheduleSelectServiceFragment_CheckedChange4;
                    break;
                case 5:
                    serviceViewToggle[5].Click += ServiceViewToggle_Click5;
                    checkBoxes[5].CheckedChange += ScheduleSelectServiceFragment_CheckedChange5;
                    break;
                case 6:
                    serviceViewToggle[6].Click += ServiceViewToggle_Click6;
                    checkBoxes[6].CheckedChange += ScheduleSelectServiceFragment_CheckedChange6;
                    break;
                case 7:
                    serviceViewToggle[7].Click += ServiceViewToggle_Click7;
                    checkBoxes[7].CheckedChange += ScheduleSelectServiceFragment_CheckedChange7;
                    break;
                case 8:
                    serviceViewToggle[8].Click += ServiceViewToggle_Click8;
                    checkBoxes[8].CheckedChange += ScheduleSelectServiceFragment_CheckedChange8;
                    break;
                case 9:
                    serviceViewToggle[9].Click += ServiceViewToggle_Click9;
                    checkBoxes[9].CheckedChange += ScheduleSelectServiceFragment_CheckedChange9;
                    break;
                case 10:
                    serviceViewToggle[10].Click += ServiceViewToggle_Click10;
                    checkBoxes[10].CheckedChange += ScheduleSelectServiceFragment_CheckedChange10;
                    break;
                case 11:
                    serviceViewToggle[11].Click += ServiceViewToggle_Click11;
                    checkBoxes[11].CheckedChange += ScheduleSelectServiceFragment_CheckedChange11;
                    break;
                case 12:
                    serviceViewToggle[12].Click += ServiceViewToggle_Click12;
                    checkBoxes[12].CheckedChange += ScheduleSelectServiceFragment_CheckedChange12;
                    break;
                case 13:
                    serviceViewToggle[13].Click += ServiceViewToggle_Click13;
                    checkBoxes[13].CheckedChange += ScheduleSelectServiceFragment_CheckedChange13;
                    break;
                case 14:
                    serviceViewToggle[14].Click += ServiceViewToggle_Click14;
                    checkBoxes[14].CheckedChange += ScheduleSelectServiceFragment_CheckedChange14;
                    break;
                case 15:
                    serviceViewToggle[15].Click += ServiceViewToggle_Click15;
                    checkBoxes[15].CheckedChange += ScheduleSelectServiceFragment_CheckedChange15;
                    break;
                case 16:
                    serviceViewToggle[16].Click += ServiceViewToggle_Click16;
                    checkBoxes[16].CheckedChange += ScheduleSelectServiceFragment_CheckedChange16;
                    break;
                case 17:
                    serviceViewToggle[17].Click += ServiceViewToggle_Click17;
                    checkBoxes[17].CheckedChange += ScheduleSelectServiceFragment_CheckedChange17;
                    break;
                case 18:
                    serviceViewToggle[18].Click += ServiceViewToggle_Click18;
                    checkBoxes[18].CheckedChange += ScheduleSelectServiceFragment_CheckedChange18;
                    break;
                case 19:
                    serviceViewToggle[19].Click += ServiceViewToggle_Click19;
                    checkBoxes[19].CheckedChange += ScheduleSelectServiceFragment_CheckedChange19;
                    break;
                case 20:
                    serviceViewToggle[20].Click += ServiceViewToggle_Click20;
                    checkBoxes[20].CheckedChange += ScheduleSelectServiceFragment_CheckedChange20;
                    break;
                case 21:
                    serviceViewToggle[21].Click += ServiceViewToggle_Click21;
                    checkBoxes[21].CheckedChange += ScheduleSelectServiceFragment_CheckedChange21;
                    break;
                case 22:
                    serviceViewToggle[22].Click += ServiceViewToggle_Click22;
                    checkBoxes[22].CheckedChange += ScheduleSelectServiceFragment_CheckedChange22;
                    break;
                case 23:
                    serviceViewToggle[23].Click += ServiceViewToggle_Click23;
                    checkBoxes[23].CheckedChange += ScheduleSelectServiceFragment_CheckedChange23;
                    break;
                case 24:
                    serviceViewToggle[24].Click += ServiceViewToggle_Click24;
                    checkBoxes[24].CheckedChange += ScheduleSelectServiceFragment_CheckedChange24;
                    break;
                case 25:
                    serviceViewToggle[25].Click += ServiceViewToggle_Click25;
                    checkBoxes[25].CheckedChange += ScheduleSelectServiceFragment_CheckedChange25;
                    break;
                case 26:
                    serviceViewToggle[26].Click += ServiceViewToggle_Click26;
                    checkBoxes[26].CheckedChange += ScheduleSelectServiceFragment_CheckedChange26;
                    break;
                case 27:
                    serviceViewToggle[27].Click += ServiceViewToggle_Click27;
                    checkBoxes[27].CheckedChange += ScheduleSelectServiceFragment_CheckedChange27;
                    break;
                case 28:
                    serviceViewToggle[28].Click += ServiceViewToggle_Click28;
                    checkBoxes[28].CheckedChange += ScheduleSelectServiceFragment_CheckedChange28;
                    break;
                case 29:
                    serviceViewToggle[29].Click += ServiceViewToggle_Click29;
                    checkBoxes[29].CheckedChange += ScheduleSelectServiceFragment_CheckedChange29;
                    break;
                case 30:
                    serviceViewToggle[30].Click += ServiceViewToggle_Click30;
                    checkBoxes[30].CheckedChange += ScheduleSelectServiceFragment_CheckedChange30;
                    break;
                case 31:
                    serviceViewToggle[31].Click += ServiceViewToggle_Click31;
                    checkBoxes[31].CheckedChange += ScheduleSelectServiceFragment_CheckedChange31;
                    break;
                case 32:
                    serviceViewToggle[32].Click += ServiceViewToggle_Click32;
                    checkBoxes[32].CheckedChange += ScheduleSelectServiceFragment_CheckedChange32;
                    break;
                case 33:
                    serviceViewToggle[33].Click += ServiceViewToggle_Click33;
                    checkBoxes[33].CheckedChange += ScheduleSelectServiceFragment_CheckedChange33;
                    break;
                case 34:
                    serviceViewToggle[34].Click += ServiceViewToggle_Click34;
                    checkBoxes[34].CheckedChange += ScheduleSelectServiceFragment_CheckedChange34;
                    break;
                case 35:
                    serviceViewToggle[35].Click += ServiceViewToggle_Click35;
                    checkBoxes[35].CheckedChange += ScheduleSelectServiceFragment_CheckedChange35;
                    break;
                case 36:
                    serviceViewToggle[36].Click += ServiceViewToggle_Click36;
                    checkBoxes[36].CheckedChange += ScheduleSelectServiceFragment_CheckedChange36;
                    break;
                case 37:
                    serviceViewToggle[37].Click += ServiceViewToggle_Click37;
                    checkBoxes[37].CheckedChange += ScheduleSelectServiceFragment_CheckedChange37;
                    break;
            }
        }

        private void ResetOldSelection(int position)
        {
            if(oldSelection != -1)
            {
                checkBoxes[oldSelection].Checked = false;
            }
           
        }

        private void ScheduleSelectServiceFragment_CheckedChange0(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
             if (!e.IsChecked)
                {
                    checkBoxes[0].Checked = false;
                }
             else
                {
                    checkBoxes[0].Checked = true;
                    CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[0].ServiceId;
                    CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[0].ServiceType;
                    CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[0].Price;
            }
            oldSelection = 0;
        }
        private void ServiceViewToggle_Click0(object sender, EventArgs e)
        {
           
            if (serviceDetailPassage[0].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[0].Text = "View Less";
                serviceViewToggle[0].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[0].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[0].Text = "View More";
                serviceViewToggle[0].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[0].Visibility = ViewStates.Gone;
            }

           
        }
        private void ScheduleSelectServiceFragment_CheckedChange1(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[1].Checked = false;
            }
            else
            {
                checkBoxes[1].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[1].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[1].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[1].Price;
            }
            oldSelection = 1;
        }
        private void ServiceViewToggle_Click1(object sender, EventArgs e)
        {
            if (serviceDetailPassage[1].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[1].Text = "View Less";
                serviceViewToggle[1].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[1].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[1].Text = "View More";
                serviceViewToggle[1].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[1].Visibility = ViewStates.Gone;
            }
        }
        private void ScheduleSelectServiceFragment_CheckedChange2(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[2].Checked = false;
            }
            else
            {
                checkBoxes[2].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[2].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[2].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[2].Price;
            }
            oldSelection = 2;
        }
        private void ServiceViewToggle_Click2(object sender, EventArgs e)
        {
            if (serviceDetailPassage[2].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[2].Text = "View Less";
                serviceViewToggle[2].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[2].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[2].Text = "View More";
                serviceViewToggle[2].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[2].Visibility = ViewStates.Gone;
            }
        }
        private void ScheduleSelectServiceFragment_CheckedChange3(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[3].Checked = false;
            }
            else
            {
                checkBoxes[3].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[3].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[3].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[3].Price;
            }
            oldSelection = 3;
        }
        private void ServiceViewToggle_Click3(object sender, EventArgs e)
        {
            if (serviceDetailPassage[3].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[3].Text = "View Less";
                serviceViewToggle[3].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[3].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[3].Text = "View More";
                serviceViewToggle[3].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[3].Visibility = ViewStates.Gone;
            }
        }
        private void ScheduleSelectServiceFragment_CheckedChange4(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[4].Checked = false;
            }
            else
            {
                checkBoxes[4].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[4].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[4].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[4].Price;
            }
            oldSelection = 4;
        }
        private void ServiceViewToggle_Click4(object sender, EventArgs e)
        {
            if (serviceDetailPassage[4].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[4].Text = "View Less";
                serviceViewToggle[4].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[4].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[4].Text = "View More";
                serviceViewToggle[4].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[4].Visibility = ViewStates.Gone;
            }
        }
        private void ScheduleSelectServiceFragment_CheckedChange5(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[5].Checked = false;
            }
            else
            {
                checkBoxes[5].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[5].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[5].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[5].Price;
            }
            oldSelection = 5;
        }
        private void ServiceViewToggle_Click5(object sender, EventArgs e)
        {
            if (serviceDetailPassage[5].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[5].Text = "View Less";
                serviceViewToggle[5].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[5].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[5].Text = "View More";
                serviceViewToggle[5].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[5].Visibility = ViewStates.Gone;
            }
        }
        private void ScheduleSelectServiceFragment_CheckedChange6(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[6].Checked = false;
            }
            else
            {
                checkBoxes[6].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[6].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[6].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[6].Price;
            }
            oldSelection = 6;
        }
        private void ServiceViewToggle_Click6(object sender, EventArgs e)
        {
            if (serviceDetailPassage[6].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[6].Text = "View Less";
                serviceViewToggle[6].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[6].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[6].Text = "View More";
                serviceViewToggle[6].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[6].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange7(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[7].Checked = false;
            }
            else
            {
                checkBoxes[7].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[7].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[7].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[7].Price;
            }
            oldSelection = 7;
        }
        private void ServiceViewToggle_Click7(object sender, EventArgs e)
        {
            if (serviceDetailPassage[7].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[7].Text = "View Less";
                serviceViewToggle[7].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[7].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[7].Text = "View More";
                serviceViewToggle[7].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[7].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange8(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[8].Checked = false;
            }
            else
            {
                checkBoxes[8].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[8].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[8].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[8].Price;
            }
            oldSelection = 8;
        }
        private void ServiceViewToggle_Click8(object sender, EventArgs e)
        {
            if (serviceDetailPassage[8].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[8].Text = "View Less";
                serviceViewToggle[8].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[8].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[8].Text = "View More";
                serviceViewToggle[8].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[8].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange9(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[9].Checked = false;
            }
            else
            {
                checkBoxes[9].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[9].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[9].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[9].Price;
            }
            oldSelection = 9;
        }
        private void ServiceViewToggle_Click9(object sender, EventArgs e)
        {
            if (serviceDetailPassage[9].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[9].Text = "View Less";
                serviceViewToggle[9].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[9].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[9].Text = "View More";
                serviceViewToggle[9].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[9].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange10(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[10].Checked = false;
            }
            else
            {
                checkBoxes[10].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[10].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[10].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[10].Price;
            }
            oldSelection = 10;
        }
        private void ServiceViewToggle_Click10(object sender, EventArgs e)
        {
            if (serviceDetailPassage[10].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[10].Text = "View Less";
                serviceViewToggle[10].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[10].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[10].Text = "View More";
                serviceViewToggle[10].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[10].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange11(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[11].Checked = false;
            }
            else
            {
                checkBoxes[11].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[11].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[11].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[11].Price;
            }
            oldSelection = 11;
        }
        private void ServiceViewToggle_Click11(object sender, EventArgs e)
        {
            if (serviceDetailPassage[11].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[11].Text = "View Less";
                serviceViewToggle[11].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[11].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[11].Text = "View More";
                serviceViewToggle[11].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[11].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange12(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[12].Checked = false;
            }
            else
            {
                checkBoxes[12].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[12].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[12].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[12].Price;
            }
            oldSelection = 12;
        }
        private void ServiceViewToggle_Click12(object sender, EventArgs e)
        {
            if (serviceDetailPassage[12].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[12].Text = "View Less";
                serviceViewToggle[12].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[12].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[12].Text = "View More";
                serviceViewToggle[12].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[12].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange13(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[13].Checked = false;
            }
            else
            {
                checkBoxes[13].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[13].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[13].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[13].Price;
            }
            oldSelection = 13;
        }
        private void ServiceViewToggle_Click13(object sender, EventArgs e)
        {
            if (serviceDetailPassage[13].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[13].Text = "View Less";
                serviceViewToggle[13].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[13].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[13].Text = "View More";
                serviceViewToggle[13].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[13].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange14(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[14].Checked = false;
            }
            else
            {
                checkBoxes[14].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[14].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[14].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[14].Price;
            }
            oldSelection = 14;
        }
        private void ServiceViewToggle_Click14(object sender, EventArgs e)
        {
            if (serviceDetailPassage[14].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[14].Text = "View Less";
                serviceViewToggle[14].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[14].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[14].Text = "View More";
                serviceViewToggle[14].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[14].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange15(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[15].Checked = false;
            }
            else
            {
                checkBoxes[15].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[15].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[15].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[15].Price;
            }
            oldSelection = 15;
        }
        private void ServiceViewToggle_Click15(object sender, EventArgs e)
        {
            if (serviceDetailPassage[15].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[15].Text = "View Less";
                serviceViewToggle[15].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[15].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[15].Text = "View More";
                serviceViewToggle[15].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[15].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange16(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[16].Checked = false;
            }
            else
            {
                checkBoxes[16].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[16].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[16].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[16].Price;
            }
            oldSelection = 16;
        }
        private void ServiceViewToggle_Click16(object sender, EventArgs e)
        {
            if (serviceDetailPassage[16].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[16].Text = "View Less";
                serviceViewToggle[16].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[16].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[16].Text = "View More";
                serviceViewToggle[16].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[16].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange17(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[17].Checked = false;
            }
            else
            {
                checkBoxes[17].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[17].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[17].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[17].Price;
            }
            oldSelection = 17;
        }
        private void ServiceViewToggle_Click17(object sender, EventArgs e)
        {
            if (serviceDetailPassage[17].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[17].Text = "View Less";
                serviceViewToggle[17].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[17].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[17].Text = "View More";
                serviceViewToggle[17].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[17].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange18(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[18].Checked = false;
            }
            else
            {
                checkBoxes[18].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[18].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[18].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[18].Price;
            }
            oldSelection = 18;
        }
        private void ServiceViewToggle_Click18(object sender, EventArgs e)
        {
            if (serviceDetailPassage[18].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[18].Text = "View Less";
                serviceViewToggle[18].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[18].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[18].Text = "View More";
                serviceViewToggle[18].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[18].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange19(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[19].Checked = false;
            }
            else
            {
                checkBoxes[19].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[19].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[19].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[19].Price;
            }
            oldSelection = 19;
        }
        private void ServiceViewToggle_Click19(object sender, EventArgs e)
        {
            if (serviceDetailPassage[19].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[19].Text = "View Less";
                serviceViewToggle[19].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[19].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[19].Text = "View More";
                serviceViewToggle[19].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[19].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange20(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[20].Checked = false;
            }
            else
            {
                checkBoxes[20].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[20].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[20].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[20].Price;
            }
            oldSelection = 20;
        }
        private void ServiceViewToggle_Click20(object sender, EventArgs e)
        {
            if (serviceDetailPassage[20].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[20].Text = "View Less";
                serviceViewToggle[20].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[20].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[20].Text = "View More";
                serviceViewToggle[20].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[20].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange21(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[21].Checked = false;
            }
            else
            {
                checkBoxes[21].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[21].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[21].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[21].Price;
            }
            oldSelection = 21;
        }
        private void ServiceViewToggle_Click21(object sender, EventArgs e)
        {
            if (serviceDetailPassage[21].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[21].Text = "View Less";
                serviceViewToggle[21].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[21].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[21].Text = "View More";
                serviceViewToggle[21].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[21].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange22(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[22].Checked = false;
            }
            else
            {
                checkBoxes[22].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[22].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[22].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[22].Price;
            }
            oldSelection = 22;
        }
        private void ServiceViewToggle_Click22(object sender, EventArgs e)
        {
            if (serviceDetailPassage[22].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[22].Text = "View Less";
                serviceViewToggle[22].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[22].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[22].Text = "View More";
                serviceViewToggle[22].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[22].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange23(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[23].Checked = false;
            }
            else
            {
                checkBoxes[23].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[23].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[23].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[23].Price;
            }
            oldSelection = 23;
        }
        private void ServiceViewToggle_Click23(object sender, EventArgs e)
        {
            if (serviceDetailPassage[23].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[23].Text = "View Less";
                serviceViewToggle[23].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[23].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[23].Text = "View More";
                serviceViewToggle[23].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[23].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange24(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[24].Checked = false;
            }
            else
            {
                checkBoxes[24].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[24].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[24].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[24].Price;
            }
            oldSelection = 24;
        }
        private void ServiceViewToggle_Click24(object sender, EventArgs e)
        {
            if (serviceDetailPassage[24].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[24].Text = "View Less";
                serviceViewToggle[24].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[24].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[24].Text = "View More";
                serviceViewToggle[24].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[24].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange25(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[25].Checked = false;
            }
            else
            {
                checkBoxes[25].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[25].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[25].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[25].Price;
            }
            oldSelection = 25;
        }
        private void ServiceViewToggle_Click25(object sender, EventArgs e)
        {
            if (serviceDetailPassage[25].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[25].Text = "View Less";
                serviceViewToggle[25].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[25].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[25].Text = "View More";
                serviceViewToggle[25].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[25].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange26(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[26].Checked = false;
            }
            else
            {
                checkBoxes[26].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[26].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[26].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[26].Price;
            }
            oldSelection = 26;
        }
        private void ServiceViewToggle_Click26(object sender, EventArgs e)
        {
            if (serviceDetailPassage[26].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[26].Text = "View Less";
                serviceViewToggle[26].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[26].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[26].Text = "View More";
                serviceViewToggle[26].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[26].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange27(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[27].Checked = false;
            }
            else
            {
                checkBoxes[27].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[27].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[27].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[27].Price;
            }
            oldSelection = 27;
        }
        private void ServiceViewToggle_Click27(object sender, EventArgs e)
        {
            if (serviceDetailPassage[27].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[27].Text = "View Less";
                serviceViewToggle[27].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[27].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[27].Text = "View More";
                serviceViewToggle[27].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[27].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange28(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[28].Checked = false;
            }
            else
            {
                checkBoxes[28].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[28].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[28].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[28].Price;
            }
            oldSelection = 28;
        }
        private void ServiceViewToggle_Click28(object sender, EventArgs e)
        {
            if (serviceDetailPassage[28].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[28].Text = "View Less";
                serviceViewToggle[28].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[28].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[28].Text = "View More";
                serviceViewToggle[28].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[28].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange29(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[29].Checked = false;
            }
            else
            {
                checkBoxes[29].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[29].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[29].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[29].Price;
            }
            oldSelection = 29;
        }
        private void ServiceViewToggle_Click29(object sender, EventArgs e)
        {
            if (serviceDetailPassage[29].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[29].Text = "View Less";
                serviceViewToggle[29].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[29].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[29].Text = "View More";
                serviceViewToggle[29].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[29].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange30(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[30].Checked = false;
            }
            else
            {
                checkBoxes[30].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[30].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[30].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[30].Price;
            }
            oldSelection = 30;
        }
        private void ServiceViewToggle_Click30(object sender, EventArgs e)
        {
            if (serviceDetailPassage[30].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[30].Text = "View Less";
                serviceViewToggle[30].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[30].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[30].Text = "View More";
                serviceViewToggle[30].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[30].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange31(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[31].Checked = false;
            }
            else
            {
                checkBoxes[31].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[31].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[31].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[31].Price;
            }
            oldSelection = 31;
        }
        private void ServiceViewToggle_Click31(object sender, EventArgs e)
        {
            if (serviceDetailPassage[31].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[31].Text = "View Less";
                serviceViewToggle[31].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[31].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[31].Text = "View More";
                serviceViewToggle[31].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[31].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange32(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[32].Checked = false;
            }
            else
            {
                checkBoxes[32].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[32].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[32].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[32].Price;
            }
            oldSelection = 32;
        }
        private void ServiceViewToggle_Click32(object sender, EventArgs e)
        {
            if (serviceDetailPassage[32].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[32].Text = "View Less";
                serviceViewToggle[32].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[32].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[32].Text = "View More";
                serviceViewToggle[32].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[32].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange33(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[33].Checked = false;
            }
            else
            {
                checkBoxes[33].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[33].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[33].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[33].Price;
            }
            oldSelection = 33;
        }
        private void ServiceViewToggle_Click33(object sender, EventArgs e)
        {
            if (serviceDetailPassage[33].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[33].Text = "View Less";
                serviceViewToggle[33].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[33].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[33].Text = "View More";
                serviceViewToggle[33].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[33].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange34(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[34].Checked = false;
            }
            else
            {
                checkBoxes[34].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[34].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[34].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[34].Price;
            }
            oldSelection = 34;
        }
        private void ServiceViewToggle_Click34(object sender, EventArgs e)
        {
            if (serviceDetailPassage[34].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[34].Text = "View Less";
                serviceViewToggle[34].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[34].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[34].Text = "View More";
                serviceViewToggle[34].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[34].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange35(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[35].Checked = false;
            }
            else
            {
                checkBoxes[35].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[35].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[35].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[35].Price;
            }
            oldSelection = 35;
        }
        private void ServiceViewToggle_Click35(object sender, EventArgs e)
        {
            if (serviceDetailPassage[35].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[35].Text = "View Less";
                serviceViewToggle[35].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[35].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[35].Text = "View More";
                serviceViewToggle[35].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[35].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange36(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[36].Checked = false;
            }
            else
            {
                checkBoxes[36].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[36].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[36].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[36].Price;
            }
            oldSelection = 36;
        }
        private void ServiceViewToggle_Click36(object sender, EventArgs e)
        {
            if (serviceDetailPassage[36].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[36].Text = "View Less";
                serviceViewToggle[36].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[36].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[36].Text = "View More";
                serviceViewToggle[36].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[36].Visibility = ViewStates.Gone;
            }

        }
        private void ScheduleSelectServiceFragment_CheckedChange37(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            ResetOldSelection(oldSelection);
            if (!e.IsChecked)
            {
                checkBoxes[37].Checked = false;
            }
            else
            {
                checkBoxes[37].Checked = true;
                CustomerScheduleInformation.ScheduleServiceID = this.ViewModel.scheduleServices.ServicesWithPrice[37].ServiceId;
                CustomerScheduleInformation.ScheduleServiceType = this.ViewModel.scheduleServices.ServicesWithPrice[37].ServiceType;
                CustomerScheduleInformation.ScheduleServicePrice = this.ViewModel.scheduleServices.ServicesWithPrice[37].Price;
            }
            oldSelection = 37;
        }
        private void ServiceViewToggle_Click37(object sender, EventArgs e)
        {
            if (serviceDetailPassage[37].Visibility == ViewStates.Gone)
            {
                serviceViewToggle[37].Text = "View Less";
                serviceViewToggle[37].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[37].Visibility = ViewStates.Visible;
            }
            else
            {
                serviceViewToggle[37].Text = "View More";
                serviceViewToggle[37].PaintFlags = PaintFlags.UnderlineText;
                serviceDetailPassage[37].Visibility = ViewStates.Gone;
            }

        }
    }
}