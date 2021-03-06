USE [Master]
GO

IF db_id('FirebaseMVC') IS NULL
	CREATE DATABASE [FirebaseMVC]
GO

USE [FirebaseMVC]
GO

DROP TABLE IF EXISTS [UserProfile];

CREATE TABLE [UserProfile] (
	[Id] INTEGER IDENTITY NOT NULL,
	[Email] NVARCHAR(255) NOT NULL,
	[FirebaseUserId] NVARCHAR(28) NOT NULL,

	CONSTRAINT [UQ_FirebaseUserId] UNIQUE([FirebaseUserId])
)

INSERT INTO UserProfile (Email, FirebaseUserId) VALUES ('luke@gmail.com', 'DExr1FQ6SogOMIzhtB4G7LNgx0F2');