CREATE TABLE "InvoiceAdditionalFields" (
    "Id" BIGSERIAL PRIMARY KEY,

    "Name" VARCHAR(100) NOT NULL,
    "Value" VARCHAR(500) NOT NULL,

    "InvoiceId" BIGINT NOT NULL,

    CONSTRAINT "FK_InvoiceAdditionalFields_Invoices_InvoiceId"
        FOREIGN KEY ("InvoiceId")
        REFERENCES "Invoices" ("Id")
        ON DELETE CASCADE
);
