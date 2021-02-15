CREATE TABLE [dbo].[sw_user](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](20) NULL,
	[LastName] [varchar](50) NULL,
	[Gender] [varchar](20) NULL,
	[Phone] [varchar](20) NULL,
	[City] [varchar](100) NULL,
	[Region] [varchar](100) NULL,
	[Picture] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]