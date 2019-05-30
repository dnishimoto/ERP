

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[usp_CreateAccount]
(
  @ParamHashTable as AccountRegistrationTableType readonly
)
AS
BEGIN
   
   select * from @ParamHashTable

END
