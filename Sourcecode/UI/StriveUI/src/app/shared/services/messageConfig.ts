export const MessageConfig = {


   CommunicationError: 'Communication Error !',
   Mandatory: 'Please Enter Mandatory fields',
   TicketNumber: 'Ticket Number is Empty',
   locationError: 'No location assigned, Pls contact Administrator..!',
   Reset: 'Reset Successfully !',
   save: 'Saved Successfully !',

   Schedule:
   {
      Add: 'Schedule added successfully !',
      Update: 'Schedule updated successfully !',
      save: 'Schedule Saved Successfully!',
      pastDates: 'New schedule is not allowed for passed dates.',
      schedulePassDate: 'Schedule can not be added for Past Date/Time.',
      passedDateTime: 'Past Date  Should not allow to Schedule',
      sameTime: 'Can not able to schedule at the same time'
   },
   Employee:
   {
      Add: 'Employee added successfully !',
      Update: 'Employee updated successfully !',
      Delete: 'Employee deleted successfully !',
      saved: 'Employee saved successfully !',
      role: 'Current role in location cannot be removed',
      location: 'Current logged in location cannot be removed',
      hourlyRate: 'Hourly Rate value should not be 0'
   },
   Customer:
   {
      Delete: 'Record deleted successfully !',
   },
   Collision: {
      Add: 'Collision added successfully !',
      Update: 'Collision updated successfully !',
      Delete: 'Collision deleted successfully !',
   },
   Document: {
      fileRequired: 'Please Choose file to upload',
      Add: 'Document added successfully !',
      upload: 'Document upload successfully !',
      fileSize: 'Maximum File Size is 10MB',
      Delete: 'Document deleted successfully !'
   },
   Wash:
   {
      Add: 'Wash added successfully !',
      Update: 'Wash updated successfully !',
      Delete: 'Wash deleted successfully !',
      type: 'Please select valid type',
      model: 'Please select valid model',
      color: 'Please select valid color'
   },
   Detail:
   {
      Add: 'Detail added successfully !',
      Update: 'Detail updated successfully !',
      Delete: 'Detail deleted successfully !',
      BarCode: 'Please enter Barcode',
      InvalidBarCode: 'Invalid BarCode'
   },
   Client:
   {
      clientExist: 'First name, Last name, Phone number combination already exist',
      emailExist: 'Client Email Already Exist',
      Add: 'Client added successfully !',
      Update: 'Client updated successfully !',
      Delete: 'Client deleted successfully !'
   },
   Sales:
   {
      Add: 'Item added successfully !',
      Update: 'Item updated successfully !',
      UpdateGiftCrd: 'Sales Gift Card Saved successfully !',
      Delete: 'Sales deleted successfully !',
      Ticket: 'Ticket Already Added',
      InvalidTicket: 'Invalid Ticket',
      ItemDelete: 'Item deleted successfully',
      quantity: 'Please enter quantity',
      validItem: 'Please enter valid ItemName',
      creditAmount: 'Credit amount exceeds the balance amount!',
      duplicate: 'Duplicate Discount',
      discountExist: 'selected discount already applied!',
      invalidDiscount: 'Invalid Discount Service & Discount Type',
      payment: 'Add any cash/credit payment and proceed',
      total: 'Total paid amount not matching with Total amount.',
      rollback: 'Rollbacked Successfully',
      paymentComplete: 'Unable to complete payment, please try again.',
      jobDelete: 'Job Deleted Successfully',
      paymentSave: 'Payment completed successfully',
      purchasedGiftcard: 'Currently purchased giftcard can\'t be used for payment'
   },
   Messenger: {
      Message: 'Please enter a message..!!!',
      empselect: 'Please select am employee',
      add: 'Group user added successfully..!!!'

   },
   PayRoll:
   {
      Add: 'PayRoll added successfully !',
      Update: 'PayRoll updated successfully !',
      Delete: 'PayRoll deleted successfully !',
      Adjustment: 'Saved Successfully!',
      Process: 'PayRoll Processed successfully !',
   },

   checkOut:
   {
      Add: 'CheckOut action successfully !',
      Hold: 'Hold action successfully !',
      Complete: 'Completed successfully !',
      checkoutRestriction: 'Checkout allowed only for completed tickets',
      unPaidTicket: 'Checkout can be done only for paid tickets.'
   },
   Admin: {
      TimeClock:
      {
         Add: 'Employee added successfully !',
         Update: 'Employee updated successfully !',
         Delete: 'Employee record deleted successfully !',
         sunday: 'Sunday should be the start of the week!',
         weekRange: 'Only one week can be selected!',
         HourFormat: 'Out Time should be greater than In Time',
         sameDay: 'Similar Timing in same Day',
         totalHour: 'Total Hours should not be Zero.',
         totalHourNegative: 'Total Hours should not be negative'
      },
      Vehicle:
      {
         Add: 'Vehicle added successfully !',
         Update: 'Vehicle updated successfully !',
         Save: 'Vehicle saved successfully !',
         Delete: 'Vehicle deleted successfully !',
         memberShip: 'Could not Delete the Vehicle due to  Assigned the Membership'
      },

      GiftCard:
      {
         GiftCardAlreadyExists: 'Gift Card Already Exists !',
         Add: 'Gift Card added successfully !',
         Update: 'Gift Card updated successfully !',
         Delete: 'Gift Card deleted successfully !',
         invalidCard: 'Invalid Card Number',
         insuffBalnce: 'Insufficient Balance',
         ActivityAdd: 'Activity Added Successfully!'
      },

      CashRegister:
      {
         Add: 'Cash Register added successfully !',
         Update: 'Cash Register Updated Successfully!',
      },
      CloseRegister:
      {
         Add: 'Close Out Register added successfully !',
         Update: 'Close Out Register Updated Successfully!',
      },
      weather:
      {
         Communication: 'Weather Communication Error!',
         Update: 'Weather Saved Successfully!',
      },


      SystemSetup: {
         BasicSetup:
         {
            Add: 'Location added successfully !',
            Update: 'Location updated successfully !',
            Delete: 'Location deleted successfully !',
            Email: 'Maximum 5 EmailId\'s  Only Allowed',
            InvalidEmail: 'Invalid Email'
         },
         ServiceSetup:
         {
            Add: 'Service added successfully !',
            Update: 'Service updated successfully !',
            Delete: 'Service deleted successfully !',
         },
         MemberShipSetup:
         {
            MemberShipName: 'MembershipName should not be None or Unk',
            Add: 'MemberShip added successfully !',
            Update: 'MemberShip updated successfully !',
            Delete: 'MemberShip deleted successfully !',
            DeleteRestrict: 'Could not Delete the Membership  Assigned to Vehicle'
         },
         ProductSetup:
         {
            Add: 'Product added successfully !',
            Update: 'Product updated successfully !',
            Delete: 'Product deleted successfully !',
         },
         AdSetup:
         {
            Add: 'Ad Setup added successfully !',
            Update: 'Ad Setup updated successfully !',
            Delete: 'Ad Setup Deleted Successfully !',
            FileType: 'Invalid file type uploaded. ',
            FileSize: 'Maximum Size 5 MB'
         },
         BonusSetup:
         {
            Add: 'Bonus Setup added successfully !',
            Update: 'Bonus Setup updated successfully !',
            Delete: 'Bonus Setup Deleted Successfully !',

         },
         CheckList:
         {
            Add: 'Check List added successfully !',
            Update: 'Check List updated successfully !',
            Delete: 'Check List Deleted Successfully !',
            roleNameValidation: 'Role Name is Required',
            CheckListNameValidation: 'Check List Name is Required',
         },
         Deal:
         {
            Add: 'Deal added successfully !',
            Update: 'Deal updated successfully !',
            Delete: 'Deal Deleted Successfully !',
            DealLimit: 'Maximum Deal set up added'
         },
         EmployeeHandBook:
         {
            Add: 'Employee HandBook added successfully !',
            Update: 'Employee HandBook updated successfully !',
            Delete: 'Employee HandBook Deleted Successfully !',
            nameValidation: '',
            FileSize: 'Maximum file size is 10 MB',
            FileType: 'Invalid file type uploaded. '
         },
         TermsCondition:
         {
            Add: 'Terms & Condition added successfully !',
            Update: 'Terms & Condition updated successfully !',
            Delete: 'Terms & Condition Deleted Successfully !',
            FileType: 'Invalid file type uploaded ',
            FileSize: 'Maximum Size 5 MB'
         },
         Vendor:
         {
            Add: 'Vendor added successfully !',
            Update: 'Vendor updated successfully !',
            Delete: 'Vendor Deleted Successfully !',
         },
         TenantSetup: {
            Add: 'Tenant added successfully !',
            Update: 'Tenant updated successfully !',
            Email: 'Email Id Already exists.'
         }
      }
   }
};
