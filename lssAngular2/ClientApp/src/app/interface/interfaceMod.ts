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
  AccountId: number;
  Location: string;
  BusUnit: string;
  Subsidiary: string;
  SubSub: string;
  Account: string;
  Description: string;
  CompanyNumber: string;
  GenCode: string;
  SubCode: string;
  ObjectNumber: string;
  SupCode: string;
  ThirdAccount: string;
  CategoryCode1: string;
  CategoryCode2: string;
  CategoryCode3: string;
  PostEditCode: string;
  CompanyId: number;
  CompanyName: string;
  Level: number;
}
export interface IAccountReceivableFlatView {
  OpenAmount: number;
  GLDate: Date;
  InvoiceId: number;
  InvoiceNumber: string;
  InvoiceDescription: string;
  DocNumber: number;
  Remarks: string;
  PaymentTerms: string;
  DiscountDueDate: Date;
  PaymentDueDate: Date;
  CustomerId: number;
  CustomerName: string;
  AddressLine1: string;
  AddressLine2: string;
  City: string;
  State: string;
  Zipcode: string;
  Account: string;
  CoaDescription: string;
  GLAmount: number;

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


