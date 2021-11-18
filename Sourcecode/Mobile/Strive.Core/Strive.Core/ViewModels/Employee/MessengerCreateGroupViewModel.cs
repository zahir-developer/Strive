using Acr.UserDialogs;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Models.Employee.Messenger.MessengerGroups;
using Strive.Core.Resources;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeList = Strive.Core.Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeMessengerContacts;

namespace Strive.Core.ViewModels.Employee
{
    public class MessengerCreateGroupViewModel : BaseViewModel
    {
        #region Properties
        public EmployeeList EmployeeLists { get; set; }
        
        public chatGroup chatGroupforCreation = new chatGroup();
        public static List<chatUserGroup> chatUserGroups = new List<chatUserGroup>();
        #endregion Properties


        #region Commands

        public async Task GetContactsList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var contactList = await MessengerService.GetContacts(new GetAllEmployeeDetail_Request
            {
                startDate = null,
                endDate = null,
                locationId = null,
                pageNo = null,
                pageSize = null,
                query = "",
                sortOrder = null,
                sortBy = null,
                status = true,
            });
            if (contactList == null)
            {
                EmployeeLists = null;
            }
            else
            {
                EmployeeLists = new EmployeeList();
                EmployeeLists.EmployeeList = new Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList();
                EmployeeLists.EmployeeList.Employee = new List<Models.Employee.Messenger.MessengerContacts.Contacts.Employee>();
                MessengerTempData.employeeList_Contact = new EmployeeList();
                MessengerTempData.employeeList_Contact.EmployeeList = new Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList();
                MessengerTempData.employeeList_Contact.EmployeeList.Employee = new List<Models.Employee.Messenger.MessengerContacts.Contacts.Employee>();
                EmployeeLists = contactList;
                MessengerTempData.employeeList_Contact = contactList;
                MessengerTempData.ChatParticipants = new Dictionary<int, int>();

                if (MessengerTempData.SelectedParticipants == null)
                {
                    MessengerTempData.SelectedParticipants = new EmployeeList();
                    MessengerTempData.SelectedParticipants.EmployeeList = new Models.Employee.Messenger.MessengerContacts.Contacts.EmployeeList();
                    MessengerTempData.SelectedParticipants.EmployeeList.Employee = new List<Models.Employee.Messenger.MessengerContacts.Contacts.Employee>();
                }
               
            }
            _userDialog.HideLoading();
        }

        public async Task CreateGroupChat()
        {
            PromptResult GroupName = await _userDialog.PromptAsync(new PromptConfig{InputType = InputType.Name,OkText="Create Group",CancelText="Cancel", Title="Create Group" });
            CreateGroupChat createGroupChat = new CreateGroupChat();

            if (GroupName.Ok&& !string.IsNullOrWhiteSpace(GroupName.Text))
            {
                _userDialog.ShowLoading(Strings.Loading);

                chatGroupforCreation.chatGroupId = 0;
                chatGroupforCreation.comments = null;
                
                chatGroupforCreation.groupName = GroupName.Text;
                chatGroupforCreation.createdBy = 0;

                chatGroupforCreation.createdDate = (System.DateTime.Now).ToString("yyy/MM/dd HH:mm:ss");
                chatGroupforCreation.isActive = true;
                chatGroupforCreation.isDeleted = false;
                chatGroupforCreation.updatedBy = EmployeeTempData.EmployeeID;
                chatGroupforCreation.updatedDate = (System.DateTime.Now).ToString("yyy/MM/dd HH:mm:ss");
                createGroupChat.chatGroup = chatGroupforCreation;
                createGroupChat.chatUserGroup = chatUserGroups;
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(createGroupChat));
                var result =await MessengerService.CreateChatGroup(createGroupChat);
                
                //     public string comments { get; set; }
                //public bool isActive { get; set; }
                //public bool isDeleted { get; set; }
                //public int createdBy { get; set; }
                //public string createdDate { get; set; }
                //public int updatedBy { get; set; }
                //public string updatedDate { get; set; }

            }
            
        }
        public void NotEnough()
        {
            _userDialog.Alert("Please add participants to the group");
        }

        #endregion Commands
    }
}
