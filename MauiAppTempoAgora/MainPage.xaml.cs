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

        private async void Buscar_Clicked(object sender, EventArgs e) // Evento de clique do botão "Buscar"
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
        } // Fecha método Buscar_Clicked
    }

} // Fecha classe MainPage
