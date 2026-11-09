USE AlunosTech;
GO

-- Registra toda mudança de status de fatura, mesmo se feita direto no banco.
CREATE OR ALTER TRIGGER trg_Fatura_Auditar
ON Fatura
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditoriaFatura (FaturaId, StatusAntigo, StatusNovo)
    SELECT d.FaturaId, d.Status, i.Status
    FROM deleted d
    JOIN inserted i ON i.FaturaId = d.FaturaId
    WHERE d.Status <> i.Status;
END;
GO
