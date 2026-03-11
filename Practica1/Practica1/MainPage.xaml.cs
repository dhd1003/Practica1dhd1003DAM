using TicTacToe;

namespace Practica1
{
    public partial class MainPage : ContentPage
    {
        private TicTacToe.TicTacToe juego = new TicTacToe.TicTacToe();
        private int victoriasLeon = 0;
        private int victoriasGrace = 0;

        private bool esTurnoDeLeon;
        private string jugadorQueEmpezo; // Guardamos quién inició esta ronda

        public MainPage()
        {
            InitializeComponent();
            ActualizarTextoTurno();
        }

        /// <summary>
        /// Determina aleatoriamente quién inicia la partida y actualiza la etiqueta 
        /// de texto con el nombre del jugador activo.
        /// </summary>
        private void ActualizarTextoTurno()
        {
            Random rnd = new Random();
            esTurnoDeLeon = rnd.Next(2) == 0;

            // Guardamos el nombre del que empieza para saber quién es el "Jugador 1" en esta partida
            jugadorQueEmpezo = esTurnoDeLeon ? "Leon" : "Grace";
            textoTurno.Text = jugadorQueEmpezo;
        }



        /// <summary>
        /// Manejador de evento que se dispara al pulsar una casilla del tablero. 
        /// Procesa la jugada, actualiza la imagen del botón y verifica si hay un ganador.
        /// </summary>
        /// <param name="sender">El objeto que disparó el evento (ImageButton).</param>
        /// <param name="e">Argumentos del evento.</param>
        private async void OnCellClicked(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;
            string posicion = button.CommandParameter.ToString();
            int fila = int.Parse(posicion[0].ToString());
            int columna = int.Parse(posicion[1].ToString());

            int resultadoJugada = juego.jugada(fila, columna);

            if (resultadoJugada != -1)
            {
                // El que tiene el turno actual pone su imagen
                button.Source = esTurnoDeLeon ? "img_x.png" : "img_o.png";
                button.IsEnabled = false;

                // Cambiamos el turno visual
                esTurnoDeLeon = !esTurnoDeLeon;
                textoTurno.Text = esTurnoDeLeon ? "Leon" : "Grace";

                int ganador = juego.Ganador();
                if (ganador != 0)
                {
                    await FinalizarPartida(ganador);
                }
                else if (resultadoJugada == 9)
                {
                    await FinalizarPartida(0);
                }
            }
        }



        /// <summary>
        /// Finaliza la ronda actual, identifica al ganador, actualiza el contador de victorias 
        /// y muestra una alerta con el resultado antes de reiniciar el tablero.
        /// </summary>
        /// <param name="ganador">Entero que representa el resultado: 0 para empate, 1 para el primer jugador, 2 para el segundo.</param>
        /// <returns>Una tarea asíncrona.</returns>
        private async Task FinalizarPartida(int ganador)
        {
            string nombreGanador = "";

            if (ganador == 1) // Ganó el que hizo el primer movimiento
            {
                nombreGanador = jugadorQueEmpezo;
            }
            else if (ganador == 2) // Ganó el que hizo el segundo movimiento
            {
                // Si empezó Leon, el segundo es Grace. Si empezó Grace, el segundo es Leon.
                nombreGanador = (jugadorQueEmpezo == "Leon") ? "Grace" : "Leon";
            }

            // Asignar puntos y preparar mensaje
            string mensaje;
            if (ganador == 0)
            {
                mensaje = "¡Empate!";
            }
            else
            {
                mensaje = $"¡Ganó {nombreGanador}!";
                if (nombreGanador == "Leon")
                {
                    victoriasLeon++;
                    textoVictoriaLeon.Text = victoriasLeon.ToString();
                }
                else
                {
                    victoriasGrace++;
                    textoVictoriaGrace.Text = victoriasGrace.ToString();
                }
            }

            await DisplayAlert("Fin del juego", mensaje, "Aceptar");
            ReiniciarTableroVisual();
        }



        /// <summary>
        /// Restablece el estado lógico del juego y limpia la interfaz gráfica (imágenes y habilitación de botones) 
        /// para comenzar una nueva partida.
        /// </summary>
        private void ReiniciarTableroVisual()
        {
            juego.Reiniciar();
            ActualizarTextoTurno(); // Sorteo nuevo y actualiza jugadorQueEmpezo

            if (TableroGrid != null)
            {
                foreach (var view in TableroGrid.Children)
                {
                    if (view is ImageButton button)
                    {
                        button.Source = null;
                        button.IsEnabled = true;
                    }
                }
            }
        }
    }
}