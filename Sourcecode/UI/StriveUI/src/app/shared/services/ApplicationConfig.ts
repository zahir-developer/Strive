export const ApplicationConfig = {


    PaginationConfig: {
        page: 1,
        TableGridSize: 10,
        Rows: [5, 10, 25, 50, 100],
        PageSize: 10
    },
    
    Sorting:
    {
        SortOrder:{
            ServiceSetup:  {
                order: 'ASC',
            },
            ProductSetup:  {
                order: 'ASC',
            },
            MemberShipSetup:  {
                order: 'ASC',
            },
            VendorSetup:  {
                order: 'ASC',
            },
           checklistSetup:  {
                order: 'ASC',
            },
            TermsAndCondition:  {
                order: 'ASC',
            },
            EmployeeHandbook:  {
                order: 'ASC',
            },
            Deals:  {
                order: 'ASC',
            },
            AdSetup:  {
                order: 'ASC',
            },
            location:  {
                order: 'ASC',
            },
            Employee:  {
                order: 'ASC',
            },
            GiftCard:  {
                order: 'ASC',
            },
            Vehicle:  {
                order: 'ASC',
            },
            TimeClock:  {
                order: 'ASC',
            },
            Client:  {
                order: 'ASC',
            },
            CheckOut:  {
                order: 'ASC',
            },
            PayRoll:  {
                order: 'ASC',
            },
            Washes:  {
                order: 'ASC',
            },
           
           
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
            TermsAndCondition : 'DocumentName',
            EmployeeHandbook : 'DocumentName',
            Employee : 'FirstName',
            Vehicle : 'VehicleNumber',
            TimeClock : 'EmployeeId',
            Client: 'FirstName',
            PayRoll : 'EmployeeId',
            CheckOut : 'TicketNumber',
            Washes: 'TicketNumber',
            Detail: '',
            GiftCard: 'GiftCardCode'
 
        }
    },
    UploadFileType:
    {
        AdSetup: ['png', 'jpeg', 'jpg'],
        EmployeeHandbook: ['pdf', 'docx', 'doc'],
        TermsAndCondition: ['pdf']
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
            AdditonalServices: 'Additonal Services',
            ServiceDiscounts: 'Service Discounts',
            OutsideServices: 'Outside Services',
            WashUpcharge: 'Wash-Upcharge',
            DetailUpcharge: 'Detail-Upcharge',
            DetailCeramicUpcharge: 'Detail-CeramicUpcharge',
            AirFresheners: 'Air Fresheners',
            Discounts: 'Discounts',
            Upcharges: 'Upcharges',
        },
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
    ]

};

