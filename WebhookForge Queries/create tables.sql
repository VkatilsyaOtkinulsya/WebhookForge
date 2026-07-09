CREATE TABLE IF NOT EXISTS Subscribers (
    Id TEXT PRIMARY KEY,
    Url TEXT NOT NULL,
    EncryptedSecret TEXT NOT NULL,
    CreatedAt TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Events (
    Id TEXT PRIMARY KEY,
    Type TEXT NOT NULL,
    Payload TEXT NOT NULL,
    CreatedAt TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS DeliveryAttempts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    EventId TEXT NOT NULL REFERENCES Events(Id),
    SubscriberId TEXT NOT NULL REFERENCES Subscribers(Id),
    Status TEXT NOT NULL,
    AttemptNumber INTEGER NOT NULL,
    ResponseCode INTEGER NULL,
    AttemptedAt TEXT NOT NULL
);