-- 1. CRIAÇÃO DO BANCO DE DADOS (SCHEMA)
CREATE DATABASE IF NOT EXISTS universo_db;

-- 2. DEFINIR O BANCO DE DADOS COMO ATIVO
USE universo_db;

-- 3. CRIAÇÃO DA TABELA DE METADADOS DA SIMULAÇÃO
CREATE TABLE IF NOT EXISTS Simulacao (
    IdSimulacao INT NOT NULL AUTO_INCREMENT,
    NomeSimulacao VARCHAR(150) NOT NULL,
    DataGravacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    TotalCorpos INT NOT NULL,
    NumInterac INT NOT NULL,        -- Número de interações salvas (estado da UI)
    NumTempoInterac INT NOT NULL,   -- Tempo entre interações (estado da UI)
    PRIMARY KEY (IdSimulacao)
);

-- 4. CRIAÇÃO DA TABELA DE ESTADO DOS CORPOS
CREATE TABLE IF NOT EXISTS Corpo (
    IdCorpo INT NOT NULL AUTO_INCREMENT,
    IdSimulacao INT NOT NULL,  -- Chave estrangeira para ligar ao cabeçalho
    Nome VARCHAR(100),
    Massa DOUBLE NOT NULL,
    Densidade DOUBLE NOT NULL,
    PosX DOUBLE NOT NULL,
    PosY DOUBLE NOT NULL,
    PosZ DOUBLE NOT NULL,
    VelX DOUBLE NOT NULL,
    VelY DOUBLE NOT NULL,
    VelZ DOUBLE NOT NULL,
    PRIMARY KEY (IdCorpo),
    -- Definindo a chave estrangeira
    FOREIGN KEY (IdSimulacao) REFERENCES Simulacao(IdSimulacao)
);

-- Mensagem de confirmação (opcional)
SELECT 'Estrutura do banco de dados universo_db criada com sucesso!' AS Status;