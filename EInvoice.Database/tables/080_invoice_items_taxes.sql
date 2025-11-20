-- einvoice."InvoiceItemTaxes" definition

-- Drop table

-- DROP TABLE "InvoiceItemTaxes";

CREATE TABLE einvoice."InvoiceItemTaxes" ( 
"Id" bigserial NOT NULL, 
"TaxCode" varchar(3) NOT NULL, 
"PercentageCode" varchar(3) NOT NULL, 
"Rate" numeric(18, 6) NULL, 
"TaxableBase" numeric(18, 6) NOT NULL, 
"Value" numeric(18, 6) NOT NULL, 
"InvoiceItemId" int8 NOT NULL, 
CONSTRAINT "InvoiceItemTaxes_pkey" PRIMARY KEY ("Id"));


-- einvoice."InvoiceItemTaxes" foreign keys

ALTER TABLE einvoice."InvoiceItemTaxes" ADD CONSTRAINT "FK_InvoiceItemTaxes_InvoiceItems_InvoiceItemId" FOREIGN KEY ("InvoiceItemId") REFERENCES "InvoiceItems"("Id") ON DELETE CASCADE;