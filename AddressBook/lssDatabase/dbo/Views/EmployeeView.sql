
CREATE view [dbo].[EmployeeView]
as
select
employeeid,
name employeeName,
JobTitleXrefId,
jobcodeUDC.KeyCode JobCode,
jobcodeUDC.Value JobCodeDescription
from employee employee join
addressbook addressbook on employee.addressid=addressbook.addressid 
     join udc jobcodeUDC on 
	 jobcodeUDC.XRefId=employee.JobTitleXrefId  
