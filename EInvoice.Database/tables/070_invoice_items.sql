CREATE TABLE "InvoiceItems" (
    "Id" BIGSERIAL PRIMARY KEY,

    "Code" VARCHAR(50) NOT NULL,
    "AuxCode" VARCHAR(50),
    "Description" VARCHAR(300) NOT NULL,

    "Quantity" numeric(18,6) NOT NULL,
    "UnitPrice" numeric(18,6) NOT NULL,
    "Discount" numeric(18,6) NOT NULL,
    "TotalWithoutTaxes" numeric(18,6) NOT NULL,

    "InvoiceId" BIGINT NOT NULL,

    CONSTRAINT "FK_InvoiceItems_Invoices_InvoiceId"
        FOREIGN KEY ("InvoiceId")
        REFERENCES "Invoices" ("Id")
        ON DELETE CASCADE
);
