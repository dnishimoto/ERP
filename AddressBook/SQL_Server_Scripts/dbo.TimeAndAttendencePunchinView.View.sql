USE [listensoftwareDB]
GO
/****** Object:  View [dbo].[TimeAndAttendencePunchinView]    Script Date: 7/30/2018 6:36:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create view [dbo].[TimeAndAttendencePunchinView]
as
SELECT [TimePunchinId]
      ,[PunchinDate]
      ,[PunchinDateTime]
      ,[PunchoutDateTime]
      ,[JobCodeXrefId]
	  ,udcJobCode.Value JobCode
      ,[Approved]
      ,employee.[EmployeeId]
	  ,employeeAddressBook.Name EmployeeName
      ,supervisor.[SupervisorId]
	  ,supervisorAddressBook.Name SupervisorName
      ,[ProcessedDate]
      ,[PunchoutDate]
      ,[Note]
      ,taschedule.[ShiftId]
      ,[mealPunchin]
      ,[mealPunchout]
      ,[ScheduledToWork]
      ,[TypeOfTimeUdcXrefId]
	  ,udcTypeOfTime.value TypeOfTime
      ,[ApprovingAddressId]
      ,[PayCodeXrefId]
	  ,udcPayCode.value PayCode
      ,taschedule.[ScheduleId]
      ,[DurationInMinutes]
  FROM [dbo].[TimeAndAttendancePunchIn] taPunchin join
  udc udcTypeOfTime on udctypeofTime.xrefid=taPunchin.TypeOfTimeUdcXrefId join
  udc udcJobCode on udcjobcode.xrefid=taPunchin.JobCodeXrefId left join
  udc udcPayCode on udcPayCode.xrefid=taPunchin.PayCodeXrefId join
 TimeAndAttendanceSchedule taSchedule on taschedule.ScheduleId=taPunchin.ScheduleId join

 supervisor supervisor on supervisor.SupervisorId=taPunchin.SupervisorId join
 addressbook supervisorAddressbook on supervisor.AddressId=supervisoraddressbook.addressid join

 employee employee on employee.employeeid = tapunchin.EmployeeId join
 addressbook employeeaddressbook on employee.AddressId=employeeaddressbook.addressid join

 addressbook addressbookApproving on addressbookApproving.AddressId=tapunchin.ApprovingAddressId

GO
