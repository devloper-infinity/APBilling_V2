SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF OBJECT_ID('dbo.CostMasterAccessMaster','U') IS NULL
CREATE TABLE dbo.CostMasterAccessMaster(
 SecurityArea nvarchar(30) NOT NULL,
 EmployeeID bigint NOT NULL,
 IsActive bit NOT NULL CONSTRAINT DF_CostMasterAccessMaster_IsActive DEFAULT(1),
 AddedBy bigint NULL,
 AddedUtc datetime2(0) NOT NULL CONSTRAINT DF_CostMasterAccessMaster_AddedUtc DEFAULT(SYSUTCDATETIME()),
 UpdatedBy bigint NULL, UpdatedUtc datetime2(0) NULL,
 CONSTRAINT PK_CostMasterAccessMaster PRIMARY KEY(SecurityArea,EmployeeID)
);
IF COL_LENGTH('dbo.CostMasterAccessMaster','SecurityArea') IS NULL BEGIN
 ALTER TABLE dbo.CostMasterAccessMaster ADD SecurityArea nvarchar(30) NOT NULL CONSTRAINT DF_CostMasterAccessMaster_Area DEFAULT('COST_MASTER');
 ALTER TABLE dbo.CostMasterAccessMaster DROP CONSTRAINT PK_CostMasterAccessMaster;
 ALTER TABLE dbo.CostMasterAccessMaster ADD CONSTRAINT PK_CostMasterAccessMaster PRIMARY KEY(SecurityArea,EmployeeID);
END;
IF OBJECT_ID('dbo.CostMasterOtpChallenge','U') IS NULL
CREATE TABLE dbo.CostMasterOtpChallenge(
 ChallengeID bigint IDENTITY(1,1) NOT NULL CONSTRAINT PK_CostMasterOtpChallenge PRIMARY KEY,
 SecurityArea nvarchar(30) NOT NULL,
 EmployeeID bigint NOT NULL, SessionID nvarchar(88) NOT NULL,
 OtpHash varbinary(32) NOT NULL, OtpSalt varbinary(32) NOT NULL,
 AttemptCount int NOT NULL CONSTRAINT DF_CostMasterOtpChallenge_Attempts DEFAULT(0),
 CreatedUtc datetime2(0) NOT NULL CONSTRAINT DF_CostMasterOtpChallenge_Created DEFAULT(SYSUTCDATETIME()),
 ExpiresUtc datetime2(0) NOT NULL, ConsumedUtc datetime2(0) NULL
);
IF COL_LENGTH('dbo.CostMasterOtpChallenge','SecurityArea') IS NULL
 ALTER TABLE dbo.CostMasterOtpChallenge ADD SecurityArea nvarchar(30) NOT NULL CONSTRAINT DF_CostMasterOtpChallenge_Area DEFAULT('COST_MASTER');
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name='IX_CostMasterOtpChallenge_EmployeeSession' AND object_id=OBJECT_ID('dbo.CostMasterOtpChallenge'))
 CREATE INDEX IX_CostMasterOtpChallenge_EmployeeSession ON dbo.CostMasterOtpChallenge(EmployeeID,SessionID,ChallengeID DESC);
IF OBJECT_ID('dbo.CostMasterAuthorization','U') IS NULL
CREATE TABLE dbo.CostMasterAuthorization(
 AuthorizationID bigint IDENTITY(1,1) NOT NULL CONSTRAINT PK_CostMasterAuthorization PRIMARY KEY,
 SecurityArea nvarchar(30) NOT NULL,
 EmployeeID bigint NOT NULL, SessionID nvarchar(88) NOT NULL,
 GrantedUtc datetime2(0) NOT NULL CONSTRAINT DF_CostMasterAuthorization_Granted DEFAULT(SYSUTCDATETIME()),
 ExpiresUtc datetime2(0) NOT NULL, RevokedUtc datetime2(0) NULL
);
IF COL_LENGTH('dbo.CostMasterAuthorization','SecurityArea') IS NULL
 ALTER TABLE dbo.CostMasterAuthorization ADD SecurityArea nvarchar(30) NOT NULL CONSTRAINT DF_CostMasterAuthorization_Area DEFAULT('COST_MASTER');
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name='IX_CostMasterAuthorization_EmployeeSession' AND object_id=OBJECT_ID('dbo.CostMasterAuthorization'))
 CREATE INDEX IX_CostMasterAuthorization_EmployeeSession ON dbo.CostMasterAuthorization(EmployeeID,SessionID,ExpiresUtc DESC);
IF OBJECT_ID('dbo.CostMasterSecurityAudit','U') IS NULL
CREATE TABLE dbo.CostMasterSecurityAudit(
 AuditID bigint IDENTITY(1,1) NOT NULL CONSTRAINT PK_CostMasterSecurityAudit PRIMARY KEY,
 EmployeeID bigint NOT NULL, SessionID nvarchar(88) NOT NULL, ActionName nvarchar(40) NOT NULL,
 WasSuccessful bit NOT NULL, SourceIP nvarchar(45) NULL,
 CreatedUtc datetime2(0) NOT NULL CONSTRAINT DF_CostMasterSecurityAudit_Created DEFAULT(SYSUTCDATETIME())
);
COMMIT TRANSACTION;

-- Add an authorized Cost Master employee:
-- INSERT dbo.CostMasterAccessMaster(SecurityArea,EmployeeID,IsActive,AddedBy) VALUES('COST_MASTER',12345,1,12345);
-- INSERT dbo.CostMasterAccessMaster(SecurityArea,EmployeeID,IsActive,AddedBy) VALUES('APPROVE_COST',67890,1,12345);
-- Disable access without deleting history:
-- UPDATE dbo.CostMasterAccessMaster SET IsActive=0,UpdatedBy=12345,UpdatedUtc=SYSUTCDATETIME() WHERE SecurityArea='COST_MASTER' AND EmployeeID=12345;
