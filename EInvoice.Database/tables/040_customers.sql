-- einvoice."Customers" definition

-- Drop table

-- DROP TABLE "Customers";

CREATE TABLE einvoice."Customers" ( 
	"Id" bigserial NOT NULL, 
	"IdentificationType" varchar(3) NOT NULL, 
	"Identification" varchar(20) NOT NULL, 
	"FullName" varchar(200) NOT NULL, 
	"Email" varchar(200) NULL, 
	"Phone" varchar(30) NULL, 
	"Address" varchar(300) NULL, 
	"CreatedAt" timestamp DEFAULT now() NOT NULL, 
	"CreatedBy" varchar(50) NULL, 
	"UpdatedAt" timestamp NULL, 
	"UpdatedBy" varchar(50) NULL, 
	CONSTRAINT "Customers_pkey" PRIMARY KEY ("Id"), 
	CONSTRAINT "UQ_Customers_Identification" UNIQUE ("Identification"));