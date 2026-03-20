-- SQL Queries for Gym Management System

-- Create Table
CREATE TABLE Miembro (
    nombre_completo TEXT NOT NULL,
    cedula TEXT PRIMARY KEY,
    telefono TEXT NOT NULL
);

-- Register
INSERT INTO Miembro (nombre_completo, cedula, telefono) VALUES ('Mario Rossi', '10987-654', '555-0199');

-- List all
SELECT * FROM Miembro;

-- Search by cedula
SELECT * FROM Miembro WHERE cedula = '10987-654';

-- Update phone
UPDATE Miembro SET telefono = '555-0200' WHERE cedula = '10987-654';

-- Delete
DELETE FROM Miembro WHERE cedula = '10987-654';
