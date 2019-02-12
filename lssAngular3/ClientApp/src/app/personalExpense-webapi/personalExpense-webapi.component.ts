
import { Component, Inject, OnInit } from '@angular/core';
import { IGeneralLedgerView, IChartOfAccountView } from '../interface/interfaceMod';
import { ApplicationService } from '../application.service';

@Component({
  selector: 'app-personalExpense-webapi',
  templateUrl: './personalExpense-webapi.component.html'
})
  

export class PersonalExpenseComponent implements OnInit {
  public personalExpense: IGeneralLedgerView;
  public coaPersonalExpenses: IChartOfAccountView[];

  public queryGeneralLedgerId: number=0;

  submitted = false;

  get diagnostic() { return JSON.stringify(this.personalExpense); }


  onSubmit() {

    //alert(this.myApp.getData());
    alert(JSON.stringify(this.personalExpense));
    
    //this.personalExpense = myApp.GetLedgerById(25);
    
    //let app = new ApplicationService();

    //alert(app.getData());
    
    this.submitted = true;
  }
  queryExpense() {
    var generalLedgerId = this.queryGeneralLedgerId;

    if (generalLedgerId >0) {
    this.myApp.getLedgerById(generalLedgerId).subscribe(
      result => { this.personalExpense = result },
      error => console.error(error)
      );
    }

    //this.personalExpense.amount = 0;
  }

  constructor(
     private myApp: ApplicationService) {
 
  }
  ngOnInit() {
    this.myApp.getPEChartOfAccountList().subscribe(
      result => { this.coaPersonalExpenses = result },
      error => console.error(error)
    );

    
   }//end ngOnInit
  
 }




