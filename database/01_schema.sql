-- Banco de dados da plataforma Alunos Tech (SQL Server)
-- Execute os scripts da pasta /database na ordem numérica.

IF DB_ID('AlunosTech') IS NULL
    CREATE DATABASE AlunosTech;
GO

USE AlunosTech;
GO

CREATE TABLE Cliente (
    ClienteId     INT IDENTITY(1,1) PRIMARY KEY,
    RazaoSocial   NVARCHAR(150) NOT NULL,
    Cnpj          CHAR(14)      NOT NULL UNIQUE,
    Email         NVARCHAR(120) NOT NULL,
    Ativo         BIT           NOT NULL DEFAULT 1,
    CriadoEm      DATETIME2     NOT NULL DEFAULT SYSDATETIME()
);

CREATE TABLE Contrato (
    ContratoId      INT IDENTITY(1,1) PRIMARY KEY,
    ClienteId       INT           NOT NULL REFERENCES Cliente(ClienteId),
    Tipo            VARCHAR(20)   NOT NULL CHECK (Tipo IN ('MENSAL_FIXO','POR_USO')),
    ValorMensal     DECIMAL(18,2) NULL,
    ValorPorUnidade DECIMAL(18,4) NULL,
    Inicio          DATE          NOT NULL,
    Fim             DATE          NULL,
    Status          VARCHAR(15)   NOT NULL DEFAULT 'ATIVO'
);

CREATE TABLE Fatura (
    FaturaId    INT IDENTITY(1,1) PRIMARY KEY,
    ContratoId  INT           NOT NULL REFERENCES Contrato(ContratoId),
    Competencia DATE          NOT NULL,
    Valor       DECIMAL(18,2) NOT NULL CHECK (Valor > 0),
    Status      VARCHAR(15)   NOT NULL DEFAULT 'ABERTA',
    EmitidaEm   DATETIME2     NOT NULL DEFAULT SYSDATETIME(),
    Versao      ROWVERSION,   -- controle otimista de concorrência
    CONSTRAINT UQ_Fatura UNIQUE (ContratoId, Competencia)
);

CREATE TABLE AuditoriaFatura (
    AuditoriaId  INT IDENTITY(1,1) PRIMARY KEY,
    FaturaId     INT          NOT NULL,
    StatusAntigo VARCHAR(15)  NULL,
    StatusNovo   VARCHAR(15)  NULL,
    AlteradoEm   DATETIME2    NOT NULL DEFAULT SYSDATETIME(),
    AlteradoPor  SYSNAME      NOT NULL DEFAULT SUSER_SNAME()
);
GO
