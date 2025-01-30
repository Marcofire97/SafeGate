#### 🏷️ **Titolo**
**L'aeroporto più sicuro al mondo**
#### 📅 **Data**
**15/01/2025**

---
## **Indice**

1. **Schema E.R.** 🎯  
2. Schema Logico ➗    
3. **Analisi** 🔎  

---
<h2>1. Schema E.R.🎯</h2>

![[SafeGate(4).png]]

---

#### 2. **Schema Logico** ➗ 

#### Passeggero
- **Id_Passeggero** (PK)
- Nome
- Cognome
- Nazionalità
- Tipo_Identificativo
- N_Identificativo
- A_Partenza
- A_Destinazione
- Motivo_Viaggio

### Merce
- **Id_Merce** (PK)
- Id_Controllo (FK)
- Descrizione
- Note
- Quantità

### Categoria
- **Id_Categoria** (PK)
- Nome
- Descrizione

### Fa_Parte
* Id_Merce (FK)
* Id_Categoria (FK)

### Controllo
- **Id_Controllo** (PK)
- Data_Ora_Inizio
- Note
- Data_Ora_Fine
- Dazio_Doganale
- Id_Funzionario (FK)
- Id_Passeggero (FK)

### Addetto
- **Id_Addetto** (PK)
- Nome
- Cognome
- Data_Ora_Login
- Username
- Password
- Ruolo
- Id_Funzionario (FK)

### Esegue
* Id_Controllo (FK)
* Id_Addetto (FK)

### Funzionario
- **Id_Funzionario** (PK)
- Nome
- Cognome
- Data_Ora_Login
- Username
- Password
- Ruolo

### Esito
- **Id_Esito** (PK)
- Descrizione
- Id_Controllo (FK)

### Punto_Controllo
- **Id_Punto_Controllo** (PK)
- Descrizione
- Nome
- Tipo

### Effettuato_in
* Id_Controllo (FK)
* Id_Punto_Controllo (FK)

---
<h2>9. Analisi 🔎</h2>
### **Contesto generale**

Il sistema gestisce:

1. **Passeggeri**: Persone che viaggiano da un luogo a un altro in aereo

2. **Merce**: Beni trasportati, che possono essere soggetti a controlli doganali e della sicurezza

3. **Controlli**: Operazioni di verifica effettuate su passeggeri e merci

4. **Addetti e Funzionari**: Personale addetto ai controlli, con ruoli e responsabilità specifiche

5. **Punti di Controllo**: Luoghi dove vengono effettuati i controlli (ad esempio, security, dogane, ecc.)

7. **Esiti**: Risultati dei controlli (ad esempio, "approvato", "respinto", "richiesta ulteriori verifiche")

### **Entità e ruoli nel sistema**

#### 1. **Passeggero**

- Rappresenta le persone che viaggiano in aereo.

- Attributi come **Nome**, **Cognome**, **Nazionalità**, **Tipo_Identificativo** (ad esempio, passaporto, carta d'identità), e **N_Identificativo** (numero del documento) sono essenziali per identificare i viaggiatori.

- **A_Partenza** e **A_Destinazione** indicano il viaggio specifico.

- **Motivo_Viaggio** è utilizzato per classificare il viaggio (ad esempio, turismo, lavoro, trasferimento).

#### 2. **Merce**

- Rappresenta i beni trasportati dal passeggero.

- **Descrizione**, **Note**, e **Quantità** sono dettagli utili per i controlli doganali.

#### 3. **Categoria**

- Permette di classificare la merce in base al tipo.

- Utile per applicare regole doganali specifiche (ad esempio, tasse per diverse categorie di merce).

#### 4. **Controllo**

- È l'operazione di verifica effettuata su un passeggero e su una merce o bagaglio.

- **Data_Ora_Inizio** e **Data_Ora_Fine** indicano la durata del controllo.

- **Dazio_Doganale** è il costo applicato a certe categorie di merce in seguito al controllo.

* Le **Note** sono fondamentali per un'indagine più approfondita

#### 5. **Addetto**

- Personale dell'aeroporto che esegue i controlli.

- **Nome**, **Cognome**, **Username**, **Password**, e **Ruolo** sono informazioni necessarie per l'autenticazione.

#### 6. **Funzionario**

- Personale dell'aeroporto con un ruolo di supervisione o gestione

* Gestisce gli addetti

- Potrebbe essere responsabile della revisione dei controlli effettuati dagli addetti o della gestione delle politiche di sicurezza.

#### 7. **Esito**

- Rappresenta il risultato di un controllo di sicurezza o di dogana.

- **Descrizione** include esiti come "approvato", "respinto", o "richiesta ulteriori verifiche".

#### 8. **Punto_Controllo**

- Luogo dove vengono effettuati i controlli.

- **Descrizione**, **Nome**, e **Tipo** aiutano a identificare il punto di controllo (ad esempio, "Dogana Aeroporto", "Controllo Sicurezza Gate A").


### **Relazioni**

#### 1. **Passeggero - Controllo**

- **Relazione**: Un **Passeggero** può essere soggetto a uno o più **Controlli** e un **Controllo** viene fatto su un solo **Passeggero**.

- **Tipo**: **1 a N**.

#### 2. **Merce - Categoria (Fa_Parte)**

- **Relazione**: Una **Merce** può appartenere a una o più **Categorie**, e una **Categoria** può essere associata a più merci.

- **Tipo**: **N a N**.

#### 3. **Controllo - Punto_Controllo (Effettuato_in)**

- **Relazione**: Un **Controllo** può essere effettuato in uno o più **Punti di Controllo**, e un **Punto di Controllo** può ospitare più controlli.

- **Tipo**: **N a N**.

#### 4. **Controllo - Addetto (Esegue)**

- **Relazione**: Un **Controllo** può essere eseguito da uno o più **Addetti**, e un **Addetto** può eseguire più controlli.

- **Tipo**: **N a N**.

#### 5. **Controllo - Funzionario**

- **Relazione**: Un **Controllo** è supervisionato da un **Funzionario** e un **Funzionario**può supervisionare più **Controlli**.

- **Tipo**: **1 a N**.

#### 6. **Controllo - Esito**

- **Relazione**: Un **Controllo** può produrre uno o più **Esiti** e un **Esito** e valido solo per un **Controllo**.

- **Tipo**: **1 a N**.

#### 7. **Addetto - Funzionario**

- **Relazione**: Un **Addetto** è supervisionato da un **Funzionario** e un **Funzionario** può gestire più **Addetti**.

- **Tipo**: **1 a N**.

#### 8. **Controllo - Merce**

- **Relazione**: Un **Controllo** può essere effettuato su una o più **Merci**.

- **Tipo**: **1 a N**.