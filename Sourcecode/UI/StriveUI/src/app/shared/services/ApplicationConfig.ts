export const ApplicationConfig = {

    DebounceTime : {
        ServiceSetup: 1000,
            ProductSetup: 1000,
            VendorSetup: 1000,
            checklistSetup: 1000,
            MemberShipSetup: 1000,
            Deals: 1000,
            AdSetup: 1000,
            location:1000,
            TermsAndCondition: 1000,
            EmployeeHandbook: 1000,
            Employee: 1000,
            Vehicle: 1000,
            TimeClock: 1000,
            Client: 1000,
            PayRoll: 1000,
            CheckOut: 1000,
            Washes: 1000,
            GiftCard: 1000,
            customerHistory: 1000,
    },

    PaginationConfig: {
        page: 1,
        TableGridSize: 10,
        Rows: [5, 10, 25, 50, 100],
        PageSize: 10
    },

    Sorting:
    {
        SortOrder: {
            ServiceSetup: {
                order: 'ASC',
            },
            ProductSetup: {
                order: 'ASC',
            },
            MemberShipSetup: {
                order: 'ASC',
            },
            VendorSetup: {
                order: 'ASC',
            },
            checklistSetup: {
                order: 'ASC',
            },
            TermsAndCondition: {
                order: 'ASC',
            },
            EmployeeHandbook: {
                order: 'ASC',
            },
            Deals: {
                order: 'ASC',
            },
            AdSetup: {
                order: 'ASC',
            },
            location: {
                order: 'ASC',
            },
            Employee: {
                order: 'ASC',
            },
            GiftCard: {
                order: 'ASC',
            },
            Vehicle: {
                order: 'ASC',
            },
            TimeClock: {
                order: 'ASC',
            },
            Client: {
                order: 'ASC',
            },
            CheckOut: {
                order: 'ASC',
            },
            PayRoll: {
                order: 'ASC',
            },
            Washes: {
                order: 'ASC',
            },

            Detail: {
                order: 'ASC',

            }
        },

        SortBy:
        {
            ServiceSetup: 'ServiceName',
            ProductSetup: 'ProductName',
            VendorSetup: 'VendorName',
            checklistSetup: 'Name',
            MemberShipSetup: 'MembershipName',
            Deals: 'Name',
            AdSetup: 'Name',
            location: 'LocationName',
            TermsAndCondition: 'DocumentName',
            EmployeeHandbook: 'DocumentName',
            Employee: 'FirstName',
            Vehicle: 'VehicleNumber',
            TimeClock: 'EmployeeId',
            Client: 'FirstName',
            PayRoll: 'EmployeeId',
            CheckOut: 'TicketNumber',
            Washes: 'TicketNumber',
            Detail: 'TicketNumber',
            GiftCard: 'GiftCardCode'

        }
    },
    UploadFileType:
    {
        AdSetup: ['png', 'jpeg', 'jpg'],
        EmployeeHandbook: ['pdf', 'docx', 'doc'],
        TermsAndCondition: ['pdf']
    },
   
    EmailSize: {
VendorSetup : 5
    },
    
    UploadSize:
    {
        AdSetup: 5120,
        EmployeeHandbook: 5120,
        TermsAndCondition: 5120
    },
    Enum: {
        ServiceType:
        {
            WashPackage: 'Wash Package',
            AdditonalServices: 'Additional Services',
            ServiceDiscounts: 'Service Discounts',
            OutsideServices: 'Outside Services',
            WashUpcharge: 'Wash-Upcharge',
            DetailUpcharge: 'Detail-Upcharge',
            DetailCeramicUpcharge: 'Detail-CeramicUpcharge',
            AirFresheners: 'Air Fresheners',
            Discounts: 'Discounts',
            Upcharges: 'Upcharges',
            Details :'Details',
            DetailPackage: 'Detail Package'
        },
    },
Category : {
documentType : 'DOCUMENTTYPE',
all : 'ALL',
cashRegister: 'CASHREGISTERTYPE',
storeStatus: 'Storestatus',
immigrationStatus: 'IMMIGRATIONSTATUS',
liablityType: 'LIABILITYTYPE',
documentSubType:'DocumentSubType',
serviceType: 'SERVICETYPE',
LiablityDetailType: 'LIABILITYDETAILTYPE',
CommisionType: "COMMISIONTYPE",
paymentType : 'PAYMENTTYPE',
paymentStatus:'PAYMENTSTATUS',
ClientType: 'CLIENTTYPE'
},
CodeValue :{
    EmployeeHandBook: 'EmployeeHandBook',
    CashIn: 'CashIn',
    CloseOut: 'CloseOut',
    Collision: 'Collision',
    Ads: 'Ads',
    Other:'Other',
    TermsAndCondition: 'TermsAndCondition',
    inProgress: 'In Progress',
    Comp : 'Comp',
    Completed : 'Completed',
    Waiting : 'Waiting',
    Details : 'Details',
    Washes:'Washes',
    additionalServices: 'Additional Services',
},
CodeValueByType : {
    ProductType: 'ProductType',
    Size : 'Size',
    serviceType: 'ServiceType',
    JobStatus: 'JobStatus'
},
    dealTimePeriods: [
        {
            name: 'Monthly',
            value: 1
        },
        {
            name: 'Yearly',
            value: 2
        },
        {
            name: 'Custom',
            value: 3
        },
    ],

    dealList: [
        {
            name: 'Buy 10, Get next FREE', value: 'Buy 10, Get next FREE'
        },
        {
            name: 'Bounce back coupon', value: 'Bounce back coupon'
        }
    ],

    storestatus: {
        open: 'Open',
        closedforWeather: 'ClosedforWeather',
        closed: 'Closed'
    },

    PaymentType:
    {

        Account: 'Account',
        Card: 'Card',
        Cash: 'Cash',
        Check: 'Check',
        Discount: 'Discount',
        Payroll: 'From Payroll',
        GiftCard: 'GiftCard',
        Membership: 'Membership',
        OnlinePayment: 'OnlinePayment',
        Tips: 'Tips'
    },
Roles: {
Admin:'Admin',
Manager: 'Manager',
Operator: 'Operator',
Cashier : 'Cashier',
Detailer : 'Detailer',
Wash :'Wash',
Client :'Client'
}
};

