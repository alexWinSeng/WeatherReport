CREATE TABLE [dbo].[location] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [locationName] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_location] PRIMARY KEY CLUSTERED ([id] ASC)
);

