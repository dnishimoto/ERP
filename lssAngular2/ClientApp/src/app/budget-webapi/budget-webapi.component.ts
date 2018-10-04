import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-budget-webapi',
  templateUrl: './budget-webapi.component.html'
})


export class BudgetWebApiComponent {
  public budget: BudgetView;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<BudgetView>(baseUrl + 'api/SampleData/Budget?budgetId=2').subscribe(result => {
      this.budget = result;
    }, error => console.error(error));
  }
}


interface BudgetView {
        budgetId: number;
        budgetHours : number;
        budgetAmount : number;
        actualHours : number;
        actualAmount : number;
        accountId : number;
        accountDescription: string;
        companyNumber: string;
        busUnit: string;
        objectNumber: string;
        subsidiary: string;
        rangeId : number;
        rangeStartDate: Date;
        rRangeEndDate: Date;
        companyCode: string;
        supervisorCode: string;
        projectedHours : number;
        projectedAmount : number;
}
