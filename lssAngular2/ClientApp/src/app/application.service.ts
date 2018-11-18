import { Component, Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IPersonalBudgetView, IAccountReceivableFlatView,IAccountSummaryView,IChartOfAccountView ,IGeneralLedgerView,IBudgetView } from './interface/interfaceMod';



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
  getLedgers(year: number) {
    return this.http.get<IAccountSummaryView[]>('/api/GeneralLedger/BySummary/'+year);
  }
  getPEChartOfAccountList() {
    return this.http.get<IChartOfAccountView[]>('/api/ChartOfAccount/PersonalExpense');
  }
  getBudget() {
    return this.http.get<IBudgetView[]>('/api/Budget');

  }
  getAccountReceivable() {
    return this.http.get<IAccountReceivableFlatView[]>('/api/AccountReceivable/OpenReceivables');
  }
  getPersonalBudgets() {
    return this.http.get<IPersonalBudgetView[]>('/api/Budget/PersonalBudgetViews');
  }
  postPersonalBudget(personalBudget: IPersonalBudgetView) {
    return this.http.post('/api/Budget/Payment/', personalBudget);

  }
}
