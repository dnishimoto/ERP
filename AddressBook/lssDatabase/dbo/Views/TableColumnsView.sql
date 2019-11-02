Create View TableColumnsView
as
select col1.TABLE_NAME
,col1.COLUMN_NAME 
,col1.DATA_TYPE
from INFORMATION_SCHEMA.COLUMNS col1
where 
1=1
--col1.TABLE_NAME='PackingSlip'
--and col1.IS_NULLABLE='NO'
and not exists
(
select col2.COLUMN_NAME
from INFORMATION_SCHEMA.KEY_COLUMN_USAGE col2
where col2.TABLE_NAME=col1.TABLE_NAME
AND col2.TABLE_SCHEMA=col1.TABLE_SCHEMA
and col2.COLUMN_NAME=col1.COLUMN_NAME
)