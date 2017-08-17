using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LsFeriados.Test {
    [TestClass]
    public class LsFeriadosTest {
        [TestMethod]
        public void Validar_Eh_Feriado() {
            var dataValidar1 = new DateTime(DateTime.Now.Year, (int)Enums.Meses.Setembro, 7);
            var dataValidar2 = new DateTime(DateTime.Now.Year, (int)Enums.Meses.Dezembro, 25);
            var dataValidar3 = new DateTime(DateTime.Now.Year, (int)Enums.Meses.Janeiro, 23); // Somente no AC

            LsFeriados.Estado = Enums.Estados.AC;

            Assert.IsTrue(dataValidar1.EhFeriado() && dataValidar2.EhFeriado() && dataValidar3.EhFeriado());
        }

        [TestMethod]
        public void Validar_Nao_Eh_Feriado() {
            var dataValidar1 = new DateTime(DateTime.Now.Year, (int)Enums.Meses.Setembro, 6);
            var dataValidar2 = new DateTime(DateTime.Now.Year, (int)Enums.Meses.Janeiro, 3);
            var dataValidar3 = new DateTime(DateTime.Now.Year, (int)Enums.Meses.Janeiro, 23); // Somente no AC

            Assert.IsFalse(dataValidar1.EhFeriado() && dataValidar2.EhFeriado() && dataValidar3.EhFeriado());
        }

        [TestMethod]
        public void Validar_Eh_Dia_Util() {
            var dataValidar1 = new DateTime(2017, (int)Enums.Meses.Agosto, 17);
            var dataValidar2 = new DateTime(2017, (int)Enums.Meses.Agosto, 18);
            var feriadoAC = new DateTime(2017, (int)Enums.Meses.Janeiro, 23); // Somente no AC

            Assert.IsTrue(dataValidar1.EhDiaUtil() && dataValidar2.EhDiaUtil() && feriadoAC.EhDiaUtil());
        }


        [TestMethod]
        public void Validar_Nao_Eh_Dia_Util() {
            var feriado = new DateTime(2017, (int)Enums.Meses.Setembro, 7);
            var sabado = new DateTime(2017, (int)Enums.Meses.Agosto, 19);
            var domingo = new DateTime(2017, (int)Enums.Meses.Agosto, 20);
            var feriadoAC = new DateTime(2017, (int)Enums.Meses.Janeiro, 23); // Somente no AC

            LsFeriados.Estado = Enums.Estados.AC;

            Assert.IsFalse(feriado.EhDiaUtil() && sabado.EhDiaUtil() && domingo.EhDiaUtil() && feriadoAC.EhDiaUtil());
        }


        [TestMethod]
        public void Validar_Proximo_Dia_Util() {
            var data = new DateTime(2017, (int)Enums.Meses.Agosto, 19);
            var proximo = new DateTime(2017, (int)Enums.Meses.Agosto, 21);
            Assert.IsTrue(proximo == data.ProximoDiaUtil());
        }


        [TestMethod]
        public void Validar_Proximo_Feriado() {
            var data = new DateTime(2017, (int)Enums.Meses.Setembro, 1);
            var proximo = new DateTime(2017, (int)Enums.Meses.Setembro, 7);
            Assert.IsTrue(proximo == data.ProximoFeriadoDoAno().Data);
        }
    }
}
