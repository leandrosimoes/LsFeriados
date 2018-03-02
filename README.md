# LsFeriados
.NET Library to deal with Brazilian Holydays

### Global Config Variables

```csharp
// Boolean variable to consider an specific holyday that is not accepted by all States.
// But why you do not just make this rule inside the code? Becaus this States that do not accept this year, can accept next year ¬¬
// Default is true
bool ConsideraSextaFeiraSanta; 

// Nullable Enum of Brazil States
// By default, the library will handle the holydays in all Brazil States, but you can set a default State with this variable. Doing this way, the library will handle just the holydays from that State
// Default is null
Estados? Estado;
```

### Enums

```csharp
// Months of the year
// Sample: LsFeriados.Enums.Meses.Janeiro for January
LsFeriados.Enums.Meses

// Brazil States
// Sample: LsFeriados.Enums.Estados.SP for São Paulo
LsFeriados.Enums.Estados
```

### Classes

```csharp
using System;
namespace LsFeriados {
    public class Feriado {
        // Holyday's name
        public string Nome { get; set; }

        // Holyday's date
        public DateTime Data { get; set; }

        // Use this to said the library if it has to throw a 
        // ArgumentNullException or just return false in the Validate function 
        // True by default
        public bool AcionarExcessaoCasoNaoSejaValido { get; set; } = true;

        // Used to validate the required props when you're including a new Holyday
        internal bool Validate() {
            if (!string.IsNullOrEmpty(Nome)) {
                if (AcionarExcessaoCasoNaoSejaValido)
                    throw new ArgumentNullException("O Nome do feriado é obrigatório"); // The name is required

                return false;
            } else if (Data != DateTime.MinValue) {
                if (AcionarExcessaoCasoNaoSejaValido)
                    throw new ArgumentNullException("A Data do feriado é obrigatória"); // The date is required

                return false;
            };

            return true;
        }
    }
}
```

### `DateTime` Extension Methods

```csharp
// Used to check if the date is a hallyday or not
// Return true or false
public static bool EhFeriado(this DateTime data);

// Used to check if the date is on weekend or not
// Return true or false
public static bool EhFimDeSemana(this DateTime data);

// Used to check if the date is a workday or not
// Return true or false
public static bool EhDiaUtil(this DateTime data);

// Used to get the next workday after the date
public static DateTime ProximoDiaUtil(this DateTime data)

// Used to get all the holydays of the year
public static Feriado[] TodosFeriadosDoAno(this DateTime data);

// Used to get all the holydays of the year, after the date
public static Feriado[] TodosProximosFeriadosDoAno(this DateTime data);

// Used to get the next holyday after the base date
public static Feriado ProximoFeriadoDoAno(this DateTime data);
```

### Methods

```csharp
// Add a new holyday to the list that is consider on validations
public static void AddFeriado(Feriado feriado);

// Add multiples holydays to the list that is consider on validations
public static void AddFeriado(Feriado[] feriados);

// Clear all the add holydays
public static void LimparFeriados();
```

### Contribute

Just open Pull Requests, open issues or just mail me: [leandro.simoes@outlook.com](mailto:leandro.simoes@outlook.com)
