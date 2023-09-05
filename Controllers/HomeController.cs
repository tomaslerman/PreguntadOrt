using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TP7.Models;
namespace TP7.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult ConfigurarJuego()
    {
        Juego.InicializarJuego();
        ViewBag.Dificultades = Juego.ObtenerDificultades();
        ViewBag.Categorias = Juego.ObtenerCategorias();
        return View("ConfigurarJuego");
    }
    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {
        Juego.CargarPartida(username, dificultad, categoria);
        
        if(Juego._Preguntas.Count == 0)
        {
            return RedirectToAction("ConfigurarJuego");
        }
        else
        {
            return RedirectToAction("Jugar");
        }
    }
    public IActionResult Jugar()
    {
        if(Juego._Preguntas.Count == 0)
        {
            ViewBag.Puntaje = Juego.PuntajeActual;
            ViewBag.PreguntasCorrectas = Juego.CantidadPreguntasCorrectas;
            ViewBag.Username = Juego.Username;
            return View("Fin");
        }
        else    
        {
            ViewBag.Puntaje = Juego.PuntajeActual;
            ViewBag.Username = Juego.Username;
            ViewBag.PreguntasCorrectas = Juego.CantidadPreguntasCorrectas;
            ViewBag.Pregunta = Juego.ObtenerProximaPregunta();
            ViewBag.Respuesta = Juego.ObtenerProximasRespuestas(ViewBag.Pregunta.IdPregunta);
            return View("Juego");
        }
    }
    [HttpPost] public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta)
    {
        ViewBag.Correcta = Juego.VerificarRespuesta(idPregunta, idRespuesta);
        return View("Respuesta"); 
    }
}
