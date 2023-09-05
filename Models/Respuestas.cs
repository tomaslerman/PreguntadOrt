using System.Collections.Generic;
using System.Data.SqlClient;

namespace TP7;
public class Respuestas
{
    public int IdRespuesta {get; set;}
    public int IdPregunta {get;set;}
    public int Opcion {get;set;}
    public string Contenido {get;set;}
    public bool Correcta {get;set;}
    public string Foto {get;set;}
}