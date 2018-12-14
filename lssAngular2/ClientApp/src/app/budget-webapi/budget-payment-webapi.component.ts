import { Component, Inject , OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IPersonalBudgetView } from '../interface/interfaceMod';

import { ApplicationService } from '../application.service';

@Component({
  selector: 'app-budget-payment-webapi',
  templateUrl: './budget-payment-webapi.component.html'
})
   
export class BudgetPaymentWebApiComponent implements OnInit {
  public budgets: IPersonalBudgetView[];
  public postPayment: IPersonalBudgetView;
  public myString: string;

  private dateChanged(newDate) {
    this.postPayment.glDate = newDate;
  }

  private queryClick(budget: IPersonalBudgetView) {
      //this.postPayment = budget;
    //copy rather than assign the object
    this.postPayment=<IPersonalBudgetView>JSON.parse(JSON.stringify(budget));
     
    //this.postPayment.glDate = new Date();
    //alert(this.postPayment.description);
  }
  private onSubmit(paymentForm:any) {
    //alert(paymentForm.valid)
    if (paymentForm.valid) {
     // alert(JSON.stringify(this.postPayment));
      this.myApp.postPersonalBudget(this.postPayment).subscribe
        (
        result => {
          alert("Posted");
        }
        , error => console.error(error)
        );
    }

  }
  constructor(private myApp: ApplicationService) {  }

  ngOnInit() {
    this.myApp.getPersonalBudgets().subscribe(
      result => {
        this.budgets = result; 
        

      },
      error => console.error(error)
    );

  }

 }



