CREATE TABLE [event].[EventLog] (
    [EventLogId]  INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EventLogUid] UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL,
    [EventType]   TINYINT          NOT NULL,
    [EventSource] NVARCHAR (MAX)   NOT NULL,
    [Description] NVARCHAR (MAX)   NOT NULL,
    [EventId]     INT              NOT NULL,
    CONSTRAINT [PK_EventLog] PRIMARY KEY CLUSTERED ([EventLogId] ASC)
);

