CREATE TABLE clientes (
    id SERIAL PRIMARY KEY,
    idbanco INT NOT NULL,
	nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    documento VARCHAR(20) NOT NULL,
    direccion VARCHAR(200),
    mail VARCHAR(100),
    celular VARCHAR(10) NOT NULL,
    estado VARCHAR(50) NOT NULL
);