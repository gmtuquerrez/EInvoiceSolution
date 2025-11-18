CREATE TABLE einvoice."InvoiceStatus" (
    "Id" BIGSERIAL PRIMARY KEY,
    "Name" VARCHAR(50) NOT NULL UNIQUE,
    "Description" TEXT NULL
);
