CREATE TABLE einvoice."Company" (
    "Id" BIGSERIAL PRIMARY KEY,
    "Ruc" VARCHAR(13) NOT NULL UNIQUE,
    "Name" VARCHAR(200) NOT NULL,
    "SignatureBase64" TEXT NOT NULL,
    "SignaturePassword" VARCHAR(100) NOT NULL,
    "LogoBase64" TEXT NULL,

    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "CreatedBy" VARCHAR(50),
    "UpdatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "UpdatedBy" VARCHAR(50)
);
