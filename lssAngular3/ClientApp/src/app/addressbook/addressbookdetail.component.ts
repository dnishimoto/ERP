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
  public addressId: number;
  private sub: any;
  public addressBookView: IAddressBookView;
  private output;

  constructor(private myApp: ApplicationService, private route: ActivatedRoute) {


  }
  onSubmit() {
    this.myApp.updateAddressBookView(this.addressBookView).subscribe
      (
      result => { alert('Put');  }
      , error => console.error(error)
      );
      
    //alert(JSON.stringify(this.addressBookView));
  }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => { this.addressId = +params['id']; });

    //alert(this.addressId.toString());
    this.myApp.getAddressBookView(this.addressId).subscribe(
      result => {
        this.addressBookView = result;
        //this.output = JSON.stringify(result);
      },
      error => console.error(error)
    );

    //let this.sub = this.route.params.subscribe(params => {
    //this.addressId = params['id'];

    //})

  }
}
