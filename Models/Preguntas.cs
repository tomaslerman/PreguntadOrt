using System.Collections.Generic;
using System.Data.SqlClient;

namespace TP7;
public class Preguntas
{
    public int IdPregunta {get; set;}
    public int IdCategoria {get;set;}
    public int IdDificultad {get;set;}
    public string Enunciado {get;set;}
    public string Foto {get;set;}
}