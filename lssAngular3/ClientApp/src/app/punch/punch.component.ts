
import { Component, Inject, OnInit } from '@angular/core';
import { ITimeAndAttendancePunchinView, ITimeAndAttendanceParam, TimeAndAttendanceParam, ITimeAndAttendanceViewContainer } from '../interface/interfaceMod';
import { ApplicationService } from '../application.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router'

@Component({
  selector: 'app-punch-webapi',
  templateUrl: './punch.component.html'
})
  

export class PunchComponent implements OnInit {
  public employeeId: number = 1;
  public account: string = "1200.215";
  public mealDeduction: number = 30;
  public note: string = "";
  public asOfDate: Date;
  public punchView: ITimeAndAttendancePunchinView;
  public container: ITimeAndAttendanceViewContainer;
  public param: ITimeAndAttendanceParam;
  public isLoaded: boolean = false;

  constructor( private myApp: ApplicationService, private router: Router) {}


  queryPunchOut() {
    this.queryPunch();
  }
  punchOut() {
    this.param = new TimeAndAttendanceParam();
    this.param.employeeId = this.employeeId;
    this.param.account = this.account;
    this.param.mealDeduction = this.mealDeduction;

    this.myApp.postTAPunchout(this.param).subscribe(
      result => {
        this.punchView = result;
        this.queryGrid();
        alert("punched out");
      },
      error => console.error(error)
    );
  }
  punchIn() {
    this.param = new TimeAndAttendanceParam();
    this.param.employeeId = this.employeeId;
    this.param.account = this.account;

    this.myApp.postTAPunchin(this.param).subscribe(
      result => {
        this.punchView = result;
        alert("punched in");
      },
      error => console.error(error)
    );

  
    //alert('punchin');
  }
  queryGrid() {
    this.param = new TimeAndAttendanceParam();
    this.param.employeeId = this.employeeId;
    this.param.pageNumber = 1;
    this.param.pageSize = 3;

    this.myApp.getTAGrid(this.param).subscribe(
      result => {
        this.container = result;
        this.isLoaded = true;
      },
      error => console.error(error)
    );
  }
  queryPunch() {
   
     this.asOfDate = new Date();

    this.myApp.getPunchOpen(this.employeeId).subscribe(
      result => {
        this.punchView = result;
        this.isLoaded = true;
      },
      error => console.error(error)
      );
  
  }

  onPunchByDuration(timePunchinId:number) {
 //   this.title = id.toString();
   // this.router.navigate(['app-addressbookdetail', id]);
    //alert(timePunchinId)
    this.router.navigate(['ngbd-modal-content', timePunchinId]);
  }


  ngOnInit() {
    //this.myApp.getPEChartOfAccountList().subscribe(
      //result => { this.punch = result },
      //error => console.error(error)
    //);
    this.queryPunch();
    this.queryGrid();
    
   }//end ngOnInit
  
 }




