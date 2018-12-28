import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { BudgetWebApiComponent } from './budget-webapi/budget-webapi.component';
import { BudgetPaymentWebApiComponent } from './budget-webapi/budget-payment-webapi.component';
import { GeneralLedgerComponent } from './generalledger-webapi/generalledger-webapi.component';
import { IncomeStatementComponent } from './generalledger-webapi/financials.component';
import { AccountReceivableComponent } from './accountreceivable-webapi/accountreceivable-webapi.component';
import { PersonalExpenseComponent } from './personalExpense-webapi/personalexpense-webapi.component';
import { IncomeComponent } from './income-webapi/income-webapi.component';
import { AddressBookComponent } from './addressbook/addressbook.component';
import {AddressBookDetailComponent } from './addressbook/addressbookdetail.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  { path: 'budget-webapi', component: BudgetWebApiComponent },
  { path: 'budget-payment-webapi', component: BudgetPaymentWebApiComponent },
  { path: 'generalledger-webapi', component: GeneralLedgerComponent },
  { path: 'accountreceivable-webapi', component: AccountReceivableComponent },
  { path: 'personalExpense-webapi', component: PersonalExpenseComponent },
  { path: 'income-webapi', component: IncomeComponent },
  { path: 'app-financials', component: IncomeStatementComponent },
  { path: 'app-addressbook', component: AddressBookComponent},
  { path: 'app-addressbookdetail/:id', component: AddressBookDetailComponent },
];
