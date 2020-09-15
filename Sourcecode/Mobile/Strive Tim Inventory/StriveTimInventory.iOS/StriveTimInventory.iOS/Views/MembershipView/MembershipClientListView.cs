﻿using System;

using Strive.Core.ViewModels.TIMInventory;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Binding.BindingContext;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class MembershipClientListView : MvxViewController<MembershipClientListViewModel>
    {
        public MembershipClientListView() : base("MembershipClientListView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBarHidden = true;

            var ClientTableSource = new ClientTableSource(ClientTableView, ViewModel);

            var set = this.CreateBindingSet<MembershipClientListView, MembershipClientListViewModel>();
            set.Bind(ClientTableSource).To(vm => vm.FilteredList);
            set.Bind(LogoutButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(ClientTableSource).For(s => s.SelectionChangedCommand).To(vm => vm.Commands["NavigateToDetail"]);
            set.Apply();

            ClientTableView.Source = ClientTableSource;
            ClientTableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            ClientTableView.SeparatorInset = new UIEdgeInsets(0, 10, 0, 10);
            ClientTableView.SeparatorColor = UIColor.Gray;
            ClientTableView.TableFooterView = new UIView(CGRect.Empty);
            ClientTableView.DelaysContentTouches = false;
            ClientTableView.ReloadData();

            ClientSearch.Placeholder = "Search";
            ClientSearch.TextChanged += SearchTextChanged;

            GetAllClients();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private async Task GetAllClients()
        {
            ViewModel.ClearCommand();
            await ViewModel.GetAllClientsCommand();
            ViewModel.ClientSearchCommand("");
        }

        private void SearchTextChanged(object sender, UISearchBarTextChangedEventArgs e)
        {
            ViewModel.ClientSearchCommand(e.SearchText);
            if (e.SearchText == "")
            {
                ClientSearch.ResignFirstResponder();
            }
        }
    }
}

