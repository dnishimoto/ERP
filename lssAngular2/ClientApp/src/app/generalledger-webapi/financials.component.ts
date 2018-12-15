import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApplicationService } from '../application.service';
import { IIncomeStatementView } from '../interface/interfaceMod';
import { Pipe, PipeTransform } from '@angular/core';

@Component({
  selector: 'app-financials',
  templateUrl: './financials.component.html'
})

@Pipe({
  name: 'myfilter',
  pure: false
})

export class IncomeStatementComponent implements OnInit, PipeTransform {
  //public accountSummaries: IAccountSummaryView[];
  public incomeStatementViews: IIncomeStatementView[];
  public fiscalYear: number;
  public accounts: string[];

  transform(items: any[], filter: Object): any {
    if (!items || !filter) {
      return items;
    }
    // filter items array, items which match and return true will be
    // kept, false will be filtered out
    return items.filter(item => item.account.indexOf(filter) !== -1);
  }

  private onSubmit() {


  }
  constructor(private myApp: ApplicationService) {
  }
  ngOnInit() {

    this.fiscalYear = new Date().getFullYear();

    this.myApp.getIncomeStatementViews(this.fiscalYear).subscribe(
      result => { this.incomeStatementViews = result },
      error => console.error(error)
    );

    this.myApp.getIncomeStatementAccounts(this.fiscalYear).subscribe(
      result => { this.accounts = result },
      error => console.error(error)
    );


  }//end ngOnInit
}
