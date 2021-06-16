export const MessageConfig = {


   CommunicationError: 'Communication Error !',
   Mandatory: 'Please enter required fields',
   TicketNumber: 'Ticket Number is invalid',
   locationError: 'No location assigned, Please contact Administrator ',
   Reset: 'Reset Successfully !',
   save: 'Saved Successfully !',

   Schedule:
   {
      Add: 'Schedule added successfully !',
      Update: 'Schedule updated successfully !',
      save: 'Schedule Saved Successfully!',
      pastDates: 'New schedule is not allowed for past dates.',
      schedulePassDate: 'Schedule cannot be added for Past Date/Time.',
      passedDateTime: 'Schedule cannot be created for past dates',
      sameTime: 'Can not able to schedule at the same time'
   },
   Employee:
   {
      Add: 'Employee added successfully !',
      Update: 'Employee updated successfully !',
      Delete: 'Employee deleted successfully !',
      saved: 'Employee saved successfully !',
      role: 'Currently loggedIn Role cannot be removed',
      location: 'You are currently logged in to this location and the same cannot be unassigned.',
      hourlyRate: 'Hourly Rate should not be 0',
      hourlyEmployeeLocation: 'Please select hourly wash rate location.'
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
      fileRequired: 'Please select a file to upload',
      Add: 'Document added successfully !',
      upload: 'Document uploaded successfully !',
      fileSize: 'Please upload a file of size less than 10 MB',
      Delete: 'Document deleted successfully !'
   },
   Wash:
   {
      Add: 'Wash Ticket added successfully !',
      Update: 'Wash Ticket updated successfully !',
      Delete: 'Wash  Ticket deleted successfully !',
      type: 'Please select a valid type',
      model: 'Please select a valid model',
      color: 'Please select a valid color'
   },
   Detail:
   {
      Add: 'Detail Ticket added successfully !',
      Update: 'Detail Ticket updated successfully !',
      Delete: 'Detail Ticket deleted successfully !',
      BarCode: 'Please enter the Barcode',
      InvalidBarCode: 'Invalid BarCode'
   },
   Client:
   {
      clientExist: 'Client with the same First Name, Last Name & Phone # already exists!',
      emailExist: 'Client Email already exists',
      Add: 'Client added successfully !',
      Update: 'Client updated successfully !',
      Delete: 'Client deleted successfully !'
   },
   Sales:
   {
      Add: 'Item added successfully !',
      Update: 'Item updated successfully !',
      UpdateGiftCrd: 'Sales Gift Card Saved successfully !',
      Delete: 'Sales Ticket deleted successfully !',
      Ticket: 'Ticket already exists !', 
      InvalidTicket: 'Invalid Ticket No.',
      ItemDelete: 'Item deleted successfully',
      quantity: 'Please enter quantity',
      validItem: 'Please enter valid Item name',
      creditAmount: 'Credit amount exceeds the balance amount',
      duplicate: 'Duplicate Discount',
      discountExist: 'Discount already applied!',
      invalidDiscount: 'Invalid Discount Service/Type',
      payment: 'Add payment method to proceed', 
      total: 'Total paid amount does not match with Total due amount.',
      rollback: 'Rollback completed successfully',
      paymentComplete: 'Unable to complete payment, please try again.',
      jobDelete: 'Ticket Deleted Successfully',
      paymentSave: 'Payment completed successfully',
      purchasedGiftcard: 'Currently purchased giftcard can\'t be used for payment',
      alreadyAdded: 'GiftCard already added'
   },
   Messenger: {
      Message: 'Please enter a message!',
      empselect: 'Please select an Employee.',
      add: 'Group user added successfully.'

   },
   PayRoll:
   {
      Update: 'PayRoll updated successfully !',
      Delete: 'PayRoll deleted successfully !',
      Adjustment: 'Adjustment updated successfully !', 
      Process: 'PayRoll Processed successfully !',
      SelectLocation: 'Please select location !'
   },

   checkOut:
   {
      Add: 'Vehicle has been CheckedOut successfully !',
      Hold: 'Vehicle is on Hold !',
      Complete: 'Services have been completed successfully !',
      checkoutRestriction: 'Only Comleted ticket can be Checkedout !',
      unPaidTicket: 'Only Paid ticket can be Checkedout !'
   },
   Admin: {
      TimeClock:
      {
         Add: 'Employee added successfully !',
         Update: 'Employee updated successfully !',
         Delete: 'Employee deleted successfully!',
         sunday: 'Sunday should be the start of the week!',
         weekRange: 'Only one week can be selected!',
         HourFormat: 'Out-Time should be greater than In-Time',
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
         memberShip: 'Vehicle cannot be deleted, Vehicle has a active membership.',
         membershipDiscountAvailable: 'Membership discount available !',
         membershipDiscountNotAvailable: 'No membership discount available !'
      },

      GiftCard:
      {
         GiftCardAlreadyExists: 'Gift Card already exists !',
         Add: 'Gift Card added successfully !',
         Update: 'Gift Card updated successfully !',
         Delete: 'Gift Card deleted successfully !',
         invalidCard: 'Invalid Gift Card Number',
         insuffBalnce: 'Insufficient Balance',
         ActivityAdd: 'Activity Added Successfully!',
         redeemAmount: 'RedeemAmount amunt is greether than available amount'
      },

      CashRegister:
      {
         Add: 'Cash Register added successfully !',
         Update: 'Cash Register updated successfully!',
      },
      CloseRegister:
      {
         Add: 'Close Out Register saved successfully !',
         Update: 'Close Out Register updated successfully!',
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
            Email: 'Only five Emailids can be added !',
            InvalidEmail: 'Please provide valid email !'
         },
         ServiceSetup:
         {
            Add: 'Service added successfully !',
            Update: 'Service updated successfully !',
            Delete: 'Service deleted successfully !',
         },
         MemberShipSetup:
         {
            MemberShipName: 'Membership Name should not be None or Unk',
            Add: 'Membership added successfully !',
            Update: 'Membership updated successfully !',
            Delete: 'Membership deleted successfully !',
            DeleteRestrict:  'Membership cannot be deleted, it is associated with a vehicle !'
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
            FileSize: 'Please upload a file of size less than 5 MB'
         },
         BonusSetup:
         {
            Add: 'Bonus Setup added successfully !',
            Update: 'Bonus Setup updated successfully !',
            Delete: 'Bonus Setup deleted successfully !',
            washesMsg: 'Wash count does not match between the entries(min/max) !'
         },
         CheckList:
         {
            Add: 'Check List added successfully !',
            Update: 'Check List updated successfully !',
            Delete: 'Check List deleted successfully !',
            roleNameValidation: 'Role Name is required',
            CheckListNameValidation: 'Check List Name is required',
         },
         Deal:
         {
            Add: 'Deal saved successfully !',
            Update: 'Deal updated successfully !',
            Delete: 'Deal deleted Successfully !',
            DealLimit: 'Only two deals can be added' 
         },
         EmployeeHandBook:
         {
            Add: 'Employee HandBook added successfully !',
            Update: 'Employee HandBook updated successfully !',
            Delete: 'Employee HandBook deleted successfully !',
            nameValidation: '',
            FileSize: 'Please upload a file of size less than 10 MB',
            FileType: 'Invalid file type uploaded. '
         },
         TermsCondition:
         {
            Add: 'Terms & Conditions document added successfully !',
            Update: 'Terms & Conditions document updated successfully !',
            Delete: 'Terms & Conditions document deleted successfully !',
            FileType: 'Invalid file type uploaded ',
            FileSize: 'Please upload a file of size less than 5 MB'
         },
         Vendor:
         {
            Add: 'Vendor added successfully !',
            Update: 'Vendor updated successfully !',
            Delete: 'Vendor deleted successfully !',
         },
         TenantSetup: {
            Add: 'Tenant saved successfully !',
            Update: 'Tenant updated successfully !',
            Email: 'Email already exists.'
         }
      }
   }
};
