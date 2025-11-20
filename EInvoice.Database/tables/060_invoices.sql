-- einvoice."Invoices" definition

-- Drop table

-- DROP TABLE "Invoices";

CREATE TABLE einvoice."Invoices" ( 
	"Id" bigserial NOT NULL, 
	"EmissionPointId" int8 NOT NULL, 
	"CustomerId" int8 NOT NULL, "StatusId" int8 NOT NULL, 
	"AccessKey" varchar(49) NOT NULL, 
	"DocumentCode" varchar(3) NOT NULL, 
	"Establishment" varchar(3) NOT NULL, 
	"EmissionPoint" varchar(3) NOT NULL, 
	"Sequential" varchar(20) NOT NULL, 
	"IssueDate" timestamp NOT NULL, 
	"AuthorizationDate" timestamp NULL, 
	"Ruc" varchar(13) NOT NULL, 
	"TotalAmount" numeric(18, 2) NOT NULL, 
	"JsonData" jsonb NOT NULL, 
	"XmlGenerated" text NULL, 
	"XmlSigned" text NULL, 
	"XmlAuthorized" text NULL, 
	"SriResponse" text NULL, 
	"CreatedAt" timestamp DEFAULT now() NOT NULL, 
	"CreatedBy" varchar(50) NULL, 
	"UpdatedAt" timestamp NULL, 
	"UpdatedBy" varchar(50) NULL, 
	"CompanyId" int8 NOT NULL, 
	CONSTRAINT "Invoices_pkey" PRIMARY KEY ("Id"));


-- einvoice."Invoices" foreign keys

ALTER TABLE einvoice."Invoices" ADD CONSTRAINT "FK_Invoices_Companies_CompanyId" FOREIGN KEY ("CompanyId") REFERENCES "Company"("Id");
ALTER TABLE einvoice."Invoices" ADD CONSTRAINT "Invoices_CustomerId_fkey" FOREIGN KEY ("CustomerId") REFERENCES "Customers"("Id");
ALTER TABLE einvoice."Invoices" ADD CONSTRAINT "Invoices_EmissionPointId_fkey" FOREIGN KEY ("EmissionPointId") REFERENCES "EmissionPoint"("Id");
ALTER TABLE einvoice."Invoices" ADD CONSTRAINT "Invoices_StatusId_fkey" FOREIGN KEY ("StatusId") REFERENCES "InvoiceStatus"("Id");