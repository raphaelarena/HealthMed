CREATE DATABASE HealthMedDB;

USE HealthMedDB;

-- Tabela de Médicos
CREATE TABLE Medico (
    Id INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(100),
    CPF NVARCHAR(11),
    CRM NVARCHAR(20),
    Email NVARCHAR(100),
    Senha NVARCHAR(100)
);

-- Tabela de Pacientes
CREATE TABLE Paciente (
    Id INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(100),
    CPF NVARCHAR(11),
    Email NVARCHAR(100),
    Senha NVARCHAR(100)
);

-- Tabela de Agendamentos
CREATE TABLE Agendamento (
    Id INT PRIMARY KEY IDENTITY,
    MedicoId INT FOREIGN KEY REFERENCES Medico(Id),
    PacienteId INT FOREIGN KEY REFERENCES Paciente(Id),
    DataHora DATETIME
);

-- Tabela de Horários Disponíveis
CREATE TABLE HorarioDisponivel (
    Id INT PRIMARY KEY IDENTITY,
    MedicoId INT FOREIGN KEY REFERENCES Medico(Id),
    DataHora DATETIME,
    Disponivel BIT
);

-- Tabela de Auditoria de Agendamentos
CREATE TABLE AgendamentoAudit (
    Id INT PRIMARY KEY IDENTITY,
    AgendamentoId INT FOREIGN KEY REFERENCES Agendamento(Id),
    ModificadoPor NVARCHAR(100),
    DataModificacao DATETIME,
    Alteracao NVARCHAR(500)
);
