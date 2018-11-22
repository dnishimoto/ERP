import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IPersonalBudgetView } from '../interface/interfaceMod';

import { ApplicationService } from '../application.service';

@Component({
  selector: 'app-budget-payment-webapi',
  templateUrl: './budget-payment-webapi.component.html'
})



export class BudgetPaymentWebApiComponent {
  public budgets: IPersonalBudgetView[];
  public postPayment: IPersonalBudgetView;
  public myString: string;

  private queryClick(budget: IPersonalBudgetView) {
    //alert(JSON.stringify(budget))
    this.postPayment = budget;
    //alert(this.postPayment.description);
  }
  private onSubmit() {
    alert(JSON.stringify(this.postPayment));


    this.myApp.postPersonalBudget(this.postPayment).subscribe
      (
      result => { }
      , error => console.error(error)
      );


  }
  constructor(private myApp: ApplicationService) {  }

  ngOnInit() {
    this.myApp.getPersonalBudgets().subscribe(
      result => { this.budgets = result;
      },
      error => console.error(error)
    );

  }

 }



