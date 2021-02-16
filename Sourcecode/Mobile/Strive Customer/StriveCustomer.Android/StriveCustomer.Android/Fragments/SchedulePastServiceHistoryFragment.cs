using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class SchedulePastServiceHistoryFragment : MvxFragment<ScheduleViewModel>
    {
        private LinearLayout PastServiceList_LinearLayout;
        private View layout;
        private View moreInfo;
        private TextView[] TicketNumber;
        private LinearLayout[] moreInfo_LinearLayout;
        int a = 0;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.ServiceHistoryFragment, null);
            this.ViewModel = new ScheduleViewModel();
            GetPastServices();
            PastServiceList_LinearLayout = rootview.FindViewById<LinearLayout>(Resource.Id.ServiceHistory_LinearLayout);

            return rootview;
        }

        private void TicketNumber_Click(object sender, EventArgs e)
        {
            
        }

        public async void GetPastServices()
        {
            await this.ViewModel.GetPastDetailsServices();
            if(this.ViewModel.pastClientServices != null)
            {
                if(this.ViewModel.pastClientServices.PastClientDetails.Count > 0)
                {
                    TicketNumber = new TextView[this.ViewModel.pastClientServices.PastClientDetails.Count];
                    moreInfo_LinearLayout = new LinearLayout[this.ViewModel.pastClientServices.PastClientDetails.Count];
                    for (int services = 0; services < this.ViewModel.pastClientServices.PastClientDetails.Count; services++)
                    {
                        layout = LayoutInflater.From(Context).Inflate(Resource.Layout.ServiceHistoryItemView, PastServiceList_LinearLayout, false);
                        var vehicleName = layout.FindViewById<TextView>(Resource.Id.makeModelColorValue_TextView);
                        var detailVisitDate = layout.FindViewById<TextView>(Resource.Id.scheduleDetailVisit_TextView);
                        var detailService = layout.FindViewById<TextView>(Resource.Id.detailServices_TextView);
                        var barcode = layout.FindViewById<TextView>(Resource.Id.barcodeValue_TextView);
                        var price = layout.FindViewById<TextView>(Resource.Id.schedulePrice_TextView); 
                        var additionalServices = layout.FindViewById<TextView>(Resource.Id.additionalServicesValue_TextView);
                        var detailedServiceCost = layout.FindViewById<TextView>(Resource.Id.scheduleTicketValue_TextView);

                        vehicleName.Text = this.ViewModel.pastClientServices.PastClientDetails[services].Color + " "
                                        + this.ViewModel.pastClientServices.PastClientDetails[services].Make + " "
                                        + this.ViewModel.pastClientServices.PastClientDetails[services].Model;

                        var dates = this.ViewModel.pastClientServices.PastClientDetails[services].DetailVisitDate.ToString();
                        var splitDates = dates.Split("T");
                        var detailedVisitDate = splitDates[0].Split("-"); ;
                        var orderedDate = detailedVisitDate[2] + "/" + detailedVisitDate[1] + "/" + detailedVisitDate[0];

                        detailVisitDate.Text = orderedDate;
                        detailService.Text = this.ViewModel.pastClientServices.PastClientDetails[services].WashOrDetailJobType;
                        additionalServices.Text = this.ViewModel.pastClientServices.PastClientDetails[services].ServiceName;
                        detailedServiceCost.Text = "$" + this.ViewModel.pastClientServices.PastClientDetails[services].Cost;
                        barcode.Text = this.ViewModel.pastClientServices.PastClientDetails[services].Barcode;
                        price.Text = this.ViewModel.pastClientServices.PastClientDetails[services].Cost;

                        TicketNumber[services] = layout.FindViewById<TextView>(Resource.Id.scheduleTicket_TextView);
                        TicketNumber[services].Text = "Ticket No:" +" ";
                        TicketNumber[services].PaintFlags = PaintFlags.UnderlineText;
                        moreInfo_LinearLayout[services] = layout.FindViewById<LinearLayout>(Resource.Id.moreInfo_LinearLayout);
                        moreInfo_LinearLayout[services].Visibility = ViewStates.Gone;

                        AssignListeners(services);

                        PastServiceList_LinearLayout.AddView(layout);
                    }
                }
            }
        }

        private void AssignListeners(int position)
        {
            switch(position)
            {
                case 0:
                    TicketNumber[0].Click += SchedulePastServiceHistoryFragment_Click0;
                    break;
                case 1:
                    TicketNumber[1].Click += SchedulePastServiceHistoryFragment_Click1;
                    break;
                case 2:
                    TicketNumber[2].Click += SchedulePastServiceHistoryFragment_Click2;
                    break;
                case 3:
                    TicketNumber[3].Click += SchedulePastServiceHistoryFragment_Click3;
                    break;
                case 4:
                    TicketNumber[4].Click += SchedulePastServiceHistoryFragment_Click4;
                    break;
                case 5:
                    TicketNumber[5].Click += SchedulePastServiceHistoryFragment_Click5;
                    break;
                case 6:
                    TicketNumber[6].Click += SchedulePastServiceHistoryFragment_Click6;
                    break;
                case 7:
                    TicketNumber[7].Click += SchedulePastServiceHistoryFragment_Click7;
                    break;
                case 8:
                    TicketNumber[8].Click += SchedulePastServiceHistoryFragment_Click8;
                    break;
                case 9:
                    TicketNumber[9].Click += SchedulePastServiceHistoryFragment_Click9;
                    break;
                case 10:
                    TicketNumber[10].Click += SchedulePastServiceHistoryFragment_Click10;
                    break;
                case 11:
                    TicketNumber[11].Click += SchedulePastServiceHistoryFragment_Click11;
                    break;
                case 12:
                    TicketNumber[12].Click += SchedulePastServiceHistoryFragment_Click12;
                    break;
                case 13:
                    TicketNumber[13].Click += SchedulePastServiceHistoryFragment_Click13;
                    break;
                case 14:
                    TicketNumber[14].Click += SchedulePastServiceHistoryFragment_Click14;
                    break;
                case 15:
                    TicketNumber[15].Click += SchedulePastServiceHistoryFragment_Click15;
                    break;
                case 16:
                    TicketNumber[16].Click += SchedulePastServiceHistoryFragment_Click16;
                    break;
                case 17:
                    TicketNumber[17].Click += SchedulePastServiceHistoryFragment_Click17;
                    break;
                case 18:
                    TicketNumber[18].Click += SchedulePastServiceHistoryFragment_Click18;
                    break;
                case 19:
                    TicketNumber[19].Click += SchedulePastServiceHistoryFragment_Click19;
                    break;
                case 20:
                    TicketNumber[20].Click += SchedulePastServiceHistoryFragment_Click20;
                    break;
                case 21:
                    TicketNumber[21].Click += SchedulePastServiceHistoryFragment_Click21;
                    break;
                case 22:
                    TicketNumber[22].Click += SchedulePastServiceHistoryFragment_Click22;
                    break;
                case 23:
                    TicketNumber[23].Click += SchedulePastServiceHistoryFragment_Click23;
                    break;
                case 24:
                    TicketNumber[24].Click += SchedulePastServiceHistoryFragment_Click24;
                    break;
                case 25:
                    TicketNumber[25].Click += SchedulePastServiceHistoryFragment_Click25;
                    break;
                case 26:
                    TicketNumber[26].Click += SchedulePastServiceHistoryFragment_Click26;
                    break;
                case 27:
                    TicketNumber[27].Click += SchedulePastServiceHistoryFragment_Click27;
                    break;
                case 28:
                    TicketNumber[28].Click += SchedulePastServiceHistoryFragment_Click28;
                    break;
                case 29:
                    TicketNumber[29].Click += SchedulePastServiceHistoryFragment_Click29;
                    break;
                case 30:
                    TicketNumber[30].Click += SchedulePastServiceHistoryFragment_Click30;
                    break;
                case 31:
                    TicketNumber[31].Click += SchedulePastServiceHistoryFragment_Click31;
                    break;
                case 32:
                    TicketNumber[32].Click += SchedulePastServiceHistoryFragment_Click32;
                    break;
                case 33:
                    TicketNumber[33].Click += SchedulePastServiceHistoryFragment_Click33;
                    break;
            }

        }

        private void SchedulePastServiceHistoryFragment_Click33(object sender, EventArgs e)
        {
            if(moreInfo_LinearLayout[33].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[33].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[33].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click32(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[32].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[32].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[32].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click31(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[31].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[31].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[31].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click30(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[30].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[30].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[30].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click29(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[29].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[29].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[29].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click28(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[28].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[28].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[28].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click27(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[27].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[27].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[27].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click26(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[26].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[26].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[26].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click25(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[25].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[25].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[25].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click24(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[24].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[24].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[24].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click23(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[23].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[23].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[23].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click22(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[22].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[22].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[22].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click21(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[21].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[21].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[21].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click20(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[20].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[20].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[20].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click19(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[19].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[19].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[19].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click18(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[18].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[18].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[18].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click17(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[17].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[17].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[17].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click16(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[16].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[16].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[16].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click15(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[15].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[15].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[15].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click14(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[14].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[14].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[14].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click13(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[13].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[13].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[13].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click12(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[12].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[12].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[12].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click11(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[11].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[11].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[11].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click10(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[10].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[10].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[10].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click9(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[9].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[9].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[9].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click8(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[8].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[8].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[8].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click7(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[7].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[7].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[7].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click6(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[6].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[6].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[6].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click5(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[5].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[5].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[5].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click4(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[4].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[4].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[4].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click3(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[3].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[3].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[3].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click2(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[2].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[2].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[2].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click1(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[1].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[1].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[1].Visibility = ViewStates.Gone;
            }
        }

        private void SchedulePastServiceHistoryFragment_Click0(object sender, EventArgs e)
        {
            if (moreInfo_LinearLayout[0].Visibility == ViewStates.Gone)
            {
                moreInfo_LinearLayout[0].Visibility = ViewStates.Visible;
            }
            else
            {
                moreInfo_LinearLayout[0].Visibility = ViewStates.Gone;
            }
        }
    }
}