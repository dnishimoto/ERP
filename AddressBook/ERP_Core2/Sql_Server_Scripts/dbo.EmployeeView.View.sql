USE [listensoftwareDB]
GO
/****** Object:  View [dbo].[EmployeeView]    Script Date: 7/30/2018 6:36:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[EmployeeView]
as
select
employeeid,
name employeeName
from employee employee join
addressbook addressbook on employee.addressid=addressbook.addressid        
GO
