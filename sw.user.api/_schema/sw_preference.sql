CREATE TABLE [dbo].[sw_preference](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayPhoneNumber] [tinyint] NULL,
	[ReceiveNotificationNewArticle] [tinyint] NULL,
	[ReceiveEmail] [tinyint] NULL,
	[Fk_UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]