export const MessageConfig = {


   CommunicationError: 'Communication Error !',
   Mandatory: 'Please Enter Mandatory fields',
   TicketNumber : 'Ticket Number is Empty',
   locationError: 'No location assigned, Pls contact Administrator..!!!',

   Schedule:
   {
      Add: 'Schedule added successfully !',
      Update: 'Schedule updated successfully !',
      pastDates: 'New schedule is not allowed for passed dates.'
   },
   Employee:
   {
      Add: 'Employee added successfully !',
      Update: 'Employee updated successfully !',
      Delete: 'Employee deleted successfully !',
      saved: 'Employee saved successfully !',
      role: 'Current role in location cannot be removed',
      location: 'Current logged in location cannot be removed'
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
      fileSize: 'Document upload successfully !',
      Delete: 'Document deleted successfully !'
   },
   Wash:
   {
      Add: 'Wash added successfully !',
      Update: 'Wash updated successfully !',
      Delete: 'Wash deleted successfully !',
      type: "Please select valid type",
      model: "Please select valid model",
      color: "Please select valid color"
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
   },
   PayRoll:
   {
      Add: 'PayRoll added successfully !',
      Update: 'PayRoll updated successfully !',
      Delete: 'PayRoll deleted successfully !',
      Adjustment: 'Saved Successfully!',
      Process: 'PayRoll Processed successfully !',
   },
   Messenger: {
      empselect: 'Please select am employee'
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
         GiftCardAlreadyExists: 'GiftCard Already Exist !',
         Add: 'Gift Card added successfully !',
         Update: 'Gift Card updated successfully !',
         Delete: 'Gift Card deleted successfully !',
         invalidCard: 'Invalid Card Number',
         insuffBalnce: 'Insufficient Balance',
         ActivityAdd: 'Activity Added Successfully!'
      },

      CashRegister:
      {
         Add: 'CashRegister added successfully !',
         Update: 'CashRegister Updated Successfully!',
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
         },
         ServiceSetup:
         {
            Add: 'Service added successfully !',
            Update: 'Service updated successfully !',
            Delete: 'Service deleted successfully !',
         },
         MemberShipSetup:
         {
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
           FileType : 'Upload Image Only',
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
            FileSize: 'Maximum Size 5 MB',
            FileType: 'Upload DOC,DOCX,PDF Only'
         },
         TermsCondition:
         {
            Add: 'Terms & Condition added successfully !',
            Update: 'Terms & Condition updated successfully !',
            Delete: 'Terms & Condition Deleted Successfully !',
            FileType: 'Upload Pdf Only',
            FileSize: 'Maximum Size 5 MB'
         },
         Vendor:
         {
            Add: 'Vendor added successfully !',
            Update: 'Vendor updated successfully !',
            Delete: 'Vendor Deleted Successfully !',
         },
      }
   }
}
