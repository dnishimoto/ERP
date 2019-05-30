create view supervisorView
as
select
supervisorid,
supervisorcode,
name supervisorname
from supervisor supervisor join
addressbook addressbook on supervisor.addressid=addressbook.addressid