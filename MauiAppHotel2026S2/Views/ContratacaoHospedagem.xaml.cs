
using MauiAppHotel2026S2.Models;

namespace MauiAppHotel2026S2.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    private int quantidadeAdultos = 1;
    private int quantidadeCriancas = 0;

    private readonly List<Quarto> lista_quartos = new()
    {
        new Quarto
        {
            Nome = "Suíte Master",
            ValorAdultos = 225.50,
            ValorCriancas = 200.30
        },
        new Quarto
        {
            Nome = "Suíte Premium",
            ValorAdultos = 180.30,
            ValorCriancas = 110.30
        },
        new Quarto
        {
            Nome = "Suíte Viajante",
            ValorAdultos = 140.00,
            ValorCriancas = 75.00
        },
        new Quarto
        {
            Nome = "Suíte Econômica",
            ValorAdultos = 100.30,
            ValorCriancas = 50.00
        }
    };

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        pck_quarto.ItemsSource = lista_quartos;

        lbl_adultos.Text = quantidadeAdultos.ToString();
        lbl_criancas.Text = quantidadeCriancas.ToString();

        dtpck_checkin.MinimumDate = DateTime.Today;
        dtpck_checkin.MaximumDate = DateTime.Today.AddMonths(3);
        dtpck_checkin.Date = DateTime.Today;

        dtpck_checkout.MinimumDate = DateTime.Today.AddDays(1);
        dtpck_checkout.MaximumDate = DateTime.Today.AddMonths(4);
        dtpck_checkout.Date = DateTime.Today.AddDays(1);
    }

    // ADULTOS

    private void AumentarAdultos_Clicked(object sender, EventArgs e)
    {
        if (quantidadeAdultos < 5)
        {
            quantidadeAdultos++;
            lbl_adultos.Text = quantidadeAdultos.ToString();
        }
    }

    private void DiminuirAdultos_Clicked(object sender, EventArgs e)
    {
        if (quantidadeAdultos > 1)
        {
            quantidadeAdultos--;
            lbl_adultos.Text = quantidadeAdultos.ToString();
        }
    }

    // CRIANCAS

    private void AumentarCriancas_Clicked(object sender, EventArgs e)
    {
        if (quantidadeCriancas < 5)
        {
            quantidadeCriancas++;
            lbl_criancas.Text = quantidadeCriancas.ToString();
        }
    }

    private void DiminuirCriancas_Clicked(object sender, EventArgs e)
    {
        if (quantidadeCriancas > 0)
        {
            quantidadeCriancas--;
            lbl_criancas.Text = quantidadeCriancas.ToString();
        }
    }

    // DATAS

    private void dtpck_checkin_DateSelected(
        object sender, DateChangedEventArgs e)
    {
        DateTime novaData = e.NewDate ?? DateTime.Today;

        DateTime menorCheckout = novaData.AddDays(1);

        dtpck_checkout.MinimumDate = menorCheckout;

        if (dtpck_checkout.Date == null ||
            dtpck_checkout.Date < menorCheckout)
        {
            dtpck_checkout.Date = menorCheckout;
        }
    }

    // CONFIRMAR RESERVA

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (pck_quarto.SelectedItem is not Quarto quarto)
        {
            await DisplayAlert(
                "Atenção",
                "Selecione uma acomodação para continuar.",
                "OK");

            return;
        }

        DateTime checkin =
            dtpck_checkin.Date ?? DateTime.Today;

        DateTime checkout =
            dtpck_checkout.Date ?? checkin.AddDays(1);

        if (checkout <= checkin)
        {
            await DisplayAlert(
                "Atenção",
                "A data de check-out deve ser posterior ao check-in.",
                "OK");

            return;
        }

        var hospedagem = new Hospedagem
        {
            QntAdultos = quantidadeAdultos,
            QntCriancas = quantidadeCriancas,
            QuartoEscolhido = quarto,
            DataCheckin = checkin,
            DataCheckout = checkout
        };

        await Navigation.PushAsync(
            new HospedagemContratada
            {
                BindingContext = hospedagem
            });
    }
}
