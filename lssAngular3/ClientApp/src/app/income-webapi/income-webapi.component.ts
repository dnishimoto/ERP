import { Component, Inject, OnInit } from '@angular/core';
import {Router} from '@angular/router'
import { PostIncomeView, IIncomeView } from '../interface/interfaceMod';

import { ApplicationService } from '../application.service';

@Component({
  selector: 'income-webapi',
  templateUrl: './income-webapi.component.html'
})

export class IncomeComponent implements OnInit {
  public income: number;
  public glDate: Date;
  //public myString: string;
  public postIncome: PostIncomeView = new PostIncomeView();
  public incomeViews: IIncomeView[];
 
  public dateChanged(newDate) {
    this.postIncome.gLDate = newDate;
  }
  public navigateToIncomeStatement() {
    this.router.navigate(['\app-financials']);
  }
  public onSubmit(incomeForm: any) {

    if (incomeForm.valid) {
      //alert(JSON.stringify(this.postIncome));
   
      this.myApp.postIncome(this.postIncome).subscribe
        (
        result => { alert('Posted'); this.loadIncomeViews(); }
        , error => console.error(error)
        );
      
    }
  }
  public loadIncomeViews() {
    this.myApp.getIncomeViews().subscribe(
      result => {
        this.incomeViews = result;
        //this.output = JSON.stringify(result);
      },
      error => console.error(error)
    );
  }
  constructor(private myApp: ApplicationService, private router: Router) {  }

  ngOnInit() {
   // this.myString="Hello World"
    this.postIncome.gLDate = new Date();
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



