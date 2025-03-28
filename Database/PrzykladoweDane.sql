-- Producenci
INSERT INTO Producenci (Nazwa, Kraj) VALUES ('Dell', 'USA');
INSERT INTO Producenci (Nazwa, Kraj) VALUES ('HP', 'USA');
INSERT INTO Producenci (Nazwa, Kraj) VALUES ('Lenovo', 'Chiny');
INSERT INTO Producenci (Nazwa, Kraj) VALUES ('Apple', 'USA');
INSERT INTO Producenci (Nazwa, Kraj) VALUES ('Asus', 'Tajwan');
INSERT INTO Producenci (Nazwa, Kraj) VALUES ('Acer', 'Tajwan');
INSERT INTO Producenci (Nazwa, Kraj) VALUES ('MSI', 'Tajwan');

-- Systemy operacyjne
INSERT INTO SystemyOperacyjne (Nazwa, Wersja, Architektura) VALUES ('Windows', '11 Pro', 'x64');
INSERT INTO SystemyOperacyjne (Nazwa, Wersja, Architektura) VALUES ('Windows', '11 Home', 'x64');
INSERT INTO SystemyOperacyjne (Nazwa, Wersja, Architektura) VALUES ('Windows', '10 Pro', 'x64');
INSERT INTO SystemyOperacyjne (Nazwa, Wersja, Architektura) VALUES ('macOS', 'Sonoma', 'ARM64');
INSERT INTO SystemyOperacyjne (Nazwa, Wersja, Architektura) VALUES ('macOS', 'Ventura', 'x64');
INSERT INTO SystemyOperacyjne (Nazwa, Wersja, Architektura) VALUES ('Linux', 'Ubuntu 22.04', 'x64');

-- Kategorie
INSERT INTO Kategorie (Nazwa, Opis) VALUES ('Biznesowy', 'Laptopy dla firm i profesjonalistów');
INSERT INTO Kategorie (Nazwa, Opis) VALUES ('Gamingowy', 'Laptopy do gier');
INSERT INTO Kategorie (Nazwa, Opis) VALUES ('Multimedialny', 'Laptopy do zastosowań multimedialnych');
INSERT INTO Kategorie (Nazwa, Opis) VALUES ('Ultrabook', 'Lekkie i smukłe laptopy');

-- Dostawcy
INSERT INTO Dostawcy (Nazwa, Adres, Telefon, Email, OsobaKontaktowa) VALUES ('IT Dystrybutor', 'Warszawa, ul. Elektroniczna 10', '+48 22 123 45 67', 'kontakt@itdystrybutor.pl', 'Jan Kowalski');
INSERT INTO Dostawcy (Nazwa, Adres, Telefon, Email, OsobaKontaktowa) VALUES ('Komputery Hurtowo', 'Kraków, ul. Procesorowa 22', '+48 12 987 65 43', 'hurt@komputeryhurtowo.pl', 'Anna Nowak');

-- Przykładowe laptopy
INSERT INTO Laptopy (ProducentID, Model, SystemID, RozmiarEkranu, Procesor, RAM, DyskTyp, DyskPojemnosc, KategoriaID, IloscSztuk, CenaJednostkowa) VALUES (1, 'XPS 13', 1, 13.3, 'Intel Core i7-1165G7', 16, 'SSD', 512, 4, 25, 5999);
INSERT INTO Laptopy (ProducentID, Model, SystemID, RozmiarEkranu, Procesor, RAM, DyskTyp, DyskPojemnosc, KategoriaID, IloscSztuk, CenaJednostkowa) VALUES (1, 'Latitude 5520', 3, 15.6, 'Intel Core i5-1135G7', 8, 'SSD', 256, 1, 15, 4299);
INSERT INTO Laptopy (ProducentID, Model, SystemID, RozmiarEkranu, Procesor, RAM, DyskTyp, DyskPojemnosc, KategoriaID, IloscSztuk, CenaJednostkowa) VALUES (2, 'Pavilion 15', 2, 15.6, 'AMD Ryzen 5 5500U', 16, 'SSD', 512, 3, 12, 3799);
INSERT INTO Laptopy (ProducentID, Model, SystemID, RozmiarEkranu, Procesor, RAM, DyskTyp, DyskPojemnosc, KategoriaID, IloscSztuk, CenaJednostkowa) VALUES (3, 'ThinkPad X1 Carbon', 1, 14.0, 'Intel Core i7-1260P', 16, 'SSD', 1024, 1, 8, 6499);
INSERT INTO Laptopy (ProducentID, Model, SystemID, RozmiarEkranu, Procesor, RAM, DyskTyp, DyskPojemnosc, KategoriaID, IloscSztuk, CenaJednostkowa) VALUES (3, 'Legion 5 Pro', 2, 16.0, 'AMD Ryzen 7 5800H', 32, 'SSD', 1024, 2, 10, 5899);
INSERT INTO Laptopy (ProducentID, Model, SystemID, RozmiarEkranu, Procesor, RAM, DyskTyp, DyskPojemnosc, KategoriaID, IloscSztuk, CenaJednostkowa) VALUES (4, 'MacBook Pro 14', 4, 14.2, 'Apple M2 Pro', 16, 'SSD', 512, 4, 5, 9999);
INSERT INTO Laptopy (ProducentID, Model, SystemID, RozmiarEkranu, Procesor, RAM, DyskTyp, DyskPojemnosc, KategoriaID, IloscSztuk, CenaJednostkowa) VALUES (4, 'MacBook Air', 5, 13.3, 'Apple M1', 8, 'SSD', 256, 4, 20, 4999);
INSERT INTO Laptopy (ProducentID, Model, SystemID, RozmiarEkranu, Procesor, RAM, DyskTyp, DyskPojemnosc, KategoriaID, IloscSztuk, CenaJednostkowa) VALUES (6, 'Predator Helios 300', 3, 15.6, 'Intel Core i7-11800H', 16, 'SSD', 512, 2, 7, 5499);
INSERT INTO Laptopy (ProducentID, Model, SystemID, RozmiarEkranu, Procesor, RAM, DyskTyp, DyskPojemnosc, KategoriaID, IloscSztuk, CenaJednostkowa) VALUES (5, 'ZenBook 14', 1, 14.0, 'Intel Core i5-1240P', 16, 'SSD', 512, 4, 18, 4999);
INSERT INTO Laptopy (ProducentID, Model, SystemID, RozmiarEkranu, Procesor, RAM, DyskTyp, DyskPojemnosc, KategoriaID, IloscSztuk, CenaJednostkowa) VALUES (5, 'ROG Strix G15', 2, 15.6, 'AMD Ryzen 9 5900HX', 32, 'SSD', 1024, 2, 5, 7899);

-- Przykładowe dostawy
INSERT INTO Dostawy (DostawcaID, DataDostawy, NumerFaktury, UwagiDoDostawy) VALUES (1, datetime('now', '-30 days', 'localtime'), 'FV/2023/10/001', 'Dostawa kwartalna');

-- Przykładowe szczegóły dostaw
INSERT INTO SzczegolyDostawy (DostawaID, LaptopID, IloscDostarczona, CenaZakupu) VALUES (1, 1, 25, 4800);
INSERT INTO SzczegolyDostawy (DostawaID, LaptopID, IloscDostarczona, CenaZakupu) VALUES (1, 2, 15, 3500);