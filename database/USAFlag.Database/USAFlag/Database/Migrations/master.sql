-- liquibase formatted sql

-- changeset iTitans:1740941972160-1
CREATE TABLE "tbl_users" ("user_id" INTEGER GENERATED BY DEFAULT AS IDENTITY (START WITH 12) NOT NULL, "user_name" VARCHAR(100) NOT NULL, "email_address" VARCHAR(250) NOT NULL, "password" VARCHAR(100) NOT NULL, "created_at" TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW(), "updated_at" TIMESTAMP WITHOUT TIME ZONE, CONSTRAINT "tbl_users_pkey" PRIMARY KEY ("user_id"));

-- changeset iTitans:1740941972160-2
CREATE UNIQUE INDEX "tbl_users_email_address_key" ON "tbl_users"("email_address");

