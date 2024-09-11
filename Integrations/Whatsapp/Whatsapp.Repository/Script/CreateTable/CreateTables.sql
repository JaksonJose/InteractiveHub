
CREATE TABLE IF NOT EXISTS public.whatsappconfig
(
    id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    companyid bigint NOT NULL,
    phonenumberid character varying(4000) COLLATE pg_catalog."default" NOT NULL,
    businessaccountid character varying(4000) COLLATE pg_catalog."default",
    accesstoken text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT whatsappsettings_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.whatsappconfig
    OWNER to postgres;