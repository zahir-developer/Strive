export const ApplicationConfig = {


    PaginationConfig: {
        page: 1,
        TableGridSize: 10,
        Rows: [5, 10, 25, 50, 100],
        PageSize: 10
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

