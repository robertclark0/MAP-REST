USE [MAP]
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateReport]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_CreateReport]
	-- Add the parameters for the stored procedure here
			@EntityCode varchar(20),
            @GUID  varchar(30),
            @User  varchar(50), 
            @Report_Name  varchar(50), 
            @Report_Type  varchar(50),
			@JSON varchar(max)

AS

DECLARE @ProductLineID int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  SET @ProductLineID = (SELECT p.ProductLineID
  FROM [MAP].[dbo].[ProductLine] P
  JOIN [MAP].dbo.Entity E
  ON E.EntityID = P.EntityID
  WHERE e.Code = @EntityCode)

    -- Insert statements for procedure here
	
INSERT INTO [dbo].[Report]
           ([ProductLineID]
           ,[GUID]
           ,[User]
           ,[Report_Name]
           ,[Report_Type]
           ,[JSON])
     VALUES
           (@ProductLineID,
           @GUID, 
           @User, 
           @Report_Name, 
           @Report_Type,
		   @JSON)


END


GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteReport]   Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[usp_DeleteReport]
	-- Add the parameters for the stored procedure here
	@GUID varchar(30)
as
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	/****** Script for SelectTopNRows command from SSMS  ******/
DELETE
  FROM [MAP].[dbo].[Report]
  WHERE GUID = @GUID
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetConnectionString]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Grieb, Lance
-- Create date: 27-JUN-2016
-- Description:	Get Connectivity info.
-- =============================================

CREATE PROCEDURE [dbo].[usp_GetConnectionString]
	-- Add the parameters for the stored procedure here
	(
	@EntityCode varchar(20),
	@Environment varchar(10)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT e.Code, c.ConnectionStringID, c.Environment, c.ConnectionString
	FROM dbo.ConnectionString c
	JOIN dbo.Entity e
	ON c.EntityID = e.EntityID
	WHERE e.Code = @EntityCode
	AND c.Environment=@Environment
	
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetDataSource]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetDataSource]
	@Code  varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT e.Code,
		DataSourceID,
        SourceName, 
		SourceType
  FROM [MAP].[dbo].[DataSource] p
  JOIN [MAP].[dbo].[Entity] e
    on e.EntityID = p.EntityID
WHERE e.Code = @Code

END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetDataSourceParameters]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetDataSourceParameters]
	@DataSourceID  int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT [DataSourceParameterID]
		,[ParameterName]
		,[DataType]
		,[ParameterType]
		,[TableReference]
		,[ColumnReference]
FROM [dbo].[DataSourceParameter]
WHERE DataSourceID = @DataSourceID 
ORDER BY 1
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetProductLine]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetProductLine]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
  SELECT E.Code, E.Name, P.Active, P.Icon, P.IconClass, P.HasPII,  P.Module, P.ModuleName
  FROM [MAP].[dbo].[ProductLine] P
  JOIN [MAP].dbo.Entity E
  ON E.EntityID = P.EntityID
    ORDER BY 2


END	


GO
/****** Object:  StoredProcedure [dbo].[usp_GetReport]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[usp_GetReport]
	-- Add the parameters for the stored procedure here
	@GUID varchar(30)
as
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [ReportID]
      ,[GUID]
      ,[User]
      ,[Report_Name]
      ,[Report_Type]
      ,[JSON]
      ,[AuditDate]
  FROM [MAP].[dbo].[Report]
  WHERE GUID = @GUID
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetReportList]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetReportList]
	-- Add the parameters for the stored procedure here
	@Code varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	/****** Script for SelectTopNRows command from SSMS  ******/
SELECT r.ReportID, r.[GUID], r.[User], r.Report_Name, r.Report_Type, r.AuditDate
  FROM [MAP].[dbo].[ProductLine] p
  JOIN [MAP].[dbo].[Entity] e
    on e.EntityID = p.EntityID
JOIN [dbo].[Report] r
	ON r.ProductLineID = p.ProductLineID
WHERE e.Code = @Code
END

GO
/****** Object:  StoredProcedure [dbo].[usp_InsClientLog]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Grieb, Lance
-- Create date: 28-JUN-2016
-- Description:	Capture server log data for MAP Application
-- =============================================
CREATE PROCEDURE [dbo].[usp_InsClientLog]
	-- parameters 
	(
	@ClientSessionID varchar(36),
	@ClientTime datetime,
	@User varchar(500),
    @RecordType varchar(50),
    @RecordValue varchar(max)
    )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statement
INSERT INTO [MAP].[dbo].[ClientLog]
           (
			[ClientSessionID],
			ClientTime,
			[User],
            [RecordType],
            [RecordValue]
            )
     VALUES
           (
			@ClientSessionID,
			@ClientTime,
			@User ,
			@RecordType,
			@RecordValue
           )

END
GO
/****** Object:  StoredProcedure [dbo].[usp_InsServerLog]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Grieb, Lance
-- Create date: 28-JUN-2016
-- Description:	Capture server log data for MAP Application
-- =============================================
CREATE PROCEDURE [dbo].[usp_InsServerLog]
	-- parameters 
	(
	@ServerSessionID varchar(36),
    @ClientSessionID varchar(36),
    @RecordType varchar(50),
    @RecordValue varchar(max)
    )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statement
INSERT INTO [MAP].[dbo].[ServerLog]
           (
			[ServerSessionID],
            [ClientSessionID],
            [RecordType],
            [RecordValue]
            )
     VALUES
           (
           @ServerSessionID,
           @ClientSessionID,
           @RecordType,
           @RecordValue
           )

END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateReport]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_UpdateReport]
	-- Add the parameters for the stored procedure here
			@GUID  varchar(30),
            @User  varchar(50), 
            @Report_Name  varchar(50), 
            @Report_Type  varchar(50),
			@JSON varchar(max)

AS

DECLARE @ProductLineID int
BEGIN


    -- Insert statements for procedure here
	
UPDATE  [dbo].[Report] SET [User] =@User,
        [Report_Name]=  @Report_Name, 
        [Report_Type] = @Report_Type,
        [JSON]= @JSON
	WHERE GUID = @GUID
          
          
		  


END


GO
/****** Object:  Table [dbo].[ClientLog]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClientLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[ClientSessionID] [varchar](36) NULL,
	[ClientTime] [datetime] NULL,
	[User] [varchar](500) NULL,
	[RecordType] [varchar](50) NULL,
	[RecordValue] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ConnectionString]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConnectionString](
	[ConnectionStringID] [int] NOT NULL,
	[EntityID] [int] NULL,
	[Environment] [varchar](10) NULL,
	[ConnectionString] [varchar](200) NULL,
 CONSTRAINT [PK_ConnectionStringID] PRIMARY KEY CLUSTERED 
(
	[ConnectionStringID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DataSource]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DataSource](
	[DataSourceID] [int] IDENTITY(1,1) NOT NULL,
	[EntityID] [int] NOT NULL,
	[SourceName] [varchar](50) NULL,
	[SourceType] [char](1) NULL,
 CONSTRAINT [PK_DataSource] PRIMARY KEY CLUSTERED 
(
	[DataSourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DataSourceParameter]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DataSourceParameter](
	[DataSourceParameterID] [int] IDENTITY(1,1) NOT NULL,
	[DataSourceID] [int] NOT NULL,
	[ParameterName] [varchar](50) NULL,
	[DataType] [varchar](50) NULL,
	[ParameterType] [varchar](50) NULL,
	[TableReference] [varchar](100) NULL,
	[ColumnReference] [varchar](100) NULL,
 CONSTRAINT [PK_DataSourceParameter] PRIMARY KEY CLUSTERED 
(
	[DataSourceParameterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Entity]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Entity](
	[EntityID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](20) NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED 
(
	[EntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductLine]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductLine](
	[ProductLineID] [int] IDENTITY(1,1) NOT NULL,
	[EntityID] [int] NOT NULL,
	[Active] [int] NULL,
	[Icon] [varchar](50) NULL,
	[IconClass] [varchar](50) NULL,
	[HasPII] [int] NULL,
	[Module] [varchar](50) NULL,
	[ModuleName] [varchar](50) NULL,
 CONSTRAINT [PK_ProductLine] PRIMARY KEY CLUSTERED 
(
	[ProductLineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Report]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Report](
	[ReportID] [int] IDENTITY(1,1) NOT NULL,
	[ProductLineID] [int] NOT NULL,
	[GUID] [varchar](30) NULL,
	[User] [varchar](50) NULL,
	[Report_Name] [varchar](50) NULL,
	[Report_Type] [varchar](50) NULL,
	[JSON] [varchar](max) NULL,
	[AuditDate] [datetime] NULL CONSTRAINT [DF_Date]  DEFAULT (getdate()),
 CONSTRAINT [PK_ReportID] PRIMARY KEY CLUSTERED 
(
	[ReportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ReportText]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReportText](
	[ReportTextID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [varchar](30) NULL,
	[Binding] [varchar](100) NULL,
	[Binding_Value] [varchar](100) NULL,
	[Text] [varchar](500) NULL,
 CONSTRAINT [PK_ReportTextID] PRIMARY KEY CLUSTERED 
(
	[ReportTextID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ServerLog]    Script Date: 8/25/2016 8:45:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ServerLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[ServerSessionID] [varchar](36) NULL,
	[ClientSessionID] [varchar](36) NULL,
	[ServerTime] [datetime] NULL CONSTRAINT [DF_DATEAudit]  DEFAULT (getdate()),
	[RecordType] [varchar](50) NULL,
	[RecordValue] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ConnectionString]  WITH CHECK ADD  CONSTRAINT [FK_ConnectionStringEntityID] FOREIGN KEY([EntityID])
REFERENCES [dbo].[Entity] ([EntityID])
GO
ALTER TABLE [dbo].[ConnectionString] CHECK CONSTRAINT [FK_ConnectionStringEntityID]
GO
ALTER TABLE [dbo].[DataSource]  WITH CHECK ADD  CONSTRAINT [FK_DataSourceEntityID] FOREIGN KEY([EntityID])
REFERENCES [dbo].[Entity] ([EntityID])
GO
ALTER TABLE [dbo].[DataSource] CHECK CONSTRAINT [FK_DataSourceEntityID]
GO
ALTER TABLE [dbo].[DataSourceParameter]  WITH CHECK ADD  CONSTRAINT [FK_DataSourceID] FOREIGN KEY([DataSourceID])
REFERENCES [dbo].[DataSource] ([DataSourceID])
GO
ALTER TABLE [dbo].[DataSourceParameter] CHECK CONSTRAINT [FK_DataSourceID]
GO
ALTER TABLE [dbo].[ProductLine]  WITH CHECK ADD  CONSTRAINT [FK_EntityID] FOREIGN KEY([EntityID])
REFERENCES [dbo].[Entity] ([EntityID])
GO
ALTER TABLE [dbo].[ProductLine] CHECK CONSTRAINT [FK_EntityID]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_ProdLineID] FOREIGN KEY([ProductLineID])
REFERENCES [dbo].[ProductLine] ([ProductLineID])
GO
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_ProdLineID]
GO
