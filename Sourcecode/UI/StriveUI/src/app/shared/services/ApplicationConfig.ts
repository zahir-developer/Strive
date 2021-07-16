export const ApplicationConfig = {


    PaginationConfig: {
        page: 1,
        TableGridSize: 10,
        Rows: [5, 10, 25, 50, 100],
        PageSize: 10
    },

    ChecklistNotification:
    {
        MaxLength: 7
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

            },
            customerHistory: {
                order: 'ASC',
            },
            tenantSetup: {
                order: 'ASC'
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
            GiftCard: 'GiftCardCode',
            customerHistory : 'ClientId',
            tenantSetup: 'CompanyName'
        }
    },
    UploadFileType:
    {
        AdSetup: ['png', ' jpg', ' jpeg'],
        EmployeeHandbook: ['pdf', ' doc', ' docx'],
        TermsAndCondition: ['pdf']
    },

    EmailSize: {
        VendorSetup: 5,
        location: 5
    },

    UploadSize:
    {
        AdSetup: 5120,
        EmployeeHandbook: 10240,
        TermsAndCondition: 5120,
        EmployeeDocument: 10240
    },
    Enum: {
        JobType:
        {
            DetailJob: 'Detail',
            WashJob: 'Wash'
        },
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
            DetailPackage: 'Detail Package',
            GiftCard: 'Gift Card'
        },
    },
    Category: {
        documentType: 'DOCUMENTTYPE',
        all: 'ALL',
        cashRegister: 'CASHREGISTERTYPE',
        storeStatus: 'Storestatus',
        immigrationStatus: 'IMMIGRATIONSTATUS',
        liablityType: 'LIABILITYTYPE',
        documentSubType: 'DocumentSubType',
        serviceType: 'SERVICETYPE',
        LiablityDetailType: 'LIABILITYDETAILTYPE',
        CommisionType: "COMMISIONTYPE",
        paymentType: 'PAYMENTTYPE',
        paymentStatus: 'PAYMENTSTATUS',
        ClientType: 'CLIENTTYPE',
        ServiceCategory: 'ServiceCategory',
        TermsAndCondition: 'TERMSANDCONDITION',
        Gender: 'GENDER'
    },
    CodeValue: {
        EmployeeHandBook: 'EmployeeHandBook',
        CashIn: 'CashIn',
        CloseOut: 'CloseOut',
        Collision: 'Collision',
        Ads: 'Ads',
        Other: 'Other',
        TermsAndCondition: 'TermsAndCondition',
        inProgress: 'In Progress',
        Comp: 'Comp',
        Completed: 'Completed',
        Waiting: 'Waiting',
        Details: 'Details',
        Washes: 'Washes',
        additionalServices: 'Additional Services',
        adjustment: 'Adjustment',
        gender: 'Gender',
        immigrationStatus: 'ImmigrationStatus',
        liablityType: 'LiabilityType',
        liablityDetailType: 'LiabilityDetailType',
        vehicleModel: 'VehicleModel',
        vehcileMake: 'VehicleManufacturer',
        vehcileColor: 'VehicleColor',
        documentType: 'DocumentType',
        documentSubType: 'DocumentSubType',
        size: 'Size',
        jobStatus: 'JobStatus',
        serviceType: 'ServiceType',
        clientType: 'ClientType',
        paymentType: 'PaymentType',
        paymentStatus: 'PaymentStatus'
    },
    CodeValueByType: {
        ProductType: 'ProductType',
        Size: 'Size',
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
        Admin: 'Admin',
        Manager: 'Manager',
        Operator: 'Operator',
        Cashier: 'Cashier',
        Detailer: 'Detailer',
        Wash: 'Washer',
        Customer: 'Customer'
    },

    dropdownSettings: {
        singleSelection: false,
        defaultOpen: false,
        idField: 'item_id',
        textField: 'item_text',
        itemsShowLimit: 1,
        enableCheckAll: false,
        allowSearchFilter: true
    },
    refreshTime: {
        refreshTime: 5
    },

    debounceTime: {
        sec: 1000
    },

    modules: {
        admin: 'Admin',
        report: 'Report'
    }

};

