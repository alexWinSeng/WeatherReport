CREATE TABLE [dbo].[weather_condition] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [element_id]      INT           NOT NULL,
    [start_time]      NVARCHAR (50) NULL,
    [end_time]        NVARCHAR (50) NULL,
    [parameter_name]  NVARCHAR (50) NULL,
    [parameter_value] NVARCHAR (50) NULL,
    CONSTRAINT [PK_weather_condition] PRIMARY KEY CLUSTERED ([id] ASC)
);

