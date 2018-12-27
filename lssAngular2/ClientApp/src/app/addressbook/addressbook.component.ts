import { Component, Inject, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { ApplicationService } from '../application.service';
import { IAddressBookView } from '../interface/interfaceMod';
import { Router, ActivatedRoute, ParamMap } from '@angular/router'
import { switchMap } from 'rxjs/operators';



@Component({
  selector: 'app-addressbook',
  templateUrl: './addressbook.component.html'
})

export class AddressBookComponent implements OnInit {
  public addressBookViews: IAddressBookView[];
  public searchName: string = "";
  private message: string = "";
  public title: string = "Hello world2";
  constructor(private myApp: ApplicationService, private router: Router) { }

  private onLoadAddressBookDetail(id: number) {
    //alert(id);
    this.title = id.toString();
    this.router.navigate(['app-addressbookdetail', id]);
  }

  private onSubmit() {
    // alert(this.fiscalYear)
    this.loadReport(this.searchName);

  }
  private loadReport(searchName : string) {
    this.myApp.getAddressBookViews(searchName).subscribe(
      result => {
        this.addressBookViews = result;
      },
      error => console.error(error)
    );

  }
  ngOnInit() {
    this.loadReport(this.searchName);
  
  }
  receiveMessage($event) {
    this.message = $event
    alert(this.message);
  }
}

@Component({
  selector: 'app-addressbook-child',
  template: `
    {{parentMessage}}
      <button (click)="sendMessage()">Send Message</button>
  `,
  //templateUrl: './addressbookDetail.component.html'
})

export class AddressBookChildComponent implements OnInit {
  message: string = "Holy Cow!";
  _childmessage: string;
  @Input() set parentMessage(value: string) { this._childmessage = value; }
  get parentMessage(): string { return this._childmessage; }

  @Output() messageEvent = new EventEmitter<string>();


  constructor() { }
  ngOnInit() {
  }
  sendMessage() {
    this.messageEvent.emit(this.message)
  }
}


@Component({
  selector: 'app-addressbookdetail',
  templateUrl: './addressbookdetail.component.html'
  ,
  //templateUrl: './addressbookDetail.component.html'
})
export class AddressBookDetailComponent implements OnInit {
  private addressId: number;
  private sub: any;
  
  constructor(private route: ActivatedRoute) {
   
   
  }
  ngOnInit() {
    this.sub = this.route.params.subscribe(params => { this.addressId = +params['id']; });

    alert(this.addressId.toString());
    //let this.sub = this.route.params.subscribe(params => {
      //this.addressId = params['id'];

    //})

  }
}


