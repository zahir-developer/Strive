using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;

namespace StriveCustomer.Android.Adapter
{
    public class ScheduleTimeSlots : BaseAdapter
    {

        Context context;
        int slotsAvailable { get; set; }
        Button[] slotSelection;
        int oldSelection { get; set; } = -1;

        AvailableScheduleSlots availableScheduleSlots = new AvailableScheduleSlots();
        List<int> Selected;

        public ScheduleTimeSlots(Context context, AvailableScheduleSlots slotsAvailable)
        {
            this.context = context;
            availableScheduleSlots.GetTimeInDetails = new List<GetTimeInDetails>();
            availableScheduleSlots = slotsAvailable;
            slotSelection = new Button[availableScheduleSlots.GetTimeInDetails.Count];
            Selected = new List<int>();

            if (CustomerScheduleInformation.ScheduleServiceSlotNumber != -1)
            {
                oldSelection = CustomerScheduleInformation.ScheduleServiceSlotNumber;
            }
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            if (convertView == null)
            {
                slotSelection[position] = new Button(context);
                slotSelection[position].LayoutParameters = new GridView.LayoutParams(150, 85);
                
                slotSelection[position].SetPadding(8, 8, 8, 8);
            }
            else
            {
                slotSelection[position] = (Button)convertView;
                
            }
            slotSelection[position].SetBackgroundResource(Resource.Drawable.TimeSlots);
            slotSelection[position].Text = availableScheduleSlots.GetTimeInDetails[position].TimeIn;
            Selected.Add(0);
            slotSelection[position].Tag = position;
            slotSelection[position].Click += ScheduleTimeSlots_Click;
            //switch(position)
            //{
            //    case 0:
            //        slotSelection[0].Click += ScheduleTimeSlots_Click0; 
            //        break;
            //    case 1:
            //        slotSelection[1].Click += ScheduleTimeSlots_Click1;
            //        break;
            //    case 2:
            //        slotSelection[2].Click += ScheduleTimeSlots_Click2;
            //        break;
            //    case 3:
            //        slotSelection[3].Click += ScheduleTimeSlots_Click3;
            //        break;
            //    case 4:
            //        slotSelection[4].Click += ScheduleTimeSlots_Click4;
            //        break;
            //    case 5:
            //        slotSelection[5].Click += ScheduleTimeSlots_Click5;
            //        break;
            //    case 6:
            //        slotSelection[6].Click += ScheduleTimeSlots_Click6;
            //        break;
            //    case 7:
            //        slotSelection[7].Click += ScheduleTimeSlots_Click7;
            //        break;
            //    case 8:
            //        slotSelection[8].Click += ScheduleTimeSlots_Click8;
            //        break;
            //    case 9:
            //        slotSelection[9].Click += ScheduleTimeSlots_Click9;
            //        break;
            //    case 10:
            //        slotSelection[10].Click += ScheduleTimeSlots_Click10;
            //        break;
            //    case 11:
            //        slotSelection[11].Click += ScheduleTimeSlots_Click11;
            //        break;
            //    case 12:
            //        slotSelection[12].Click += ScheduleTimeSlots_Click12;
            //        break;
            //    case 13:
            //        slotSelection[13].Click += ScheduleTimeSlots_Click13;
            //        break;
            //    case 14:
            //        slotSelection[14].Click += ScheduleTimeSlots_Click14;
            //        break;
            //    case 15:
            //        slotSelection[15].Click += ScheduleTimeSlots_Click15;
            //        break;
            //    case 16:
            //        slotSelection[16].Click += ScheduleTimeSlots_Click16;
            //        break;
            //    case 17:
            //        slotSelection[17].Click += ScheduleTimeSlots_Click17;
            //        break;
            //    case 18:
            //        slotSelection[18].Click += ScheduleTimeSlots_Click18;
            //        break;
            //    case 19:
            //        slotSelection[19].Click += ScheduleTimeSlots_Click19;
            //        break;
            //    case 20:
            //        slotSelection[20].Click += ScheduleTimeSlots_Click20;
            //        break;
            //    case 21:
            //        slotSelection[21].Click += ScheduleTimeSlots_Click21;
            //        break;
            //    case 22:
            //        slotSelection[22].Click += ScheduleTimeSlots_Click22;
            //        break;
            //    case 23:
            //        slotSelection[23].Click += ScheduleTimeSlots_Click23;
            //        break;
            //    case 24:
            //        slotSelection[24].Click += ScheduleTimeSlots_Click24;
            //        break;
            //    case 25:
            //        slotSelection[25].Click += ScheduleTimeSlots_Click25;
            //        break;
            //    case 26:
            //        slotSelection[26].Click += ScheduleTimeSlots_Click26;
            //        break;
            //    case 27:
            //        slotSelection[27].Click += ScheduleTimeSlots_Click27;
            //        break;
            //    case 28:
            //        slotSelection[28].Click += ScheduleTimeSlots_Click28;
            //        break;
            //    case 29:
            //        slotSelection[29].Click += ScheduleTimeSlots_Click29;
            //        break;
            //    case 30:
            //        slotSelection[30].Click += ScheduleTimeSlots_Click30;
            //        break;
            //    case 31:
            //        slotSelection[31].Click += ScheduleTimeSlots_Click31;
            //        break;
            //    case 32:
            //        slotSelection[32].Click += ScheduleTimeSlots_Click32;
            //        break;
            //    case 33:
            //        slotSelection[33].Click += ScheduleTimeSlots_Click33;
            //        break;
            //    case 34:
            //        slotSelection[34].Click += ScheduleTimeSlots_Click34;
            //        break;
            //    case 35:
            //        slotSelection[35].Click += ScheduleTimeSlots_Click35;
            //        break;
            //    case 36:
            //        slotSelection[36].Click += ScheduleTimeSlots_Click36;
            //        break;
            //    case 37:
            //        slotSelection[37].Click += ScheduleTimeSlots_Click37;
            //        break;
            //    case 38:
            //        slotSelection[38].Click += ScheduleTimeSlots_Click38;
            //        break;
            //    case 39:
            //        slotSelection[39].Click += ScheduleTimeSlots_Click39;
            //        break;
            //    case 40:
            //        slotSelection[40].Click += ScheduleTimeSlots_Click40;
            //        break;
            //    case 41:
            //        slotSelection[41].Click += ScheduleTimeSlots_Click41;
            //        break;
            //    case 42:
            //        slotSelection[42].Click += ScheduleTimeSlots_Click42;
            //        break;
            //    case 43:
            //        slotSelection[43].Click += ScheduleTimeSlots_Click43;
            //        break;
            //    case 44:
            //        slotSelection[44].Click += ScheduleTimeSlots_Click44;
            //        break;
            //    case 45:
            //        slotSelection[45].Click += ScheduleTimeSlots_Click45;
            //        break;
            //    case 46:
            //        slotSelection[46].Click += ScheduleTimeSlots_Click46;
            //        break;
            //    case 47:
            //        slotSelection[47].Click += ScheduleTimeSlots_Click47;
            //        break;
            //    case 48:
            //        slotSelection[48].Click += ScheduleTimeSlots_Click48;
            //        break;
            //    case 49:
            //        slotSelection[49].Click += ScheduleTimeSlots_Click49;
            //        break;
            //    case 50:
            //        slotSelection[50].Click += ScheduleTimeSlots_Click50;
            //        break;
            //    case 51:
            //        slotSelection[51].Click += ScheduleTimeSlots_Click51;
            //        break;
            //    case 52:
            //        slotSelection[52].Click += ScheduleTimeSlots_Click52;
            //        break;
            //    case 53:
            //        slotSelection[53].Click += ScheduleTimeSlots_Click53;
            //        break;
            //    case 54:
            //        slotSelection[54].Click += ScheduleTimeSlots_Click54;
            //        break;
            //    case 55:
            //        slotSelection[55].Click += ScheduleTimeSlots_Click55;
            //        break;
            //    case 56:
            //        slotSelection[56].Click += ScheduleTimeSlots_Click56;
            //        break;
            //    case 57:
            //        slotSelection[57].Click += ScheduleTimeSlots_Click57;
            //        break;
            //    case 58:
            //        slotSelection[58].Click += ScheduleTimeSlots_Click58;
            //        break;
            //    case 59:
            //        slotSelection[59].Click += ScheduleTimeSlots_Click59;
            //        break;
            //    case 60:
            //        slotSelection[60].Click += ScheduleTimeSlots_Click60;
            //        break;
            //    case 61:
            //        slotSelection[61].Click += ScheduleTimeSlots_Click61;
            //        break;
            //    case 62:
            //        slotSelection[62].Click += ScheduleTimeSlots_Click62;
            //        break;
            //    case 63:
            //        slotSelection[63].Click += ScheduleTimeSlots_Click63;
            //        break;
            //    case 64:
            //        slotSelection[64].Click += ScheduleTimeSlots_Click64;
            //        break;
            //    case 65:
            //        slotSelection[65].Click += ScheduleTimeSlots_Click65;
            //        break;
            //    case 66:
            //        slotSelection[66].Click += ScheduleTimeSlots_Click66;
            //        break;
            //    case 67:
            //        slotSelection[67].Click += ScheduleTimeSlots_Click67;
            //        break;
            //    case 68:
            //        slotSelection[68].Click += ScheduleTimeSlots_Click68;
            //        break;
            //    case 69:
            //        slotSelection[69].Click += ScheduleTimeSlots_Click69;
            //        break;
            //    case 70:
            //        slotSelection[70].Click += ScheduleTimeSlots_Click70;
            //        break;
            //    case 71:
            //        slotSelection[71].Click += ScheduleTimeSlots_Click71;
            //        break;
            //    case 72:
            //        slotSelection[72].Click += ScheduleTimeSlots_Click72;
            //        break;
            //    case 73:
            //        slotSelection[73].Click += ScheduleTimeSlots_Click73;
            //        break;
            //    case 74:
            //        slotSelection[74].Click += ScheduleTimeSlots_Click74;
            //        break;
            //    case 75:
            //        slotSelection[75].Click += ScheduleTimeSlots_Click75;
            //        break;
            //}

            if (CustomerScheduleInformation.ScheduleServiceSlotNumber == position)
            {
                if(Selected[position]!=0)
                slotSelection[position].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
            }
            NotifyDataSetChanged();
            return slotSelection[position];
        }

        private void ScheduleTimeSlots_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int position = (int)button.Tag;
            if (oldSelection != -1 && oldSelection != position)
            {
                slotSelection[oldSelection]?.SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[position] == 0)
            {
                slotSelection[position]?.SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[position] = 1;

            }
            else
            {
                slotSelection[position]?.SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[position] = 0;
            }
            oldSelection = position;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
            CustomerScheduleInformation.ScheduleTime = CustomerScheduleInformation.ScheduleFullDate + "T" + CustomerScheduleInformation.ScheduleServiceTime;
            CustomerScheduleInformation.ScheduledBayId = availableScheduleSlots.GetTimeInDetails[position].BayId;
        }

        private void ScheduleTimeSlots_Click75(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 75)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[75] == 0)
            {
                slotSelection[75].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[75] = 1;
            }
            else
            {
                slotSelection[75].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[75] = 0;
            }
            oldSelection = 75;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click74(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 74)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[74] == 0)
            {
                slotSelection[74].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[74] = 1;
            }
            else
            {
                slotSelection[74].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[74] = 0;
            }
            oldSelection = 74;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click73(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 73)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[73] == 0)
            {
                slotSelection[73].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[73] = 1;
            }
            else
            {
                slotSelection[73].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[73] = 0;
            }
            oldSelection = 73;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click72(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 72)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[72] == 0)
            {
                slotSelection[72].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[72] = 1;
            }
            else
            {
                slotSelection[72].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[72] = 0;
            }
            oldSelection = 72;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click71(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 71)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[71] == 0)
            {
                slotSelection[71].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[71] = 1;
            }
            else
            {
                slotSelection[71].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[71] = 0;
            }
            oldSelection = 71;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click70(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 70)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[70] == 0)
            {
                slotSelection[70].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[70] = 1;
            }
            else
            {
                slotSelection[70].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[70] = 0;
            }
            oldSelection = 70;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click69(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 69)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[69] == 0)
            {
                slotSelection[69].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[69] = 1;
            }
            else
            {
                slotSelection[69].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[69] = 0;
            }
            oldSelection = 69;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click68(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 68)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[68] == 0)
            {
                slotSelection[68].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[68] = 1;
            }
            else
            {
                slotSelection[68].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[68] = 0;
            }
            oldSelection = 68;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click67(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 67)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[67] == 0)
            {
                slotSelection[67].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[67] = 1;
            }
            else
            {
                slotSelection[67].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[67] = 0;
            }
            oldSelection = 67;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click66(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 66)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[66] == 0)
            {
                slotSelection[66].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[66] = 1;
            }
            else
            {
                slotSelection[66].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[66] = 0;
            }
            oldSelection = 66;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click65(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 65)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[65] == 0)
            {
                slotSelection[65].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[65] = 1;
            }
            else
            {
                slotSelection[65].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[65] = 0;
            }
            oldSelection = 65;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click64(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 64)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[64] == 0)
            {
                slotSelection[64].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[64] = 1;
            }
            else
            {
                slotSelection[64].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[64] = 0;
            }
            oldSelection = 64;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click63(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 63)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[63] == 0)
            {
                slotSelection[63].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[63] = 1;
            }
            else
            {
                slotSelection[63].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[63] = 0;
            }
            oldSelection = 63;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click62(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 62)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[62] == 0)
            {
                slotSelection[62].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[62] = 1;
            }
            else
            {
                slotSelection[62].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[62] = 0;
            }
            oldSelection = 62;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click61(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 61)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[61] == 0)
            {
                slotSelection[61].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[61] = 1;
            }
            else
            {
                slotSelection[61].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[61] = 0;
            }
            oldSelection = 61;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click60(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 60)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[60] == 0)
            {
                slotSelection[60].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[60] = 1;
            }
            else
            {
                slotSelection[60].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[60] = 0;
            }
            oldSelection = 60;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click59(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 59)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[59] == 0)
            {
                slotSelection[59].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[59] = 1;
            }
            else
            {
                slotSelection[59].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[59] = 0;
            }
            oldSelection = 59;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click58(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 58)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[58] == 0)
            {
                slotSelection[58].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[58] = 1;
            }
            else
            {
                slotSelection[58].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[58] = 0;
            }
            oldSelection = 58;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click57(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 57)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[57] == 0)
            {
                slotSelection[57].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[57] = 1;
            }
            else
            {
                slotSelection[57].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[57] = 0;
            }
            oldSelection = 57;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click56(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 56)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[56] == 0)
            {
                slotSelection[56].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[56] = 1;
            }
            else
            {
                slotSelection[56].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[56] = 0;
            }
            oldSelection = 56;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click55(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 55)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[55] == 0)
            {
                slotSelection[55].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[55] = 1;
            }
            else
            {
                slotSelection[55].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[55] = 0;
            }
            oldSelection = 55;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click54(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 54)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[54] == 0)
            {
                slotSelection[54].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[54] = 1;
            }
            else
            {
                slotSelection[54].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[54] = 0;
            }
            oldSelection = 54;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click53(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 53)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[53] == 0)
            {
                slotSelection[53].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[53] = 1;
            }
            else
            {
                slotSelection[53].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[53] = 0;
            }
            oldSelection = 53;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click52(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 52)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[52] == 0)
            {
                slotSelection[52].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[52] = 1;
            }
            else
            {
                slotSelection[52].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[52] = 0;
            }
            oldSelection = 52;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click51(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 51)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[51] == 0)
            {
                slotSelection[51].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[51] = 1;
            }
            else
            {
                slotSelection[51].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[51] = 0;
            }
            oldSelection = 51;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click50(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 50)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[50] == 0)
            {
                slotSelection[50].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[50] = 1;
            }
            else
            {
                slotSelection[50].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[50] = 0;
            }
            oldSelection = 50;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click49(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 49)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[49] == 0)
            {
                slotSelection[49].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[49] = 1;
            }
            else
            {
                slotSelection[49].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[49] = 0;
            }
            oldSelection = 49;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click48(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 48)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[48] == 0)
            {
                slotSelection[48].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[48] = 1;
            }
            else
            {
                slotSelection[48].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[48] = 0;
            }
            oldSelection = 48;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click47(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 47)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[47] == 0)
            {
                slotSelection[47].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[47] = 1;
            }
            else
            {
                slotSelection[47].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[47] = 0;
            }
            oldSelection = 47;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click46(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 46)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[46] == 0)
            {
                slotSelection[46].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[46] = 1;
            }
            else
            {
                slotSelection[46].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[46] = 0;
            }
            oldSelection = 46;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click45(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 45)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[45] == 0)
            {
                slotSelection[45].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[45] = 1;
            }
            else
            {
                slotSelection[45].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[45] = 0;
            }
            oldSelection = 45;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click44(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 44)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[44] == 0)
            {
                slotSelection[44].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[44] = 1;
            }
            else
            {
                slotSelection[44].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[44] = 0;
            }
            oldSelection = 44;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click43(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 43)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[43] == 0)
            {
                slotSelection[43].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[43] = 1;
            }
            else
            {
                slotSelection[43].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[43] = 0;
            }
            oldSelection = 43;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click42(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 42)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[42] == 0)
            {
                slotSelection[42].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[42] = 1;
            }
            else
            {
                slotSelection[42].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[42] = 0;
            }
            oldSelection = 42;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click41(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 41)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[41] == 0)
            {
                slotSelection[41].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[41] = 1;
            }
            else
            {
                slotSelection[41].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[41] = 0;
            }
            oldSelection = 41;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click40(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 40)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[40] == 0)
            {
                slotSelection[40].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[40] = 1;
            }
            else
            {
                slotSelection[40].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[40] = 0;
            }
            oldSelection = 40;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click39(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 39)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[39] == 0)
            {
                slotSelection[39].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[39] = 1;
            }
            else
            {
                slotSelection[39].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[39] = 0;
            }
            oldSelection = 39;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click38(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 38)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[38] == 0)
            {
                slotSelection[38].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[38] = 1;
            }
            else
            {
                slotSelection[38].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[38] = 0;
            }
            oldSelection = 38;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click37(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 37)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[37] == 0)
            {
                slotSelection[37].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[37] = 1;
            }
            else
            {
                slotSelection[37].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[37] = 0;
            }
            oldSelection = 37;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click36(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 36)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[36] == 0)
            {
                slotSelection[36].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[36] = 1;
            }
            else
            {
                slotSelection[36].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[36] = 0;
            }
            oldSelection = 36;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click35(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 35)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[35] == 0)
            {
                slotSelection[35].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[35] = 1;
            }
            else
            {
                slotSelection[35].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[35] = 0;
            }
            oldSelection = 35;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click34(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 34)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[34] == 0)
            {
                slotSelection[34].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[34] = 1;
            }
            else
            {
                slotSelection[34].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[34] = 0;
            }
            oldSelection = 34;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click33(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 33)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[33] == 0)
            {
                slotSelection[33].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[33] = 1;
            }
            else
            {
                slotSelection[33].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[33] = 0;
            }
            oldSelection = 33;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click32(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 32)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[32] == 0)
            {
                slotSelection[32].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[32] = 1;
            }
            else
            {
                slotSelection[32].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[32] = 0;
            }
            oldSelection = 32;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click31(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 31)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[31] == 0)
            {
                slotSelection[31].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[31] = 1;
            }
            else
            {
                slotSelection[31].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[31] = 0;
            }
            oldSelection = 31;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click30(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 30)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[30] == 0)
            {
                slotSelection[30].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[30] = 1;
            }
            else
            {
                slotSelection[30].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[30] = 0;
            }
            oldSelection = 30;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click29(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 29)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[29] == 0)
            {
                slotSelection[29].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[29] = 1;
            }
            else
            {
                slotSelection[29].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[29] = 0;
            }
            oldSelection = 29;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click28(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 28)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[28] == 0)
            {
                slotSelection[28].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[28] = 1;
            }
            else
            {
                slotSelection[28].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[28] = 0;
            }
            oldSelection = 28;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click27(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 27)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[27] == 0)
            {
                slotSelection[27].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[27] = 1;
            }
            else
            {
                slotSelection[27].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[27] = 0;
            }
            oldSelection = 27;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click26(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 26)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[26] == 0)
            {
                slotSelection[26].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[26] = 1;
            }
            else
            {
                slotSelection[26].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[26] = 0;
            }
            oldSelection = 26;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click25(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 25)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[25] == 0)
            {
                slotSelection[25].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[25] = 1;
            }
            else
            {
                slotSelection[25].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[25] = 0;
            }
            oldSelection = 25;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click24(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 24)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[24] == 0)
            {
                slotSelection[24].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[24] = 1;
            }
            else
            {
                slotSelection[24].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[24] = 0;
            }
            oldSelection = 24;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click23(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 23)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[23] == 0)
            {
                slotSelection[23].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[23] = 1;
            }
            else
            {
                slotSelection[23].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[23] = 0;
            }
            oldSelection = 23;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click22(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 22)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[22] == 0)
            {
                slotSelection[22].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[22] = 1;
            }
            else
            {
                slotSelection[22].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[22] = 0;
            }
            oldSelection = 22;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click21(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 21)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[21] == 0)
            {
                slotSelection[21].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[21] = 1;
            }
            else
            {
                slotSelection[21].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[21] = 1;

            }
            oldSelection = 21;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click20(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 20)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[20] == 0)
            {
                slotSelection[20].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[20] = 1;
            }
            else
            {
                slotSelection[20].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[20] = 0;
            }
            oldSelection = 20;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click19(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 19)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[19] == 0)
            {
                slotSelection[19].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[19] = 1;
            }
            else
            {
                slotSelection[19].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[19] = 0;
            }
            oldSelection = 19;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click18(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 18)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[18] == 0)
            {
                slotSelection[18].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[18] = 1;
            }
            else
            {
                slotSelection[18].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[18] = 0;
            }
            oldSelection = 18;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click17(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 17)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[17] == 0)
            {
                slotSelection[17].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[17] = 1;
            }
            else
            {
                slotSelection[17].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[17] = 0;
            }
            oldSelection = 17;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click16(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 16)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[16] == 0)
            {
                slotSelection[16].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[16] = 1;
            }
            else
            {
                slotSelection[16].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[16] = 0;
            }
            oldSelection = 16;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click15(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 15)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[15] == 0)
            {
                slotSelection[10].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[15] = 1;
            }
            else
            {
                slotSelection[10].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[15] = 0;
            }
            oldSelection = 15;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click14(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 14)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[14] == 0)
            {
                slotSelection[14].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[14] = 1;
            }
            else
            {
                slotSelection[14].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[14] = 0;
            }
            oldSelection = 14;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click13(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 13)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[13] == 0)
            {
                slotSelection[13].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[13] = 1;
            }
            else
            {
                slotSelection[13].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[13] = 0;
            }
            oldSelection = 13;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }
        private void ScheduleTimeSlots_Click12(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 12)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[12] == 0)
            {
                slotSelection[12].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[12] = 1;
            }
            else
            {
                slotSelection[12].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[12] = 0;
            }
            oldSelection = 12;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;

        }
        private void ScheduleTimeSlots_Click11(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 11)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[11] == 0)
            {
                slotSelection[11].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[11] = 1;
            }
            else
            {
                slotSelection[11].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[11] = 0; 
            }
            oldSelection = 11;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }


        private void ScheduleTimeSlots_Click10(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 10)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[10] == 0)
            {
                slotSelection[10].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[10] = 1;
            }
            else
            {
                slotSelection[10].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[10] = 0;
            }
            oldSelection = 10;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;

        }
        private void ScheduleTimeSlots_Click9(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 9)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[9] == 0)
            {
                slotSelection[9].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[9] = 1;
            }
            else
            {
                slotSelection[9].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[9] = 0;
            }
            oldSelection = 9;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;

        }

        private void ScheduleTimeSlots_Click8(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 8)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[8] == 0)
            {
                slotSelection[8].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[8] = 1;
            }
            else
            {
                slotSelection[8].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[8] = 0;
            }
            oldSelection = 8;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click7(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 7)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[7] == 0)
            {
                slotSelection[7].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[7] = 1;
            }
            else
            {
                slotSelection[7].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[7] = 0;
            }
            oldSelection = 7;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }


        private void ScheduleTimeSlots_Click6(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 6)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[6] == 0)
            {
                slotSelection[6].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[6] = 1;
            }
            else
            {
                slotSelection[6].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[6] = 0;
            }
            oldSelection = 6;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click5(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 5)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[5] == 0)
            {
                slotSelection[5].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[5] = 1;
            }
            else
            {
                slotSelection[5].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[5] = 0;
            }
            oldSelection = 5;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }


        private void ScheduleTimeSlots_Click4(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 4)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[4] == 0)
            {
                slotSelection[4].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[4] = 1;

            }
            else
            {
                slotSelection[4].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[4] = 0;
            }
            oldSelection = 4;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click3(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 3)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[3] == 0)
            {
                slotSelection[3].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[3] = 1;
            }
            else
            {
                slotSelection[3].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[3] = 0;
            }
            oldSelection = 3;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click2(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (oldSelection != -1 && oldSelection != 2)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[2] == 0)
            {
                slotSelection[2].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[1] = 1;
            }
            else
            {
                slotSelection[2].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[1] = 0;
            }
            oldSelection = 2;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
        }

        private void ScheduleTimeSlots_Click1(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if(oldSelection != -1 && oldSelection != 1)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if(Selected[1] == 0)
            {
                slotSelection[1].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[1] = 1;

            }
            else
            {
                slotSelection[1].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[1] = 0;
            }
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            oldSelection = 1;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
            CustomerScheduleInformation.ScheduleTime = CustomerScheduleInformation.ScheduleFullDate + "T" + CustomerScheduleInformation.ScheduleServiceTime;
            CustomerScheduleInformation.ScheduledBayId = availableScheduleSlots.GetTimeInDetails[0].BayId;
        }

        private void ScheduleTimeSlots_Click0(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int position = (int)button.Tag;
            if (oldSelection != -1 && oldSelection != 0)
            {
                slotSelection[oldSelection].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[oldSelection] = 0;
            }
            if (Selected[0] == 0)
            {
                slotSelection[0].SetBackgroundResource(Resource.Drawable.TimeSlotSelected);
                Selected[0] = 1;

            }
            else
            {
                slotSelection[0].SetBackgroundResource(Resource.Drawable.TimeSlots);
                Selected[0] = 0;
            }
            oldSelection = 0;
            CustomerScheduleInformation.ScheduleServiceTime = button.Text;
            CustomerScheduleInformation.ScheduleServiceSlotNumber = oldSelection;
            CustomerScheduleInformation.ScheduleTime = CustomerScheduleInformation.ScheduleFullDate + "T" + CustomerScheduleInformation.ScheduleServiceTime;
            CustomerScheduleInformation.ScheduledBayId = availableScheduleSlots.GetTimeInDetails[position].BayId; 
        }


        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return availableScheduleSlots.GetTimeInDetails.Count;
            }
        }

    }

    class ScheduleTimeSlotsViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}