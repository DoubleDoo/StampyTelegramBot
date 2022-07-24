CREATE TABLE public."Coin"
(
    id uuid NOT NULL,
    name text NOT NULL,
    series text,
    date date,
    catalogId text,
    nominal text NOT NULL,
    diameter text,
    metal text,
    circulation text,
    obverse uuid,
    reverse uuid,
    link text NOT NULL,
    PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS public."Coin"
    OWNER to bot;

CREATE TABLE IF NOT EXISTS public."Image"
(
    id uuid NOT NULL,
    link text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT image_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Image"
    OWNER to bot;


    CREATE TABLE public."Stamp"
(
    id uuid NOT NULL,
    name text NOT NULL,
    date date,
    set text,
    nominal text NOT NULL,
    format text,
    protection text,
    circulation text,
    perforation text,
    paper text,
    printMetod text,
    design text,
    country uuid,
    obverse uuid,
    link text NOT NULL,
    PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS public."Stamp"
    OWNER to bot;