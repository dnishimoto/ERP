import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { IPersonalBudgetView } from '../interface/interfaceMod';

import { ApplicationService } from '../application.service';

@Component({
  selector: 'income-webapi',
  templateUrl: './income-webapi.component.html'
})



export class IncomeComponent {
  public income: number;
  public glDate: Date;
 
  
  private onSubmit() {
    //alert(JSON.stringify(this.postPayment));

    /*
    this.myApp.postPersonalBudget(this.postPayment).subscribe
      (
      result => { }
      , error => console.error(error)
      );
      */


  }
  constructor(private myApp: ApplicationService) {  }

  ngOnInit() {
    //this.myApp.getPersonalBudgets().subscribe(
     // result => { this.budgets = result;
     // },
     // error => console.error(error)
   // );

  }

 }



