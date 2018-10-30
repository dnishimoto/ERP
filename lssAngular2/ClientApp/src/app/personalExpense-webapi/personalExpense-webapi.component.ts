
import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { IGeneralLedgerView } from '../interface/interfaceMod';
import { IChartOfAccountView } from '../interface/interfaceMod';
//import { ViewChild, AfterViewInit } from '@angular/core';


import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from '../app.component';

@Component({
  selector: 'app-personalExpense-webapi',
  templateUrl: './personalExpense-webapi.component.html'
})

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClient
  ],
  declarations: [
    AppComponent,
    Component
  ],
  providers: [],
  bootstrap: [AppComponent]
})


export class PersonalExpenseComponent  {
  public personalExpense: IGeneralLedgerView;
  public coaPersonalExpenses: IChartOfAccountView[];
  submitted = false;


  onSubmit() {
    alert('reached');
    
    this.submitted = true;
  }
  newHero() { alert('reached new hero');}

  constructor(
    http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    http.get<IGeneralLedgerView>(baseUrl + 'api/GeneralLedger/ById/25').subscribe(result => {

      this.personalExpense = result;


    }, error => console.error(error));

    http.get<IChartOfAccountView[]>(baseUrl + 'api/ChartOfAccount/PersonalExpense').subscribe(result => {
      this.coaPersonalExpenses = result;

    }, error => console.error(error));


  }
  
 }




