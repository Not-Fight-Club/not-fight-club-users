CREATE DATABASE NotFightClubUser

use NotFightClubUser;
go

CREATE TABLE UserInfo(
UserId uniqueidentifier not null default newId() primary key,
UserName nvarchar(50) not null,
PWord nvarchar(100) not null,
Email nvarchar(50) not null,
DOB date not null,
Bucks int default 20 check (Bucks>=20),
Active bit not null,
LastLogin date not null,
LoginStreak int not null,
ProfilePic varchar(MAX)
RewardCollected bit not null,
)



CREATE TABLE Rewards(
RewardsId uniqueidentifier not null default newId() primary key,
Title nvarchar(20) not null, 
Image_Url nvarchar not null,
StartDate Date not null,
EndDate Date not null,
RewardTier int not null
)

DROP TABLE UserInfo
