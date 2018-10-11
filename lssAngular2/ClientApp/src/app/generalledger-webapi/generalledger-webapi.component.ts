import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-generalledger-webapi',
  templateUrl: './generalledger-webapi.component.html'
})



export class GeneralLedgerComponent {
  public accountSummaries: IAccountSummaryView[];
  public mystring: string;

  constructor(
    http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    http.get<IAccountSummaryView[]>(baseUrl + 'api/GeneralLedger/2018').subscribe(result => {

      this.accountSummaries = result;

      this.mystring = "Hello World";

      //console.log(result);


    }, error => console.error(error));


  }
  
 }


interface IAccountSummaryView {
  accountId : number;
  fiscalPeriod : number;
  fiscalYear : number;
  description : string;
  amount : number;
  ledgers: IGeneralLedgerView[];
}
interface IGeneralLedgerView {
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
        debitAmount : number;
        creditAmount : number;
        fiscalYear : number;
        fiscalPeriod : number;
        checkNumber: string;
        purchaseOrderNumber: string;
        units : number;
}


