-- Utworzenie tabeli dla producentów laptopów
CREATE TABLE IF NOT EXISTS Producenci (
    ProducentID INTEGER PRIMARY KEY AUTOINCREMENT,
    Nazwa TEXT NOT NULL UNIQUE,
    Kraj TEXT,
    DataDodania TEXT DEFAULT (datetime('now', 'localtime'))
);

-- Utworzenie tabeli dla systemów operacyjnych
CREATE TABLE IF NOT EXISTS SystemyOperacyjne (
    SystemID INTEGER PRIMARY KEY AUTOINCREMENT,
    Nazwa TEXT NOT NULL,
    Wersja TEXT NOT NULL,
    Architektura TEXT,
    UNIQUE(Nazwa, Wersja)
);

-- Utworzenie tabeli dla kategorii laptopów
CREATE TABLE IF NOT EXISTS Kategorie (
    KategoriaID INTEGER PRIMARY KEY AUTOINCREMENT,
    Nazwa TEXT NOT NULL UNIQUE,
    Opis TEXT
);

-- Główna tabela dla laptopów
CREATE TABLE IF NOT EXISTS Laptopy (
    LaptopID INTEGER PRIMARY KEY AUTOINCREMENT,
    ProducentID INTEGER,
    Model TEXT NOT NULL,
    SystemID INTEGER,
    RozmiarEkranu REAL,
    Procesor TEXT,
    RAM INTEGER,  -- w GB
    DyskTyp TEXT, -- SSD, HDD
    DyskPojemnosc INTEGER, -- w GB
    KategoriaID INTEGER,
    IloscSztuk INTEGER NOT NULL DEFAULT 0,
    CenaJednostkowa REAL,
    DataDodania TEXT DEFAULT (datetime('now', 'localtime')),
    OstatniaAktualizacja TEXT DEFAULT (datetime('now', 'localtime')),
    Uwagi TEXT,
    FOREIGN KEY (ProducentID) REFERENCES Producenci(ProducentID),
    FOREIGN KEY (SystemID) REFERENCES SystemyOperacyjne(SystemID),
    FOREIGN KEY (KategoriaID) REFERENCES Kategorie(KategoriaID)
);

-- Tabela dla historii transakcji (przyjęcie/wydanie z magazynu)
CREATE TABLE IF NOT EXISTS Transakcje (
    TransakcjaID INTEGER PRIMARY KEY AUTOINCREMENT,
    LaptopID INTEGER,
    Typ TEXT NOT NULL, -- "Przyjęcie" lub "Wydanie"
    Ilosc INTEGER NOT NULL,
    DataTransakcji TEXT DEFAULT (datetime('now', 'localtime')),
    NumerDokumentu TEXT,
    Uzytkownik TEXT,
    Opis TEXT,
    FOREIGN KEY (LaptopID) REFERENCES Laptopy(LaptopID)
);

-- Tabela dla dostawców
CREATE TABLE IF NOT EXISTS Dostawcy (
    DostawcaID INTEGER PRIMARY KEY AUTOINCREMENT,
    Nazwa TEXT NOT NULL UNIQUE,
    Adres TEXT,
    Telefon TEXT,
    Email TEXT,
    OsobaKontaktowa TEXT
);

-- Tabela dla dostaw
CREATE TABLE IF NOT EXISTS Dostawy (
    DostawaID INTEGER PRIMARY KEY AUTOINCREMENT,
    DostawcaID INTEGER,
    DataDostawy TEXT DEFAULT (datetime('now', 'localtime')),
    NumerFaktury TEXT,
    UwagiDoDostawy TEXT,
    FOREIGN KEY (DostawcaID) REFERENCES Dostawcy(DostawcaID)
);

-- Tabela dla szczegółów dostaw
CREATE TABLE IF NOT EXISTS SzczegolyDostawy (
    SzczegolyDostawyID INTEGER PRIMARY KEY AUTOINCREMENT,
    DostawaID INTEGER,
    LaptopID INTEGER,
    IloscDostarczona INTEGER NOT NULL,
    CenaZakupu REAL,
    FOREIGN KEY (DostawaID) REFERENCES Dostawy(DostawaID),
    FOREIGN KEY (LaptopID) REFERENCES Laptopy(LaptopID)
);

-- Utworzenie widoku dla łatwiejszego raportowania
CREATE VIEW IF NOT EXISTS WidokLaptopow AS
SELECT 
    L.LaptopID,
    P.Nazwa AS Producent,
    L.Model,
    SO.Nazwa || ' ' || SO.Wersja AS SystemOperacyjny,
    L.RozmiarEkranu,
    L.Procesor,
    L.RAM,
    L.DyskTyp,
    L.DyskPojemnosc,
    K.Nazwa AS Kategoria,
    L.IloscSztuk,
    L.CenaJednostkowa,
    L.IloscSztuk * L.CenaJednostkowa AS WartoscCalkowita,
    L.DataDodania,
    L.OstatniaAktualizacja
FROM Laptopy L
LEFT JOIN Producenci P ON L.ProducentID = P.ProducentID
LEFT JOIN SystemyOperacyjne SO ON L.SystemID = SO.SystemID
LEFT JOIN Kategorie K ON L.KategoriaID = K.KategoriaID;

-- Utworzenie wyzwalacza do aktualizacji daty modyfikacji
CREATE TRIGGER IF NOT EXISTS update_laptop_modification_date
AFTER UPDATE ON Laptopy
FOR EACH ROW
BEGIN
    UPDATE Laptopy SET OstatniaAktualizacja = datetime('now', 'localtime') WHERE LaptopID = NEW.LaptopID;
END;

-- Utworzenie wyzwalacza do aktualizacji stanu magazynowego przy dostawie
CREATE TRIGGER IF NOT EXISTS update_stock_after_delivery
AFTER INSERT ON SzczegolyDostawy
FOR EACH ROW
BEGIN
    UPDATE Laptopy SET 
        IloscSztuk = IloscSztuk + NEW.IloscDostarczona,
        OstatniaAktualizacja = datetime('now', 'localtime')
    WHERE LaptopID = NEW.LaptopID;
    
    -- Dodaj wpis do tabeli transakcji
    INSERT INTO Transakcje (LaptopID, Typ, Ilosc, NumerDokumentu, Opis)
    SELECT NEW.LaptopID, 'Przyjęcie', NEW.IloscDostarczona, D.NumerFaktury, 'Automatycznie z dostawy'
    FROM Dostawy D
    WHERE D.DostawaID = NEW.DostawaID;
END;

-- Utworzenie indeksów dla optymalizacji zapytań
CREATE INDEX IF NOT EXISTS idx_laptopy_producent ON Laptopy(ProducentID);
CREATE INDEX IF NOT EXISTS idx_laptopy_system ON Laptopy(SystemID);
CREATE INDEX IF NOT EXISTS idx_laptopy_kategoria ON Laptopy(KategoriaID);
CREATE INDEX IF NOT EXISTS idx_transakcje_laptop ON Transakcje(LaptopID);
CREATE INDEX IF NOT EXISTS idx_dostawy_dostawca ON Dostawy(DostawcaID);
CREATE INDEX IF NOT EXISTS idx_szczegoly_dostawa ON SzczegolyDostawy(DostawaID);
CREATE INDEX IF NOT EXISTS idx_szczegoly_laptop ON SzczegolyDostawy(LaptopID);