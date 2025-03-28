# Baza danych Magazynu Laptopów

## Struktura bazy danych

Baza danych SQLite zawiera następujące tabele:

### Tabele główne

-   **Laptopy** - główna tabela z informacjami o laptopach
-   **Producenci** - producenci laptopów
-   **SystemyOperacyjne** - dostępne systemy operacyjne
-   **Kategorie** - kategorie laptopów

### Tabele obsługi magazynu

-   **Transakcje** - historia przyjęć i wydań z magazynu
-   **Dostawcy** - informacje o dostawcach
-   **Dostawy** - informacje o dostawach
-   **SzczegolyDostawy** - szczegóły dostaw

### Widoki

-   **WidokLaptopow** - widok zbierający najważniejsze informacje o laptopach
