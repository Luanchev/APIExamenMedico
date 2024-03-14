using Laboratorio.Core.Entities;
using Laboratorio.Core.Interfaces.IExamen;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Proyecto.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Proyecto.Infrastucture.Repositories.ExamenRepositories
{
    public class ExamenRepository : IExamen

    {
        public Connection connection;
        private readonly IConfiguration configuration; //instanciamos la configuracion predeterminada en context
        string connString = "Host=localhost;Port=2508;Username=postgres;Password=250819;Database=ExamenesMedicos;"; //cadena de conexion a la base de datos(credenciales)
        ResponseService r = new ResponseService();

        //constructor

        public ExamenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connection = new Connection();
        }

        #region METODOS GET
        #region servicio obtener todos los examenes
        public async Task<ResponseService> GetExamen()//aqui declaro el metodo que trae todos los registros
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString)) //instanciar el objeto de la clase que hace la conexion con la bd
            {
                try
                {
                    await conn.OpenAsync();
                    string sentence = "SELECT * FROM public.examenmedico;";
                    List<ExamenDTO> ListExamenes= new List<ExamenDTO>();
                    ExamenDTO examen = null;

                    var cmd = new NpgsqlCommand(sentence, conn);// Crear objeto que ejecuta la sentencia en la base datos recibiendo como paraetro la sentencia y la conexion de la base de datos
                    cmd.CommandType = CommandType.Text;
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync()) //mientras reader tenga registos por retornar va a ejecutarse
                    {
                        examen = new ExamenDTO();

                        if (reader["IdExamen"] != DBNull.Value) // si IdExamen no es nulo entra en la funcion
                        {
                            examen.IdExamen = Convert.ToInt32(reader["IdExamen"]); //lo que hacemos aca es asignarle a IdExamen el valor encontrado
                        }

                        if (reader["Codigo"] != DBNull.Value)
                        {
                            examen.Codigo = Convert.ToString(reader["Codigo"]);
                        }

                        if (reader["Nombre"] != DBNull.Value)
                        {
                            examen.Nombre = Convert.ToString(reader["Nombre"]);
                        }

                        if (reader["Precio"] != DBNull.Value)
                        {
                            examen.Precio = Convert.ToDouble(reader["Precio"]);
                        }

                        if (reader["Idmuestrafk"] != DBNull.Value)
                        {
                            examen.IdMuestrafk = Convert.ToInt32(reader["Idmuestrafk"]);
                        }

                        if (reader["Idpacientefk"] != DBNull.Value)
                        {
                            examen.IdPacientefk = Convert.ToInt32(reader["Idpacientefk"]);
                        }

                        ListExamenes.Add(examen);

                    }

                    await conn.CloseAsync(); //cerrar la lectura de la ejecucion

                    //validaciones

                    if(ListExamenes.Count == 0)
                    {
                        r.Message = "No hay registros";
                    }else
                    {
                        r.Data = ListExamenes;
                        r.Message = "Se ejecuto de manera correcta";

                    }

                    r.Status = 200;
                   

                    return r;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

        }
        #endregion


        #region servicio obtener examen por codigo
        public async Task<ResponseService> GetExamenByKey(string codigo)//aqui declaro el metodo que trae todos los registros con parametro codigo
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString)) //instanciar el objeto de la clase que hace la conexion con la bd
            {
                try
                {
                    await conn.OpenAsync(); //abrimos conexion con la bd
                    string sentence = "SELECT * " +
                        "FROM examenmedico " +
                        $"where codigo = '{codigo}';";

                    

                    var cmd = new NpgsqlCommand(sentence, conn);// Crear objeto que ejecuta la sentencia en la base datos recibiendo como parametro la sentencia y la conexion de la base de datos
                    cmd.CommandType = CommandType.Text;
                    var reader = await cmd.ExecuteReaderAsync();
                    ExamenDTO examen = null;

                    while (await reader.ReadAsync()) //mientras reader tenga registos por retornar va a ejecutarse
                    {
                        examen = new ExamenDTO();

                        if (reader["IdExamen"] != DBNull.Value) // si IdExamen no es nulo entra en la funcion
                        {
                            examen.IdExamen = Convert.ToInt32(reader["IdExamen"]); //lo que hacemos aca es asignarle a pruebaId el valor encontrado
                        }

                        if (reader["Codigo"] != DBNull.Value)
                        {
                            examen.Codigo = Convert.ToString(reader["Codigo"]);
                        }

                        if (reader["Nombre"] != DBNull.Value)
                        {
                            examen.Nombre = Convert.ToString(reader["Nombre"]);
                        }

                        if (reader["Precio"] != DBNull.Value)
                        {
                            examen.Precio = Convert.ToDouble(reader["Precio"]);
                        }

                        if (reader["Idmuestrafk"] != DBNull.Value)
                        {
                            examen.IdMuestrafk = Convert.ToInt32(reader["Idmuestrafk"]);
                        }

                        if (reader["Idpacientefk"] != DBNull.Value)
                        {
                            examen.IdPacientefk = Convert.ToInt32(reader["Idpacientefk"]);
                        }

                    }

                    await conn.CloseAsync(); //cerrar la lectura de la ejecucion

                    //validaciones

                    if (examen == null)
                    {
                        r.Status = 400;
                        r.Message = "No se encontro ningun dato con el codigo seleccionado";
                    }
                    else
                    {

                        r.Data = examen;
                        r.Status = 200;
                        r.Message = "Se ejecuto de manera correcta";

                    }                  


                    return r;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

        }
        #endregion


        #region servicio obtener examen por numero de documento del paciente
        public async Task<ResponseService> GetExamenByDP(string numdoc)//vamos a traer la informacion de todo el examen con el tipo de muestra, como parametro el documento de identidad del paciente
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString)) //instanciar el objeto de la clase que hace la conexion con la bd
            {
                try
                {
                    await conn.OpenAsync(); //abrimos conexion con la bd


                    //CONSULTA QUE NOS MUESTRA CON EL NUMERO DE DOCUMENTO DEL PACIENTE LA INFORMACION DEL EXAMEN QUE SE HIZO,
                    //CON LA INFORMACION DEL TIPO DE MUESTRA, Y LA INFORMACION IMPORTANTE DEL PACIENTE
                    string sentence = "SELECT " +
                        "E.IDEXAMEN, " +
                        "E.CODIGO, " +
                        "E.NOMBRE, " +
                        "E.PRECIO, " +
                        "E.IDMUESTRAFK, " +
                        "TP.IDMUESTRA, " +
                        "TP.TIPOMUESTRA, " +
                        "E.idpacientefk, " +
                        "P.idpaciente, " +
                        "P.NUMDOC, " +
                        "P.NOMBREPAC, " +
                        "P.APELLIDO, " +
                        "P.FECHACUMP, " +
                        "P.EDAD, " +
                        "P.IDTDFK " +
                        "FROM EXAMENMEDICO AS E " +
                        "JOIN tipomuestra AS TP ON E.IDMUESTRAFK = TP.IDMUESTRA " +
                        "JOIN paciente  AS P ON E.idpacientefk = P.idpaciente " +
                        $"WHERE numdoc = '{numdoc}'; ";


                    var cmd = new NpgsqlCommand(sentence, conn);// Crear objeto que ejecuta la sentencia en la base datos recibiendo como parametro la sentencia y la conexion de la base de datos
                    cmd.CommandType = CommandType.Text;
                    var reader = await cmd.ExecuteReaderAsync();


                    ExamenDTO examen = null;
                    PacienteDTO paciente = null;
                    TipoMuestraDTO muestra = null;


                    //inicializamos por fuera del while para que no se sobreescriban las listas
                    List<ExamenDTO> ListExamenes = new List<ExamenDTO>();
                    List<TipoMuestraDTO> ListMuestra = new List<TipoMuestraDTO>();
                   



                    while (await reader.ReadAsync()) //mientras reader tenga registos por retornar va a ejecutarse
                    {

                       
                        examen = new ExamenDTO();                        
                        paciente = new PacienteDTO();                        
                        muestra = new TipoMuestraDTO();                            
                       


                        if (reader["IdExamen"] != DBNull.Value) // si IdExamen no es nulo entra en la funcion
                        {
                            examen.IdExamen = Convert.ToInt32(reader["IdExamen"]); //lo que hacemos aca es asignarle a pruebaId el valor encontrado
                        }

                        if (reader["Codigo"] != DBNull.Value)
                        {
                            examen.Codigo = Convert.ToString(reader["Codigo"]);
                        }

                        if (reader["Nombre"] != DBNull.Value)
                        {
                            examen.Nombre = Convert.ToString(reader["Nombre"]);
                        }

                        if (reader["Precio"] != DBNull.Value)
                        {
                            examen.Precio = Convert.ToDouble(reader["Precio"]);
                        }

                        if (reader["Idmuestrafk"] != DBNull.Value)
                        {
                            examen.IdMuestrafk = Convert.ToInt32(reader["Idmuestrafk"]);
                        }

                        if (reader["Idpacientefk"] != DBNull.Value)
                        {
                            examen.IdPacientefk = Convert.ToInt32(reader["Idpacientefk"]);
                        }


                        // aqui empezamos la validacion de TipoMuestraDTO

                        if (reader["idmuestra"] != DBNull.Value)
                        {
                            muestra.IdMuestra = Convert.ToInt32(reader["idmuestra"]);
                        }

                        if (reader["tipomuestra"] != DBNull.Value)
                        {
                            muestra.TipoMuestra = Convert.ToString(reader["tipomuestra"]);
                        }


                        // aqui empezamos la validacion de PacienteDTO

                        if (reader["idpaciente"] != DBNull.Value)
                        {
                            paciente.IdPaciente = Convert.ToInt32(reader["idpaciente"]);
                        }

                        if (reader["NumDoc"] != DBNull.Value)
                        {
                            paciente.NumDoc = Convert.ToString(reader["NumDoc"]);
                        }

                        if (reader["nombrepac"] != DBNull.Value)
                        {
                            paciente.NombrePac = Convert.ToString(reader["nombrepac"]);
                        }

                        if (reader["Apellido"] != DBNull.Value)
                        {
                            paciente.Apellido = Convert.ToString(reader["Apellido"]);
                        }

                        if (reader["FechaCump"] != DBNull.Value)
                        {
                            paciente.FechaCump = Convert.ToDateTime(reader["FechaCump"]);
                        }

                        if (reader["edad"] != DBNull.Value)
                        {
                            paciente.Edad = Convert.ToInt32(reader["edad"]);
                        }

                        if (reader["IdTdFk"] != DBNull.Value)
                        {
                            paciente.IdTdFk = Convert.ToInt32(reader["IdTdFk"]);
                        }

                        //agregamos en cada lista para agregar si hay varios examenes con la misma lista
                        ListExamenes.Add(examen);
                        ListMuestra.Add(muestra);
                        


                    }                  
                    
                    await conn.CloseAsync(); //cerrar la lectura de la ejecucion

                    //validaciones

                    if (paciente == null)
                    {
                        r.Status = 400;
                        r.Message = "Con el numero de documento registrado no se encuentra ningun examen";

                    }
                    else
                    {
                        r.Data = new { Examen = ListExamenes, Muestra = ListMuestra, Paciente = paciente };
                        r.Status = 200;
                        r.Message = "Se ejecuto de manera correcta";
                    }

                    
                    return r;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

        }
        #endregion

        #endregion

    
        #region Servicio agregar nuevos registros

        public async Task<ResponseService> CreateExam(ExamenDTO examen)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                try
                {
                    await conn.OpenAsync(); //una espera para abrir la conexion de la base de datos siempre se debe escribir 

                    if (await GetIdExamenByKey(examen.Codigo, conn) > 0)
                    {
                        r.Status = 400;
                        r.Message = "No se puede adicionar un examen con un codigo ya registrado";
                        return r;
                    }

                    string sentence = "INSERT INTO examenmedico (codigo, nombre, precio, idmuestrafk, idpacientefk) " +
                        "VALUES (" +
                        $"'{examen.Codigo}', '{examen.Nombre}', '{examen.Precio}', '{examen.IdMuestrafk}', '{examen.IdPacientefk}');"; //lo que hacemos aqui es la sentencia SQL

                    var cmd = new NpgsqlCommand(sentence, conn);
                    cmd.CommandType = CommandType.Text;
                    await cmd.ExecuteNonQueryAsync();

                    r.Data = examen;
                    r.Status = 200;
                    r.Message = "Se ha agregado el registro de manera exitosa";

                    return r;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        #endregion


        #region Servicio para actualizar un registro existente
        public async Task<ResponseService> EditExam(ExamenDTO examen) //aqui declaro el metodo que actualiza un registro
        {

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                try
                {
                    await conn.OpenAsync(); //una espera para abrir la conexion de la base de datos siempre se debe escribir 

                    //con esto validamos si idCamion existe en la tabla sino retorna 0 y arroja el error
                    if (await ValidateIdExam(examen.IdExamen, conn) == 0)
                    {
                        r.Status = 400;
                        r.Message = "No se puede editar el camion ya que no se encuentra en la base de datos";
                        return r;
                    }
                    string sentence = "UPDATE examenmedico " +
                              "SET codigo = '" + examen.Codigo + "', " +
                              "nombre = '" + examen.Nombre + "', " +
                              "precio = '" + examen.Precio + "', " +
                              "idmuestrafk = '" + examen.IdMuestrafk + "', " +
                              "idpacientefk = '" + examen.IdPacientefk+ "' " +
                              "WHERE idexamen = " + examen.IdExamen;


                    var cmd = new NpgsqlCommand(sentence, conn);
                    cmd.CommandType = CommandType.Text;
                    await cmd.ExecuteNonQueryAsync();

                    r.Status = 200;
                    r.Data = examen;
                    r.Message = "Se ha editado el registro de manera exitosa";
                    return r;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        #endregion


        #region Servicio para borrar registros existentes
        public async Task<ResponseService> DeleteExam(int IdExamen) //aqui declaro el metodo que elimina un registro
        {

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                try
                {
                    await conn.OpenAsync(); //una espera para abrir la conexion de la base de datos siempre se debe escribir 

                    if (await ValidateIdExam(IdExamen, conn) == 0)
                    {
                        r.Status = 400;
                        r.Message = "No se puede eliminar el examen ya que no se encuentra el id en la base de datos";
                        return r;
                    }
                    string sentence = "DELETE FROM examenmedico WHERE IdExamen = " + $"'{IdExamen}';";


                    var cmd = new NpgsqlCommand(sentence, conn);
                    cmd.CommandType = CommandType.Text;
                    await cmd.ExecuteNonQueryAsync();

                    r.Status = 200;
                    r.Message = "Se ha eliminado el registro: " + IdExamen + " de manera exitosa " ;

                    return r;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        #endregion


        #region servicio obtener IdExamen por codigo
        public async Task<int> GetIdExamenByKey(string codigo, NpgsqlConnection conn)//aqui declaro el metodo que trae un registro con parametro codigo
        {          
            try
            { 
                string sentence = "SELECT idexamen " +
                    "FROM examenmedico " +
                    $"where codigo = '{codigo}';";


                var cmd = new NpgsqlCommand(sentence, conn);// Crear objeto que ejecuta la sentencia en la base datos recibiendo como parametro la sentencia y la conexion de la base de datos
                cmd.CommandType = CommandType.Text;
                var reader = await cmd.ExecuteReaderAsync();
                int idExamenDb = 0;

                while (await reader.ReadAsync()) //mientras reader tenga registos por retornar va a ejecutarse
                {
                    if (reader["idexamen"] != DBNull.Value)
                    {
                        idExamenDb = Convert.ToInt32(reader["idexamen"]);
                    }

                }

                await reader.CloseAsync(); //cerrar la lectura de la ejecucion

                return idExamenDb;
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion


        #region Servicio para validar el IdCamion
        public async Task<int> ValidateIdExam(int IdExamen, NpgsqlConnection conn)
        {
            try
            {
                string sentence = "Select IdExamen " +
                     "FROM examenmedico " +
                     $"where IdExamen = '{IdExamen}';";

                var cmd = new NpgsqlCommand(sentence, conn);
                cmd.CommandType = CommandType.Text;
                var reader = await cmd.ExecuteReaderAsync();
                int idExamenDb = 0; //este Id es si no encuentra el valor entoncer retorna 0

                while (await reader.ReadAsync())
                {
                    if (reader["IdExamen"] != DBNull.Value)
                    {
                        idExamenDb = Convert.ToInt32(reader["IdExamen"]);
                    }
                }

                await reader.CloseAsync();

                return idExamenDb;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        #endregion

    }
}