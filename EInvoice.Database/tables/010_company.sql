-- einvoice."Company" definition

-- Drop table

-- DROP TABLE "Company";

CREATE TABLE einvoice."Company" ( 
	"Id" bigserial NOT NULL, 
	"Ruc" varchar(13) NOT NULL, 
	"BusinessName" varchar(200) NOT NULL, 
	"TradeName" varchar(200) NULL, 
	"SignatureBase64" text NOT NULL, 
	"SignaturePassword" varchar(200) NOT NULL, 
	"LogoBase64" text NULL, 
	"CreatedAt" timestamp DEFAULT now() NOT NULL, 
	"CreatedBy" varchar(50) NULL, 
	"UpdatedAt" timestamp NULL, 
	"UpdatedBy" varchar(50) NULL, 
	CONSTRAINT "Company_Ruc_key" UNIQUE ("Ruc"), 
	CONSTRAINT "Company_pkey" PRIMARY KEY ("Id"));