using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Buscar_Previsao_Clicked(object sender, EventArgs e) // Evento de clique do botão "Buscar"
            // Precisa ser void por causa do botao
        {
            try
            {
                string cidade = txt_cidade.Text; // Obtém o texto da caixa de entrada
                if (!string.IsNullOrEmpty(cidade)) // Verifica se a caixa de entrada está vazia
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text); // Chama o serviço para obter a previsão do tempo

                    if (t != null) // Verifica se o objeto Tempo não é nulo
                    {
                        string dados_previsao = ""; // Inicializa a string para armazenar os dados da previsão

                        dados_previsao = $"Latitude: {t.lat}\n" + // Obtém a latitude
                                        $"Longitude: {t.lon}\n" + // Obtém a longitude
                                        $"Descrição: {t.description}\n" + // Obtém a descrição do tempo
                                        $"Temperatura mínima: {t.temp_min}°C\n" + // Obtém a temperatura mínima
                                        $"Temperatura máxima: {t.temp_max}°C\n" + // Obtém a temperatura máxima
                                        $"Visibilidade: {t.visibility} m\n" + // Obtém a visibilidade
                                        $"Velocidade do vento: {t.speed} m/s\n" + // Obtém a velocidade do vento
                                        $"Nascer do sol: {t.sunrise}\n" + // Obtém o horário do nascer do sol
                                        $"Pôr do sol: {t.sunset}"; // Obtém o horário do pôr do sol


                        lbl_res.Text = dados_previsao; // Exibe os dados da previsão no rótulo

                        // mapa do windy usando a url do iframe
                        string mapa = $"https://embed.windy.com/embed.html?type=map&location=coordinates&metricRain=mm&metricTemp=°C&metricWind=km/h&zoom=5&overlay=wind&product=ecmwf&level=surface&lat={t.lat.ToString().Replace(",", ".")}&lon={t.lon.ToString().Replace(",", ".")}";
                        wv_mapa.Source = mapa;

                    }
                    else //Verifica se o objeto Tempo é nulo
                    {
                        lbl_res.Text = "Sem dados de previsão.";
                    }
                }
                else // Verifica se a caixa de entrada não está vazia
                {
                    lbl_res.Text = "Preencha a cidade."; // Exibe uma mensagem padrão
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK"); // Exibe um alerta em caso de erro
            } // Fecha try-catch
        } // Fecha método Buscar_Previsao_Clicked

        private async void Minha_Localizacao_Clicked(object sender, EventArgs e) // Evento de clique do botão "Buscar"
                                                                                 // Precisa ser void por causa do botao
        {
            try
            {
                GeolocationRequest request = new GeolocationRequest(
                        GeolocationAccuracy.Medium, 
                        TimeSpan.FromSeconds(10)
                        ); // Cria uma solicitação de geolocalização

                Location? local = await Geolocation.Default.GetLocationAsync(request); // Obtém a localização atual // ? -> null se não conseguir obter a localização
                
                if(local != null)
                {
                    string local_disp = $"Latitude: {local.Latitude}\n" + // Obtém a latitude
                                        $"Longitude: {local.Longitude}\n"; // Obtém a longitude
                    lbl_coords.Text = local_disp; // Exibe as coordenadas no rótulo

                    GetCidade(local.Latitude, local.Longitude); // Chama o método para obter a cidade
                }
                else
                {
                    lbl_coords.Text = "Nenhuma localização encontrada.";
                }
            }
            catch (FeatureNotSupportedException fnsEx) // fns -> funcionalidade nao suporta
            {
                await DisplayAlert("Erro: Dispositivo não suporta", fnsEx.Message, "OK");
            }
            catch (FeatureNotEnabledException fnhEx) //fnh -> funcionalidade não habilitada
            {
                await DisplayAlert("Erro: Localização não habilitada", fnhEx.Message, "OK");
            }
            catch (PermissionException pEx) // pEx -> permissao
            {
                await DisplayAlert("Erro: Permissão não concedida", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            } // Fecha try-catch
        }

        private async void GetCidade(double lat, double lon)
        {
            try
            {
                IEnumerable<Placemark> places = await Geocoding.Default.GetPlacemarksAsync(lat, lon); // Obtém os marcadores de localização

                Placemark? place = places.FirstOrDefault(); // Obtém o primeiro marcador ou nulo se não houver

                if (place != null)
                {
                    txt_cidade.Text = place.Locality; // Exibe a localidade no campo de texto
                }
            }
            catch (Exception ex) // Exceção genérica
            {
                await DisplayAlert("Erro: obtenção do nome da cidade", ex.Message, "OK"); // Exibe um alerta em caso de erro
            }
        }
    }

} // Fecha classe MainPage
