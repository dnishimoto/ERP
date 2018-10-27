export interface IAccountSummaryView {
  accountId : number;
  fiscalPeriod : number;
  fiscalYear : number;
  description : string;
  amount : number;
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


