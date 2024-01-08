CREATE DATABASE DBFinanceiro;
GO

USE DBFinanceiro;
GO

CREATE TABLE TB_Risco (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	GrauRisco INT NOT NULL,
	Descricao VARCHAR(100) NOT NULL
);
GO

CREATE TABLE TB_Usuario (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdRisco INT NOT NULL,
	Nome VARCHAR(100) NOT NULL,
	Email VARCHAR(100) UNIQUE NOT NULL,
	Telefone VARCHAR(11) NOT NULL,
	CPF VARCHAR(11) UNIQUE NOT NULL,
	Ativo BIT NOT NULL DEFAULT 1,
	CONSTRAINT FK_TBUsuario_TBRisco FOREIGN KEY (IdRisco) REFERENCES TB_Risco(Id)
);
GO

CREATE TABLE TB_Segmento (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdRisco INT NOT NULL,
	TipoSegmento VARCHAR(100) NOT NULL,
	PercentualRendimento DECIMAL NOT NULL,
	TaxaAdm DECIMAL NOT NULL,
	MesesVigencia VARCHAR(11) UNIQUE NOT NULL,
	CONSTRAINT FK_TBSegmento_TBRisco FOREIGN KEY (IdRisco) REFERENCES TB_Risco(Id)
);
GO

CREATE TABLE TB_Investimento (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdUsuario INT NOT NULL,
	IdSegmento INT NOT NULL,
	Saldo DECIMAL NOT NULL,
	ValorRendimento DECIMAL NOT NULL,
	ValorFinal DECIMAL NOT NULL,
	CONSTRAINT FK_TBInvestimento_TBUsuario FOREIGN KEY (IdUsuario) REFERENCES TB_Usuario(Id),
	CONSTRAINT FK_TBInvestimento_TBSegmento FOREIGN KEY (IdSegmento) REFERENCES TB_Segmento(Id)
);
GO

CREATE TABLE TB_Aplicacao (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdInvestimento INT NOT NULL,
	Valor DECIMAL NOT NULL,
	DataAplicacao DATETIME NOT NULL,
	CONSTRAINT FK_TBAplicacao_TBInvestimento FOREIGN KEY (IdInvestimento) REFERENCES TB_Investimento(Id)
);
GO

CREATE TABLE TB_Resgate (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdInvestimento INT NOT NULL,
	Valor DECIMAL NOT NULL,
	DataResgate DATETIME NOT NULL,
	CONSTRAINT FK_TBResgate_TBInvestimento FOREIGN KEY (IdInvestimento) REFERENCES TB_Investimento(Id)
);
GO