USE [ESC]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetFull]    Script Date: 8/27/2024 9:44:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_GetFull]
	@employeeID int = null,
	@fName varchar(20) = null,
	@lName varchar(20) = null,
	@email varchar(100) = null, 
	@dName varchar(30) = null,
	@cName varchar(40) = null,
	@rName varchar(25) = null,
	@getType bit = 1

AS
	SET NOCOUNT ON

	SELECT 
		a.employee_id as [Employee.ID],
		a.first_name as [Employee.FirstName],
		a.last_name as [Employee.LastName],
		a.email as [Employee.Email],
		a.phone_number as [Employee.PhoneNumber],
		a.hire_date as [Employee.HireDate],
		a.job_id as [Employee.JobID],
		a.department_id as [Employee.DepartmentID],
		a.manager_id as [Employee.ManagerID],
		b.dependent_id as [Dependent.ID],
		b.employee_id as [Dependent.EmployeeID],
		b.first_name as [Dependent.FirstName],
		b.last_name as [Dependent.LastName],
		b.relationship as[Dependent.Relationship],
		c.job_id as [Job.ID],
		c.job_title as [Job.Title],
		c.min_salary as [Job.MinSalary],
		c.max_salary as [Job.MaxSalary],
		d.department_id as [Department.ID],
		d.department_name as [Department.Name],
		d.location_id as [Department.LocationID],
		e.location_id as [Location.ID],
		e.country_id as [Location.CountryID],
		e.street_address as [Location.StreetAddress],
		e.city as [Location.City],
		e.state_province as [Location.StateProvince],
		e.postal_code as [Location.PostalCode],
		f.country_id as [Country.ID],
		f.region_id as [Country.RegionID],
		f.country_name as [Country.Name],
		g.region_id as [Region.ID],
		g.region_name as [Region.Name]
	
	FROM
		EMPLOYEES a
	LEFT JOIN 
		DEPENDENTS b
	ON
		a.employee_id = b.employee_id
	JOIN
		JOBS c
	ON
		a.job_id = c.job_id
	JOIN
		DEPARTMENTS d
	ON
		a.department_id = d.department_id
	JOIN
		LOCATIONS e
	ON
		d.location_id = e.location_id
	JOIN
		COUNTRIES f
	ON
		e.country_id = f.country_id
	JOIN
		REGIONS g
	on
		f.region_id = g.region_id
	
	WHERE
	(		
		@getType = 0
			AND
			(
					a.employee_id = @employeeID
				OR
					a.first_name = @fName
				OR
					a.last_name = @lName
				OR
					a.email = @email
				OR
					d.department_name = @dName
				OR
					f.country_name = @cName
				OR
					g.region_name = @rName
			)
		OR 
		@getType = 1
	)
FOR JSON PATH
	
RETURN 0