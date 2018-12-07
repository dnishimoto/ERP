import { Component, Inject } from '@angular/core';
import { PostIncomeView, IIncomeView } from '../interface/interfaceMod';

import { ApplicationService } from '../application.service';

@Component({
  selector: 'income-webapi',
  templateUrl: './income-webapi.component.html'
})



export class IncomeComponent {
  public income: number;
  public glDate: Date;
  //public myString: string;
  public postIncome: PostIncomeView = new PostIncomeView();
  public incomeViews: IIncomeView[];
 

  private onSubmit() {
    //alert(JSON.stringify(this.postIncome));

    this.myApp.postIncome(this.postIncome).subscribe
      (
      result => { alert('Posted'); this.loadIncomeViews(); }
      , error => console.error(error)
      );
  }
  private loadIncomeViews() {
    this.myApp.getIncomeViews().subscribe(
      result => {
        this.incomeViews = result;
        //this.output = JSON.stringify(result);
      },
      error => console.error(error)
    );
  }
  constructor(private myApp: ApplicationService) {   }

  ngOnInit() {
   // this.myString="Hello World"
    this.loadIncomeViews();
    /*
    this.myApp.getIncomeViews().subscribe(
      result => {
        this.incomeViews = result;
        //this.output = JSON.stringify(result);
      },
      error => console.error(error)
    );
    */

  }

 }



