BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "ServiceOffer" (
	"Id"	INTEGER UNIQUE,
	"Name"	TEXT NOT NULL,
	"Description"	TEXT,
	"Price"	NUMERIC,
	"Category"	TEXT NOT NULL,
	"DurationMinutes"	INTEGER DEFAULT 60,
	"IsAcrive"	TEXT,
	"CreatedDate"	DATE,
	"LastUpdated"	INTEGER,
	PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "User" (
	"Id"	INTEGER UNIQUE,
	"Fullname"	TEXT NOT NULL,
	"Email"	TEXT NOT NULL,
	"Password"	TEXT NOT NULL,
	"Phone"	TEXT NOT NULL,
	PRIMARY KEY("Id")
);
COMMIT;
