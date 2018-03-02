
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
