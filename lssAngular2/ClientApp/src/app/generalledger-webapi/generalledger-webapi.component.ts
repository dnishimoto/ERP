import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApplicationService } from '../application.service';
import { IAccountSummaryView } from '../interface/interfaceMod';

@Component({
  selector: 'app-generalledger-webapi',
  templateUrl: './generalledger-webapi.component.html'
})



export class GeneralLedgerComponent {
  public accountSummaries: IAccountSummaryView[];
  public mystring: string;

  constructor(private myApp: ApplicationService) {
  }    
  ngOnInit() {
    this.myApp.getLedgers().subscribe(
      result => { this.accountSummaries = result },
      error => console.error(error)
    );


  }//end ngOnInit
 }





