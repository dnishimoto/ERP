import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
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
import { AddressBookComponent, AddressBookChildComponent } from './addressbook/addressbook.component'
import { AddressBookDetailComponent } from './addressbook/addressbookdetail.component';
import { PunchComponent } from './punch/punch.component';
import { ConfigurationService } from "./configuration/configuration.service";

const appInitializerFn = (appConfig: ConfigurationService) => {

  return () => {

    return appConfig.loadConfig();

  };

};

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
    PunchComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes
    )
  ],
  providers: [ApplicationService,

    ConfigurationService,

    {

      provide: APP_INITIALIZER,

      useFactory: appInitializerFn,

      multi: true,

      deps: [ConfigurationService]

    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
export function getBaseUrl() {

  return document.getElementsByTagName('base')[0].href;

}
