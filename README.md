# LocadoraMVC
Locadora feita em ASP NET Core MVC 5.

-- Database: LocadoraSolutis

-- DROP DATABASE "LocadoraSolutis";

CREATE DATABASE "LocadoraSolutis"
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

COMMENT ON DATABASE "LocadoraSolutis"
    IS 'Estrutura de um banco para uma locadora de filmes.';

-- Table: public."Aluguel"

-- DROP TABLE public."Aluguel";

CREATE TABLE public."Aluguel"
(
    "ValorTotal" money NOT NULL,
    "DataEmprestimo" date NOT NULL,
    "DataDevolucao" date NOT NULL,
    "StatusEmprestimo" boolean NOT NULL,
    "IdAluguel" uuid NOT NULL,
    "IdCliente" uuid,
    CONSTRAINT "Aluguel_pkey" PRIMARY KEY ("IdAluguel"),
    CONSTRAINT "fk_IdCliente" FOREIGN KEY ("IdCliente")
        REFERENCES public."Cliente" ("IdCliente") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Aluguel"
    OWNER to postgres;

-- Table: public."AluguelFilme"

-- DROP TABLE public."AluguelFilme";

CREATE TABLE public."AluguelFilme"
(
    "IdAluguel" uuid NOT NULL,
    "IdFilme" uuid NOT NULL,
    CONSTRAINT "AluguelFilme_pkey" PRIMARY KEY ("IdFilme", "IdAluguel"),
    CONSTRAINT "fk_IdAluguel" FOREIGN KEY ("IdAluguel")
        REFERENCES public."Aluguel" ("IdAluguel") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT "fk_IdFilme" FOREIGN KEY ("IdFilme")
        REFERENCES public."Filme" ("IdFilme") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."AluguelFilme"
    OWNER to postgres;

-- Table: public."Cliente"

-- DROP TABLE public."Cliente";

CREATE TABLE public."Cliente"
(
    "NomeCliente" character varying COLLATE pg_catalog."default" NOT NULL,
    "CPFCliente" character varying COLLATE pg_catalog."default" NOT NULL,
    "IdCliente" uuid NOT NULL,
    "CEP" character varying COLLATE pg_catalog."default",
    "Endereco" character varying COLLATE pg_catalog."default",
    "Cidade" character varying COLLATE pg_catalog."default",
    "Estado" character varying COLLATE pg_catalog."default",
    "Bairro" character varying COLLATE pg_catalog."default",
    CONSTRAINT "Cliente_pkey" PRIMARY KEY ("IdCliente")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Cliente"
    OWNER to postgres;

-- Table: public."Filme"

-- DROP TABLE public."Filme";

CREATE TABLE public."Filme"
(
    "CodigoFilme" integer NOT NULL,
    "NomeFilme" character varying COLLATE pg_catalog."default" NOT NULL,
    "ValorEmprestimo" money NOT NULL,
    "QtdEstoque" integer NOT NULL,
    "GeneroFilme" integer NOT NULL,
    "FaixaEtaria" integer NOT NULL,
    "IdFilme" uuid NOT NULL,
    CONSTRAINT "Filme_pkey" PRIMARY KEY ("IdFilme")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Filme"
    OWNER to postgres;
