import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { IAccountReceivableFlatView } from '../interface/interfaceMod';

@Component({
  selector: 'app-accountreceivable-webapi',
  templateUrl: './accountreceivable-webapi.component.html'
})



export class AccountReceivableComponent {
  public accountReceivables: IAccountReceivableFlatView[];

 

  constructor(
    http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

       http.get<IAccountReceivableFlatView[]>(baseUrl + 'api/AccountReceivable/OpenReceivables').subscribe(result => {

      this.accountReceivables = result;

      
      //console.log(result);


    }, error => console.error(error));


  }

}



