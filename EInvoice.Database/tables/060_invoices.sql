CREATE TABLE einvoice."Invoices" ( 
    "Id" BIGSERIAL PRIMARY KEY,

    "CompanyId" BIGINT NOT NULL REFERENCES einvoice."Company"("Id"),
    "EmissionPointId" BIGINT NOT NULL REFERENCES einvoice."EmissionPoint"("Id"),

    "AccessKey" VARCHAR(49) NOT NULL,
    "DocumentCode" VARCHAR(3) NOT NULL,
    "Establishment" VARCHAR(3) NOT NULL,
    "EmissionPoint" VARCHAR(3) NOT NULL,
    "Sequential" VARCHAR(20) NOT NULL,

    "IssueDate" TIMESTAMP NOT NULL,
    "AuthorizationDate" TIMESTAMP NULL,
    "Ruc" VARCHAR(13) NOT NULL,
    "TotalAmount" DECIMAL(18,2) NOT NULL,

    "CustomerId" BIGINT NOT NULL REFERENCES einvoice."Customers"("Id"),

    "JsonData" JSONB NOT NULL,
    "XmlGenerated" TEXT NULL,
    "XmlSigned" TEXT NULL,
    "XmlAuthorized" TEXT NULL,
    "SriResponse" TEXT NULL,

    "StatusId" BIGINT NOT NULL REFERENCES einvoice."InvoiceStatus"("Id"),

    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "CreatedBy" VARCHAR(50),
    "UpdatedAt" TIMESTAMP NULL,
    "UpdatedBy" VARCHAR(50)
);
