IF NOT EXISTS (Select * from sys.databases where name = 'BookingSystem')
BEGIN
	CREATE DATABASE BookingSystem
	Use BookingSystem
END

IF NOT EXISTS (Select * from sys.tables where name = 'MemberInfo')
BEGIN
	CREATE Table MemberInfo (
		Id INT PRIMARY KEY IDENTITY,
		[Name] VARCHAR(255),
		Surname VARCHAR(255),
		BookingCount INT,
		Date_Joined DATE,
	);
END

-- INSERT INTO MemberInfo VALUES ('Sophie',	'Davis'			, 1,	'2024-01-02T12:10:11')
-- INSERT INTO MemberInfo VALUES ('Emily'		,'Johnson'		, 0,	'2024-11-12T12:10:12')
-- INSERT INTO MemberInfo VALUES ('Jessica'	,'Rodriguez'	, 3,	'2024-01-02T12:10:13')
-- INSERT INTO MemberInfo VALUES ('Chloe'		,'Brown'		, 2,	'2024-01-02T12:10:14')
-- INSERT INTO MemberInfo VALUES ('Amelia'		,'Williams'		, 0,	'2024-01-02T12:10:15')
-- INSERT INTO MemberInfo VALUES ('Grace'		,'Miller'		, 1,	'2023-01-02T12:10:15')
-- INSERT INTO MemberInfo VALUES ('Lily'		,'Garcia'		, 1,	'2023-01-02T12:10:15')
-- INSERT INTO MemberInfo VALUES ('Ruby'		,'Jones'		, 1,	'2023-01-02T12:10:18')
-- INSERT INTO MemberInfo VALUES ('Charlotte'	,'Martinez'		, 1,	'2023-01-02T12:10:19')
-- INSERT INTO MemberInfo VALUES ('Evie'		,'Smith'		, 2,	'2023-01-02T12:10:20')
-- INSERT INTO MemberInfo VALUES ('Matthew'	,'Davis'		, 1,	'2023-01-02T12:10:21')
-- INSERT INTO MemberInfo VALUES ('Samuel'		,'Garcia'		, 1	,	'2023-01-02T12:10:22')
-- INSERT INTO MemberInfo VALUES ('Joshua'		,'Smith'		, 1	,	'2023-01-02T12:10:23')
-- INSERT INTO MemberInfo VALUES ('Jack'		,'Rodriguez'	, 1,	'2023-01-02T12:10:24')
-- INSERT INTO MemberInfo VALUES (' James'		,'Brown'		, 0,	'2024-01-02T12:10:25')
-- INSERT INTO MemberInfo VALUES ('Grace'		,'Miller'		, 0,	'2020-11-12T12:10:12')
-- INSERT INTO MemberInfo VALUES ('Joseph'		,'Miller'		, 0,	'2020-11-12T12:10:12')
-- INSERT INTO MemberInfo VALUES ('Jacob'		,'Frye'			, 2,	'2024-01-02T12:10:28')
-- INSERT INTO MemberInfo VALUES ('Evie'		,'Frye'			, 0,	'2024-01-02T12:10:29')
-- INSERT INTO MemberInfo VALUES ('Daniel'		,'Johnson'		, 3,	'2024-01-02T12:10:30')
-- INSERT INTO MemberInfo VALUES ('Thomas'		,'Williams'		, 0,	'2024-01-02T12:10:31')
-- INSERT INTO MemberInfo VALUES ('Luke'		,'Wilson'		, 0,	'2024-01-02T12:10:32')
-- INSERT INTO MemberInfo VALUES ('Benjamin'	,'Jones'		, 1,	'2024-01-02T12:10:33')
-- INSERT INTO MemberInfo VALUES ('Alexander'	,'Martin'		, 1,	'2022-01-02T12:10:34')
-- INSERT INTO MemberInfo VALUES ('Harry'		,'Moore'		, 2	,	'2022-01-02T12:10:35')
-- INSERT INTO MemberInfo VALUES ('Faye'		,'Hurst'		, 1	,	'2022-01-02T12:10:36')
-- INSERT INTO MemberInfo VALUES ('Tiana'		,'Silva'		, 0	,	'2022-01-02T12:10:37')
-- INSERT INTO MemberInfo VALUES ('Aleena'		,'Mahoney'		, 0	,	'2022-01-02T12:10:38')
-- INSERT INTO MemberInfo VALUES ('Robert'		,'Allen'		, 0	,	'2022-01-02T12:10:39')
-- INSERT INTO MemberInfo VALUES ('Aaron'		,'Mitchell'		, 0	,	'2022-01-02T12:10:40')
-- INSERT INTO MemberInfo VALUES ('Sophie'		,'Davis'		, 2	,	'2022-01-02T12:10:41')
-- INSERT INTO MemberInfo VALUES ('Jacob'		,'Green'		, 0	,	'2022-01-02T12:10:42')
-- INSERT INTO MemberInfo VALUES ('Ben'		,'Hill'			, 0	,	'2022-01-02T12:10:43')
-- INSERT INTO MemberInfo VALUES ('Owen'		,'Turner'		, 0	,	'2022-01-02T12:10:44')
-- INSERT INTO MemberInfo VALUES ('Louis'		,'Anderson'		, 0	,	'2024-01-02T12:10:45')
-- INSERT INTO MemberInfo VALUES ('Liam'		,'Jackson'		, 2	,	'2024-01-02T12:10:46')
-- INSERT INTO MemberInfo VALUES ('Ryan'		,'Hernandez'	, 0 ,	'2024-01-02T12:10:47')
-- INSERT INTO MemberInfo VALUES ('Adam'		,'Thomas'		, 0,	'2024-01-02T12:10:48')
-- INSERT INTO MemberInfo VALUES ('Lewis'		,'Thompson'		, 0,	'2024-01-02T12:10:49')
-- INSERT INTO MemberInfo VALUES ('Oliver'		,'White'		, 0,	'2024-01-02T12:10:50')
-- INSERT INTO MemberInfo VALUES ('Callum'		,'Taylor'		, 2,	'2024-01-02T12:10:51')
-- INSERT INTO MemberInfo VALUES ('William'	,'Martinez'		, 0,	'2024-01-02T12:10:52')
-- INSERT INTO MemberInfo VALUES ('Lauren'		,'Lang'			, 1,	'2024-01-02T12:10:53')
-- INSERT INTO MemberInfo VALUES ('Lena'		,'Fernandez'	, 2,	'2024-01-02T12:10:54')
-- INSERT INTO MemberInfo VALUES ('Eden'		,'Oneal'		, 3,	'2024-01-02T12:10:55')

Select Count(*) from MemberInfo


IF NOT EXISTS (Select * from sys.tables where name = 'Inventory')
BEGIN
	CREATE Table Inventory (
		Id INT PRIMARY KEY IDENTITY,
		Title VARCHAR(255),
		Description VARCHAR(4000),
		RemainingCount INT,
		Expiration_Date DATE,
	);
END

INSERT INTO Inventory VALUES ('Bali', 'Suspendisse congue erat ac ex venenatis mattis. Sed finibus sodales nunc, nec maximus tellus aliquam id. Maecenas non volutpat nisl. Curabitur vestibulum ante non nibh faucibus, sit amet pulvinar turpis finibus',	5,	'2030-11-19')
INSERT INTO Inventory VALUES ('Madeira',	'Donec condimentum, risus non mollis sollicitudin, est neque sagittis metus, eget aliquam orci quam eget dui. Nam imperdiet, lorem quis lacinia imperdiet, augue libero tincidunt sem, eget pulvinar massa est non metus. Pellentesque et massa nibh.', 4,	'2030-11-20')
INSERT INTO Inventory VALUES ('Paris trip',	'Pellentesque non aliquam eros, quis posuere leo. Nullam sit amet tempor orci. Phasellus quam velit, aliquet nec nisl et, commodo rutrum lorem. Proin egestas nisl eget magna commodo sagittis. Integer egestas sodales mi eu maximus. Mauris et auctor felis, at dictum ipsum. Curabitur eros neque, commodo non mi congue, eleifend molestie quam. Cras feugiat, turpis sed ullamcorper vestibulum, enim lectus sagittis dui, ac semper nulla leo id arcu. Suspendisse auctor risus eu magna dapibus suscipit. Cras luctus dapibus turpis, vel ornare diam dignissim sed. Sed odio ipsum, placerat et mi eu, eleifend bibendum risus. Aliquam sed iaculis elit. Donec interdum egestas metus, quis sollicitudin nisi imperdiet id. Integer non ante eleifend mi viverra interdum eu nec nibh. Sed ac tortor lorem',	3,	'2030-11-21')
INSERT INTO Inventory VALUES ('F1 stage',	'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam in augue ut velit condimentum',	2,	'2030-11-22')
INSERT INTO Inventory VALUES ('Hot air balloon', 'Etiam molestie sem id luctus facilisis. Proin vestibulum, mauris vitae suscipit suscipit, metus enim faucibus metus, a bibendum nibh lorem id nisl',	1,	'2021-11-23')
INSERT INTO Inventory VALUES ('Route 66', 'Quisque at feugiat purus. Praesent feugiat eget sem quis tincidunt. Aliquam dui magna, auctor sit amet turpis nec, porttitor porta ero' , 0, '2022-11-24')
INSERT INTO Inventory VALUES ('Milano',	'Nunc laoreet purus eget tristique suscipit. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos',	1,	'2030-11-25')
INSERT INTO Inventory VALUES ('Italy',	'Suspendisse potenti', 	2, 	'2030-11-26')
INSERT INTO Inventory VALUES ('Rome',	'Suspendisse dui felis, convallis in tortor vitae, lobortis molestie leo',	5,	'2030-11-27')
INSERT INTO Inventory VALUES ('France',	'Ut at euismod massa', 4,	'2030-11-28')
INSERT INTO Inventory VALUES ('Versailles',	'Sed tristique quam in elit bibendum tristique. Aliquam auctor, ante et pellentesque tristique, ipsum urna condimentum ante, vitae sodales ipsum augue ac massa.',	3,	'2030-11-29')
INSERT INTO Inventory VALUES ('London',	'Vestibulum in purus eu massa rutrum pretium ut non metus. Donec condimentum id elit eu convallis',	2, '2030-11-30')
INSERT INTO Inventory VALUES ('London',	'Quisque ut eleifend turpis', 1, '2023-12-01')
INSERT INTO Inventory VALUES ('Halifax', 'Nudus, turpis, Putridus, findere, Acerbus, crudus, Raptus, contemptio', 2, '2030-12-02')
INSERT INTO Inventory VALUES ('England', 'Ut mauris orci, sodales ac pulvinar nec, posuere et metus.', 10, '2030-12-03')

Select * from Inventory


IF NOT EXISTS(Select * from sys.tables where name = 'BookingDetails')
BEGIN
	CREATE TABLE BookingDetails(
		BookingId INT PRIMARY KEY IDENTITY,
		MemberId INT NOT NULL,
		InventoryId INT NOT NULL,
		Booking_Date DATE NOT NULL,
		Cancellation_Date DATE,
		FOREIGN KEY (MemberId) REFERENCES MemberInfo(Id),
		FOREIGN KEY (InventoryId) REFERENCES Inventory(Id)
	);
END


Select * from BookingDetails
Select * from MemberInfo Order by Id Desc
Select * from Inventory Where Id = 1