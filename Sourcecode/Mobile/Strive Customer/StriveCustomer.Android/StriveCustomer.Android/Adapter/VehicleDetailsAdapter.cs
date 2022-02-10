using System;
using System.Security;
using Acr.UserDialogs;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Java.IO;
using MvvmCross;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Fragments;
using StriveCustomer.Android.Resources.Enums;
using StriveCustomer.Android.Services;
using Xamarin.Essentials;
using File = System.IO.File;
using Uri = Android.Net.Uri;
namespace StriveCustomer.Android.Adapter
{
    public class VehicleRecyclerHolder : RecyclerView.ViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        public IItemClickListener vehicleItemClickListener;
        public TextView vehicleReg;
        public TextView vehicleName;
        public TextView vehicleMembership;
        public ImageButton deleteButton;
        public ImageButton editButton;
        public ImageButton downloadButton;
        public Context cxt;
        public VehicleInfoFragment vehicleInfo;
        VehicleInfoDisplayFragment InfoFragment = new VehicleInfoDisplayFragment();
        public VehicleRecyclerHolder(View itemVehicle) : base(itemVehicle)
        {
            deleteButton = itemVehicle.FindViewById<ImageButton>(Resource.Id.deleteButton);
            editButton = itemVehicle.FindViewById<ImageButton>(Resource.Id.editButton);
            downloadButton = itemVehicle.FindViewById<ImageButton>(Resource.Id.downloadButton);
            vehicleReg = itemVehicle.FindViewById<TextView>(Resource.Id.regNumber);
            vehicleName = itemVehicle.FindViewById<TextView>(Resource.Id.carNames);
            vehicleMembership = itemVehicle.FindViewById<TextView>(Resource.Id.vehicleMembership);
            deleteButton.Click += DeleteButton_Click;
            editButton.Click += EditButton_Click;
            downloadButton.Click += DownloadButton_Click;
            itemVehicle.SetOnClickListener(this);
        }

        private async void DownloadButton_Click(object sender, EventArgs e)
        {
            if (ActivityCompat.CheckSelfPermission(this.cxt, Manifest.Permission.WriteExternalStorage) == Permission.Granted)
            {
                CustomerInfo.actionType = 3;
                vehicleItemClickListener.OnClick(null, AdapterPosition, false);
            }
            else
            {
                await AndroidPermissions.checkExternalStoragePermission(vehicleInfo);
                CustomerInfo.actionType = 3;
                vehicleItemClickListener.OnClick(null, AdapterPosition, false);
            }
            
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (CustomerVehiclesInformation.vehiclesList.Status.Count > 0)
            {
                var data = CustomerVehiclesInformation.vehiclesList.Status[Position];
                AppCompatActivity activity = (AppCompatActivity)this.ItemView.Context;
                CustomerVehiclesInformation.selectedVehicleInfo = data.VehicleId;
                MembershipDetails.clientVehicleID = data.VehicleId;
                MembershipDetails.vehicleNumber = data.VehicleNumber;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, InfoFragment).Commit();
            }

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            CustomerInfo.actionType = 1;
            vehicleItemClickListener.OnClick(null, AdapterPosition, false);
        }

        public void SetItemClickListener(IItemClickListener itemClickListener)
        {
            this.vehicleItemClickListener = itemClickListener;
        }
        public void OnClick(View view)
        {
            CustomerInfo.actionType = 2;
            vehicleItemClickListener.OnClick(view, AdapterPosition, false);
        }

        public bool OnLongClick(View v)
        {
            return true;
        }
    }

    public class VehicleDetailsAdapter : RecyclerView.Adapter, IItemClickListener
    {

        Context context;
        VehicleInfoFragment vehicleInfoFragment;
        public VehicleList vehicleLists = new VehicleList();
        private VehicleRecyclerHolder vehicleRecyclerHolder;
        private static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();
        public override int ItemCount
        {
            get
            {
                return vehicleLists.Status.Count;
            }
        }


        public VehicleDetailsAdapter(Context context, VehicleList data, VehicleInfoFragment vehicleInfoFragment)
        {
            this.context = context;
            this.vehicleLists = data;
            this.vehicleInfoFragment = vehicleInfoFragment;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            vehicleRecyclerHolder = holder as VehicleRecyclerHolder;
            vehicleRecyclerHolder.cxt = context;
            vehicleRecyclerHolder.vehicleInfo = vehicleInfoFragment;
            vehicleRecyclerHolder.vehicleReg.Text = vehicleLists.Status[position].Barcode ?? "";
            vehicleRecyclerHolder.vehicleName.Text = vehicleLists.Status[position].VehicleColor + " " + vehicleLists.Status[position].VehicleMfr + " " + vehicleLists.Status[position].VehicleModel ?? "";
            vehicleRecyclerHolder.vehicleMembership.Text = vehicleLists.Status[position].MembershipName;
            vehicleRecyclerHolder.SetItemClickListener(this);

        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public async void OnClick(View itemView, int position, bool isLongClick)
        {
            if (CustomerInfo.actionType == (int)VehicleClickEnums.Delete)
            {
                VehicleInfoViewModel vehicleInfo = new VehicleInfoViewModel();
                var data = CustomerVehiclesInformation.vehiclesList.Status[position];
                var deleted = await vehicleInfo.DeleteCustomerVehicle(data.VehicleId);
                if (deleted)
                {
                    vehicleLists.Status.RemoveAt(position);
                    NotifyItemRemoved(position);
                    NotifyItemRangeChanged(position, vehicleLists.Status.Count);
                }
            }
            if (CustomerInfo.actionType == (int)VehicleClickEnums.Download)
            {
                VehicleInfoViewModel vehicleInfo = new VehicleInfoViewModel();
                var data = CustomerVehiclesInformation.vehiclesList.Status[position];
                if (data.DocumentId.HasValue)
                {
                    var documentBase64 = await vehicleInfo.DownloadTerms((int)data.DocumentId);
                    if (!string.IsNullOrEmpty(documentBase64))
                    {
                        byte[] dataconverted = System.Convert.FromBase64String(documentBase64);
                        try
                        {

                            string base64 = Base64.EncodeToString(dataconverted,
                                    Base64Flags.Default);
                            byte[] bfile = Base64.Decode(base64, Base64Flags.Default);
                            var file = SaveBinary(vehicleInfo?.documentFileName, bfile);
                            if (file != null)
                            {
                                _userDialog.Toast("Document downloaded successfully");
                            }
                        }
                        catch (UnsupportedEncodingException ex)
                        {

                        }
                    }
                }
            }
        }
        
       

        private string SaveBinary(string filename, byte[] bytes)
            {
                string filepath = GetFilePath(filename);
                if (File.Exists((filepath)))
                {
                    File.Delete(filepath);
                }
                File.WriteAllBytes(filepath, bytes);

                return filepath;
            }

        string GetFilePath(string filename)
        {
            return System.IO.Path.Combine(GetPath(), filename);
        }

        string GetPath()
        {
            try
            {
                return context.ApplicationContext.GetExternalFilesDir(null).AbsolutePath;
            }catch(SecurityException ex)
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }           
            
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(parent.Context);
            View itemView = layoutInflater.Inflate(Resource.Layout.VehicleInfoList, parent, false);
            return new VehicleRecyclerHolder(itemView);
        }

        
    }
}