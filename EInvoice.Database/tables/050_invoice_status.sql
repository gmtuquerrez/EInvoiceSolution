-- einvoice."InvoiceStatus" definition

-- Drop table

-- DROP TABLE "InvoiceStatus";

CREATE TABLE einvoice."InvoiceStatus" ( 
	"Id" bigserial NOT NULL, 
	"Name" varchar(40) NOT NULL, 
	"CreatedAt" timestamp DEFAULT now() NOT NULL, 
	"CreatedBy" varchar(50) NULL, 
	"UpdatedAt" timestamp NULL, 
	"UpdatedBy" varchar(50) NULL, 
	CONSTRAINT "InvoiceStatus_Name_key" UNIQUE ("Name"), 
	CONSTRAINT "InvoiceStatus_pkey" PRIMARY KEY ("Id"));