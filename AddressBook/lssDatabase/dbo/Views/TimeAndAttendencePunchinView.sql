
CREATE view [dbo].[TimeAndAttendencePunchinView]
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
	  ,taShift.ShiftName
	  ,taShift.ShiftStartTime
	  ,taShift.ShiftEndTime
  FROM [dbo].[TimeAndAttendancePunchIn] taPunchin 
	join   udc udcTypeOfTime 
		on udctypeofTime.xrefid=taPunchin.TypeOfTimeUdcXrefId 
	join  udc udcJobCode 
		on udcjobcode.xrefid=taPunchin.JobCodeXrefId 
	left join  udc udcPayCode 
		on udcPayCode.xrefid=taPunchin.PayCodeXrefId 
	left join TimeAndAttendanceSchedule taSchedule 
		on taschedule.ScheduleId=taPunchin.ScheduleId 
	left join TimeAndAttendanceShift taShift
		on taPunchin.ShiftId=taShift.ShiftId
	join supervisor supervisor 
		on supervisor.SupervisorId=taPunchin.SupervisorId 
	join addressbook supervisorAddressbook 
		on supervisor.AddressId=supervisoraddressbook.addressid 
	join employee employee 
		on employee.employeeid = tapunchin.EmployeeId 
	join addressbook employeeaddressbook 
		on employee.AddressId=employeeaddressbook.addressid 
	join addressbook addressbookApproving 
		on addressbookApproving.AddressId=tapunchin.ApprovingAddressId
