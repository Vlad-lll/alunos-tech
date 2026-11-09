USE AlunosTech;
GO

-- Figura 11: fechamento mensal e o resultado na tabela Fatura
EXEC sp_GerarFaturasMensais @Competencia = '2026-11-01';
SELECT * FROM Fatura;

-- Function
SELECT ClienteId, RazaoSocial, dbo.fn_TotalEmAberto(ClienteId) AS TotalEmAberto
FROM Cliente;

-- Figura 12: mudança de status e o registro criado pelo trigger
UPDATE Fatura SET Status = 'PAGA' WHERE FaturaId = 1;
SELECT * FROM AuditoriaFatura;

-- Figura 13: ligue "Incluir Plano de Execução Real" no SSMS (Ctrl+M) e execute.
-- Rode antes sem o índice (DROP INDEX) e depois com ele (04_indices.sql) para comparar.
SELECT FaturaId, Competencia, Valor
FROM Fatura
WHERE ContratoId = 1 AND Status = 'ABERTA';
GO
