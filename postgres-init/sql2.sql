-- Create the table if it does not exist
CREATE TABLE public."Users"
(
    "UserID" uuid NOT NULL,
    "PersonName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Email" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Password" character varying(256) COLLATE pg_catalog."default" NOT NULL,
    "Gender" character varying(15) COLLATE pg_catalog."default",
    CONSTRAINT "Users_pkey" PRIMARY KEY ("UserID")
);

CREATE TABLE public."RefreshTokens"
(
    "RefreshTokenId" uuid PRIMARY KEY,
    "UserId" uuid NOT NULL
        REFERENCES public."Users" ("UserId")
        ON DELETE CASCADE,

    "TokenHash" character varying(64) NOT NULL UNIQUE,
    "ExpiresAtUtc" timestamp with time zone NOT NULL,
    "CreatedAtUtc" timestamp with time zone NOT NULL DEFAULT now(),
    "RevokedAtUtc" timestamp with time zone NULL,
    "ReplacedByTokenHash" character varying(64) NULL
);

CREATE INDEX "IX_RefreshTokens_UserId"
    ON public."RefreshTokens" ("UserId");