CREATE TABLE [dbo].[AspNetUsers](
    [Id] NVARCHAR(450) NOT NULL,
    [AccessFailedCount] INT NOT NULL,
    [ConcurrencyStamp] NVARCHAR(MAX) NULL,
    [Email] NVARCHAR(256) NULL,
    [EmailConfirmed] INT NOT NULL,
    [LockoutEnabled] INT NOT NULL,
    [LockoutEnd] DATETIMEOFFSET(7) NULL,
    [NormalizedEmail] NVARCHAR(256) NULL,
    [NormalizedUserName] NVARCHAR(256) NULL,
    [PasswordHash] NVARCHAR(MAX) NULL,
    [PhoneNumber] NVARCHAR(MAX) NULL,
    [PhoneNumberConfirmed] INT NOT NULL,
    [SecurityStamp] NVARCHAR(MAX) NULL,
    [TwoFactorEnabled] INT NOT NULL,
    [UserName] NVARCHAR(256) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
