CREATE TABLE [dbo].[Loans]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[BookId] INT NOT NULL,
	[MemberId] INT NOT NULL,
	[LoanDate] DATETIME NOT NULL,
	[ReturnDate] DATETIME NULL,
	[Status] NVARCHAR(10) NOT NULL DEFAULT 'Issued',

	CONSTRAINT FK_Owner FOREIGN KEY ([MemberId]) REFERENCES [dbo].[Users] (Id) on delete cascade,
	CONSTRAINT FK_Copy FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] (Id) on delete cascade
)
