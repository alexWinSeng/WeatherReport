CREATE TABLE [dbo].[weather_element] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [location_id]  INT           NOT NULL,
    [element_name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_weather_element] PRIMARY KEY CLUSTERED ([id] ASC)
);

