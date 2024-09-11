
-- Table: public.Consultant
CREATE TABLE IF NOT EXISTS public."Consultant"
(
    "Id" bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    "FullName" character varying(255) COLLATE pg_catalog."default" NOT NULL,
    "CompanyId" bigint,
    "PhotoUrl" text COLLATE pg_catalog."default",
    "Active" boolean NOT NULL DEFAULT true,
    "TimeLastLeadAssigned" timestamp with time zone,
    "AspNetUserId" text COLLATE pg_catalog."default",
    CONSTRAINT consultant_pkey PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Consultant"
    OWNER to postgres;


-- Table: public.Lead
CREATE TABLE IF NOT EXISTS public."Lead"
(
    "Id" bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    "Name" character varying(255) COLLATE pg_catalog."default" NOT NULL,
    "PhoneNumber" character varying(20) COLLATE pg_catalog."default",
    "Email" character varying(255) COLLATE pg_catalog."default",
    "CompanyId" bigint NOT NULL,
    "ConsultantId" bigint,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "Status" character varying(40) COLLATE pg_catalog."default",
    "Identifier" character varying(4000) COLLATE pg_catalog."default",
    "SourceChannel" character varying(255) COLLATE pg_catalog."default",
    "PathReference" character varying(4000) COLLATE pg_catalog."default",
    CONSTRAINT lead_id_unique UNIQUE ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Lead"
    OWNER to postgres;



-- Table: public.ChatMessage
CREATE TABLE IF NOT EXISTS public."ChatMessage"
(
    "Id" bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    "LeadId" bigint NOT NULL,
    "ConsultantId" bigint,
    "MessageBody" text COLLATE pg_catalog."default" NOT NULL,
    "MessageType" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "MessageSender" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "MessageStatus" character varying(20) COLLATE pg_catalog."default",
    "MessageDate" timestamp with time zone NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_lead_id FOREIGN KEY ("LeadId")
        REFERENCES public."Lead" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."ChatMessage"
    OWNER to postgres;