CREATE TABLE einvoice."Establishments" (
    "Id" BIGSERIAL PRIMARY KEY,
    "CompanyId" BIGINT NOT NULL REFERENCES einvoice."Company"("Id"),
    "Code" VARCHAR(3) NOT NULL,
    "Address" VARCHAR(300) NOT NULL,

    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "CreatedBy" VARCHAR(50),
    "UpdatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "UpdatedBy" VARCHAR(50)
);
