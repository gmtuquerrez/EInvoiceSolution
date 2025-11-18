CREATE TABLE einvoice."EmissionPoint" (
    "Id" BIGSERIAL PRIMARY KEY,
    "EstablishmentId" BIGINT NOT NULL REFERENCES einvoice."Establishments"("Id"),
    "Code" VARCHAR(3) NOT NULL,

    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "CreatedBy" VARCHAR(50),
    "UpdatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "UpdatedBy" VARCHAR(50)
);
