using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
namespace TP7;

public static class BD
{
    private static string _connectionString = @"Server=DESKTOP-3DKDU00\SQLEXPRESS;DataBase=PreguntadOrt;Trusted_Connection=True;"; //cambiar a localhost

    public static List<Categorias> ObtenerCategorias()
    {
        using (SqlConnection DB = new SqlConnection(_connectionString))
        {
            string SQL = "Select * from Categorias";
            return DB.Query<Categorias>(SQL).ToList();
        }
    }
    public static List<Dificultades> ObtenerDificultades()
    {
        using (SqlConnection DB = new SqlConnection(_connectionString))
        {
            string SQL = "Select * from Dificultades";
            return DB.Query<Dificultades>(SQL).ToList();
        }
    }
    public static List<Preguntas> ObtenerPreguntas(int dificultad, int categoria)
    {
        List<Preguntas> listaPreguntas = new List<Preguntas>();
        using (SqlConnection DB = new SqlConnection(_connectionString))
        {
            if (dificultad == -1 && categoria == -1)
            {
                string SQL = "select * from Preguntas";
                listaPreguntas = DB.Query<Preguntas>(SQL, new{@categoria=categoria, @dificultad = dificultad}).ToList();
            }
            else if(dificultad == -1)
            {
                string SQL = "select * from Preguntas where IdCategoria = @categoria";
                listaPreguntas = DB.Query<Preguntas>(SQL, new{@categoria=categoria, @dificultad = dificultad}).ToList();
            }
            else if(categoria == -1)
            {
                string SQL = "select * from Preguntas where IdDificultad = @dificultad";
                listaPreguntas = DB.Query<Preguntas>(SQL, new{@categoria=categoria, @dificultad = dificultad}).ToList();
            }
            else
            {
                string SQL = "select * from Preguntas where IdCategoria = @categoria and IdDificultad = @dificultad";
                listaPreguntas = DB.Query<Preguntas>(SQL, new{@categoria=categoria, @dificultad = dificultad}).ToList();
            }
        }
        return listaPreguntas;
    }
    public static List<Respuestas> ObtenerRespuestas(List<Preguntas> listaPreguntas)
    {
        List<Respuestas> listaRespuestas = new List<Respuestas>();
        using (SqlConnection DB = new SqlConnection(_connectionString))
        {
            string SQL = "";
            foreach (var item in listaPreguntas)
            {
                SQL = "select * from Respuestas where IdPregunta = @pIdPregunta";
                listaRespuestas.AddRange(DB.Query<Respuestas>(SQL, new {pIdPregunta = item.IdPregunta}).ToList());                
            }
        }
        return listaRespuestas;
    }

}