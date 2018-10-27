import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { IAccountSummaryView } from '../interface/interfaceMod';

@Component({
  selector: 'app-generalledger-webapi',
  templateUrl: './generalledger-webapi.component.html'
})



export class GeneralLedgerComponent {
  public accountSummaries: IAccountSummaryView[];
  public mystring: string;

  constructor(
    http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    http.get<IAccountSummaryView[]>(baseUrl + 'api/GeneralLedger/BySummary/2018').subscribe(result => {

      this.accountSummaries = result;

      this.mystring = "Hello World";

      //console.log(result);


    }, error => console.error(error));


  }
  
 }





