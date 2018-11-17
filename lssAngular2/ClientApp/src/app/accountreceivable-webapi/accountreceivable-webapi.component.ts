import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { IAccountReceivableFlatView } from '../interface/interfaceMod';
import { ApplicationService } from '../application.service';

@Component({
  selector: 'app-accountreceivable-webapi',
  templateUrl: './accountreceivable-webapi.component.html'
})



export class AccountReceivableComponent {
  public accountReceivables: IAccountReceivableFlatView[];

 

  constructor(private myApp: ApplicationService ) {  }
  ngOnInit() {
    this.myApp.getAccountReceivable().subscribe(
      result => { this.accountReceivables= result },
      error => console.error(error)
    );
}



