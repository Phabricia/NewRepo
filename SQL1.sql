CREATE DATABASE  [NewClinicaDentariaBD];

USE "NewClinicaDentariaBD";

CREATE TABLE Team (
	id				INT PRIMARY KEY IDENTITY(1,1),
	nome			VARCHAR(250) NOT NULL,
	aniversario		DATE NOT NULL,
	morada			VARCHAR(250) NOT NULL,
	nif				INT  NOT NULL,
	cargo			VARCHAR(250) NOT NULL,
	especialidade	VARCHAR(250) NOT NULL,
);

CREATE TABLE Client (
id				INT PRIMARY KEY IDENTITY(1,1),
nome			VARCHAR(250) NOT NULL,
aniversario		DATE NOT NULL,
morada			VARCHAR(250) NOT NULL,
nif				INT  NOT NULL,
seguroSaude     INT NOT NULL,

);

CREATE TABLE Consulta (
id					INT PRIMARY KEY IDENTITY(1,1),
numeroConsulta		INT NOT NULL,
datatempo			DATETIME NOT NULL,
affirmative			BIT NOT NULL,
observation			VARCHAR(450) NOT NULL,
FOREIGN KEY (id) REFERENCES Client(id),
FOREIGN KEY (id) REFERENCES Team(id),
); 

CREATE TABLE Factura (
id				INT PRIMARY KEY IDENTITY(1,1),
numeroFactura	INT NOT NULL,
descricao		VARCHAR(250) NOT NULL,
valor			INT NOT NULL,
estadoPagamento VARCHAR(250) NOT NULL,
FOREIGN KEY (ConsultaID) REFERENCES Consulta(ConsultaID),
FOREIGN KEY (ClientID) REFERENCES Client(ClientID),
);

SELECT *
FROM Team;

SELECT *
FROM Client;

SELECT *
FROM Consulta;

EXEC sp_rename 'Consulta', 'ConsultaAntiga';

SELECT *
FROM Factura;

DELETE Team;



INSERT INTO Team 
(nome, aniversario, morada, nif, cargo, especialidade)
VALUES
('João', '03-03-1997', 'Rua A, 27', 333000222, 'Funcionário', 'Cirurgião Dentista'),
('Ana', '01-01-1995', 'Rua B, 20', 333222000, 'Funcionário', 'Cirurgião'),
('Pedro', '04-05-1999', 'Rua C, 10', 333111444, 'Funcionário', 'Dentista Pediatra');

INSERT INTO Client 
(nome, aniversario, morada, nif, seguroSaude)
VALUES
('José', '03-07-1997', 'Rua D, 27', 333000242, 3332),
('Maria', '02-01-1995', 'Rua E, 20', 333222010, 30002 ),
('Patrícia', '04-09-1999', 'Rua F, 10', 333111474, 244654);