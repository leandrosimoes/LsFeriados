using System;
namespace LsFeriados {
    public class Feriado {
        public string Nome { get; set; }
        public DateTime Data { get; set; }

        internal bool Validate() {
            if (!string.IsNullOrEmpty(Nome)) {
                throw new Exception("O Nome do feriado é obrigatório");
            } else if (Data != DateTime.MinValue) {
                throw new Exception("A Data do feriado é obrigatória");
            };

            return true;
        }
    }
}
