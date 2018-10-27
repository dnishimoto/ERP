import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IGeneralLedgerView } from '../interface/interfaceMod';

@Component({
  selector: 'app-personalExpense-webapi',
  templateUrl: './personalExpense-webapi.component.html'
})



export class PersonalExpenseComponent {
  public personalExpense: IGeneralLedgerView;


  constructor(
    http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    http.get<IGeneralLedgerView>(baseUrl + 'api/GeneralLedger/ById/25').subscribe(result => {

      this.personalExpense = result;


    }, error => console.error(error));


  }
  
 }




