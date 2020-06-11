using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Timers;

namespace CountDownTimer
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        DateTime DataSelecionada;
        TimeSpan HoraSelecionada;
        Timer t = new Timer();

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnIniciarContagem(object sender, EventArgs e)
        {
            etNomeDoEvento.IsEnabled = false;
            DataSelecionada = dtData.Date;
            HoraSelecionada = tmHora.Time;
            if (etNomeDoEvento.Text == null)
                etNomeDoEvento.PlaceholderColor = Color.Red;
            else if (DataSelecionada.AddHours(HoraSelecionada.Hours).AddMinutes(HoraSelecionada.Minutes) - DateTime.Now > TimeSpan.Zero)
            {
                t.Interval = 1000;
                t.Elapsed += IniciarContagemRegressiva;
                t.Start();
            }
            else
                DisplayAlert("Atenção", "A data está inválida", "OK");
        }

        private void IniciarContagemRegressiva(object sender, ElapsedEventArgs e)
        {
            TimeSpan time;
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                if (tmHora.IsVisible == false)
                    time = DataSelecionada - DateTime.Now;
                else
                    time = DataSelecionada.AddHours(HoraSelecionada.Hours).AddMinutes(HoraSelecionada.Minutes) - DateTime.Now;

                lbDia.Text = time.Days.ToString();
                lbHora.Text = time.Hours.ToString();
                lbMinuto.Text = time.Minutes.ToString();
                lbSegundos.Text = time.Seconds.ToString();

                if (time.Days == 0 && time.Hours == 0 && time.Minutes == 0 && time.Seconds == 0)
                    t.Stop();
            });
        }

        private void OnSelecionarHorario(object sender, ToggledEventArgs e)
        {
            if (((Switch)sender).IsToggled)
                tmHora.IsVisible = true;
            else
                tmHora.IsVisible = false;
        }
    }
}
