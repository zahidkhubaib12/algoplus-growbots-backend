
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[applicationuser](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](256) NULL,
	[FirstName] [varchar](60) NULL,
	[LastName] [varchar](60) NULL,
	[IdentificationNumber] [varchar](20) NOT NULL,
	[Address1] [varchar](100) NULL,
	[Address2] [varchar](100) NULL,
	[PostalCode] [varchar](20) NULL,	
	[EmailConfirmed] [bit]  NULL,
	[PasswordHash] [varchar](256) NULL,		
	[PhoneNumber] [varchar](20) NULL,
	[AccessFailedCount] [int]  NULL,
	[CreatedById] [bigint] NULL,
	[CreatedDate] [datetime2](0) NULL,
	[ModifiedById] [bigint] NULL,
	[ModifiedDate] [datetime2](0) NULL,
	[Active] [bit] NOT NULL
 CONSTRAINT [PK_applicationuser_UserId] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


