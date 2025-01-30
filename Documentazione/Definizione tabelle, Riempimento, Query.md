#### 🏷️ **Titolo**
**L'aeroporto più sicuro al mondo**
#### 📅 **Data**
**19/01/2025**

---
## **Indice**

1. Definizione Tabelle 🎯  
2. Riempimento ➗    
3. Query 🔎  

---
<h2>1. Definizione Tabelle🎯</h2>

~~~~sql
CREATE TABLE Passeggero (
    Id_Passeggero INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(50) NOT NULL,
    Cognome NVARCHAR(50) NOT NULL,
    Nazionalità NVARCHAR(50) NOT NULL,
    Tipo_Identificativo NVARCHAR(50) NOT NULL,
    N_Identificativo NVARCHAR(50) UNIQUE,
    A_Partenza NVARCHAR(100) NOT NULL,
    A_Destinazione NVARCHAR(100) NOT NULL,
    Motivo_Viaggio NVARCHAR(100) NOT NULL
);
~~~~

~~~~sql
CREATE TABLE Merce (
    Id_Merce INT PRIMARY KEY IDENTITY,
    Descrizione NVARCHAR(255) NOT NULL,
    Note NVARCHAR(255),
    Quantità INT NOT NULL,
	Id_Controllo INT NOT NULL FOREIGN KEY REFERENCES Controllo(Id_Controllo)
);
~~~~

~~~~sql
CREATE TABLE Categoria (
    Id_Categoria INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(100) NOT NULL,
    Descrizione NVARCHAR(255) NOT NULL
);
~~~~

~~~~sql
CREATE TABLE Fa_Parte (
    Id_Merce INT NOT NULL FOREIGN KEY REFERENCES Merce(Id_Merce),
    Id_Categoria INT NOT NULL FOREIGN KEY REFERENCES Categoria(Id_Categoria),
    PRIMARY KEY (Id_Merce, Id_Categoria),
);
~~~~

~~~~sql
CREATE TABLE Funzionario (
    Id_Funzionario INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(50) NOT NULL,
    Cognome NVARCHAR(50) NOT NULL,
    Data_Ora_Login DATETIME NOT NULL,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Ruolo NVARCHAR(50) NOT NULL
);
~~~~

~~~~sql
CREATE TABLE Punto_Controllo (
    Id_Punto_Controllo INT PRIMARY KEY IDENTITY,
    Descrizione NVARCHAR(255) NOT NULL,
    Nome NVARCHAR(100) NOT NULL,
    Tipo NVARCHAR(100) NOT NULL
);
~~~~

~~~~sql
CREATE TABLE Controllo (
    Id_Controllo INT PRIMARY KEY IDENTITY,
    Data_Ora_Inizio DATETIME NOT NULL,
    Note NVARCHAR(MAX),
    Data_Ora_Fine DATETIME NOT NULL,
    Dazio_Doganale FLOAT,
    Id_Funzionario INT NOT NULL FOREIGN KEY REFERENCES Funzionario(Id_Funzionario),
    Id_Passeggero INT NOT NULL FOREIGN KEY REFERENCES Passeggero(Id_Passeggero)
);
~~~~

~~~~sql
CREATE TABLE Addetto (
    Id_Addetto INT PRIMARY KEY IDENTITY,
    Nome NVARCHAR(50) NOT NULL,
    Cognome NVARCHAR(50) NOT NULL,
    Data_Ora_Login DATETIME NOT NULL,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Ruolo NVARCHAR(50) NOT NULL,
    Id_Funzionario INT NOT NULL FOREIGN KEY REFERENCES Funzionario(Id_Funzionario)
);
~~~~

~~~~sql
CREATE TABLE Esegue (
    Id_Controllo INT NOT NULL FOREIGN KEY REFERENCES Controllo(Id_Controllo),
    Id_Addetto INT NOT NULL FOREIGN KEY REFERENCES Addetto(Id_Addetto),
    PRIMARY KEY (Id_Controllo, Id_Addetto)
);
~~~~

~~~~sql
CREATE TABLE Esito (
    Id_Esito INT PRIMARY KEY IDENTITY,
    Descrizione NVARCHAR(255) NOT NULL,
    Id_Controllo INT NOT NULL FOREIGN KEY REFERENCES Controllo(Id_Controllo),
);
~~~~

~~~~sql
CREATE TABLE Effettuato_in (
    Id_Controllo INT NOT NULL FOREIGN KEY REFERENCES Controllo(Id_Controllo),
    Id_Punto_Controllo INT NOT NULL FOREIGN KEY REFERENCES
	    Punto_Controllo(Id_Punto_Controllo),
    PRIMARY KEY (Id_Controllo, Id_Punto_Controllo)
);
~~~~

---

#### 2. **Riempimento** ➗ 


~~~~sql
INSERT INTO Passeggero (Nome, Cognome, Nazionalità,
						Tipo_Identificativo, N_Identificativo, A_Partenza,
						A_Destinazione, Motivo_Viaggio)
VALUES
	('Mario', 'Rossi', 'Italiana', 'Passaporto', 'AB123456', 'Roma Fiumicino',
		'New York JFK', 'Turismo'),
	('Laura', 'Bianchi', 'Francese', 'Carta Identità', 'FR987654', 'Parigi
		CDG', 'Roma Fiumicino', 'Lavoro');
~~~~

~~~~sql
INSERT INTO Categoria (Nome, Descrizione)
VALUES
	('Alcolici', 'Bevande alcoliche'),
	('Latticini', 'Prodotti derivati dal latte');
~~~~

~~~~sql
INSERT INTO Funzionario (Nome, Cognome, Data_Ora_Login, Username,
						 Password, Ruolo)
VALUES
	('Giovanni', 'Verdi', '2023-10-01 08:00:00', 'gverdi', 'password123',
		'Supervisore'),
	('Anna', 'Neri', '2023-10-01 09:00:00', 'aneri', 'password456', 'Agente');
~~~~

~~~~sql
INSERT INTO Punto_Controllo (Descrizione, Nome, Tipo)
VALUES
	('Controllo Doganale Merci', 'Dogana Merci', 'Dogana'),
	('Controllo Sicurezza Passeggeri', 'Sicurezza Passeggeri', 'Sicurezza');
~~~~

~~~~sql
INSERT INTO Controllo (Data_Ora_Inizio, Note, Data_Ora_Fine,
					   Dazio_Doganale, Id_Funzionario,
					   Id_Passeggero)
VALUES
	('2023-10-01 10:00:00', 'Controllo routine', '2023-10-01 10:30:00', 23.43,
		1, 1),
	('2023-10-01 11:00:00', 'Controllo approfondito', '2023-10-01 11:45:00',
		120.46, 2, 2);
~~~~

~~~~sql
INSERT INTO Merce (Descrizione, Note, Quantità, Id_Controllo)
VALUES
	('Vino Rosso', 'Bottiglie di Chianti', 12, 1),
	('Formaggio', 'Pecorino Romano', 5, 2);
~~~~

~~~~sql
INSERT INTO Fa_Parte (Id_Merce, Id_Categoria)
VALUES
	(1, 1),
	(2, 2);
~~~~

~~~~sql
INSERT INTO Addetto (Nome, Cognome, Data_Ora_Login, Username,
					 Password, Ruolo, Id_Funzionario)
VALUES
	('Luigi', 'Gialli', '2023-10-01 08:30:00', 'lgialli', 'password789',
		'Operatore', 1),
	('Sara', 'Violi', '2023-10-01 09:30:00', 'svioli', 'password012',
		'Operatore', 2);
~~~~

~~~~sql
INSERT INTO Esegue (Id_Controllo, Id_Addetto)
VALUES
	(1, 1),
	(2, 2);
~~~~

~~~~sql
INSERT INTO Esito (Descrizione, Id_Controllo)
VALUES
	('Approvato', 1),
	('Respinto', 2);
~~~~

~~~~sql
INSERT INTO Effettuato_in (Id_Controllo, Id_Punto_Controllo)
VALUES
	(1, 1),
	(2, 2);
~~~~

---
<h2>9. Query 🔎</h2>
#### **1. Visualizzare i dati di tutti i passeggeri controllati in ciascun punto di dogana nell’arco della giornata:**

~~~~sql
SELECT 
    P.Nome, P.Cognome, P.Nazionalità, P.Tipo_Identificativo, P.N_Identificativo, 
    P.A_Partenza, P.A_Destinazione, P.Motivo_Viaggio, 
    PC.Nome AS Punto_Controllo, C.Data_Ora_Inizio, C.Data_Ora_Fine
FROM 
    Passeggero P
JOIN 
    Controllo C ON P.Id_Passeggero = C.Id_Passeggero
JOIN
	Effettuato_in EFIN ON C.Id_Controllo = EFIN.Id_Controllo
JOIN 
    Punto_Controllo PC ON EFIN.Id_Punto_Controllo = PC.Id_Punto_Controllo
WHERE 
    CAST(C.Data_Ora_Inizio AS DATE) = CAST(GETDATE() AS DATE);
~~~~

Questa query seleziona i dati dei passeggeri controllati in data odierna, unendoli con le tabelle `Controllo`, `Effettuato_in` e `Punto_Controllo`. La clausola `WHERE` filtra i risultati per la data odierna usando la funzione `CAST` per ottenere solo la data dal `DATETIME` e `GETDATE()` per ottenere la data odierna.

#### **2. Visualizzare per ciascun punto di controllo l’ammontare dei dazi doganali registrati:**

~~~~sql
SELECT 
    PC.Nome AS Punto_Controllo, SUM(C.Dazio_Doganale) AS Totale_Dazi
FROM 
    Controllo C
JOIN
	Effettuato_in EFIN ON C.Id_Controllo = EFIN.Id_Controllo
JOIN 
    Punto_Controllo PC ON EFIN.Id_Punto_Controllo = PC.Id_Punto_Controllo
GROUP BY 
    PC.Nome;
~~~~

La query somma i dazi doganali (`Dazio_Doganale`) usando la funzione `SUM` per ogni punto di controllo, raggruppando i risultati per il nome del punto di controllo. Per fare ciò esegue il join della tabella `Effettuato_in` e `Punto_Controllo`

#### **3. Calcolare e visualizzare quante merci per ogni categoria sono state respinte dall’inizio dell’anno:**

~~~~sql
SELECT 
    Cat.Nome AS Categoria, COUNT(*) AS Merci_Respinte
FROM 
    Esito E
JOIN 
    Controllo C ON E.Id_Controllo = C.Id_Controllo
JOIN 
    Merce M ON C.Id_Controllo = M.Id_Controllo
JOIN 
	Fa_Parte FP ON M.Id_Merce = FP.Id_Merce
JOIN 
    Categoria Cat ON FP.Id_Categoria = Cat.Id_Categoria
WHERE 
    E.Descrizione = 'Respinto' AND YEAR(C.Data_Ora_Inizio) = YEAR(GETDATE())
GROUP BY 
    Cat.Nome;
~~~~

La query conta le merci respinte (`Esito.Descrizione = 'Respinto'`) per ogni categoria, considerando solo i controlli effettuati nell’anno corrente.
Per fare ciò collega con il join le tabelle `Controllo`, `Merce`, `Fa_Parte` e `Categoria`.
Il `GROUP BY` viene utilizzato per raggruppare i risultati per nome della categoria.

#### **4. Calcolare e visualizzare quante contestazioni sono state registrate da ciascun addetto:**

~~~~sql
SELECT 
    A.Nome, A.Cognome, COUNT(E.Id_Esito) AS Numero_Contestazioni
FROM 
    Addetto A
JOIN 
    Esegue Ese ON A.Id_Addetto = Ese.Id_Addetto
JOIN 
    Esito E ON Ese.Id_Controllo = E.Id_Controllo
WHERE 
    E.Descrizione = 'Contestato'
GROUP BY 
    A.Nome, A.Cognome;
~~~~

La query conta le contestazioni totali (`Esito.Descrizione = 'Contestato'`)  di ciascun addetto. 
Per fare ciò collega con il join le tabelle `Esegue` e `Esito`.
Il `GROUP BY` raggruppa i risultati per nome e cognome dell'addetto.

#### **5. Calcolare la durata media dei controlli per ogni punto di controllo nell’arco della giornata:**

~~~~sql
SELECT 
    PC.Nome AS Punto_Controllo, 
    AVG(DATEDIFF(MINUTE, C.Data_Ora_Inizio, C.Data_Ora_Fine)) AS
	    Durata_Media_Minuti
FROM 
    Controllo C
JOIN 
    Effettuato_in EFIN ON C.Id_Controllo = EFIN.Id_Controllo
JOIN 
    Punto_Controllo PC ON EFIN.Id_Punto_Controllo = PC.Id_Punto_Controllo
WHERE 
    CAST(C.Data_Ora_Inizio AS DATE) = CAST(GETDATE() AS DATE)
GROUP BY 
    PC.Nome;
~~~~

La query calcola la durata media dei controlli in minuti per ogni punto di controllo, considerando solo i controlli effettuati nella data odierna.
Per fare ciò collega con il `Join` le tabelle `Effettuato_in` e `Punto_Controllo`.
La funzione `DATEDIFF` calcola la differenza in minuti tra l'inizio e la fine del controllo, mentre `AVG` calcola la media. Il `GROUP BY` raggruppa i risultati per nome del punto di controllo.

#### **6. Visualizzare l’elenco, in ordine alfabetico, raggruppato per nazionalità, dei passeggeri in stato di fermo, registrati dall’inizio dell’anno in tutti i punti di controllo:**

~~~~sql
SELECT 
    P.Nazionalità, P.Nome, P.Cognome
FROM 
    Passeggero P
JOIN 
    Controllo C ON P.Id_Passeggero = C.Id_Passeggero
JOIN 
    Esito E ON C.Id_Controllo = E.Id_Controllo
WHERE 
    E.Descrizione = 'Fermo' AND YEAR(C.Data_Ora_Inizio) = YEAR(GETDATE())
ORDER BY 
    P.Nazionalità, P.Nome, P.Cognome;
~~~~

La query seleziona i passeggeri in stato di fermo (`Esito.Descrizione = 'Fermo'`), raggruppandoli per nazionalità e ordinandoli alfabeticamente.
Per fare ciò collega con il `Join` le tabelle `Controllo` e `Esito`.
`WHERE` filtra i risultati per l'anno corrente, mentre `ORDER BY` ordina i risultati in ordine alfabetico per nazionalità, nome e cognome.

#### **7. Visualizzare gli addetti in servizio nella giornata, suddivisi per nome del funzionario incaricato:**

~~~~sql
SELECT 
    F.Nome AS Nome_Funzionario, F.Cognome AS Cognome_Funzionario, 
    A.Nome AS Nome_Addetto, A.Cognome AS Cognome_Addetto
FROM 
    Addetto A
JOIN 
    Funzionario F ON A.Id_Funzionario = F.Id_Funzionario
WHERE 
    CAST(A.Data_Ora_Login AS DATE) = CAST(GETDATE() AS DATE)
~~~~

La query mostra gli addetti che hanno effettuato il login in data odierna, raggruppandoli per il funzionario incaricato.
Per fare ciò collega con il `Join` la tabella `Funzionario`.
La funzione `WHERE` filtra i risultati per la data odierna.