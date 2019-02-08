import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApplicationService } from '../application.service';
import { IAccountSummaryView } from '../interface/interfaceMod';

@Component({
  selector: 'app-generalledger-webapi',
  templateUrl: './generalledger-webapi.component.html'
})



export class GeneralLedgerComponent implements OnInit{
  public accountSummaries: IAccountSummaryView[];
  public mystring: string;

  public queryFiscalYear: number = 2018;

  query() {

    if (this.queryFiscalYear > 0) {
      this.myApp.getLedgers(this.queryFiscalYear).subscribe(
        result => { this.accountSummaries = result },
        error => console.error(error)
      );
    }
  }

  constructor(private myApp: ApplicationService) {
  }    
  ngOnInit() {
    this.myApp.getLedgers(this.queryFiscalYear).subscribe(
      result => { this.accountSummaries = result },
      error => console.error(error)
    );


  }//end ngOnInit
 }





