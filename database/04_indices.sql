USE AlunosTech;
GO

-- Índice de cobertura para a consulta mais frequente:
-- faturas em aberto de um contrato, sem precisar voltar à tabela.
CREATE NONCLUSTERED INDEX IX_Fatura_Contrato_Status
ON Fatura (ContratoId, Status)
INCLUDE (Competencia, Valor);
GO

-- Leituras não bloqueadas pelas escritas durante o fechamento mensal.
ALTER DATABASE AlunosTech SET READ_COMMITTED_SNAPSHOT ON WITH ROLLBACK IMMEDIATE;
GO
