-- einvoice."EmissionPoint" definition

-- Drop table

-- DROP TABLE "EmissionPoint";

CREATE TABLE einvoice."EmissionPoint" ( 
	"Id" bigserial NOT NULL, 
	"EstablishmentId" int8 NOT NULL, 
	"Code" varchar(3) NOT NULL, 
	"CreatedAt" timestamp DEFAULT now() NOT NULL, 
	"CreatedBy" varchar(50) NULL, 
	"UpdatedAt" timestamp NULL, 
	"UpdatedBy" varchar(50) NULL, 
	CONSTRAINT "EmissionPoint_pkey" PRIMARY KEY ("Id"), 
	CONSTRAINT "UQ_EmissionPoint_Estab_Code" UNIQUE ("EstablishmentId", "Code"));


-- einvoice."EmissionPoint" foreign keys

ALTER TABLE einvoice."EmissionPoint" ADD CONSTRAINT "EmissionPoint_EstablishmentId_fkey" FOREIGN KEY ("EstablishmentId") REFERENCES "Establishment"("Id");