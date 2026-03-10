namespace TicTacToe;
/// <summary>
/// Funcionalidad del juego
/// </summary>
public class TicTacToe
{
    private int turno ;
    public int[,] tablero; 
    
/// <summary>
/// Constructor que inicializa el tablero vacío y el turno a 0
/// </summary>
    public TicTacToe()
    {
        Reiniciar();
    }
/// <summary>
/// Realiza jugadas
/// </summary>
/// <param name="c">Columna de la jugada</param>
/// <param name="f">Fila de la jugada</param>
/// <returns>Número de turno o -1 si la jugada es invalida</returns>
    public int jugada(int c, int f)
    {
        if (tablero[c, f] == 0)
        {
            if (turno % 2 == 0)
            {
                tablero[c, f] = 1;
            }
            else
            {
                tablero[c, f] = 2;
            }

            turno += 1;
        }
        else
        {
            return -1;
        }

        return turno;
    }
/// <summary>
/// Determina que jugador ha ganado si es que algún jugador ha ganado
/// </summary>
/// <returns>Devuelve el jugador ganador 1 o 2 y 0 si no hay ganador</returns>
    public int Ganador()
    {
        if (turno % 2 == 0)
        {
            if (PartidaFinalizada(2))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (PartidaFinalizada(1))
            {
                return 2;
            }else
            {
                return 0;
            }
        }
    }
/// <summary>
/// Nos devuelve si hay un ganador 
/// </summary>
/// <param name="jugador"> Recibe el jugador del cual ha sido el turno</param>
/// <returns>Devuelve Verdadero si hay un ganador</returns>
    private bool PartidaFinalizada(int jugador)
    {
        int col = 0;
        int fil = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (tablero[i, j] == jugador)
                {
                    fil += 1;
                }
                if (tablero[j, i] == jugador)
                {
                    col += 1;
                }
            }

            if (col == 3 || fil == 3){
                return true;
            }
            col = 0;
            fil = 0;
        }

        if ((tablero[0,0] == jugador && tablero[1,1] == jugador && tablero[2,2] == jugador )|| (tablero[0,2] == jugador && tablero[2,0] == jugador && tablero[1,1] == jugador))
        {
            return true;
        }

        return false;
    }
/// <summary>
/// Reinicia el tablero
/// </summary>
    public void Reiniciar()
    {
        turno = 0;
        tablero = new int[3, 3]
        {
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 }
        };
    }

}