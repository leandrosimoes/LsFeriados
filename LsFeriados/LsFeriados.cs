using System;
using System.Linq;
using LsFeriados.Enums;
using LsFeriados.ClassesInternas;

namespace LsFeriados {
    public static class LsFeriados {
        /// <summary>
        /// Estado que deve ter seus feriados próprios considerados
        /// </summary>
        public static Estados? Estado { get; set; }

        /// <summary>
        /// Alguns estados não consideram a Sexta Feira Santa como um feriado, sendo assim, pode-se escolher se deve ou não ser considerada na validação
        /// Default: true
        /// </summary>
        public static bool ConsideraSextaFeiraSanta { get; set; } = true;

        /// <summary>
        /// Adiciona um feriado avulso para ser considerado na validação
        /// </summary>
        /// <param name="feriado">Feriado a ser adicionado</param>
        public static void AddFeriado(Feriado feriado) {
            var listaFeriados = new ListaFeriados(ConsideraSextaFeiraSanta);
            listaFeriados.AddFeriadoAvulso(feriado);
        }


        /// <summary>
        /// Adiciona feriados avulsos para serem considerados na validação
        /// </summary>
        /// <param name="feriados">Array de Feriado a ser adicionado</param>
        public static void AddFeriado(Feriado[] feriados) {
            var listaFeriados = new ListaFeriados(ConsideraSextaFeiraSanta);
            
            foreach (var feriado in feriados) {
                listaFeriados.AddFeriadoAvulso(feriado);
            }
        }

        /// <summary>
        /// Limpa todos os feriados avulsos que foram adicionados manualmente
        /// </summary>
        public static void LimparFeriados() {
            var listaFeriados = new ListaFeriados(ConsideraSextaFeiraSanta);
            listaFeriados.FeriadosMunicipaisEOutros.Clear();
        }

        /// <summary>
        /// Verifica se a data em questão é considerada um feriado
        /// </summary>
        /// <param name="data">Data que será validada</param>
        /// <returns></returns>
        public static bool EhFeriado(this DateTime data) {
            var listaFeriados = new ListaFeriados(ConsideraSextaFeiraSanta);

            return listaFeriados.FeriadosEleicoes.Any(f => f.Data == data && f.Data != DateTime.MinValue)
                || listaFeriados.FeriadosMunicipaisEOutros.Any(f => f.Data == data)
                || listaFeriados.FeriadosNacionaisFixos.Any(f => f.Data == data)
                || listaFeriados.FeriadosNacionaisNaoFixos().Any(f => f.Data == data)
                || (Estado.HasValue && listaFeriados.FeriadosEstaduais[Estado.GetValueOrDefault()].Any(f => f.Data == data));
        }


        /// <summary>
        /// Verifica se a data em questão é considerada um dia do fim de semana
        /// </summary>
        /// <param name="data">Data que será validada</param>
        /// <returns></returns>
        public static bool EhFimDeSemana(this DateTime data) {
            return data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday;
        }


        /// <summary>
        /// Verifica se a data em questão é considerada um dia útil
        /// </summary>
        /// <param name="data">Data que será validada</param>
        /// <returns></returns>
        public static bool EhDiaUtil(this DateTime data) {
            return !EhFeriado(data) && !EhFimDeSemana(data);
        }


        /// <summary>
        /// Obtem o próximo dia útil a partir da data informada.
        /// Caso a data informada já seja um dia útil, retorna a mesma
        /// </summary>
        /// <param name="data">Data que será validada</param>
        /// <returns></returns>
        public static DateTime ProximoDiaUtil(this DateTime data) {
            while (!EhDiaUtil(data)) {
                data = data.AddDays(1);
            }
            return data;
        }

        /// <summary>
        /// Obtem todos os feriados do ano atual
        /// </summary>
        /// <param name="data">Data que será usada com referência</param>
        /// <returns>Retorna uma classe do tipo Feriado</returns>
        public static Feriado[] TodosFeriadosDoAno(this DateTime data) {
            var listaFeriados = new ListaFeriados(ConsideraSextaFeiraSanta);
            return listaFeriados.ObterTodosFeriados(Estado).ToArray();
        }

        /// <summary>
        /// Obter próximos feriados baseado na data informada
        /// </summary>
        /// <param name="data">Data que será usada com referência</param>
        /// <returns>Retorna um array de classes do tipo Feriado</returns>
        public static Feriado[] TodosProximosFeriadosDoAno(this DateTime data) {
            var listaFeriados = new ListaFeriados(ConsideraSextaFeiraSanta);
            return listaFeriados.ObterTodosFeriados(Estado).Where(f => f.Data >= data).ToArray();
        }

        /// <summary>
        /// Obtem o próximo feriado baseado na data informada
        /// </summary>
        /// <param name="data">Data que será usada com referência</param>
        /// <returns>Retorna uma classe do tipo Feriado</returns>
        public static Feriado ProximoFeriadoDoAno(this DateTime data) {
            return data.TodosProximosFeriadosDoAno().FirstOrDefault(f => f.Data >= data);
        }
    }
}
