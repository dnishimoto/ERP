"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var home_component_1 = require("./home/home.component");
var counter_component_1 = require("./counter/counter.component");
var fetch_data_component_1 = require("./fetch-data/fetch-data.component");
var budget_webapi_component_1 = require("./budget-webapi/budget-webapi.component");
var budget_payment_webapi_component_1 = require("./budget-webapi/budget-payment-webapi.component");
var generalledger_webapi_component_1 = require("./generalledger-webapi/generalledger-webapi.component");
var financials_component_1 = require("./generalledger-webapi/financials.component");
var accountreceivable_webapi_component_1 = require("./accountreceivable-webapi/accountreceivable-webapi.component");
var personalexpense_webapi_component_1 = require("./personalExpense-webapi/personalexpense-webapi.component");
var income_webapi_component_1 = require("./income-webapi/income-webapi.component");
var addressbook_component_1 = require("./addressbook/addressbook.component");
var addressbookdetail_component_1 = require("./addressbook/addressbookdetail.component");
exports.routes = [
    { path: '', component: home_component_1.HomeComponent, pathMatch: 'full' },
    { path: 'counter', component: counter_component_1.CounterComponent },
    { path: 'fetch-data', component: fetch_data_component_1.FetchDataComponent },
    { path: 'budget-webapi', component: budget_webapi_component_1.BudgetWebApiComponent },
    { path: 'budget-payment-webapi', component: budget_payment_webapi_component_1.BudgetPaymentWebApiComponent },
    { path: 'generalledger-webapi', component: generalledger_webapi_component_1.GeneralLedgerComponent },
    { path: 'accountreceivable-webapi', component: accountreceivable_webapi_component_1.AccountReceivableComponent },
    { path: 'personalExpense-webapi', component: personalexpense_webapi_component_1.PersonalExpenseComponent },
    { path: 'income-webapi', component: income_webapi_component_1.IncomeComponent },
    { path: 'app-financials', component: financials_component_1.IncomeStatementComponent },
    { path: 'app-addressbook', component: addressbook_component_1.AddressBookComponent },
    { path: 'app-addressbookdetail/:id', component: addressbookdetail_component_1.AddressBookDetailComponent },
];
//# sourceMappingURL=app.routes.js.map