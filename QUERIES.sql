-- Get a list of tables and views in the current database
SELECT table_catalog [database], table_schema [schema], table_name name, table_type type
FROM information_schema.tables
GO



-- Select rows from a Table or View 'Users' in schema 'dbo'
SELECT * FROM dbo.Users
 	/* add search conditions here */
GO