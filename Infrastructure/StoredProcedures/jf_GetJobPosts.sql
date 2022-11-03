CREATE PROCEDURE [jf_GetJobPosts]
@keyword VARCHAR(100) = '',
@pageNumber INT = 1,
@pageSize INT = 10,
@sortingCol VARCHAR(100) = 'CreatedAt',
@sortDirection VARCHAR(100) = 'ASC',
@totalCount INT OUTPUT
AS
BEGIN


SET @keyword = CASE WHEN (@keyword IS NULL) THEN '' ELSE @keyword END 
SET @sortDirection = CASE WHEN (@sortDirection IS NULL OR @sortDirection = '' ) THEN 'ASC' ELSE @sortDirection END 
SET @sortingCol = CASE WHEN (@sortingCol IS NULL OR @sortingCol = '') THEN 'CreatedAt' ELSE @sortingCol END 
SET @pageNumber = CASE WHEN (@pageNumber IS NULL OR @pageNumber = 0) THEN 1 ELSE @pageNumber END 

DECLARE @OrderByClause NVARCHAR(MAX) 
DECLARE @WhereClause NVARCHAR(MAX) 
DECLARE @ItemsSql NVARCHAR(MAX) 
DECLARE @CountSql NVARCHAR(MAX) 
DECLARE @SqlStatement NVARCHAR(MAX) 
DECLARE @TotalRowCount INT

SET @CountSql = 'SELECT @TotalRowCount = COUNT(*) ';
SET @ItemsSql = 'SELECT
					j.ID as JobID, p.ID as PostID,
					j.fk_CorporateID, j.fk_JobPostedByProfileID, j.CreatedAt,
					c.Company, c.HeadQuarterName, p.[Text] as Caption, 
					STRING_AGG(ISNULL(pt.[Text], ''''), '','') AS PostTags,
					STRING_AGG(ISNULL(pf.FileKey, ''''), '','') AS PostFiles,
					COUNT(pc.ID) AS Comments,
					COUNT(pr.ID) AS Reactions,
					COUNT(ja.ID) AS Applications,
					j.JobType,
					j.WorkPlaceType,
					j.Location ';
SET @WhereClause = 		 
	'FROM dbo.CorporateJob j
		INNER JOIN dbo.Corporate c ON j.fk_CorporateID = c.ID
		INNER JOIN dbo.Post p ON j.fk_PostID = p.ID
		INNER JOIN dbo.PostTag pt ON p.ID = pt.fk_PostID
		LEFT JOIN dbo.PostFile pf ON p.ID = pf.fk_PostID
		LEFT JOIN dbo.PostComment pc ON p.ID = pc.fk_PostID
		LEFT JOIN dbo.PostReaction pr ON p.ID = pr.fk_PostID
		LEFT JOIN dbo.JobApplicant ja ON j.ID = ja.fk_CorporateJobID
	WHERE 
		(j.IsDeleted = 0 AND j.IsActive = 1)
		AND
		(
			(''' + @keyword + ''' IS NULL OR ''' + @keyword + ''' = '''') 
					OR (p.[Text] LIKE ''%'' + ''' + @keyword + ''' + ''%'')
					OR (c.Company LIKE ''%'' + ''' + @keyword + ''' + ''%'')
					OR (c.HeadQuarterName LIKE ''%'' + ''' + @keyword + ''' + ''%'')
					OR (pt.[Text] LIKE ''%'' + ''' + @keyword + ''' + ''%'')
						
		)
	GROUP BY 	j.ID, p.ID, j.fk_CorporateID, j.fk_JobPostedByProfileID, j.CreatedAt, p.[Text], c.Company, c.HeadQuarterName,j.JobType,j.WorkPlaceType,Location ';





SET @OrderByClause =
        ' ORDER BY ''' + @sortingCol + ''' ' + @sortDirection ;

IF @pageSize > 0 
BEGIN
	SET @OrderByClause = @OrderByClause + ' OFFSET(' + CAST(@pageNumber AS VARCHAR(MAX)) +' - 1) * ' + CAST(@pageSize AS VARCHAR(MAX)) +
        ' ROWS FETCH NEXT ' + CAST(@pageSize AS VARCHAR(MAX)) + ' ROWS ONLY';
END

-- DATA
SET @SqlStatement = @ItemsSql + @WhereClause + @OrderByClause;
EXEC(@SqlStatement);
--Paging DATA
SET @SqlStatement = @CountSql + @WhereClause;
EXEC sp_executesql @SqlStatement, N'@TotalRowCount INT OUTPUT', @totalCount OUTPUT


END