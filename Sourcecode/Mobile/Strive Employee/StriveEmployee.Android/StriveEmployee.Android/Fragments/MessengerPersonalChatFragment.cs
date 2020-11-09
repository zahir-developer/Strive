using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Employee.Messenger.PersonalChat;
using Strive.Core.ViewModels.Employee;

namespace StriveEmployee.Android.Fragments
{
    [Activity (WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MessengerPersonalChatFragment : MvxFragment<MessengerPersonalChatViewModel>
    {
        private EditText chatMessage_EditText;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MessengerPersonalChat_Fragment, null);
            this.ViewModel = new MessengerPersonalChatViewModel();

            chatMessage_EditText = rootView.FindViewById<EditText>(Resource.Id.chatMessage_EditText);
            chatMessage_EditText.SetCompoundDrawablesWithIntrinsicBounds(0, 0, Resource.Drawable.sendcircle, 0);

            getChatData();
            return rootView;
        }

        private async void getChatData()
        {
            ChatDataRequest chatData = new ChatDataRequest
            {
                SenderId = 1,
                RecipientId = 123,
                GroupId = 0
            };
            await this.ViewModel.GetAllMessages(chatData);
        }
    }
}