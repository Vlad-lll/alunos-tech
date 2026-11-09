USE AlunosTech;
GO

-- Fechamento mensal: gera as faturas de mensalidade fixa de uma competência.
-- Pode ser executada de novo com segurança, pois ignora contratos que já têm fatura.
CREATE OR ALTER PROCEDURE sp_GerarFaturasMensais
    @Competencia DATE
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Fatura (ContratoId, Competencia, Valor)
        SELECT c.ContratoId, @Competencia, c.ValorMensal
        FROM Contrato c
        WHERE c.Status = 'ATIVO'
          AND c.Tipo = 'MENSAL_FIXO'
          AND NOT EXISTS (SELECT 1
                          FROM Fatura f
                          WHERE f.ContratoId = c.ContratoId
                            AND f.Competencia = @Competencia);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

-- Total em aberto de um cliente (relatórios e portal do cliente).
CREATE OR ALTER FUNCTION fn_TotalEmAberto (@ClienteId INT)
RETURNS DECIMAL(18,2)
AS
BEGIN
    RETURN (SELECT ISNULL(SUM(f.Valor), 0)
            FROM Fatura f
            JOIN Contrato c ON c.ContratoId = f.ContratoId
            WHERE c.ClienteId = @ClienteId
              AND f.Status = 'ABERTA');
END;
GO
