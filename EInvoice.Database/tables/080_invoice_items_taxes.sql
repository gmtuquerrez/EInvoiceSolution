CREATE TABLE "InvoiceItemTaxes" (
    "Id" BIGSERIAL PRIMARY KEY,

    "TaxCode" VARCHAR(3) NOT NULL,
    "PercentageCode" VARCHAR(3) NOT NULL,
    "Rate" numeric(18,6),
    "TaxableBase" numeric(18,6) NOT NULL,
    "Value" numeric(18,6) NOT NULL,

    "InvoiceItemId" BIGINT NOT NULL,

    CONSTRAINT "FK_InvoiceItemTaxes_InvoiceItems_InvoiceItemId"
        FOREIGN KEY ("InvoiceItemId")
        REFERENCES "InvoiceItems" ("Id")
        ON DELETE CASCADE
);
