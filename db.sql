CREATE TABLE public."Coin"
(
    id uuid NOT NULL,
    name text,
    series text,
    date date,
    catalogId text,
    nominal money,
    diameter double precision,
    metal text,
    circulation bigint,
    obverse uuid,
    reverse uuid,
    link text,
    PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS public."Coin"
    OWNER to bot;

CREATE TABLE IF NOT EXISTS public."Image"
(
    id uuid NOT NULL,
    link text COLLATE pg_catalog."default",
    CONSTRAINT image_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Image"
    OWNER to bot;


    CREATE TABLE public."Stamp"
(
    id uuid NOT NULL,
    name text,
    date date,
    set text,
    nominal money,
    format text,
    protection text,
    circulation bigint,
    perforation text,
    paper text,
    printMetod text,
    design text,
    country uuid,
    obverse uuid,
    link text,
    PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS public."Stamp"
    OWNER to bot;