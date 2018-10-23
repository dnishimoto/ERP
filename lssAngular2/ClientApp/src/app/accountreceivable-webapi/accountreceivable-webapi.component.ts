import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-accountreceivable-webapi',
  templateUrl: './accountreceivable-webapi.component.html'
})



export class AccountReceivableComponent {
  public accountReceivables: IAccountReceivableFlatView[];

 

  constructor(
    http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

       http.get<IAccountReceivableFlatView[]>(baseUrl + 'api/AccountReceivable/OpenReceivables').subscribe(result => {

      this.accountReceivables = result;

      
      //console.log(result);


    }, error => console.error(error));


  }

}



interface IAccountReceivableFlatView {
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

