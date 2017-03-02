USE OnlineShop
GO

IF OBJECT_ID('[OnlineShop].[Shop].[Products]', 'U')	IS NOT NULL	    DROP TABLE [OnlineShop].[Shop].[Products]

CREATE TABLE [OnlineShop].[Shop].[Products](
ID          INT IDENTITY   NOT NULL PRIMARY KEY NONCLUSTERED,
Title       NVARCHAR(MAX)  NOT NULL,
Description NVARCHAR(MAX)  NOT NULL,
Link        NVARCHAR(MAX)  NOT NULL,
ImageBase64 VARCHAR(MAX)
)

IF OBJECT_ID('[OnlineShop].[Shop].[Prices]', 'U')	IS NOT NULL	    DROP TABLE [OnlineShop].[Shop].[Prices]

CREATE TABLE [OnlineShop].[Shop].[Prices](
ID          INT IDENTITY   NOT NULL PRIMARY KEY NONCLUSTERED,
ID_Product  INT FOREIGN KEY REFERENCES [OnlineShop].[Shop].[Products](ID) NOT NULL,
Price		INT			   NOT NULL,
PublishDate INT            NOT NULL,
)