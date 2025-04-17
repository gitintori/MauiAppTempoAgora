namespace MauiAppTempoAgora.Models
{
    public class Tempo
    {
        public double? lon { get; set; } // Longitude
        public double? lat { get; set; } // Latitude
        public double? temp { get; set; } // Temperatura
        public int? visibility { get; set; } // Visibilidade
        public double? speed { get; set; } // Velocidade do vento
        public string? sunrise { get; set; } // Nascer do sol // String para converter para DateTime (porque estava em segundos desde janeiro de 1970)
        public string? sunset { get; set; } // Pôr do sol // String para converter para DateTime (porque estava em segundos desde janeiro de 1970)
        public string? main {  get; set; } // Condição do tempo
        public string? description { get; set; } // Descrição do tempo
    }
}
