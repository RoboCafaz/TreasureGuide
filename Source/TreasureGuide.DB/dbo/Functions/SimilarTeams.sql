﻿CREATE FUNCTION [dbo].[SimilarTeams]
(
    @teamId INT,
    @stageId INT,
    @unit1 INT,
    @unit2 INT,
    @unit3 INT,
    @unit4 INT,
    @unit5 INT,
    @unit6 INT
)
RETURNS @returntable TABLE
(
    [TeamId] INT,
    [StageId] INT,
    [Matches] INT
)
AS
BEGIN
    INSERT @returntable
        SELECT
            *
        FROM (
            SELECT
                [TeamId],
                [StageId],
                ([Slot3Match] + [Slot4Match] + [Slot5Match] + [Slot6Match]) AS [Matches]
            FROM (
                SELECT
                    D.[Id] AS [TeamId],
                    D.[StageId] AS [StageId],
                    CASE WHEN @unit3 IN (D.[Slot3],D.[Slot4],D.[Slot5],D.[Slot6]) THEN 1 ELSE 0 END AS [Slot3Match],
                    CASE WHEN @unit4 IN (D.[Slot3],D.[Slot4],D.[Slot5],D.[Slot6]) THEN 1 ELSE 0 END AS [Slot4Match],
                    CASE WHEN @unit5 IN (D.[Slot3],D.[Slot4],D.[Slot5],D.[Slot6]) THEN 1 ELSE 0 END AS [Slot5Match],
                    CASE WHEN @unit6 IN (D.[Slot3],D.[Slot4],D.[Slot5],D.[Slot6]) THEN 1 ELSE 0 END AS [Slot6Match]
                FROM [dbo].[TeamMinis] AS D
                    WHERE @teamId != D.[Id] AND (@stageId IS NULL OR D.[StageId] = @stageId) AND (
                            ((@unit1 = D.[Slot1] AND @unit2 = D.[Slot2]) OR (@unit1 = D.[Slot2] AND @unit2 = D.[Slot1]))
                        )
                    ) AS T
                ) AS M
            WHERE M.[Matches] > 1
            RETURN
END