CREATE TABLE "InvoicePayments" (
    "Id" BIGSERIAL PRIMARY KEY,

    "Method" VARCHAR(10) NOT NULL,
    "Amount" numeric(18,2) NOT NULL,
    "Term" INTEGER,
    "TimeUnit" VARCHAR(10),

    "InvoiceId" BIGINT NOT NULL,

    CONSTRAINT "FK_InvoicePayments_Invoices_InvoiceId"
        FOREIGN KEY ("InvoiceId")
        REFERENCES "Invoices" ("Id")
        ON DELETE CASCADE
);
