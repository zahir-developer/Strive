using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Utils.TimInventory;
using Strive.Core.ViewModels.Owner;
using static Android.App.ActionBar;
using Xamarin.Essentials;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using System.Threading.Tasks;
using Android.Graphics;
using Path = System.IO.Path;
using System.IO;
using Android.Support.V7.App;
using StriveOwner.Android.Fragments;
using OperationCanceledException = System.OperationCanceledException;
using MvvmCross.Binding.BindingContext;
using StriveOwner.Android.Adapter;
using System.Collections.ObjectModel;
using Strive.Core.Models.TimInventory;

namespace StriveOwner.Android.Resources.Fragments
{
    public class InventoryEditFragment : MvxFragment<InventoryEditViewModel>
    {
        private EditText item_code;
        private EditText item_name;
        private EditText item_description;
        private EditText item_quantity;
        private Spinner item_type;
        private Spinner item_location;
        private EditText item_cost;
        private EditText item_price;
        private Spinner supplier_name;
        public  ImageView edit_image;
        private Button cancelButton;
        private Button saveButton;
        private Button editImageBtn;
        //private InventoryMainFragment inventoryMainFragment;
        private Context context;
        private SpinnerAdapter<string> LocationAdapter;
        private SpinnerAdapter<string> ProductAdapter;
        private SpinnerAdapter<string> SupplierAdapter;
        private int position;
        private List<string> ProductTypes;
        private List<string> Locations;
        private List<string> Vendors;
        Dialog chooseImageDialog;
        private string PhotoPath;
        private bool isDefaultText;
        private bool isSaveClicked;
        private InventoryEditImagePickerFragment inventoryEditImagePickerFragment;

        private string selectedIcon;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            context = this.Context;
            Platform.Init((Activity)context, savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.InventoryEdit_Fragment, null);
            if(ViewModel == null)
            {
             this.ViewModel = new InventoryEditViewModel();
            }
            item_code = rootView.FindViewById<EditText>(Resource.Id.item_code);
            item_name = rootView.FindViewById<EditText>(Resource.Id.item_name);
            item_description = rootView.FindViewById<EditText>(Resource.Id.item_description);
            item_quantity = rootView.FindViewById<EditText>(Resource.Id.item_quantity);
            item_type = rootView.FindViewById<Spinner>(Resource.Id.item_type);
            item_location = rootView.FindViewById<Spinner>(Resource.Id.item_location);
            item_cost = rootView.FindViewById<EditText>(Resource.Id.item_cost);
            item_price = rootView.FindViewById<EditText>(Resource.Id.item_price);
            edit_image = rootView.FindViewById<ImageView>(Resource.Id.edit_Image);
            supplier_name = rootView.FindViewById<Spinner>(Resource.Id.supplier_name);
            editImageBtn = rootView.FindViewById<Button>(Resource.Id.edit_ImageView);
            editImageBtn.PaintFlags = PaintFlags.UnderlineText;
            cancelButton = rootView.FindViewById<Button>(Resource.Id.edit_cancel);
            saveButton = rootView.FindViewById<Button>(Resource.Id.edit_save);
            var set = this.CreateBindingSet<InventoryEditFragment, InventoryEditViewModel>();
            set.Bind(item_name).To(vm => vm.ItemName);
            set.Bind(item_code).To(vm => vm.ItemCode);
            set.Bind(item_description).To(vm => vm.ItemDescription);
            set.Bind(item_quantity).To(vm => vm.ItemQuantity);
            set.Bind(item_cost).To(vm => vm.ItemCost);
            set.Bind(item_price).To(vm => vm.ItemPrice);
           // set.Bind(saveButton).To(vm => vm.Commands["AddorUpdate"]);
            set.Apply();
            isSaveClicked = false;
            if (ViewModel != null)
            {
                if (!string.IsNullOrEmpty(selectedIcon))
                {
                    ViewModel.Base64String = selectedIcon;
                }
                if (!string.IsNullOrEmpty(ViewModel.Base64String))
                {
                    edit_image.SetImageBitmap(Base64ToBitmap(ViewModel.Base64String));
                }
            }
            isSaveClicked = false;
            cancelButton.Click += CancelButton_Click;
            saveButton.Click += SaveButton_ClickAsync;
            editImageBtn.Click += EditImageBtn_Click;
            item_location.ItemSelected += LocationSpinner_ItemSelected;
            item_type.ItemSelected += ProductTypeSpinner_ItemSelected;
            supplier_name.ItemSelected += SupplierSpinner_ItemSelected;

            LoadProductTypes();      
            return rootView;
        }

        private void EditImageBtn_Click(object sender, EventArgs e)
        {
            chooseImageDialog = new Dialog(context);
            chooseImageDialog.SetContentView(Resource.Layout.ChooseImageDialog);
            chooseImageDialog.Window.SetSoftInputMode(SoftInput.AdjustResize);
            chooseImageDialog.Show();
            chooseImageDialog.Window.SetLayout(LayoutParams.MatchParent, LayoutParams.WrapContent);
            var cameraBtn = chooseImageDialog.FindViewById<ImageView>(Resource.Id.btn_camera);
            var galleryBtn = chooseImageDialog.FindViewById<ImageView>(Resource.Id.btn_gallery);
            var iconBtn = chooseImageDialog.FindViewById<ImageView>(Resource.Id.btn_icon);
            var notNowTxt = chooseImageDialog.FindViewById<TextView>(Resource.Id.notNowtxt);
            notNowTxt.PaintFlags = PaintFlags.UnderlineText;
            cameraBtn.Click += CameraBtn_Click;
            galleryBtn.Click += GalleryBtn_Click;
            iconBtn.Click += IconBtn_Click;
            notNowTxt.Click += NotNow_Click;
        }

        private void NotNow_Click(object sender, EventArgs e)
        {
            chooseImageDialog.Dismiss();
        }

        private void IconBtn_Click(object sender, EventArgs e)
        {
            inventoryEditImagePickerFragment = new InventoryEditImagePickerFragment((parameter,file) => {
                selectedIcon = parameter;
                if(ViewModel != null)
                ViewModel.Filename = file + ".png";
            });
            chooseImageDialog.Dismiss();
            AppCompatActivity activity = (AppCompatActivity)context;
            activity.SupportFragmentManager.BeginTransaction().AddToBackStack("tag").Replace(Resource.Id.content_Frame,inventoryEditImagePickerFragment).Commit();
        }

        private void GalleryBtn_Click(object sender, EventArgs e)
        {
            chooseImageDialog.Dismiss();
            if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.ReadExternalStorage) == Permission.Granted)
            {
                _ = PickPhotoAsync();
            }
            else
            {
                RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage }, 3);
            }
        }
        
        private void CameraBtn_Click(object sender, EventArgs e)
        {
            chooseImageDialog.Dismiss();
            if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.Camera) == Permission.Granted)
            {
                _ = TakePhotoAsync();
            }
            else
            {
                RequestPermissions(new string[] { Manifest.Permission.Camera }, 1);
            }

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == 1)
            {
                if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.WriteExternalStorage) == Permission.Granted)
                {
                    _ = TakePhotoAsync();
                }
                else
                {
                    RequestPermissions(new string[] { Manifest.Permission.WriteExternalStorage }, 2);
                }
            }
            else if(requestCode == 2)
            {
                _ = TakePhotoAsync();
            }
            else if (requestCode == 3)
            {
                _ = PickPhotoAsync();
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            }
            
        }
        
        async Task TakePhotoAsync()
        {
            try
            {
                
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }
            // save the file into local storage
            if(ViewModel!=null)
            ViewModel.Filename = photo.FileName;
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);
            PhotoPath = newFile;
            ConvertImagetoBase64(PhotoPath);
        }
        public  byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);
            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Png, 100, ms);
                return ms.ToArray();
            }
        }
        private void ConvertImagetoBase64(string path)
        {
            try
            {
                byte[] data = File.ReadAllBytes(path);
                byte[] resizedImage = ResizeImage(data, 140, 140);
                string base64 = Convert.ToBase64String(resizedImage);
                if(ViewModel != null)
                {
                    ViewModel.Base64String = base64;
                }
                if(!string.IsNullOrEmpty(base64))
                edit_image.SetImageBitmap(Base64ToBitmap(base64));
            }catch(Exception ex)
            {
                Console.WriteLine($"ConvertImagetoBase64 THREW: {ex.Message}");
            }
        }
        async Task PickPhotoAsync()
        {
            try
            {

                var photo = await MediaPicker.PickPhotoAsync();
                await PickedPhotoAsync(photo);
                Console.WriteLine($"PickPhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PickPhotoAsync THREW: {ex.Message}");
            }
        }
        async Task PickedPhotoAsync(FileResult result)
        {
            // canceled
            if (result == null)
            {
                return;
            }
            var stream = await result.OpenReadAsync();
            if (ViewModel != null)
                ViewModel.Filename = result.FileName;
            ConvertImagetoBase64(result.FullPath);
        }
        public Bitmap Base64ToBitmap(string base64String)
        {
            byte[] imageAsBytes = Base64.Decode(base64String, Base64Flags.Default);
            return BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
        }
        private void SupplierSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position == 0 && supplier_name.SelectedItem.ToString() == "Name")
            {
                ((TextView)supplier_name.SelectedView).SetTextColor(Color.ParseColor("#bbbcbc"));
                position = e.Position;
            }
            else
            {
                var vendorName = Vendors[e.Position];
                var vendor = this.ViewModel.VendorList.IndexOf(this.ViewModel.VendorList.Where(X => X.VendorName == vendorName).FirstOrDefault());
                if (vendor != null)
                {
                    ViewModel.PickerSelectionCommand(ViewModel.VendorList[vendor]);
                    position = e.Position;
                }
            }

        }
        private void LoadSuppliers()
        {
            if (ViewModel != null && ViewModel.VendorList.Count != 0)
            {
                Vendors = new List<string>();
                foreach (var data in ViewModel.VendorList)
                {
                    Vendors.Add(data.VendorName);
                }
                if (string.IsNullOrEmpty(this.ViewModel.SupplierName))
                {
                    Vendors.Insert(0, "Name");
                    position = 0;
                    isDefaultText = true;
                    SupplierAdapter = new SpinnerAdapter<string>(context, Resource.Layout.support_simple_spinner_dropdown_item, Vendors, isDefaultText);
                }
                else
                {
                    var Name = this.ViewModel.SupplierName;
                    position = this.ViewModel.VendorList.IndexOf(this.ViewModel.VendorList.Where(X => X.VendorName == Name).FirstOrDefault());
                    isDefaultText = false;
                    SupplierAdapter = new SpinnerAdapter<string>(context, Resource.Layout.support_simple_spinner_dropdown_item, Vendors ,isDefaultText);

                }

                supplier_name.Adapter = SupplierAdapter;
                supplier_name.SetSelection(position);
            }
        }
        private void LocationSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position == 0 && item_location.SelectedItem.ToString() == "Location")
            {
                ((TextView)item_location.SelectedView).SetTextColor(Color.ParseColor("#bbbcbc"));
                position = e.Position;
            }
            else
            {
                var locName = Locations[e.Position];
                var location = this.ViewModel.LocationList.IndexOf(this.ViewModel.LocationList.Where(X => X.LocationName == locName).FirstOrDefault());
                if (location != null)
                {
                    ViewModel.setLocationCommand(ViewModel.LocationList[location]);
                    position = e.Position;
                }
            }
        }
        private async void LoadLocations()
        {
            try
            {
                ViewModel.LocationList = new ObservableCollection<LocationDetail>();
                await ViewModel.GetAllLocNameCommand();
                if (ViewModel != null && ViewModel.LocationList.Count != 0)
                {
                    Locations = new List<string>();
                    foreach (var data in ViewModel.LocationList)
                    {
                        Locations.Add(data.LocationName);
                    }
                    if (string.IsNullOrEmpty(this.ViewModel.ItemLocation))
                    {
                        Locations.Insert(0, "Location");
                        position = 0;
                        isDefaultText = true;
                        LocationAdapter = new SpinnerAdapter<string>(context, Resource.Layout.support_simple_spinner_dropdown_item, Locations , isDefaultText);
                    }
                    else
                    {
                        var locName = this.ViewModel.ItemLocation;
                        position = this.ViewModel.LocationList.IndexOf(this.ViewModel.LocationList.Where(X => X.LocationName == locName).FirstOrDefault());
                        isDefaultText = false;
                        LocationAdapter = new SpinnerAdapter<string>(context, Resource.Layout.support_simple_spinner_dropdown_item, Locations ,isDefaultText);
                    }

                    item_location.Adapter = LocationAdapter;
                    item_location.SetSelection(position);
                }
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
            
            LoadSuppliers();
        }
        private void ProductTypeSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position == 0 && item_type.SelectedItem.ToString() == "Item Type")
            {
                ((TextView)item_type.SelectedView).SetTextColor(Color.ParseColor("#bbbcbc"));
                position = e.Position;
            }
            else
            {
                var itemValue = ProductTypes[e.Position];
                var productItem = this.ViewModel.ProductTypeList.IndexOf(this.ViewModel.ProductTypeList.Where(X => X.CodeValue == itemValue).FirstOrDefault());
                if (productItem != null)
                {
                    ViewModel.setProdTypeCommand(ViewModel.ProductTypeList[productItem]);
                    position = e.Position;
                }
            }
        }
        private async void LoadProductTypes()
        {
            try
            {
                ViewModel.ProductTypeList = new ObservableCollection<Code>();
                await ViewModel.GetProductTypeCommand();
                if (ViewModel != null && ViewModel.ProductTypeList.Count != 0)
                {
                    ProductTypes = new List<string>();
                    foreach (var data in ViewModel.ProductTypeList)
                    {
                        ProductTypes.Add(data.CodeValue);
                    }
                    if (string.IsNullOrEmpty(this.ViewModel.ItemType))
                    {
                        ProductTypes.Insert(0, "Item Type");
                        position = 0;
                        isDefaultText = true;
                        ProductAdapter = new SpinnerAdapter<string>(context, Resource.Layout.support_simple_spinner_dropdown_item, ProductTypes , isDefaultText);
                    }
                    else
                    {
                        var type = this.ViewModel.ItemType;
                        position = this.ViewModel.ProductTypeList.IndexOf(this.ViewModel.ProductTypeList.Where(X => X.CodeValue == type).FirstOrDefault());
                        isDefaultText = false;
                        ProductAdapter = new SpinnerAdapter<string>(context, Resource.Layout.support_simple_spinner_dropdown_item, ProductTypes , isDefaultText);

                    }
                    item_type.Adapter = ProductAdapter;
                    item_type.SetSelection(position);
                }
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }     
            LoadLocations();
        }
        private async void SaveButton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (!isSaveClicked)
                {
                    await this.ViewModel.AddorUpdateCommandAndroid();
                    if(this.ViewModel.isValidationError == true)
                    {
                        isSaveClicked = false;
                    }
                    else if (this.ViewModel.isValidationError == false)
                    {
                        isSaveClicked = true;
                        var selected_MvxFragment = new InventoryMainFragment(context);
                        FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
                    }
                   
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            var selected_MvxFragment = new InventoryMainFragment(context);
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, selected_MvxFragment).Commit();
        }
    }
}