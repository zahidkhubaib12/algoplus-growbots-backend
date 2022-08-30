

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[RegisterUser]    
   @PUserId bigint  OUTPUT,  
   @PFirstName nvarchar(60),  
   @PLastName nvarchar(60),  
   @PIdentificationNumber nvarchar(20),  
   @PEmail nvarchar(256),   
   @PPasswordHash nvarchar(max),   
   @PAddress1 nvarchar(max)   

AS   
   BEGIN  

INSERT INTO applicationuser(Email,
FirstName,
LastName,
IdentificationNumber,
Address1,
PasswordHash,
Active)

VALUES

(@PEmail,
@PFirstName,
@PLastName,
@PIdentificationNumber,
@PAddress1,
@PPasswordHash,
1);

 SET @PUserId = scope_identity();  
  
END  




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[GetUserByEmail]    
    
   @PEmail nvarchar(256)   

AS   
   BEGIN  

   SET NOCOUNT ON;

SELECT UserId,Email,FirstName,LastName,PasswordHash 
FROM  applicationuser 
WHERE Email = @PEmail;

   END  
   SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[GetUserById]    
    
   @PUserId nvarchar(256)   

AS   
   BEGIN  

   SET NOCOUNT ON;

SELECT UserId,Email,FirstName,LastName,PasswordHash 
FROM  applicationuser 
WHERE UserId = @PUserId;

   END  


   

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[GetUsersList]    
    
AS   
   BEGIN  

   SET NOCOUNT ON;

SELECT UserId,Email,FirstName,LastName,PasswordHash 
FROM  applicationuser ;

   END  


   

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[UpdateUser]    
    @PUserId bigint ,  
   @PFirstName nvarchar(60),  
   @PLastName nvarchar(60),  
   @PEmail nvarchar(256)
AS   
   BEGIN  

   SET NOCOUNT ON;

UPDATE applicationuser
SET Email = @PEmail,
	FirstName = @PFirstName,
	LastName = @PLastName
	
WHERE UserId = @PUserId;

   END  

   

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[PasswordReset]    
    @PUserId bigint,      
    @PPasswordHash nvarchar(256)
AS   
   BEGIN  

   SET NOCOUNT ON;

	UPDATE applicationuser
	SET PasswordHash = @PPasswordHash
	WHERE UserId = @PUserId;

   END  