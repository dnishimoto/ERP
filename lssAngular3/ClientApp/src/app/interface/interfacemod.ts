export interface ITimeAndAttendanceViewContainer {
  pageNumber: number;
  pageSize: number;
  totalItemCount: number;
  items: ITimeAndAttendancePunchinView[];
}
export class TimeAndAttendanceParam {
  employeeId: number;
  account: string;
  mealDeduction: number;
  pageNumber: number;
  pageSize: number;
}

export interface ITimeAndAttendanceParam {
  employeeId: number;
  account: string;
  mealDeduction: number;
  pageNumber: number;
  pageSize: number;
}
export interface ITimeAndAttendancePunchinView {
  timePunchinId: number;
  punchinDate: Date;
  punchinDateTime: string;
  punchoutDateTime: string;
  jobCodeXrefId: number;
  jobCode: string;
  approved: boolean;
  employeeId: number;
  employeeName: string;
  supervisorId: number;
  supervisorName: string;
  processedDate: Date;
  note: string;
  shiftId: number;
  scheduledToWork: boolean;
  typeOfTimeUdcXrefId: number;
  typeOfTime: string;
  approvingAddressId: number;
  payCodeXrefId: number;
  payCode: string;
  scheduleId: number;
  durationInMinutes: number;
  mealDurationInMinutes: number;
  areaCode: string;
  departmentCode: string;

}

export interface IAccountSummaryView {
  accountId: number;
  fiscalPeriod: number;
  fiscalYear: number;
  description: string;
  amount: number;
  ledgers: IGeneralLedgerView[];
}
export interface IGeneralLedgerView {
  generalLedgerId: number;
  docNumber: number;
  docType: string;
  amount: number;
  ledgerType: string;
  gldate: Date;
  accountId: number;
  createdDate: Date;
  addressId: number;
  comment: string;
  debitAmount: number;
  creditAmount: number;
  fiscalYear: number;
  fiscalPeriod: number;
  checkNumber: string;
  purchaseOrderNumber: string;
  units: number;
}
export interface IChartOfAccountView {
  accountId: number;
  location: string;
  busUnit: string;
  subsidiary: string;
  subSub: string;
  account: string;
  description: string;
  companyNumber: string;
  genCode: string;
  subCode: string;
  objectNumber: string;
  supCode: string;
  thirdAccount: string;
  categoryCode1: string;
  categoryCode2: string;
  categoryCode3: string;
  postEditCode: string;
  companyId: number;
  companyName: string;
  level: number;
}
export interface IAccountReceivableFlatView {
  openAmount: number;
  gLDate: Date;
  acctRecId: number;
  invoiceId: number;
  invoiceNumber: string;
  invoiceDescription: string;
  docNumber: number;
  acctRecDocType: string;
  remarks: string;
  paymentTerms: string;
  discountDueDate: Date;
  paymentDueDate: Date;
  customerId: number;
  customerName: string;
  addressLine1: string;
  addressLine2: string;
  city: string;
  state: string;
  zipcode: string;
  account: string;
  coaDescription: string;
  gLAmount: number;

}

export interface IBudgetView {
  budgetId: number;
  budgetHours: number;
  budgetAmount: number;
  actualHours: number;
  actualAmount: number;
  accountId: number;
  accountDescription: string;
  companyNumber: string;
  busUnit: string;
  objectNumber: string;
  subsidiary: string;
  rangeId: number;
  rangeStartDate: Date;
  rangeEndDate: Date;
  companyCode: string;
  supervisorCode: string;
  projectedHours: number;
  projectedAmount: number;
}

export interface IIncomeView {

  description: string;
  account: string;
  accountId: number;
  generalLedgerId: number;
  docType: string;
  ledgerType: string;
  addressId: number;
  name: string;
  amount: number;
  gLDate: Date;
  fiscalPeriod: number;
  fiscalYear: number;

}
export class PostIncomeView {
  amount: number;
  gLDate: Date;
  comment: string;
  checkNumber: string;
}
export interface IAddressBookView {
  addressId: number;
  name: string;
  firstName: string;
  lastName: string;
  companyName: string;
  categoryCodeChar1: string;
  categoryCodeChar2: string;
  categoryCodeChar3: string;
  categoryCodeInt1: number;
  categoryCodeInt2: number;
  categoryCodeInt3: number;
  categoryCodeDate1: Date;
  categoryCodeDate2: Date;
  categoryCodeDate3: Date;
}
export interface IPersonalBudgetView {

  accountId: number;
  location: string;
  busUnit: string;
  objectNumber: string;
  supCode: string;
  subsidiary: string;
  subSub: string;
  account: string;
  description: string;
  companyNumber: string;
  budgetAmount: number;
  budgetHours: number;
  startDate: Date;
  endDate: Date;
  paymentAmount;
  paymentHours;
  glDate: Date;
  payCycles: number;

}
export interface IIncomeStatementView {
  account: string;
  description: string;
  fiscalPeriod: number;
  fiscalYear: number;
  amount: number;
  gLDate: Date;
}


