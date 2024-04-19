CREATE TABLE facturas (
    id SERIAL PRIMARY KEY,
    idcliente INT NOT NULL,
    numerofactura VARCHAR(15) NOT NULL,
    fechahora TIMESTAMP NOT NULL,
    total DECIMAL(18, 2) NOT NULL,
    totaliva5 DECIMAL(18, 2) NOT NULL,
    totaliva10 DECIMAL(18, 2) NOT NULL,
    totaliva DECIMAL(18, 2) NOT NULL,
    totalenletras VARCHAR(500) NOT NULL,
    sucursal VARCHAR(100),
    FOREIGN KEY (idcliente) REFERENCES clientes (id)
);