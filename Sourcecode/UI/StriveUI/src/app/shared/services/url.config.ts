

export const UrlConfig = {
  totalUrl:
  {
    login: `Auth/Login`,
    getCode: `Admin/Common/GetCodesByCategory/`,
    getEmployees: `Admin/Employee/GetAllEmplloyeeList`,
    saveEmployees: `Admin/Employee/Add`,
    refreshToken: `Admin/Refresh`,
    getLocation: `Admin/Location/GetAll`,
    saveLocation: `Admin/Location/Add`,
    deleteLocation: `Admin/Location/Delete`,
    updateLocation: `Admin/Location/Update`,
    getLocationById: `Admin/Location/GetById`,
    getServiceSetup: `Admin/ServiceSetup/GetAll`,
    getServiceType: `Admin/ServiceSetup/GetAllServiceType`,
    getService: `Admin/ServiceSetup/GetService`,
    addServiceSetup: `Admin/ServiceSetup/Add`,
    updateServiceSetup: `Admin/ServiceSetup/Update`,
    deleteServiceSetup: `Admin/ServiceSetup/Delete`,
    getServiceSetupById: `Admin/ServiceSetup/GetServiceById`,
    getProduct: `Admin/Product/GetAll`,
    getProductById: `Admin/Product/GetProductById`,
    addProduct: `Admin/Product/Add`,
    updateProduct: `Admin/Product/Update`,
    deleteProduct: `Admin/Product/Delete`,
    getVendor: `Admin/Vendor/GetAll`,
    saveVendor: `Admin/Vendor/Add`,
    updateVendor: `Admin/Vendor/Update`,
    deleteVendor: `Admin/Vendor/Delete/`,
    getVendorById: `Admin/Vendor/GetVendorById/`,
    getCashRegister: `Admin/CashRegister/Get`,
    saveCashRegister: `Admin/CashRegister/Save`,
    stateList: `Admin/Common/StateList`,
    countryList: `Admin/Common/CountryList`,
    getEmployeeDetail: `Admin/Employee/GetEmployeeById`,
    deleteEmployee: `Admin/Employee/Delete/`,
    getAllRoles: `Admin/Employee/GetAllRoles`,
    getDropdownValue: `Admin/Common/GetCodesByCategory/`,
    getOtpCode: `Auth/SendOTP/`,
    uploadDocument: `Admin/Document/SaveEmployeeDocument`,
    getAllDocument: `Admin/Document/GetEmployeeDocument/`,
    getDocumentById: `Admin/Document/GetEmployeeDocumentById/`,
    getWeather: `Admin/Weather/GetWeatherData/`,
    saveWeather: `Admin/Weather/SaveWeatherPrediction`,
    verifyOtp: `Auth/VerfiyOTP/`,
    resetPassword: `Auth/ResetPassword`,
    getAllCollision: `Admin/Collision/GetCollisionByEmpId/`,
    getTargetBusinessData: `Admin/Weather/GetWeatherPrediction/`,
    getClient: `Admin/Client/GetAll`,
    addClient: `Admin/Client/InsertClientDetails`,
    updateClient: `Admin/Client/UpdateClientVehicle`,
    deleteClient: `Admin/Client/`,
    getClientById: `Admin/Client/GetClientById/`,
    getAllVehicle: `Admin/Vehicle/GetAll`,
    deleteDocument: `Admin/Document/DeleteEmployeeDocument/`,
    updateVehicle: `Admin/Vehicle/SaveClientVehicleMembership`,
    deleteVehicle: `Admin/Vehicle/Delete`,
    getVehicleByClientId: `Admin/Vehicle/GetVehicleByClientId`,
    deleteCollision: `Admin/Collision/Delete/`,
    getDetailCollision: `Admin/Collision/GetCollisionById/`,
    saveCollision: `Admin/Collision/Add`,
    getVehicleCodes: `Admin/Vehicle/GetVehicleCodes`,
    getVehicleById: `Admin/Vehicle/GetVehicleId`,
    getVehicleMembership: `Admin/Vehicle/GetVehicleMembership`,
    updateCollision: `Admin/Collision/Update`,
    getAllGiftCard: `Admin/GiftCard/GetAllGiftCard/`,
    getAllGiftCardHistory: `Admin/GiftCard/GetAllGiftCardHistory/`,
    getGiftCard: `Admin/GiftCard/GetGiftCard/`,
    updateEmployee: `Admin/Employee/Update`,
    saveGiftCard: `Admin/GiftCard/AddGiftCard`,
    updateStatus: `Admin/GiftCard/ChangeStatus`,
    getAllWash: `Admin/Washes/GetAllWashes/`,
    getWashById: `Admin/Washes/GetWashTimeDetail/`,
    addWash: `Admin/Washes/AddWashTime`,
    updateWash: `Admin/Washes/UpdateWashTime`,
    deleteWash: `Admin/washes/Delete`,
    searchEmployee: `Admin/Employee/GetAllEmployeeDetail/`,
    getDashBoardCount: `Admin/Washes/DashboardCount`,
    addCardHistory: `Admin/GiftCard/AddGiftCardHistory`,
    getByBarcode: `Admin/Washes/GetByBarCode/`,
    getAllMembership: `Admin/MembershipSetup/GetAll`,
    getMembershipService: `Admin/ServiceSetup/GetService`,
    getMembershipById: `Admin/MembershipSetup/GetMembershipAndServiceByMembershipId/`,
    addMembership: `Admin/MembershipSetup/Add`,
    updateMembership: `Admin/MembershipSetup/Update`,
    deleteMembership: `Admin/MembershipSetup/Delete/`,
    getClientByName: `Admin/Client/GetClientSearch`,
    getClientScore: `Admin/Client/GetClientCodes`,
    updateBalance: `Admin/GiftCard/UpdateGiftCardBalance`,
    getBalance: `Admin/GiftCard/GetGiftCardBalance/`,
    addSchedule: `Admin/Schedule/ScheduleSave`,
    getSchedule: `Admin/Schedule/GetSchedule`,
    deleteSchedule: `Admin/Schedule/DeleteSchedule`,
    getScheduleById: `Admin/Schedule/GetScheduleById`,
    getLocationSearch: `Admin/Location/GetLocationSearch`,
    getStatementByClientId: `Admin/Client/GetStatementByClientId/`,
    getHistoryByClientId: `Admin/Client/GetHistoryByClientId/`,
    getServiceSearch: `Admin/ServiceSetup/GetServiceSearch`,
    getProductSearch: `Admin/Product/GetProductSearch`,
    getVendorSearch: `Admin/Vendor/GetVendorSearch`,
    getTicketNumber: `Admin/Washes/GetTicketNumber`,
    getMembershipByVehicle: `Admin/Vehicle/GetVehicleMembershipDetailsByVehicleId`,
    getVechileList: `Admin/Collision/GetVehicleListByClientId/`,
    getVehicleMembershipDetailsByVehicleId: `Admin/Vehicle/GetVehicleMembershipDetailsByVehicleId`,
    cityList: `Admin/Common/GetCodesByCategory/`,
    addDetail: `Admin/Details/AddDetails`,
    updateDetail: `Admin/Details/UpdateDetails`,
    getDetailById: `Admin/Details/GetDetailsById/`,
    getAllBayById: `Admin/Details/GetAllBayById/`,
    getScheduleDetailsByDate: `Admin/Details/GetBaySchedulesDetails`,
    deleteDetail: `Admin/Details/Delete`,
    getJobType: `Admin/Details/GetJobType`,
    getItemByTicketNumber: `Admin/Sales/GetScheduleByTicketNumber`,
    getTodayDateScheduleList: `Admin/Details/GetAllDetails`,
    deleteItemById: `Admin/Sales/DeleteItemById`,
    getPastClientNotesById: `Admin/Details/GetPastClientNotesById/`,
    addVehicle: 'Admin/Vehicle/AddVehicle',
    addItem: `Admin/Sales/AddListItem`,
    getTicketNumberforItem: `Admin/Sales/GetTicketNumber`,
    updateListItem: `Admin/Sales/UpdateListItem`,
    updateItem: `Admin/Sales/UpdateItem`,
    addPayment: `Admin/Sales/AddPayment`,
    deleteJob: `Admin/Sales/DeleteJob`,
    rollbackTransaction: `Admin/Sales/RollBackPayment`,
    deleteTransaction: `Admin/Sales/DeleteTransactions`,
    getTimeClockWeekDetails: `Admin/TimeClock/GetTimeClockWeekDetails`,
    saveTimeClock: `Admin/TimeClock/Save`,
    getTimeClockEmployeeDetails: `Admin/TimeClock/GetTimeClockEmployeeDetails`,
    deleteTimeClockEmployee: `Admin/TimeClock/DeleteTimeClockEmployee`,
    getServiceAndProduct: `Admin/Sales/GetAllServiceAndProductList`,
    saveEmployeeWithService: `Admin/Details/AddServiceEmployee`,
    updateProductObj: `Admin/Sales/SaveProductItem`,
    getJobStatus: `Admin/Common/GetCodesByCategory/`,
    getPaymentStatus: `Admin/Common/GetCodesByCategory/`,
    getUncheckedVehicleDetails: `Admin/Checkout/GetCheckedInVehicleDetails`,
    checkoutVehicle: `Admin/Checkout/UpdateCheckoutDetails`,
    getAllWhiteLabelDetail: `Admin/WhiteLabelling/GetAll`,
    addWhiteLabelDetail: `Admin/WhiteLabelling/Add`,
    updateWhiteLabelDetail: `Admin/WhiteLabelling/Update`,
    saveCustomColor: `Admin/WhiteLabelling/Save`,
    uploadWhiteLabel: `Admin/WhiteLabelling/Update`,
    getPayroll: `Admin/PayRoll/GetPayroll`,
    holdoutVehicle: `Admin/Checkout/UpdateJobStatusHold`,
    getAccountDetails: `Admin/Sales/GetAccountDetails`,
    updateAccountBalance: `Admin/Client/UpdateAccountBalance`,
    completedVehicle: `Admin/Checkout/UpdateJobStatusComplete`,
    updateAdjustment: `Admin/PayRoll/UpdateEmployeeAdjustment`,
    getDashboardStatistics: `Admin/DashboardStatistics/GetDashboardStatisticsForLocationId/`,
    getCheckList : `Admin/Checklist/GetChecklist`,
    addCheckList : `Admin/Checklist/Add`,
    DeleteCheckList : `Admin/Checklist/Delete`,


  },
  Messenger:
  {
    getEmployeeList: `Admin/Messenger/GetChatEmployeeList/`,
    sendMessage: `Admin/Messenger/SendChatMessage`,
    updateChatCommunicationDetail: `Admin/Messenger/ChatCommunication`,
    getChatMessage: `Admin/Messenger/GetChatMessage`,
    createGroup:  `Admin/Messenger/CreateChatGroup`,
    getUnReadMessageCount: `Admin/Messenger/GetUnReadMessageCount/`,
    getGroupMemberList: `Admin/Messenger/GetChatGroupEmployeelist/`,
    deleteGroupUser: `Admin/Messenger/DeleteChatGroupUser/`,
    changeUnreadMessageState: `Admin/Messenger/ChangeUnreadMessageState`
  },
  reports: {
    getMonthlySalesReport: `Admin/Report/GetMonthlySalesReport`,
    getCustomerSummaryReport: `Admin/Report/GetCustomerSummaryReport`,
    getCustomerMonthlyDetailReport: `Admin/Report/GetCustomerMonthlyDetailedReport`,
    getDailyTipReport: ``,
    getMonthlyTipReport: ``,
    getDailyStatusReport: `Admin/Report/DailyStatusReport`,
    getMonthlyDailyTipReport: `Admin/Report/MonthlyDailyTipReport`,
    getDetailStatusInfo: `Admin/Report/DailyStatusDetailInfo`,
    getDailyClockDetail: `Admin/TimeClock/GetTimeClockEmployeeHourDetails`,
    getCashRegister: `Admin/CashRegister/Get`,
    getMonthlyMoneyOwnedReport: `Admin/Report/GetMonthlyMoneyOwnedReport/`,
    getEodSaleReport: `Admin/Report/EODSalesReport`,
    getTimeClockEmpHoursDetail: `Admin/TimeClock/GetTimeClockEmployeeHourDetails`,
    getDailySalesReport:`Admin/Report/DailySalesReport`
  },
  dashboard: {
    getDashboardLocation: `Admin/Location/GetAll`,
    getTodayScheduleList: `Admin/Details/GetAllDetails`,
    getDashboardStatistics: `Admin/Dashboard/GetDashboardStatistics`
  },
  bonusSetup: {
    saveBonus: `Admin/BonusSetup/Add`,
    getBonusList: `Admin/BonusSetup/GetBonus`,
    editBonus: `Admin/BonusSetup/Update`
  },
  document: {
    addDocument:`Admin/Document/AddDocument`,
    getDocument:`Admin/Document/GetDocument/`,
    deleteDocument:`Admin/Document/DeleteDocument/`
  }
};
