

export const UrlConfig = {
  Auth:
  {
    login: `Auth/Login`,
    refreshToken: `Auth/Refresh`,
    getOtpCode: `Auth/SendOTP/`,
    verifyOtp: `Auth/VerfiyOTP/`,
    resetPassword: `Auth/ResetPassword`,

  },
  AdSetup: {
    getadSetup: `Admin/AdSetup/GetAll`,
    insertadSetup: `Admin/AdSetup/Add`,
    updateadSetup: `Admin/AdSetup/Update`,
    deleteadSetup: `Admin/AdSetup/Delete`,
    getByIdadSetup: `Admin/AdSetup/GetById`

  },
  DealSetup: {
    getDealSetupSetup: `Admin/DealSetup/GetAllDeals`,
    insertDealSetup: `Admin/DealSetup/Add`,
    updatedeals: `Admin/DealSetup/Update`
  },
  cashRegister: {
    getCashRegister: `Admin/CashRegister/Get`,
    saveCashRegister: `Admin/CashRegister/Save`,
    getTips : `Admin/CashRegister/GetTips`,
  },
  common: {
    getCode: `Admin/Common/GetCodesByCategory/`,
    getJobStatus: `Admin/Common/GetCodesByCategory/`,
    getPaymentStatus: `Admin/Common/GetCodesByCategory/`,
    cityList: `Admin/Common/GetCodesByCategory/`,
    cityByStateId: `Admin/Common/GetCityByStateId/`,
    modelByMakeId: `Admin/Common/GetModelById/`,
    getUpchargeType : `Admin/Common/GetUpchargeType/`,
    getDropdownValue: `Admin/Common/GetCodesByCategory/`,
    stateList: `Admin/Common/StateList`,
    makeList: `Admin/Common/GetAllMake`,

    countryList: `Admin/Common/CountryList`,
    getTicketNumber: `Admin/Common/GetTicketNumber/`,

  },
  collision: {
    getVechileList: `Admin/Collision/GetVehicleListByClientId/`,
    deleteCollision: `Admin/Collision/Delete/`,
    getDetailCollision: `Admin/Collision/GetCollisionById/`,
    saveCollision: `Admin/Collision/Add`,
    updateCollision: `Admin/Collision/Update`,
    getAllCollision: `Admin/Collision/GetCollisionByEmpId/`,

  },

  Checklist: {
    getCheckList: `Admin/Checklist/GetChecklist`,
    addCheckList: `Admin/Checklist/Add`,
    DeleteCheckList: `Admin/Checklist/Delete`,
    getById : `Admin/Checklist/GetChecklistById`,
    updateChecklist: `Admin/Checklist/Update`
  },
  checkOut: {
    getUncheckedVehicleDetails: `Admin/Checkout/GetAllCheckoutDetails`,
    checkoutVehicle: `Admin/Checkout/UpdateCheckoutDetails`,
    holdoutVehicle: `Admin/Checkout/UpdateJobStatusHold`,
    completedVehicle: `Admin/Checkout/UpdateJobStatusComplete`,
  },
    client: {
    clientEmailCheck: `Admin/Client/ClientEmailExist`,
    getClient: `Admin/Client/GetAll`,
    sameClientName: `Admin/Client/IsClientName`,
    getClientByName: `Admin/Client/GetClientSearch`,
    getClientName: `Admin/Client/GetAllName/`,
    getClientScore: `Admin/Client/GetClientCodes`,
    updateAccountBalance: `Admin/Client/UpdateAccountBalance`,
    getStatementByClientId: `Admin/Client/GetStatementByClientId/`,
    getHistoryByClientId: `Admin/Client/GetHistoryByClientId/`,
    addClient: `Admin/Client/InsertClientDetails`,
    updateClient: `Admin/Client/UpdateClientVehicle`,
    deleteClient: `Admin/Client/`,
    getClientById: `Admin/Client/GetClientById/`,
    getClientList: `Admin/Client/EmailBlast/`,
    getCSVClientList: `Admin/Client/EmailBlastCSV/`,
  },
  details: {
    addDetail: `Admin/Details/AddDetails`,
    getPastClientNotesById: `Admin/Details/GetPastClientNotesById/`,
    updateDetail: `Admin/Details/UpdateDetails`,
    getDetailById: `Admin/Details/GetDetailsById/`,
    getAllBayById: `Admin/Details/GetAllBayById/`,
    getScheduleDetailsByDate: `Admin/Details/GetBaySchedulesDetails`,
    deleteDetail: `Admin/Details/Delete`,
    getJobType: `Admin/Details/GetJobType`,
    getTodayDateScheduleList: `Admin/Details/GetAllDetails`,
    saveEmployeeWithService: `Admin/Details/AddServiceEmployee`,
    getDetailScheduleStatus: `Admin/Details/GetDetailScheduleStatus`

  },


  employee: {
    getEmployeeDetail: `Admin/Employee/GetEmployeeById`,
    deleteEmployee: `Admin/Employee/Delete/`,
    getAllRoles: `Admin/Employee/GetAllRoles`,
    searchEmployee: `Admin/Employee/GetAllEmployeeDetail/`,
    getEmployees: `Admin/Employee/GetAllEmplloyeeList`,
    getAllEmployeeList: `Admin/Employee/GetAllEmployeeDetail`,
    saveEmployees: `Admin/Employee/Add`,
    getRoleByEmpId: `Admin/Employee/GetEmployeeRoleById/`,
    updateEmployee: `Admin/Employee/Update`,
    getAllEmployeeDocument: `Admin/Document/GetEmployeeDocument/`,
    getEmployeeDocumentById: `Admin/Document/GetEmployeeDocumentById/`,
    deleteEmployeeDocument: `Admin/Document/DeleteEmployeeDocument/`,
    getAllEmployeeName: `Admin/Employee/GetAllEmployeeName`,
    getEmployeeHourlyRateById: `Admin/Employee/GetEmployeeHourlyRateById`,
    validateEmail: `Admin/Common/GetEmailIdExist/`
  },

  giftCard: {
    giftCardExist: 'Admin/GiftCard/IsGiftCardExist/',
    getAllGiftCard: `Admin/GiftCard/GetAllGiftCard`,
    getAllGiftCardHistory: `Admin/GiftCard/GetAllGiftCardHistory/`,
    getGiftCard: `Admin/GiftCard/GetGiftCard/`,
    saveGiftCard: `Admin/GiftCard/AddGiftCard`,
    updateStatus: `Admin/GiftCard/ChangeStatus`,
    addCardHistory: `Admin/GiftCard/AddGiftCardHistory`,
    updateBalance: `Admin/GiftCard/UpdateGiftCardBalance`,
    getBalance: `Admin/GiftCard/GetGiftCardBalance/`,
    getGiftCardHistoryByTicketNmber: `Admin/GiftCard/GetGiftCardBalanceHistory/`,
    deleteGiftCard: `Admin/GiftCard/Delete`
  },
  location: {
    getLocation: `Admin/Location/GetAll`,
    saveLocation: `Admin/Location/Add`,
    deleteLocation: `Admin/Location/Delete`,
    updateLocation: `Admin/Location/Update`,
    getLocationById: `Admin/Location/GetById`,
    getLocationSearch: `Admin/Location/GetLocationSearch`,
    getAllLocationName: `Admin/Location/GetAllLocationName`,
    getMerchantDetails: `Admin/Location/GetMerchantDetails`,
  },
  MembershipSetup: {
    getMembershipByName: `Admin/MembershipSetup/GetAllMembershipName`,

   
    getMembershipById: `Admin/MembershipSetup/GetMembershipAndServiceByMembershipId/`,
    addMembership: `Admin/MembershipSetup/Add`,
    updateMembership: `Admin/MembershipSetup/Update`,
    deleteMembership: `Admin/MembershipSetup/Delete/`,
    getMembershipService: `Admin/ServiceSetup/GetService`,
    membershipSearch: `Admin/MembershipSetup/GetMembershipSearch`,
    getAllMembership: `Admin/MembershipSetup/GetAll`,
    getAllMembershipName: `Admin/MembershipSetup/GetAllMembershipName`,
    deleteRestrictionMembershipByVehicleId: `Admin/MembershipSetup/GetVehicleMembershipByMembershipId/`,
  },

  Messenger:
  {
    getEmployeeList: `Admin/Messenger/GetChatEmployeeList/`,
    sendMessage: `Admin/Messenger/SendChatMessage`,
    updateChatCommunicationDetail: `Admin/Messenger/ChatCommunication`,
    getChatMessage: `Admin/Messenger/GetChatMessage`,
    createGroup: `Admin/Messenger/CreateChatGroup`,
    getUnReadMessageCount: `Admin/Messenger/GetUnReadMessageCount/`,
    getGroupMemberList: `Admin/Messenger/GetChatGroupEmployeelist/`,
    deleteGroupUser: `Admin/Messenger/DeleteChatGroupUser/`,
    changeUnreadMessageState: `Admin/Messenger/ChangeUnreadMessageState`,
    getAllEmployeeName: `Admin/Messenger/getAllEmployeeName`
  },
  ServiceSetup: {
    getServiceSetup: `Admin/ServiceSetup/GetAll`,
    getServiceType: `Admin/ServiceSetup/GetAllServiceType`,
    getService: `Admin/ServiceSetup/GetService`,
    addServiceSetup: `Admin/ServiceSetup/Add`,
    updateServiceSetup: `Admin/ServiceSetup/Update`,
    deleteServiceSetup: `Admin/ServiceSetup/Delete`,
    getServiceSetupById: `Admin/ServiceSetup/GetServiceById`,
    getServiceSearch: `Admin/ServiceSetup/GetServiceSearch`,
    getAllServiceDetail: `Admin/ServiceSetup/GetAllServiceDetail`
  },
  payRoll: {
    getPayroll: `Admin/PayRoll/GetPayroll`,
    updateAdjustment: `Admin/PayRoll/UpdateEmployeeAdjustment`,
    editRestrict: `Admin/PayRoll/GetPayrollProcessStatus`,
    addPayrollProcess: `Admin/PayRoll/AddPayRollProcess`,
  },
  product: {
    getProduct: `Admin/Product/GetAll`,
    getProductById: `Admin/Product/GetProductById`,
    getProductDetailById: `Admin/Product/GetProductDetailById`,
    addProduct: `Admin/Product/Add`,
    updateProduct: `Admin/Product/Update`,
    deleteProduct: `Admin/Product/Delete`
  },
  reports: {
    getMonthlySalesReport: `Admin/Report/GetMonthlySalesReport`,
    getCustomerSummaryReport: `Admin/Report/GetCustomerSummaryReport`,
    getCustomerMonthlyDetailReport: `Admin/Report/GetCustomerMonthlyDetailedReport`,
    getDailyTipReport: ``,
    getMonthlyTipReport: ``,
    getDailyStatusReport: `Admin/Report/DailyStatusReport`,
    getDailyStatusWashReport: `Admin/Report/DailyStatusInfo`,
    EODExcelReport: `Admin/Report/EODSalesExport`,
    dailyStatusExcelReport: `Admin/Report/DailyStatusExport`,
    getMonthlyDailyTipReport: `Admin/Report/MonthlyDailyTipReport`,
    getDetailStatusInfo: `Admin/Report/DailyStatusInfo`,
    getDailyClockDetail: `Admin/TimeClock/GetTimeClockEmployeeHourDetails`,
    getCashRegister: `Admin/CashRegister/Get`,
    getMonthlyMoneyOwnedReport: `Admin/Report/GetMonthlyMoneyOwnedReport`,
    getEodSaleReport: `Admin/Report/EODSalesReport`,
    getTimeClockEmpHoursDetail: `Admin/TimeClock/GetTimeClockEmployeeHourDetails`,
    getDailySalesReport: `Admin/Report/DailySalesReport`,
    getHourlyWashReport: `Admin/Report/GetHourlyWashReport`
  },
  sales: {
    addListItem: `Admin/Sales/AddListItem`,
    updateListItem: `Admin/Sales/UpdateListItem`,
    updateItem: `Admin/Sales/UpdateItem`,
    addPayment: `Admin/Sales/AddPayment`,
    deleteJob: `Admin/Sales/DeleteJob`,
    rollbackTransaction: `Admin/Sales/RollBackPayment`,
    deleteTransaction: `Admin/Sales/DeleteTransactions`,
    getAccountDetails: `Admin/Sales/GetAccountDetails`,
    getServiceAndProduct: `Admin/Sales/GetAllServiceAndProductList/`,
    getItemByTicketNumber: `Admin/Sales/GetScheduleByTicketNumber`,
    deleteItemById: `Admin/Sales/DeleteItemById`,
    updateProductObj: `Admin/Sales/SaveProductItem`,
    AddCreditCardPayment: `Payment/PaymentGateway/TrasactionWithUserFields`,
    getTicketsByPaymentId: `Admin/Sales/GetTicketsByPaymentId/`
  },
  dashboard: {
    getDashboardLocation: `Admin/Location/GetAll`,
    getTodayScheduleList: `Admin/Details/GetAllDetails`,
    getDashboardStatistics: `Admin/Dashboard/GetDashboardStatistics`,
    getAvailablilityScheduleTime: `Admin/Dashboard/GetAvailablilityScheduleTime`,
    getAllLocationWashTime : `Admin/Washes/GetAllLocationWashTime/`,
  },
  dashboardStatice: {
    getDashboardStatistics: `Admin/DashboardStatistics/GetDashboardStatisticsForLocationId/`,

  },

  bonusSetup: {
    saveBonus: `Admin/BonusSetup/Add`,
    getBonusList: `Admin/BonusSetup/GetBonus`,
    editBonus: `Admin/BonusSetup/Update`
  },
  document: {
    addDocument: `Admin/Document/AddDocument`,
    getDocument: `Admin/Document/GetDocument/`,
    getAllDocument: `Admin/Document/GetAllDocument/`,
    deleteDocumentById: `Admin/Document/DeleteDocumentById/`,
    deleteDocument: `Admin/Document/DeleteDocument/`,
    getDocumentById: `Admin/Document/GetDocumentById/`,
    uploadDocument: `Admin/Document/SaveEmployeeDocument`,
    updateDocument: `Admin/Document/UpdateDocument`,

  },
  schedule: {
    addSchedule: `Admin/Schedule/ScheduleSave`,
    getSchedule: `Admin/Schedule/GetSchedule`,
    deleteSchedule: `Admin/Schedule/DeleteSchedule`,
    getScheduleById: `Admin/Schedule/GetScheduleById`,
    getScheduleAndForcasted: `Admin/Schedule/GetScheduleAndForcasted`
  },
  vendor: {
    getALLVendorName: `Admin/Vendor/GetAllVendorName`,
    getVendor: `Admin/Vendor/GetAll`,
    saveVendor: `Admin/Vendor/Add`,
    updateVendor: `Admin/Vendor/Update`,
    deleteVendor: `Admin/Vendor/Delete/`,
    getVendorById: `Admin/Vendor/GetVendorById/`,
    getVendorSearch: `Admin/Vendor/GetVendorSearch`


  },
  vehicle: {
    getAllVehicle: `Admin/Vehicle/GetAll`,
    updateVehicle: `Admin/Vehicle/SaveClientVehicleMembership`,
    deleteVehicle: `Admin/Vehicle/Delete`,
    getMembershipByVehicle: `Admin/Vehicle/GetVehicleMembershipDetailsByVehicleId`,
    getVehicleMembershipDetailsByVehicleId: `Admin/Vehicle/GetVehicleMembershipDetailsByVehicleId`,
    getVehicleByClientId: `Admin/Vehicle/GetVehicleByClientId`,
    getVehicleCodes: `Admin/Vehicle/GetVehicleCodes`,
    getVehicleById: `Admin/Vehicle/GetVehicleId`,
    getVehicleMembership: `Admin/Vehicle/GetVehicleMembership`,
    addVehicle: 'Admin/Vehicle/AddVehicle',
    getAllVehicleThumbnail: `Admin/Vehicle/GetAllVehicleThumbnail/`,
    getVehicleImageById: `Admin/Vehicle/GetVehicleImageById/`,
    getMembershipDiscountStatus: `Admin/Vehicle/GetMembershipDiscountStatus/`
  },
  washes: {
    getAllWash: `Admin/Washes/GetAllWashes`,
    getWashById: `Admin/Washes/GetWashTimeDetail/`,
    addWash: `Admin/Washes/AddWashTime`,
    updateWash: `Admin/Washes/UpdateWashTime`,
    deleteWash: `Admin/washes/Delete`,
    getDashBoardCount: `Admin/Washes/DashboardCount`,
    getByBarcode: `Admin/Washes/GetByBarCode/`,
    getWashTimeByLocationId: `Admin/Washes/GetWashTimeByLocationId`
  },
  weather: {
    getTargetBusinessData: `Admin/Weather/GetWeatherPrediction/`,
    getWeather: `Admin/Weather/GetWeatherData/`,
    saveWeather: `Admin/Weather/SaveWeatherPrediction`,

  },

  whiteLabelling: {
    getAllWhiteLabelDetail: `Admin/WhiteLabelling/GetAll`,
    addWhiteLabelDetail: `Admin/WhiteLabelling/Add`,
    updateWhiteLabelDetail: `Admin/WhiteLabelling/Update`,
    saveCustomColor: `Admin/WhiteLabelling/Save`,
    uploadWhiteLabel: `Admin/WhiteLabelling/Update`,
  },

  timeClock: {
    getTimeClockWeekDetails: `Admin/TimeClock/GetTimeClockWeekDetails`,
    saveTimeClock: `Admin/TimeClock/Save`,
    getTimeClockEmployeeDetails: `Admin/TimeClock/GetTimeClockEmployeeDetails`,
    deleteTimeClockEmployee: `Admin/TimeClock/DeleteTimeClockEmployee`,
    getClockedInDetailer: `Admin/TimeClock/GetClockedInDetailer`
  },

  customerHistory: {
    getCustomerHistory: `Admin/Checkout/GetCustomerHistory`
  },

  tenantSetup: {
    getModuleList: `Admin/Tenant/AllModule`,
    addTenantSetup: `Admin/Tenant/CreateTenant`,
    getTenantSetupList: `Admin/Tenant/AllTenant`,
    getTenantDetailById: `Admin/Tenant/TenantById/`,
    updateTenant: `Admin/Tenant/UpdateTenant`,
    getStateList: `Admin/Tenant/StateList`,
    getCityByStateId: `Admin/Tenant/GetCityByStateId/`,
    getMaxLocationCount :'Admin/Tenant/GetLocationMaxLimit/'
  },

  paymentGateway: {
    paymentAuth: `Payroll/PaymentGateway/Auth`,
    paymentCapture: `Payroll/PaymentGateway/Capture`
  }
};
