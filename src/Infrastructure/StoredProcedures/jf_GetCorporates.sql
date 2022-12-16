CREATE PROCEDURE [jf_GetCorporates]
@keyword VARCHAR(100) = '',
@pageNumber INT = 1,
@pageSize INT = 10,
@sortingCol VARCHAR(100) = 'ID',
@sortDirection VARCHAR(100) = 'ASC',
@totalCount INT OUTPUT
AS
BEGIN


SET @keyword = CASE WHEN (@keyword IS NULL) THEN '' ELSE @keyword END 
SET @sortDirection = CASE WHEN (@sortDirection IS NULL OR @sortDirection = '' ) THEN 'ASC' ELSE @sortDirection END
SET @sortingCol = CASE 
									WHEN (@sortingCol IS NULL OR @sortingCol = '') THEN 'ID' 
									WHEN (@sortingCol = 'Company') THEN 'c.Company' 
									WHEN (@sortingCol = 'HeadQuarterName') THEN 'c.HeadQuarterName' 
									WHEN (@sortingCol = 'HeadQuarterContact') THEN 'c.HeadQuarterContact' 
									ELSE @sortingCol END 
SET @pageNumber = CASE WHEN (@pageNumber IS NULL OR @pageNumber = 0) THEN 1 ELSE @pageNumber END 

DECLARE @OrderByClause NVARCHAR(MAX) 
DECLARE @WhereClause NVARCHAR(MAX) 
DECLARE @ItemsSql NVARCHAR(MAX) 
DECLARE @CountSql NVARCHAR(MAX) 
DECLARE @SqlStatement NVARCHAR(MAX) 
DECLARE @TotalRowCount INT

SET @CountSql = 'SELECT @TotalRowCount = COUNT(*) ';
SET @ItemsSql = '
		SELECT 
			u.ID, u.UserName, u.FirstName, u.LastName, 
			u.PhoneNumber, u.Gender, u.Ethnicity, 
			u.IsOnBoarded, u.IsActive, u.fk_RoleID,
			c.ID AS CorporateID, c.Company, c.HeadQuarterName, c.HeadQuarterContact ';
SET @WhereClause = 	'
		FROM [User] AS u
			INNER JOIN [UserCorporateProfile] AS cp ON u.ID = cp.fk_UserID
			INNER JOIN [Corporate] AS c ON cp.fk_CorporateID = c.ID
		WHERE 
			(u.IsDeleted = 0 AND cp.IsDeleted = 0 AND cp.IsActive = 1)
			AND
			(
				( 
					(''' +  LOWER(REPLACE(REPLACE(@keyword,' ',''),'-','')) +  ''' IN (''male'',''female'', ''prefernottoanswer''))
					AND
					(	
						(u.Gender = 1 AND ''' +  LOWER(REPLACE(REPLACE(@keyword,' ',''),'-','')) +  '''  LIKE ''%male%'')
						OR 
						(u.Gender = 2 AND ''' +  LOWER(REPLACE(REPLACE(@keyword,' ',''),'-','')) +  '''  LIKE ''%female%'')
						OR 
						(u.Gender = 3 AND ''' +  LOWER(REPLACE(REPLACE(@keyword,' ',''),'-','')) +  '''  LIKE ''%prefernottoanswer%'')
					)
				)
				OR
				( 
					(''' +  LOWER(REPLACE(REPLACE(@keyword,' ',''),'-','')) +  ''' IN (''american indian'',''asian'', ''black or african american'', ''hispanic or latino'', ''white''))
					AND
					(	
						(u.Ethnicity = 1 AND ''' +  LOWER(REPLACE(REPLACE(@keyword,' ',''),'-','')) +  ''' LIKE ''%american indian%'')
						OR 
						(u.Ethnicity = 2 AND ''' +  LOWER(REPLACE(REPLACE(@keyword,' ',''),'-','')) +  '''  LIKE ''%asian%'')
						OR 
						(u.Ethnicity = 3 AND ''' +  LOWER(REPLACE(REPLACE(@keyword,' ',''),'-','')) +  '''  LIKE ''%black or african american%'')
						OR 
						(u.Ethnicity = 4 AND ''' +  LOWER(REPLACE(REPLACE(@keyword,' ',''),'-','')) +  '''  LIKE ''%hispanic or latino%'')
						OR 
						(u.Ethnicity = 5 AND ''' +  LOWER(REPLACE(REPLACE(@keyword,' ',''),'-','')) +  '''  LIKE ''%white%'')
					)
				)
				OR
				(
					(''' + @keyword + ''' IS NULL OR ''' + @keyword + ''' = '''') 
					OR (u.UserName LIKE ''%'' + ''' + @keyword + ''' + ''%'')
					OR (u.FirstName LIKE ''%'' + ''' + @keyword + ''' + ''%'')
					OR (u.LastName LIKE ''%'' + ''' + @keyword + ''' + ''%'')
					OR (u.PhoneNumber LIKE ''%'' + ''' + @keyword + ''' + ''%'')
					OR (c.Company LIKE ''%'' + ''' + @keyword + ''' + ''%'')
					OR (c.HeadQuarterName LIKE ''%'' + ''' + @keyword + ''' + ''%'')
					OR (c.HeadQuarterContact LIKE ''%'' + ''' + @keyword + ''' + ''%'')
				)
			)
';



SET @OrderByClause =		
		' ORDER BY ''' + @sortingCol + ''' ' + @sortDirection ;

IF @pageSize > 0 
BEGIN
	SET @OrderByClause = @OrderByClause + ' OFFSET(' + CAST(@pageNumber AS VARCHAR(MAX)) + ' - 1) * ' + CAST(@pageSize AS VARCHAR(MAX)) + 
		' ROWS FETCH NEXT ' + CAST(@pageSize AS VARCHAR(MAX)) + ' ROWS ONLY';
END

-- DATA
SET @SqlStatement = @ItemsSql + @WhereClause + @OrderByClause;
EXEC(@SqlStatement);
-- Paging DATA
SET @SqlStatement = @CountSql + @WhereClause;
EXEC sp_executesql @SqlStatement, N'@TotalRowCount INT OUTPUT', @totalCount OUTPUT


END