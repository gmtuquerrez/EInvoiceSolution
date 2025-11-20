-- einvoice."Establishment" definition

-- Drop table

-- DROP TABLE "Establishment";

CREATE TABLE einvoice."Establishment" ( 
	"Id" bigserial NOT NULL, 
	"CompanyId" int8 NOT NULL, 
	"Code" varchar(3) NOT NULL, 
	"Address" varchar(300) NOT NULL, 
	"CreatedAt" timestamp DEFAULT now() NOT NULL, 
	"CreatedBy" varchar(50) NULL, 
	"UpdatedAt" timestamp NULL, 
	"UpdatedBy" varchar(50) NULL, 
	CONSTRAINT "Establishment_pkey" PRIMARY KEY ("Id"), 
	CONSTRAINT "UQ_Establishment_Company_Code" UNIQUE ("CompanyId", "Code"));


-- einvoice."Establishment" foreign keys

ALTER TABLE einvoice."Establishment" ADD CONSTRAINT "Establishment_CompanyId_fkey" FOREIGN KEY ("CompanyId") REFERENCES "Company"("Id");