USE [listensoftwareDB]
GO
/****** Object:  View [dbo].[supervisorView]    Script Date: 7/30/2018 6:36:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[supervisorView]
as
select
supervisorid,
supervisorcode,
name supervisorname
from supervisor supervisor join
addressbook addressbook on supervisor.addressid=addressbook.addressid
GO
