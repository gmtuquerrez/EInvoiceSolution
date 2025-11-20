-- einvoice."InvoiceItems" definition

-- Drop table

-- DROP TABLE "InvoiceItems";

CREATE TABLE einvoice."InvoiceItems" ( 
"Id" bigserial NOT NULL, 
"Code" varchar(50) NOT NULL, 
"AuxCode" varchar(50) NULL, 
"Description" varchar(300) NOT NULL, 
"Quantity" numeric(18, 6) NOT NULL, 
"UnitPrice" numeric(18, 6) NOT NULL, 
"Discount" numeric(18, 6) NOT NULL, 
"TotalWithoutTaxes" numeric(18, 6) NOT NULL, 
"InvoiceId" int8 NOT NULL, CONSTRAINT 
"InvoiceItems_pkey" PRIMARY KEY ("Id"));


-- einvoice."InvoiceItems" foreign keys

ALTER TABLE einvoice."InvoiceItems" ADD CONSTRAINT "FK_InvoiceItems_Invoices_InvoiceId" FOREIGN KEY ("InvoiceId") REFERENCES "Invoices"("Id") ON DELETE CASCADE;