import { Component, Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IGeneralLedgerView } from './interface/interfaceMod';
import { IChartOfAccountView } from './interface/interfaceMod';


@Injectable()


export class ApplicationService
{
  constructor(
    private http: HttpClient) {     }

  //http.get<IGeneralLedgerView>(baseUrl + 'api/GeneralLedger/ById/25').subscribe(result => {

    getData()
  {
      return ('reached');
      //return this.http.get
  }
  getLedgerById(id: number) {

    return this.http.get<IGeneralLedgerView>('/api/GeneralLedger/ById/' + id);
  }
  getPEChartOfAccountList() {
    return this.http.get<IChartOfAccountView[]>('/api/ChartOfAccount/PersonalExpense');
  }
}
