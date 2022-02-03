﻿using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.ViewModels.Owner;
using StriveOwner.Android.Adapter.CheckOut;
using StriveOwner.Android.Helper;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveOwner.Android.Fragments.CheckOut
{

    public class CheckOutFragment : MvxFragment<CheckOutViewModel>, SwipeRefreshLayout.IOnRefreshListener
    {
        private RecyclerView Checkout_RecyclerView;
        private CheckOutDetailsAdapter checkOutDetailsAdapter;
        public AlertDialog.Builder Builder;
        private EventHandler<DialogClickEventArgs> okHandler;
        private EventHandler<DialogClickEventArgs> removePhotoHandler;
        private bool isPullToRefresh;
        private bool isSwipeCalled;
        SwipeRefreshLayout swipeRefreshLayout;
        Context Context;
        static MySwipeHelper mySwipe;
        public CheckOutFragment(Context context)
        {
            this.Context = context;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.CheckOut_Fragment, null);
            this.ViewModel = new CheckOutViewModel();

            Checkout_RecyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.checkout_RecyclerView);
            swipeRefreshLayout = rootView.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            isPullToRefresh = false;
            isSwipeCalled = false;
            GetCheckoutDetails();
            swipeRefreshLayout.SetOnRefreshListener(this);
            return rootView;
        }

        private async void GetCheckoutDetails()
        {
            try
            {
                await ViewModel.GetCheckOutDetails();
                if (ViewModel.CheckOutVehicleDetails != null)
                {
                    if (ViewModel.CheckOutVehicleDetails != null || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails != null
                        || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel != null || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel.Count > 0)
                    {
                        checkOutDetailsAdapter = new CheckOutDetailsAdapter(Context, ViewModel.CheckOutVehicleDetails);
                        var layoutManager = new LinearLayoutManager(Context);
                        Checkout_RecyclerView.SetLayoutManager(layoutManager);
                        Checkout_RecyclerView.SetAdapter(checkOutDetailsAdapter);
                    }
                    if (!isPullToRefresh || !isSwipeCalled)
                    {
                        mySwipe = new MyImplementSwipeHelper(Context, Checkout_RecyclerView, 200, ViewModel);
                        isSwipeCalled = true;
                    }
                    if (mySwipe != null)
                    {
                        mySwipe.checkOut.CheckOutVehicleDetails = ViewModel.CheckOutVehicleDetails;

                    }
                }
                else
                {
                    Checkout_RecyclerView.SetAdapter(null);
                    Checkout_RecyclerView.SetLayoutManager(null);
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
       
        public async void GetCheckoutDetails(RecyclerView recyclerView)
        {

            Checkout_RecyclerView = recyclerView;
            try
            {
                await ViewModel.GetCheckOutDetails();
                if (ViewModel.CheckOutVehicleDetails != null)
                {
                    if (ViewModel.CheckOutVehicleDetails != null || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails != null
                        || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel != null || ViewModel.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel.Count > 0)
                    {
                        checkOutDetailsAdapter = new CheckOutDetailsAdapter(Context, ViewModel.CheckOutVehicleDetails);
                        var layoutManager = new LinearLayoutManager(Context);
                        Checkout_RecyclerView.SetLayoutManager(layoutManager);
                        Checkout_RecyclerView.SetAdapter(checkOutDetailsAdapter);
                    }
                    mySwipe.checkOut.CheckOutVehicleDetails = ViewModel.CheckOutVehicleDetails;
                    mySwipe.buttonBuffer.Clear();

                }
                else
                {
                    Checkout_RecyclerView.SetAdapter(null);
                    Checkout_RecyclerView.SetLayoutManager(null);
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
        public void HoldTicket(checkOutViewModel checkOut, RecyclerView Checkout_RecyclerView)
        {

            Builder = new AlertDialog.Builder(Context);
            Builder.SetMessage("Are you sure want to change the status to hold?");
            Builder.SetTitle("Hold");
            okHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
            {
                HoldCheckout(checkOut, Checkout_RecyclerView);
            });
            removePhotoHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
            {

            });
            Builder.SetPositiveButton("Ok", okHandler);
            Builder.SetNegativeButton("Cancel", removePhotoHandler);
            Builder.Create();
            Builder.Show();
        }
        public async void HoldCheckout(checkOutViewModel checkout, RecyclerView recyclerView)
        {
            RecyclerView recyclerView1;
            ViewModel = new CheckOutViewModel();
            try
            {
                await ViewModel.updateHoldStatus(int.Parse(checkout.TicketNumber));

                if (ViewModel.holdResponse != null)
                {
                    if (ViewModel.holdResponse.UpdateJobStatus)
                    {
                        Builder = new AlertDialog.Builder(Context);
                        Builder.SetMessage("Service status changed to hold successfully");
                        Builder.SetTitle("Hold");
                        okHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
                        {
                            recyclerView1 = recyclerView;
                            GetCheckoutDetails(recyclerView1);

                        });
                        Builder.SetPositiveButton("Ok", okHandler);
                        Builder.Create();

                        Builder.Show();
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
        public void CompleteTicket(checkOutViewModel checkOut, RecyclerView Checkout_RecyclerView)
        {
            Builder = new AlertDialog.Builder(Context);
            Builder.SetMessage("Are you sure want to change the status to complete?");
            Builder.SetTitle("Complete");
            okHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
            {
                CompleteCheckout(checkOut, Checkout_RecyclerView);
            });
            removePhotoHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
            {

            });
            Builder.SetPositiveButton("Ok", okHandler);
            Builder.SetNegativeButton("Cancel", removePhotoHandler);
            Builder.Create();
            Builder.Show();
        }
        public async void CompleteCheckout(checkOutViewModel checkout, RecyclerView recyclerView)
        {
            RecyclerView recyclerView1;
            ViewModel = new CheckOutViewModel();
            try
            {
                await ViewModel.updateCompleteStatus(int.Parse(checkout.TicketNumber));

                if (ViewModel.holdResponse != null)
                {
                    if (ViewModel.holdResponse.UpdateJobStatus)
                    {
                        Builder = new AlertDialog.Builder(Context);
                        Builder.SetMessage("Service has been completed successfully");
                        Builder.SetTitle("Complete");
                        okHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
                        {
                            recyclerView1 = recyclerView;
                            GetCheckoutDetails(recyclerView1);
                        });
                        Builder.SetPositiveButton("Ok", okHandler);
                        Builder.Create();

                        Builder.Show();
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
        public void CheckoutTicket(checkOutViewModel checkout, RecyclerView Checkout_RecyclerView)
        {
            if (checkout.MembershipNameOrPaymentStatus.Contains("Paid"))
            {
                Builder = new AlertDialog.Builder(Context);
                Builder.SetMessage("Are you sure want to change the status to checkout?");
                Builder.SetTitle("Checkout");
                okHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
                {
                    Checkout(checkout, Checkout_RecyclerView);
                });
                removePhotoHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
                {

                });
                Builder.SetPositiveButton("Ok", okHandler);
                Builder.SetNegativeButton("Cancel", removePhotoHandler);
                Builder.Create();
                Builder.Show();
            }
            else
            {
                Builder = new AlertDialog.Builder(Context);
                Builder.SetMessage("Can't Checkout without payment");
                Builder.SetTitle("Checkout");
                okHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
                {


                });
                Builder.SetPositiveButton("Ok", okHandler);
                Builder.Create();

                Builder.Show();
            }
        }
        public async void Checkout(checkOutViewModel checkout, RecyclerView recyclerView)
        {
            RecyclerView recyclerView1;
            ViewModel = new CheckOutViewModel();
            try
            {
                await ViewModel.DoCheckout(int.Parse(checkout.TicketNumber));
                if (ViewModel.status != null)
                {
                    if (ViewModel.status.SaveCheckoutTime)
                    {
                        Builder = new AlertDialog.Builder(Context);
                        Builder.SetMessage("Vehicle has been checked out successfully");
                        Builder.SetTitle("Checkout");
                        okHandler = new EventHandler<DialogClickEventArgs>((object s, DialogClickEventArgs de) =>
                        {
                            recyclerView1 = recyclerView;
                            GetCheckoutDetails(recyclerView1);

                        });
                        Builder.SetPositiveButton("Ok", okHandler);
                        Builder.Create();
                        Builder.Show();
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

        public void OnRefresh()
        {
            GetCheckoutDetails();
            swipeRefreshLayout.Refreshing = false;
            isPullToRefresh = true;
        }

        private class MyImplementSwipeHelper : MySwipeHelper
        {
            Context Context;
            RecyclerView checkout_RecyclerView;
            int buttonWidth;
            CheckOutViewModel CheckoutDetail;


            public MyImplementSwipeHelper(Context context, RecyclerView checkout_RecyclerView, int buttonWidth, CheckOutViewModel viewModel) : base(context, checkout_RecyclerView, 200,viewModel)
            {
                Context = context;
                this.checkout_RecyclerView = checkout_RecyclerView;
                this.buttonWidth = buttonWidth;
                this.CheckoutDetail = viewModel;


            }

            public override void InstantiateMyButton(RecyclerView.ViewHolder viewHolder, List<MyButton> buffer)
            {
                int itemposition = viewHolder.AdapterPosition;
                var selectedItem = CheckoutDetail.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[itemposition];
                CheckOutFragment fragment = new CheckOutFragment(Context);
                //button-1
                buffer.Add(new MyButton(Context,
                    "Hold",
                    35,
                    Resource.Drawable.Check,
                    "#FBB80F",
                    new HoldButtonClick(this, Context, selectedItem, fragment, checkout_RecyclerView), selectedItem));
                //button-2
                buffer.Add(new MyButton(Context,
                    "Complete",
                    35,
                    Resource.Drawable.Check,
                    "#006400",
                    new CompleteButtonClick(this, Context, selectedItem, fragment, checkout_RecyclerView), selectedItem));
                //button-3
                buffer.Add(new MyButton(Context,
                    "CheckOut",
                    35,
                    Resource.Drawable.Check,
                    "#808080",
                    new CheckOutButtonClick(this, Context, selectedItem, fragment, checkout_RecyclerView), selectedItem));


                
            }
            public override void InstantiateUpdatedMyButton(RecyclerView.ViewHolder viewHolder, List<MyButton> buffer)
            {
                int itemposition = viewHolder.AdapterPosition;
                var selectedItem = CheckoutDetail.CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel[itemposition];
                CheckOutFragment fragment = new CheckOutFragment(Context);

                //button-1
                buffer.Add(new MyButton(Context,
                 "Hold",
                 35,
                 Resource.Drawable.Check,
                 "#FBB80F",
                 new HoldButtonClick(this, Context, selectedItem, fragment, checkout_RecyclerView), selectedItem));

                // button - 2
                buffer.Add(new MyButton(Context,
                    "CheckOut",
                    35,
                    Resource.Drawable.Check,
                    "#808080",
                    new CheckOutButtonClick(this, Context, selectedItem, fragment, checkout_RecyclerView), selectedItem));


             
            }
            //Click Event for holdbutton
            public class HoldButtonClick : MyButtonClickListener
            {
                private MyImplementSwipeHelper myImplementSwipeHelper;

                RecyclerView checkout_RecyclerView;

                public AlertDialog.Builder Builder;
                private EventHandler<DialogClickEventArgs> okHandler;
                private EventHandler<DialogClickEventArgs> removePhotoHandler;
                Context Context;
                checkOutViewModel CheckOutViewModel;
                CheckOutFragment fragment;

                public HoldButtonClick(MyImplementSwipeHelper myImplementSwipeHelper, Context context, checkOutViewModel checkOutViewModel, CheckOutFragment fragment, RecyclerView recyclerView)
                {
                    this.myImplementSwipeHelper = myImplementSwipeHelper;
                    this.Context = context;
                    this.CheckOutViewModel = checkOutViewModel;
                    this.fragment = fragment;
                    this.checkout_RecyclerView = recyclerView;
                }

                public void OnClick(int position, checkOutViewModel checkOut)
                {
                    fragment.HoldTicket(checkOut, checkout_RecyclerView);
                }

            }

            //Click Event for Completebutton
            private class CompleteButtonClick : MyButtonClickListener
            {
                private MyImplementSwipeHelper myImplementSwipeHelper;
                Context Context;
                checkOutViewModel CheckOutViewModel;
                CheckOutFragment fragment;
                RecyclerView checkout_RecyclerView;


                public CompleteButtonClick(MyImplementSwipeHelper myImplementSwipeHelper, Context context, checkOutViewModel checkOutViewModel, CheckOutFragment fragment, RecyclerView recyclerView)
                {
                    this.myImplementSwipeHelper = myImplementSwipeHelper;
                    this.Context = context;
                    this.CheckOutViewModel = checkOutViewModel;
                    this.fragment = fragment;
                    this.checkout_RecyclerView = recyclerView;
                }


                public void OnClick(int position, checkOutViewModel checkOut)
                {
                    fragment.CompleteTicket(checkOut, checkout_RecyclerView);
                }

            }
            //Click Event for CheckOutButton
            private class CheckOutButtonClick : MyButtonClickListener
            {
                private MyImplementSwipeHelper myImplementSwipeHelper;
                Context Context;
                checkOutViewModel CheckOutViewModel;
                CheckOutFragment fragment;
                RecyclerView checkout_RecyclerView;
                public CheckOutButtonClick(MyImplementSwipeHelper myImplementSwipeHelper, Context context, checkOutViewModel checkOutViewModel, CheckOutFragment fragment, RecyclerView recyclerView)
                {
                    this.myImplementSwipeHelper = myImplementSwipeHelper;
                    this.Context = context;
                    this.CheckOutViewModel = checkOutViewModel;
                    this.fragment = fragment;
                    this.checkout_RecyclerView = recyclerView;

                }
                public void OnClick(int position, checkOutViewModel checkOut)
                {
                    fragment.CheckoutTicket(checkOut, checkout_RecyclerView);
                }


            }
        }
    }
}