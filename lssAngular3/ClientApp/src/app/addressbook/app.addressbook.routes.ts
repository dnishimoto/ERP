import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddressBookComponent } from '../addressbook/addressbook.component';


export const addressBookRoutes: Routes = [{
  path: 'app-addressbook',
  component: AddressBookComponent,
  children: [
    {path: '',pathMatch:'full', redirectTo:'app-addressbook'},
    { path: 'app-addressbook', component: AddressBookComponent },
    { path: 'app-addressbook/:id', component: AddressBookComponent },
    { path: 'app-addressbook/new', component: AddressBookComponent}
  ]
}
];

export const addressBookRouting: ModuleWithProviders =
  RouterModule.forChild(addressBookRoutes);

