using System.Collections.Generic;
namespace TP7;
using System.Linq;
using System.Data.SqlClient;
public static class Juego
{
    public static string Username { get; set; }
    public static int PuntajeActual { get; set; }
    public static int CantidadPreguntasCorrectas { get; set; }
    public static List<Preguntas> _Preguntas { get; set; }
    public static List<Respuestas> _Respuestas { get; set; }

    public static void InicializarJuego()
    {
        Username = null;
        PuntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
        _Preguntas = new List<Preguntas>();
        _Respuestas = new List<Respuestas>();
    }
    public static List<Categorias> ObtenerCategorias()
    {
        return BD.ObtenerCategorias();
    }
    public static List<Dificultades> ObtenerDificultades()
    {
        return BD.ObtenerDificultades();
    }
    public static void CargarPartida(string username, int dificultad, int categoria)
    {
        Username = username;
        _Preguntas = BD.ObtenerPreguntas(dificultad, categoria);
        _Respuestas = BD.ObtenerRespuestas(_Preguntas);
    }
    public static Preguntas ObtenerProximaPregunta()
    {
        Random rnd = new Random();
        int indiceAleatorio = rnd.Next(0, _Preguntas.Count);
        return _Preguntas[indiceAleatorio];
    }
    public static List<Respuestas> ObtenerProximasRespuestas(int idPregunta)
    {
        List<Respuestas> lista = new List<Respuestas>();
        foreach (var item in _Respuestas)
        {
            if (item.IdPregunta == idPregunta)
            {
                lista.Add(item);
            }
        }
        return lista;
    }
    public static bool VerificarRespuesta(int idPregunta, int idRespuesta)
    {
        bool correcta = false;
        foreach (var item in _Respuestas)
        {
            if (item.Correcta == true && item.IdRespuesta == idRespuesta)
            {
                PuntajeActual += 5;
                correcta = true;
                CantidadPreguntasCorrectas++;
            }
        }
        _Preguntas.Remove(_Preguntas.Find(Preguntas => Preguntas.IdPregunta == idPregunta));
        
        return correcta;
    }
}