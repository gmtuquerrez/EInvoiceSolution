-- einvoice."InvoiceAdditionalFields" definition

-- Drop table

-- DROP TABLE "InvoiceAdditionalFields";

CREATE TABLE einvoice."InvoiceAdditionalFields" ( 
	"Id" bigserial NOT NULL, 
	"Name" varchar(100) NOT NULL, 
	"Value" varchar(500) NOT NULL, 
	"InvoiceId" int8 NOT NULL, 
	CONSTRAINT "InvoiceAdditionalFields_pkey" PRIMARY KEY ("Id"));


-- einvoice."InvoiceAdditionalFields" foreign keys

ALTER TABLE einvoice."InvoiceAdditionalFields" ADD CONSTRAINT "FK_InvoiceAdditionalFields_Invoices_InvoiceId" FOREIGN KEY ("InvoiceId") REFERENCES "Invoices"("Id") ON DELETE CASCADE;