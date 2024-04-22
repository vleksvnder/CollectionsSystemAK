#### Collections System AK
Repozytorium zawiera prostą aplikację do zarządzania kolekcjami przedmiotów. Pozwala ona na dodawanie, edycję, usuwanie oraz przeglądanie elementów w kolekcji.

#### Struktura Projektu
Projekt składa się z następujących komponentów:

1. **Modele**:

`Collection.cs:` Definiuje klasę Collection z właściwościami dla identyfikatora i nazwy kolekcji.
`Item.cs:` Definiuje klasę Item z właściwościami dla identyfikatora, identyfikatora kolekcji, nazwy, opisu, ceny, statusu, oceny i ścieżki obrazu.

2. **Widoki**:

`AddItemPage.xaml:` Strona umożliwiająca dodanie nowego elementu do kolekcji.
`CollectionItemsPage.xaml:` Strona wyświetlająca elementy w danej kolekcji.
`EditItemPage.xaml:` Strona umożliwiająca edycję istniejącego elementu kolekcji.
`MainPage.xaml:` Główna strona aplikacji wyświetlająca listę kolekcji.

3. **Kod-behind**:

`AddItemPage.xaml.cs`, `CollectionItemsPage.xaml.cs`, `EditItemPage.xaml.cs`, `MainPage.xaml.cs:` Zawiera logikę obsługi interakcji użytkownika i zarządzania danymi.

#### Funkcjonalność
**Strona Główna (MainPage.xaml):**

Wyświetla listę kolekcji.
Umożliwia dodanie nowej kolekcji.

**Strona Elementów Kolekcji (CollectionItemsPage.xaml):**

Wyświetla elementy w wybranej kolekcji.
Umożliwia dodanie nowego elementu.

**Strona Dodawania Elementu (AddItemPage.xaml):**

Pozwala użytkownikom wprowadzić dane nowego elementu do kolekcji.
Umożliwia wybór grafiki.

**Strona Edycji Elementu (EditItemPage.xaml):**

Pozwala użytkownikom edytować dane istniejącego elementu kolekcji.

#### Użycie
Aby skorzystać z aplikacji:

1. Sklonuj to repozytorium na swój lokalny komputer.
2. Otwórz rozwiązanie w programie Visual Studio lub innym kompatybilnym środowisku.
3. Zbuduj i uruchom aplikację na wybranej platformie.

#### Użyte Technologie
1..NET MAUI
2. C#
3. XAML

~ Aleksander Kosała | vleksvnder
