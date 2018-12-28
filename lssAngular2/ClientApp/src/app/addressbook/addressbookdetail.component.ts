import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '../application.service';
import { IAddressBookView } from '../interface/interfaceMod';
import { ActivatedRoute, ParamMap } from '@angular/router'
@Component({
  selector: 'app-addressbookdetail',
  templateUrl: './addressbookdetail.component.html'
  ,
  //templateUrl: './addressbookDetail.component.html'
})
export class AddressBookDetailComponent implements OnInit {
  private addressId: number;
  private sub: any;
  private addressBookView: IAddressBookView;

  constructor(private myApp: ApplicationService, private route: ActivatedRoute) {


  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => { this.addressId = +params['id']; });

    //alert(this.addressId.toString());
    this.myApp.getAddressBookView(this.addressId).subscribe(
      result => {
        this.addressBookView = result;
      },
      error => console.error(error)
    );

    //let this.sub = this.route.params.subscribe(params => {
    //this.addressId = params['id'];

    //})

  }
}
