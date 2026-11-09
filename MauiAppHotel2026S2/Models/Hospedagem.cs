namespace MauiAppHotel2026S2.Models
{
    public class Hospedagem
    {
        public Quarto QuartoEscolhido {  get; set; }
        public int QntAdultos { get; set; }
        public int QntCriancas { get; set; }
        public DateTime DataCheckin {  get; set; }
        public DateTime DataCheckout { get; set; }

        public int Estadia
        {
            get => DataCheckout.Subtract(DataCheckin).Days;
        }

        public double TotalHospedagem
        {
            get
            {
                double total_adultos = QntAdultos * QuartoEscolhido.ValorAdultos;
                double total_criancas = QntCriancas * QuartoEscolhido.ValorCriancas;

                return (total_adultos + total_criancas) * Estadia;
            }
        }
    }
}
