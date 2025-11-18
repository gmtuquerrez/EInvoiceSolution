CREATE TABLE einvoice."Customers" (
    "Id" BIGSERIAL PRIMARY KEY,
    "RucOrId" VARCHAR(20) NOT NULL,
    "Name" VARCHAR(200) NOT NULL,
    "Address" VARCHAR(300),
    "Email" VARCHAR(150),

    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "CreatedBy" VARCHAR(50),
    "UpdatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "UpdatedBy" VARCHAR(50)
);
