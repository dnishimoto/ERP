import { Component, Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ITimeAndAttendanceViewContainer,ITimeAndAttendanceParam, ITimeAndAttendancePunchinView, IAddressBookView, IIncomeStatementView, IIncomeView, IPersonalBudgetView, IAccountReceivableFlatView,IAccountSummaryView,IChartOfAccountView ,IGeneralLedgerView,IBudgetView, PostIncomeView } from './interface/interfaceMod';
//import { ConfigurationService } from "./configuration/configuration.service";


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
  getTAGrid(param: ITimeAndAttendanceParam) {
    return this.http.get<ITimeAndAttendanceViewContainer>('/api/TimeAndAttendance/TAPunchPage/'+param.employeeId+'/'+param.pageNumber+'/'+param.pageSize);
  }
  postTAPunchout(param: ITimeAndAttendanceParam) {

    return this.http.post<ITimeAndAttendancePunchinView>('/api/TimeAndAttendance/TAPunchout', param);
  }
  postTAPunchin(param: ITimeAndAttendanceParam) {
 
    return this.http.post<ITimeAndAttendancePunchinView>('/api/TimeAndAttendance/TAPunchin',param);
  }

  getPunchOpen(employeeId: number) {
    return this.http.get<ITimeAndAttendancePunchinView>('api/TimeAndAttendance/TAOpenPunch/'+employeeId);
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
  getLedgers(fiscalYear: number) {
    return this.http.get<IAccountSummaryView[]>('/api/GeneralLedger/BySummary/'+fiscalYear);
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
    return this.http.post('/api/GeneralLedger/IncomeShortView', income);
  }
  getIncomeStatementViews(fiscalYear: number) {
    return this.http.get<IIncomeStatementView[]>('/api/GeneralLedger/IncomeStatementViews/'+fiscalYear);
  }
  getIncomeStatementAccounts(fiscalYear: number) {
    return this.http.get<string[]>('/api/GeneralLedger/IncomeStatementAccounts/' + fiscalYear);
  }
}
