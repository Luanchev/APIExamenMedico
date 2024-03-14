DROP TABLE IF EXISTS examenMedico;
DROP TABLE IF EXISTS tipoMuestra;
DROP TABLE IF EXISTS paciente;


CREATE TABLE tipoMuestra(
	idMuestra SERIAL PRIMARY KEY,
	tipoMuestra VARCHAR(30) NOT NULL
	
	
);

SELECT * FROM tipoMuestra;

CREATE TABLE paciente(
	idpaciente SERIAL PRIMARY KEY,
	numDoc VARCHAR(30) NOT NULL,
	nombre VARCHAR(30),
	apellido VARCHAR(30),	
	fechaCump DATE,
	edad SMALLINT CHECK (edad > 0)
);
SELECT * FROM paciente;
CREATE TABLE tipoDocumento(
	idTd SERIAL PRIMARY KEY,
	tipoDoc VARCHAR(30) NOT NULL
);
SELECT * FROM tipoDocumento;

--Creamos la tabla examen donde va a venir la relacion de las tablas 
CREATE TABLE examenMedico(
	idExamen SERIAL PRIMARY KEY,
	codigo VARCHAR(10)NOT NULL,
	nombre VARCHAR(30),
	precio DOUBLE PRECISION,
	idMuestraFk INT,
	idpacienteFk INT,
	FOREIGN KEY (idMuestraFk) REFERENCES tipoMuestra(idMuestra),
	FOREIGN KEY (idpacienteFk) REFERENCES paciente(idpaciente)
);

SELECT * FROM examenMedico;
ALTER TABLE examenMedico
ALTER COLUMN precio SET DATA TYPE DOUBLE PRECISION;


--modifica la tabla tipoMuestra, agregamos la llave foranea idexamen perteneciente a la tabla de examen medico

ALTER TABLE tipoMuestra
ADD COLUMN idExamenFk INT
REFERENCES examenMedico (idExamen);


--modifica la tabla paciente, agregamos la llave foranea idexamen, idmuestra, idDoc perteneciente a las tablas

ALTER TABLE paciente
ADD COLUMN idTdFk INT REFERENCES tipoDocumento (idTd);


-- insertamos en la tabla tipomuestra los registros

INSERT INTO tipoMuestra
(tipoMuestra)
VALUES
('sangre'),
('suero'),
('plasma'),
('orina'),
('hisopos'),
('tejido');


-- insertamos en la tabla paciente los registros

INSERT INTO paciente
(numDoc, nombre, apellido, fechaCump, edad)
VALUES
('69069497Z','pedro', 'Friedman', '1987-06-17', 36 ),
('82035315l','Luciano', 'acosta', '1989-02-25', 34 ),
('17686631E','Vidal', 'Keith', '1997-12-27', 26 ),
('56527969H','Maria', 'Brandt', '2001-01-28', 22),
('19822622A','Antonio', 'Horne', '1974-03-17', 49 ),
('65673551H','Paola', 'Blankenship', '1960-03-17', 63 ),
('17134511V','Luis', 'Potts', '1989-06-24', 34 ),
('96047379T','Alvaro', 'Wilkins', '2006-08-16', 17 ),
('33263531B','Marcial', 'Trujillo', '1998-11-11', 25 ),
('87292315R','Olga', 'Perez', '1996-12-08', 27 );


--insertamos en la tabla tipoDoc los registros
INSERT INTO examenMedico
(tipoDoc)
VALUES
('cedula ciudadania'),
('cedula de extranjeria'),
('tarjeta de indentidad'),
('registro civil');

--insertamos en la tabla examenes los registros

INSERT INTO examenMedico
(codigo, precio, idmuestrafk, idpacientefk)
VALUES
('SB7L289N', 41475.22, 1, 1),
('QCK03WW4', 66113.82, 5, 2),
('GV1C5YPW', 61422.46, 2, 3 ),
('Q1GEJCU6', 75434.32, 5, 4),
('G9ZTN5LI', 23790.20, 3, 5 ),
('11USNJBH', 47277.05, 6, 6 ),
('ARIZYP3I', 34658.70, 2, 7 ),
('QJIPX254', 39586.95, 4, 8 ),
('4LWZ53PP', 89696.62, 1, 9 ),
('T2RWGA25', 81401.87, 6, 10 );
