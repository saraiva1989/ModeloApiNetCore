using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModeloNetCoreBiblioteca.ClassesAuxiliar;

namespace ModeloNetCoreTeste.Auxiliar
{
    [TestClass]
    public class CriptografiaTeste
    {
        [TestMethod]
        [DataRow("Teste", "NVigjXTR9PJjwel4nGD0ww==")]
        [DataRow("Saraiva", "S59s/diSbtvpjP36PhRLWw==")]
        [DataRow("Ocelot", "cDoh94A3McoBKMpj90LUaQ==")]
        public void EncryptStringTeste(string texto, string valorEsperado)
        {
            string textoCriptografado = Criptografia.EncryptString(texto);
            Assert.AreEqual(valorEsperado, textoCriptografado);
        }

        [TestMethod]
        [DataRow("NVigjXTR9PJjwel4nGD0ww==", "Teste")]
        [DataRow("S59s/diSbtvpjP36PhRLWw==", "Saraiva")]
        [DataRow("cDoh94A3McoBKMpj90LUaQ==", "Ocelot")]
        public void DecryptStringTeste(string texto, string valorEsperado)
        {
            string textoCriptografado = Criptografia.DecryptString(texto);
            Assert.AreEqual(valorEsperado, textoCriptografado);
        }

        [TestMethod]
        [DataRow("Teste", "89F308210C7C7820BAD0974F31E751BFA433D2066A93E808947C3188DEDBA6E3")]
        [DataRow("Saraiva", "368AAB0D8E5CE9637E0066378D9631459F60700906CFADB2813E78EFA1A30F51")]
        [DataRow("Ocelot", "820AE1FAF7D052E75D7FA22D0FC3EBE7321081D6559C775C43051573EEBEBA7F")]
        public void StringSha256HashTeste(string texto, string valorEsperado)
        {
            string textoCriptografado = Criptografia.StringSha256Hash(texto);
            Assert.AreEqual(valorEsperado, textoCriptografado);
        }
    }
}
