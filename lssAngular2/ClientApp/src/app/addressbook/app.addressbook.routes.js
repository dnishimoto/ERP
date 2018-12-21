"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var addressbook_component_1 = require("../addressbook/addressbook.component");
exports.addressBookRoutes = [{
        path: 'app-addressbook',
        component: addressbook_component_1.AddressBookComponent,
        children: [
            { path: '', pathMatch: 'full', redirectTo: 'app-addressbook' },
            { path: 'app-addressbook', component: addressbook_component_1.AddressBookComponent },
            { path: 'app-addressbook/:id', component: addressbook_component_1.AddressBookComponent },
            { path: 'app-addressbook/new', component: addressbook_component_1.AddressBookComponent }
        ]
    }
];
exports.addressBookRouting = router_1.RouterModule.forChild(exports.addressBookRoutes);
//# sourceMappingURL=app.addressbook.routes.js.map