import { Component, Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IAddressBookView, IIncomeStatementView, IIncomeView, IPersonalBudgetView, IAccountReceivableFlatView,IAccountSummaryView,IChartOfAccountView ,IGeneralLedgerView,IBudgetView, PostIncomeView } from './interface/interfaceMod';



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
  updateAddressBookView(addressBookView: IAddressBookView) {
    return this.http.put('/api/AddressBook/', addressBookView);
  }
  getAddressBookView(id: number) {
    return this.http.get<IAddressBookView>('/api/AddressBook/' + id);
  }
  getAddressBookViews(searchName: string) {
    return this.http.get<IAddressBookView[]>('/api/AddressBook/People/' + searchName);
  }
  getLedgerById(id: number) {

    return this.http.get<IGeneralLedgerView>('/api/GeneralLedger/ById/' + id);
  }
  getLedgers() {
    return this.http.get<IAccountSummaryView[]>('/api/GeneralLedger/BySummary');
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
  getIncomeViews() {
    return this.http.get<IIncomeView[]>('/api/GeneralLedger/IncomeViews');
  }
  postIncome(income: PostIncomeView) {
    alert(JSON.stringify(income))
    return this.http.post('/api/GeneralLedger/IncomeShortView', income);
  }
  getIncomeStatementViews(fiscalYear: number) {
    return this.http.get<IIncomeStatementView[]>('/api/GeneralLedger/IncomeStatementViews/'+fiscalYear);
  }
  getIncomeStatementAccounts(fiscalYear: number) {
    return this.http.get<string[]>('/api/GeneralLedger/IncomeStatementAccounts/' + fiscalYear);
  }
}
