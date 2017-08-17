using LsFeriados.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LsFeriados.ClassesInternas {
    internal class ListaFeriados {
        public ListaFeriados(bool consideraSextaFeiraSanta) {
            ConsideraSextaFeiraSanta = consideraSextaFeiraSanta;
        }

        #region VariaveisEMetodosPublicos
        public void AddFeriadoAvulso(Feriado feriado) {
            if(feriado.Validate() && FeriadosMunicipaisEOutros.Any(f => f.Data == feriado.Data)) {
                FeriadosMunicipaisEOutros.Add(feriado);
            }
        }

        public List<Feriado> ObterTodosFeriados(Estados? estado) {
            var retorno = new List<Feriado>();

            retorno.AddRange(FeriadosEleicoes);

            if (estado.HasValue) {
                retorno.AddRange(FeriadosEstaduais[estado.GetValueOrDefault()]);
            }

            retorno.AddRange(FeriadosMunicipaisEOutros);
            retorno.AddRange(FeriadosNacionaisFixos);
            retorno.AddRange(FeriadosNacionaisNaoFixos());

            return retorno.OrderBy(r => r.Data).ToList();
        }

        public void AlterarDatasElecoes(DateTime primeiroTurno, DateTime? segundoTurno) {
            FeriadosEleicoes[0].Data = primeiroTurno;
            FeriadosEleicoes[1].Data = segundoTurno.GetValueOrDefault();
        }
        #endregion

        #region VariaveisEMetodosPrivados
        private static bool ConsideraSextaFeiraSanta { get; set; }

        private static DateTime DataAtual { get; set; } = DateTime.Now;
        #endregion

        #region VariaveisEMetodosInternos
        internal List<Feriado> FeriadosMunicipaisEOutros = new List<Feriado>();
        internal List<Feriado> FeriadosEleicoes = new List<Feriado> {
            new Feriado { Nome = "Primeiro Turno", Data = new DateTime(DataAtual.Year, (int)Meses.Outubro, 3) },
            new Feriado { Nome = "Segundo Turno", Data = new DateTime(DataAtual.Year, (int)Meses.Novembro, 15) }
        };

        internal List<Feriado> FeriadosNacionaisFixos = new List<Feriado> {
            new Feriado { Nome = "Ano Novo", Data = new DateTime(DataAtual.Year, (int)Meses.Janeiro, 1) },
            new Feriado { Nome = "Dia de Tiradentes", Data = new DateTime(DataAtual.Year, (int)Meses.Abril, 21) },
            new Feriado { Nome = "Dia do Trabalho", Data = new DateTime(DataAtual.Year, (int)Meses.Maio, 1) },
            new Feriado { Nome = "Dia da Independência do Brasil", Data = new DateTime(DataAtual.Year, (int)Meses.Setembro, 7) },
            new Feriado { Nome = "Dia de Nossa Sra. Aparecida", Data = new DateTime(DataAtual.Year, (int)Meses.Outubro, 12) },
            new Feriado { Nome = "Dia de Finados", Data = new DateTime(DataAtual.Year, (int)Meses.Novembro, 2) },
            new Feriado { Nome = "Dia da Proclamação da República", Data = new DateTime(DataAtual.Year, (int)Meses.Novembro, 15) },
            new Feriado { Nome = "Natal", Data = new DateTime(DataAtual.Year, (int)Meses.Dezembro, 25) }
        };

        internal List<Feriado> FeriadosNacionaisNaoFixos() {
            var retorno = new List<Feriado>();

            var pascoa = CalcularDataPascoa();
            var carnaval = pascoa.AddDays(-47);
            var corpusChristi = pascoa.AddDays(60);

            retorno.Add(new Feriado { Nome = "Páscoa", Data = pascoa });
            retorno.Add(new Feriado { Nome = "Carnaval", Data = carnaval });
            retorno.Add(new Feriado { Nome = "Corpus Christi", Data = corpusChristi });

            if (ConsideraSextaFeiraSanta) {
                var sextaFeiraSanta = CalcularSextaFeiraSanta(pascoa);
                retorno.Add(new Feriado { Nome = "Sexta Feira Santa", Data = sextaFeiraSanta });
            }

            return retorno;
        }

        internal Dictionary<Estados, List<Feriado>> FeriadosEstaduais = new Dictionary<Estados, List<Feriado>> {
            {
                Estados.AC,
                new List<Feriado> {
                    new Feriado { Nome = "Dia dos Evangélicos", Data = new DateTime(DataAtual.Year, (int)Meses.Janeiro, 23) },
                    new Feriado { Nome = "Dia das Mulheres", Data = new DateTime(DataAtual.Year, (int)Meses.Marco, 8) },
                    new Feriado { Nome = "Dia do Aniversário do Estado", Data = new DateTime(DataAtual.Year, (int)Meses.Junho, 15) },
                    new Feriado { Nome = "Dia da Amazônia", Data = new DateTime(DataAtual.Year, (int)Meses.Setembro, 5) },
                    new Feriado { Nome = "Dia da Assinatura do Tratado de Petrópolis", Data = new DateTime(DataAtual.Year, (int)Meses.Novembro, 17) }
                }
            },
            {
                Estados.AL,
                new List<Feriado> {
                    new Feriado { Nome = "Dia de São João", Data = new DateTime(DataAtual.Year, (int)Meses.Junho, 24) },
                    new Feriado { Nome = "Dia de São Pedro", Data = new DateTime(DataAtual.Year, (int)Meses.Junho, 29) },
                    new Feriado { Nome = "Dia de Emancipação Política", Data = new DateTime(DataAtual.Year, (int)Meses.Setembro, 16) },
                    new Feriado { Nome = "Dia da Morte de Zumbi dos Palmares", Data = new DateTime(DataAtual.Year, (int)Meses.Novembro, 20) }
                }
            },
            {
                Estados.AP,
                new List<Feriado> {
                    new Feriado { Nome = "Dia de São José", Data = new DateTime(DataAtual.Year, (int)Meses.Marco, 19) },
                    new Feriado { Nome = "Criação do Território Federal", Data = new DateTime(DataAtual.Year, (int)Meses.Setembro, 13) }
                }
            },
            {
                Estados.AM,
                new List<Feriado> {
                    new Feriado { Nome = "Dia da Elevação do Amazonas à Categoria de Província", Data = new DateTime(DataAtual.Year, (int)Meses.Setembro, 5) },
                    new Feriado { Nome = "Dia da Consciência Negra", Data = new DateTime(DataAtual.Year, (int)Meses.Novembro, 20) }
                }
            },
            {
                Estados.BA,
                new List<Feriado> {
                    new Feriado { Nome = "Dia da Independência da Bahia", Data = new DateTime(DataAtual.Year, (int)Meses.Julho, 2) }
                }
            },
            {
                Estados.CE,
                new List<Feriado> {
                    new Feriado { Nome = "Dia da Abolição da Escravatura", Data = new DateTime(DataAtual.Year, (int)Meses.Marco, 25) }
                }
            },
            {
                Estados.DF,
                new List<Feriado> {
                    new Feriado { Nome = "", Data = new DateTime(DataAtual.Year, (int)Meses.Abril, 21) },
                    new Feriado { Nome = "", Data = new DateTime(DataAtual.Year, (int)Meses.Novembro, 30) }
                }
            },
            {
                Estados.ES,
                new List<Feriado> ()
            },
            {
                Estados.GO,
                new List<Feriado>()
            },
            {
                Estados.MA,
                new List<Feriado> {
                    new Feriado { Nome = "Dia da Adesão do Maranhão à Independência do Brasil", Data = new DateTime(DataAtual.Year, (int)Meses.Julho, 28) }
                }
            },
            {
                Estados.MT,
                new List<Feriado> {
                    new Feriado { Nome = "Dia da Consciência Negra", Data = new DateTime(DataAtual.Year, (int)Meses.Novembro, 20) }
                }
            },
            {
                Estados.MS,
                new List<Feriado> {
                    new Feriado { Nome = "Dia do Aniversário do Estado", Data = new DateTime(DataAtual.Year, (int)Meses.Outubro, 11) }
                }
            },
            {
                Estados.MG,
                new List<Feriado> {
                    new Feriado { Nome = "Dia de Magna do Estado", Data = new DateTime(DataAtual.Year, (int)Meses.Abril, 21) }
                }
            },
            {
                Estados.PA,
                new List<Feriado> {
                    new Feriado { Nome = "Dia da Adesão do Grão-Para à Independência do Brasil", Data = new DateTime(DataAtual.Year, (int)Meses.Agosto, 15) }
                }
            },
            {
                Estados.PB,
                new List<Feriado> {
                    new Feriado { Nome = "Dia de Homenagem à Memória do Ex-Presidente João Pessoa", Data = new DateTime(DataAtual.Year, (int)Meses.Julho, 26) },
                    new Feriado { Nome = "Dia do Aniversário do Estado", Data = new DateTime(DataAtual.Year, (int)Meses.Agosto, 5) }
                }
            },
            {
                Estados.PR,
                new List<Feriado> {
                    new Feriado { Nome = "Dia da Emancipação Política", Data = new DateTime(DataAtual.Year, (int)Meses.Dezembro, 19) }
                }
            },
            {
                Estados.PE,
                new List<Feriado>()
            },
            {
                Estados.PI,
                new List<Feriado> {
                    new Feriado { Nome = "Dia do Aniversário do Estado", Data = new DateTime(DataAtual.Year, (int)Meses.Outubro, 19) }
                }
            },
            {
                Estados.RJ,
                new List<Feriado> {
                    new Feriado { Nome = "Dia de São Jorge", Data = new DateTime(DataAtual.Year, (int)Meses.Abril, 23) },
                    new Feriado { Nome = "Dia da Consciência Negra", Data = new DateTime(DataAtual.Year, (int)Meses.Novembro, 20) }
                }
            },
            {
                Estados.RN,
                new List<Feriado> {
                    new Feriado { Nome = "Dia da Proclamação da República do Estado", Data = new DateTime(DataAtual.Year, (int)Meses.Outubro, 5) }
                }
            },
            {
                Estados.RO,
                new List<Feriado> {
                    new Feriado { Nome = "Dia do Aniversário do Estado", Data = new DateTime(DataAtual.Year, (int)Meses.Janeiro, 4) },
                    new Feriado { Nome = "Dia do Evangélico", Data = new DateTime(DataAtual.Year, (int)Meses.Junho, 18) }
                }
            },
            {
                Estados.RR,
                new List<Feriado> {
                    new Feriado { Nome = "Dia do Aniversário do Estado", Data = new DateTime(DataAtual.Year, (int)Meses.Outubro, 5) }
                }
            },
            {
                Estados.SC,
                new List<Feriado> {
                    new Feriado { Nome = "Dia do Aniversário do Estado", Data = new DateTime(DataAtual.Year, (int)Meses.Agosto, 11) },
                    new Feriado { Nome = "Dia de Santa Catarina de Alexandria", Data = new DateTime(DataAtual.Year, (int)Meses.Novembro, 25) }
                }
            },
            {
                Estados.SP,
                new List<Feriado> {
                    new Feriado { Nome = "Dia da Revolução Constitucionalista de 1932", Data = new DateTime(DataAtual.Year, (int)Meses.Julho, 9) }
                }
            },
            {
                Estados.SE,
                new List<Feriado> {
                    new Feriado { Nome = "Dia da Emancipação Política do Estado", Data = new DateTime(DataAtual.Year, (int)Meses.Julho, 8) }
                }
            },
            {
                Estados.TO,
                new List<Feriado> {
                    new Feriado { Nome = "Dia do Aniversário do Estado", Data = new DateTime(DataAtual.Year, (int)Meses.Outubro, 5) },
                    new Feriado { Nome = "Dia da Emancipação Política do Estado", Data = new DateTime(DataAtual.Year, (int)Meses.Marco, 18) },
                    new Feriado { Nome = "Dia de Nossa Sra. da Natividade", Data = new DateTime(DataAtual.Year, (int)Meses.Setembro, 8) }
                }
            }
        };

        internal DateTime CalcularSextaFeiraSanta(DateTime dataPascoa) {
            var dia = dataPascoa.Day;

            while (dataPascoa.DayOfWeek != DayOfWeek.Friday) {
                dataPascoa = dataPascoa.AddDays(-1);
            }

            return dataPascoa;
        }

        internal DateTime CalcularDataPascoa() {
            var anoAtual = DataAtual.Year;
            var x = 0;
            var y = 0;

            if (anoAtual >= 1582 && anoAtual <= 1599) {
                x = 22; y = 2;
            } else if (anoAtual >= 1600 && anoAtual <= 1699) {
                x = 22; y = 2;
            } else if (anoAtual >= 1700 && anoAtual <= 1799) {
                x = 23; y = 3;
            } else if (anoAtual >= 1800 && anoAtual <= 1899) {
                x = 24; y = 4;
            } else if (anoAtual >= 1900 && anoAtual <= 2019) {
                x = 24; y = 5;
            } else if (anoAtual >= 2020 && anoAtual <= 2099) {
                x = 24; y = 5;
            } else if (anoAtual >= 2100 && anoAtual <= 2199) {
                x = 24; y = 6;
            } else if (anoAtual >= 2200 && anoAtual <= 2299) {
                x = 25; y = 7;
            }

            var a = anoAtual % 19;
            var b = anoAtual % 4;
            var c = anoAtual % 7;
            var d = (19 * a + x) % 30;
            var e = (2 * b + 4 * c + 6 * d + y) % 7;

            var dataPascoa = DataAtual;
            if ((d + e) > 9) {
                dataPascoa = new DateTime(anoAtual, (int)Meses.Abril, (d + e - 9));
            } else {
                dataPascoa = new DateTime(anoAtual, (int)Meses.Marco, (d + e + 22));
            }

            if (dataPascoa.Month == (int)Meses.Abril) {
                if (dataPascoa.Day == 26) {
                    dataPascoa = new DateTime(dataPascoa.Year, dataPascoa.Month, 19);
                }

                if (dataPascoa.Day == 25 && d == 28 && a > 10) {
                    dataPascoa = new DateTime(dataPascoa.Year, dataPascoa.Month, 18);
                }
            }

            return dataPascoa;
        }
        #endregion
    }
}
