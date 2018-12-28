import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IBudgetView } from '../interface/interfaceMod';
import { ApplicationService } from '../application.service';

@Component({
  selector: 'app-budget-webapi',
  templateUrl: './budget-webapi.component.html'
})



export class BudgetWebApiComponent {
  public budgets: IBudgetView[];
  //public budget: IBudgetView;
  public myString: string;

  constructor(private myApp : ApplicationService)   {  }
  
  ngOnInit() {
    this.myApp.getBudget().subscribe(
      result => { this.budgets = result },
      error => console.error(error)
    );
  }
}




