import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { routes } from './app.routes'
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FilterAcctPipe } from './shared/pipes/acct-filter.pipe';
import { ApplicationService } from './application.service';
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
import { AddressBookComponent,AddressBookChildComponent,AddressBookDetailComponent } from './addressbook/addressbook.component'


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    BudgetWebApiComponent,
    BudgetPaymentWebApiComponent,
    GeneralLedgerComponent,
    AccountReceivableComponent,
    PersonalExpenseComponent,
    IncomeComponent,
    IncomeStatementComponent,
    FilterAcctPipe,
    AddressBookComponent,
    AddressBookChildComponent,
    AddressBookDetailComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes
    )
  ],
  providers: [ApplicationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
