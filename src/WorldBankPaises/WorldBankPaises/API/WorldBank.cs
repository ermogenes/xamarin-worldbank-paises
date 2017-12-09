using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Net.Http;
using WorldBankPaises.Model;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace WorldBankPaises.API
{
    public class WorldBank
    {
        private string uriAPIPais = "http://api.worldbank.org/countries/";
        private string metodoAPIPaisGdp = "/indicators/NY.GDP.PCAP.CD?date=2000:2016&format=json";
        private string metodoAPIPaisTax = "/indicators/GC.TAX.TOTL.GD.ZS?date=2000:2016&format=json";
        private string metodoAPIPaises = "?per_page=500&format=json";

        private HttpClient http = new HttpClient();
        private CultureInfo culturaAPI = CultureInfo.InvariantCulture;
        private NumberStyles estiloNumeroAPI = NumberStyles.Number | NumberStyles.AllowDecimalPoint;

        public async Task<ObservableCollection<Pais>> ObtemListaPaises()
        {

            var paises = new ObservableCollection<Pais>();

            try
            {
                string result = await http.GetStringAsync(uriAPIPais + metodoAPIPaises);

                JArray listaPaises = JArray.Parse(result);

                foreach (var paisAPI in listaPaises[1])
                {

                    string Id;
                    string Nome;
                    string Regiao;
                    string Capital;
                    double Latitude;
                    double Longitude;

                    try
                    {
                        Regiao = paisAPI.Value<JObject>("region").Value<string>("value");

                        if (Regiao == "Aggregates") continue;

                        Id = paisAPI.Value<string>("id");
                        Nome = paisAPI.Value<string>("name");
                        
                        Capital = paisAPI.Value<string>("capitalCity");
                        Double.TryParse(paisAPI.Value<string>("latitude"), estiloNumeroAPI, culturaAPI, out Latitude);
                        Double.TryParse(paisAPI.Value<string>("longitude"), estiloNumeroAPI, culturaAPI, out Longitude);
                    }
                    catch
                    {
                        continue;
                    }

                    paises.Add(new Pais {Id = Id, Nome = Nome, Regiao = Regiao, Capital = Capital, Latitude = Latitude,Longitude = Longitude});
                }
            }
            catch (Exception ex)
            {
                // segue sem fazer nada
            }

            return paises;
        }

        public async Task<Pais> ComplementaDetalhePais(Pais pais){
            if (!pais.Complementado)
            {
                try
                {
                    await ComplementaImpostosPercentual(pais);
                    await ComplementaGDPPerCapita(pais);
                    pais.Complementado = true;
                }
                catch (Exception ex)
                {
                    // continua
                }
                return pais;
            }
            return null;
        }

        private async Task ComplementaImpostosPercentual(Pais pais)
        {
            try
            {
                string result = await http.GetStringAsync(uriAPIPais + pais.Id + metodoAPIPaisTax);

                JArray data = JArray.Parse(result);

                foreach (var ano in data[1])
                {
                    string valor = ano.Value<string>("value");
                    double dblValor = 0;
                    if (Double.TryParse(valor, estiloNumeroAPI, culturaAPI, out dblValor))
                    {
                        pais.ImpostosPercentual = dblValor;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // segue sem fazer nada
            }
        }

        private async Task ComplementaGDPPerCapita(Pais pais)
        {
            try
            {
                string result = await http.GetStringAsync(uriAPIPais + pais.Id + metodoAPIPaisGdp);

                JArray data = JArray.Parse(result);

                foreach (var ano in data[1])
                {
                    string valor = ano.Value<string>("value");
                    double dblValor = 0;
                    if (Double.TryParse(valor, estiloNumeroAPI, culturaAPI, out dblValor))
                    {
                        pais.GDPPerCapita = dblValor;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // segue sem fazer nada
            }
        }
    }
}
