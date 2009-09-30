

DROP TABLE IF EXISTS Instrument;

CREATE TABLE Instrument(	Id INTEGER PRIMARY KEY,
				symbol VARCHAR(120) UNIQUE,
				current_price FLOAT,
				description VARCHAR(120),
				instrument_type VARCHAR(5),
				FundNm  VARCHAR(120),
				FundHouse VARCHAR(120),
				DivOption VARCHAR(120) );




DROP TABLE IF EXISTS transactiontbl;
CREATE TABLE transactiontbl (	Id INTEGER PRIMARY KEY,
				transaction_Type VARCHAR(5),
				Quantity FLOAT,
				InstrumentId INTEGER,				
				TransactionDate TIMESTAMP,
				UnitPrice FLOAT,
				Tax FLOAT,
				Brokerage FLOAT,
				FOREIGN KEY (InstrumentId) REFERENCES Instrument(Id));


