import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router'
import { ApplicationService } from '../application.service';
import { IIncomeStatementView } from '../interface/interfaceMod';

@Component({
  selector: 'app-financials',
  templateUrl: './financials.component.html'
})

export class IncomeStatementComponent implements OnInit {
  //public accountSummaries: IAccountSummaryView[];
  public incomeStatementViews: IIncomeStatementView[];
  public fiscalYear: number;
  public accounts: string[];

  loadReport(fiscalYear: number) {
    this.myApp.getIncomeStatementViews(this.fiscalYear).subscribe(
      result => { this.incomeStatementViews = result },
      error => console.error(error)
    );

    this.myApp.getIncomeStatementAccounts(this.fiscalYear).subscribe(
      result => { this.accounts = result },
      error => console.error(error)
    );
  }

  private onSubmit() {
   // alert(this.fiscalYear)
    this.loadReport(this.fiscalYear);

  }
  constructor(private myApp: ApplicationService, private route: ActivatedRoute) {
  }
  ngOnInit() {

    this.fiscalYear = new Date().getFullYear();

    this.loadReport(this.fiscalYear);

  }
  
}
