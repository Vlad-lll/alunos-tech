USE AlunosTech;
GO

-- Dados fictícios só para demonstração e para tirar os prints.
INSERT INTO Cliente (RazaoSocial, Cnpj, Email) VALUES
    (N'Padaria Pão Quente Ltda',  '11111111000191', N'contato@paoquente.example'),
    (N'Oficina Rápida ME',        '22222222000191', N'oficina@rapida.example'),
    (N'Clínica Bem Estar S/A',    '33333333000191', N'ti@bemestar.example');

INSERT INTO Contrato (ClienteId, Tipo, ValorMensal, ValorPorUnidade, Inicio) VALUES
    (1, 'MENSAL_FIXO', 1500.00, NULL, '2026-01-01'),
    (2, 'MENSAL_FIXO',  800.00, NULL, '2026-03-01'),
    (3, 'POR_USO',        NULL, 2.5000, '2026-02-01');
GO
