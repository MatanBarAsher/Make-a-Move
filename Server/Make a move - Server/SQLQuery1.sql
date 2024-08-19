CREATE TABLE [dbo].[CitiesTable] (
    [cityCode] smallint NOT NULL ,
    [cityName] nvarchar (20) NOT NULL ,
    PRIMARY KEY (cityCode)
)

CREATE TABLE [dbo].[PersonalInterestTable] (
    [interestCode] smallint NOT NULL ,
    [interestDesc] nvarchar (50) NOT NULL ,
    PRIMARY KEY (interestCode)
)

CREATE TABLE [dbo].[FeedbacksTable] (
    [serialNumber] smallint NOT NULL ,
    [feedbackDescription] nvarchar (50) NOT NULL ,
    [firstOption] nvarchar (20) NOT NULL ,
    [secontOption] nvarchar (20) NOT NULL ,
    [thirdOption] nvarchar (20) NOT NULL ,
    [fourthdOption] nvarchar (20) NOT NULL ,
    [required] bit NOT NULL ,
    PRIMARY KEY (serialNumber)
)

CREATE TABLE [dbo].[TypeOfPlaceTable] (
    [typeOfPlaceCode] smallint NOT NULL ,
    [typeOfPlaceDescription] nvarchar (50) NOT NULL ,
	PRIMARY KEY (typeOfPlaceCode)
)

drop table [dbo].[PreferencesTable]
CREATE TABLE [dbo].[PreferencesTable] (
    [preferenceCode] smallint NOT NULL ,
    [preferenceDescription] nvarchar (50) NOT NULL ,
    [firstOption] nvarchar (20) NOT NULL ,
    [secondOption] nvarchar (20) NOT NULL ,
    [thirdOption] nvarchar (20) NOT NULL ,
    [fourthOption] nvarchar (20) NOT NULL ,
    [required] bit NOT NULL ,
	PRIMARY KEY (preferenceCode)
)

CREATE TABLE [dbo].[AdminTable] (
    [adminCode] smallint NOT NULL ,
    [adminName] nvarchar (20) NOT NULL ,
	PRIMARY KEY (adminCode)
)

CREATE TABLE [dbo].[MatchesTable] (
    [userIds] nvarchar (30) NOT NULL ,
    [timeStamp] datetime NOT NULL ,
    [isMatch] bit NULL ,
    [serialNumber] smallint NOT NULL ,
    PRIMARY KEY (userIds, timeStamp), -- Composite primary key
    FOREIGN KEY ([serialNumber]) REFERENCES [dbo].[FeedbacksTable] ([serialNumber])
)

CREATE TABLE [dbo].[PlacsTable] (
    [placeCode] smallint NOT NULL ,
    [name] nvarchar (20) NOT NULL ,
    [adress] nvarchar (30) NOT NULL ,
    [userIds] nvarchar (30) NOT NULL ,
    [timeStamp] datetime NOT NULL ,
	PRIMARY KEY (placeCode),
    FOREIGN KEY ([userIds], [timeStamp]) REFERENCES [dbo].[MatchesTable] ([userIds], [timeStamp])
)

CREATE TABLE [dbo].[UsersTable] (
    [email] nvarchar (30) NOT NULL ,
    [firstName] nvarchar (20) NOT NULL ,
    [lastName] nvarchar (20) NOT NULL ,
    [password] nvarchar (10) NOT NULL ,
    [image] nvarchar (30) NOT NULL ,
    --need to check image
	[gender] tinyint NOT NULL CHECK (gender IN (1, 2 )), -- 1: Male, 2: Female
    [height] smallint NOT NULL ,
    [birthday] date NOT NULL ,
    [phoneNumber] nvarchar (10) NOT NULL ,
    [isActive] bit NULL ,
    [placeCode] smallint NOT NULL ,
    [cityCode] smallint NOT NULL ,
    [userIds] nvarchar (30) NOT NULL ,
    [timeStamp] datetime NOT NULL ,
	PRIMARY KEY (email),
    FOREIGN KEY ([placeCode]) REFERENCES [dbo].[PlacsTable] ([placeCode]),
    FOREIGN KEY ([cityCode]) REFERENCES [dbo].[CitiesTable] ([cityCode]),
   FOREIGN KEY ([userIds], [timeStamp]) REFERENCES [dbo].[MatchesTable] ([userIds], [timeStamp])
    )






