INSERT INTO einvoice."InvoiceStatus" ("Id", "Name", "Description")
VALUES 
    (1, 'GENERATED', 'Factura recibida por la API'),
    (2, 'SIGNED', 'XML firmado con firma digital'),
    (3, 'SENT', 'Enviado al SRI'),
    (4, 'AUTHORIZED', 'Autorizado por el SRI'),
    (5, 'REJECTED', 'Rechazado por el SRI'),
    (6, 'ERROR', 'Error en generación, firmado o envío')
ON CONFLICT ("Id") DO NOTHING;
