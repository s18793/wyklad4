use s18793;

Alter PROCEDURE PromoteStudents @Name varchar(50), @Semester INT
    AS
	 BEGIN
	
	BEGIN TRAN
	
	DECLARE @IdStudies INT = (SELECT IdStudy From Studies where Name=@Name);
	IF @IdStudies IS NULL
		BEGIN
	      ROLLBACK 
	      RETURN;
		END
		
		DECLARE @IdEnrollment INT = (Select IdEnrollment from Enrollment where IdStudy = @IdStudies AND Semester = @Semester );
		DECLARE @IdEnrollmentPlus INT = (Select IdEnrollment from Enrollment where IdStudy = @IdStudies AND Semester = (@Semester +1) );

		IF @IdEnrollmentPlus IS NULL
			BEGIN
				DECLARE @NewIdEnrollment INT = (Select MAX(IdEnrollment) from Enrollment) + 1;
				INSERT INTO Enrollment(IdEnrollment, Semester, IdStudy, StartDate) VALUES(@NewIdEnrollment, @Semester + 1, @IdStudies, CURRENT_TIMESTAMP); 
				UPDATE Student set IdEnrollment = @NewIdEnrollment where IdEnrollment = @IdEnrollment
			Commit
			END
			
		UPDATE Student set IdEnrollment = @IdEnrollmentPlus where IdEnrollment = @IdEnrollment;
		Commit
		
	       END;