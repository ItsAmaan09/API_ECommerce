CREATE TABLE mstUser(
	[Id] [INT] PRIMARY KEY IDENTITY(1,1),
	[Username] [NVARCHAR](100) NOT NULL,
	[Email] [NVARCHAR](100) NOT NULL,
	[PasswordHash] [NVARCHAR](255) NOT NULL,

)