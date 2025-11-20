-- einvoice."InvoicePayments" definition

-- Drop table

-- DROP TABLE "InvoicePayments";

CREATE TABLE einvoice."InvoicePayments" ( 
	"Id" bigserial NOT NULL, 
	"Method" varchar(10) NOT NULL, 
	"Amount" numeric(18, 2) NOT NULL, 
	"Term" int4 NULL, 
	"TimeUnit" varchar(10) NULL, 
	"InvoiceId" int8 NOT NULL, 
	CONSTRAINT "InvoicePayments_pkey" PRIMARY KEY ("Id"));


-- einvoice."InvoicePayments" foreign keys

ALTER TABLE einvoice."InvoicePayments" ADD CONSTRAINT "FK_InvoicePayments_Invoices_InvoiceId" FOREIGN KEY ("InvoiceId") REFERENCES "Invoices"("Id") ON DELETE CASCADE;