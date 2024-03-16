using Laboratorio.Core.Entities;
using Laboratorio.Core.Interfaces.IPaciente;
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

namespace Laboratorio.Infrastucture.Repositories.PacienteRepositories
{
    public class PacienteRepository : IPaciente
    {

        public Connection connection;
        private readonly IConfiguration configuration; //instanciamos la configuracion predeterminada en context
        string connString = "Host=localhost;Port=2508;Username=postgres;Password=250819;Database=ExamenesMedicos;"; //cadena de conexion a la base de datos(credenciales)
        ResponseService r = new ResponseService();

        //constructor
        public PacienteRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connection = new Connection();
        }

        
        #region servicio obtener todos los pacientes
        public async Task<ResponseService> GetPaciente()//aqui declaro el metodo que trae todos los registros
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString)) //instanciar el objeto de la clase que hace la conexion con la bd
            {
                try
                {
                    await conn.OpenAsync();
                    string sentence = "SELECT * FROM public.paciente;";
                    List<PacienteDTO> ListPacientes = new List<PacienteDTO>();
                    PacienteDTO paciente = null;

                    var cmd = new NpgsqlCommand(sentence, conn);// Crear objeto que ejecuta la sentencia en la base datos recibiendo como paraetro la sentencia y la conexion de la base de datos
                    cmd.CommandType = CommandType.Text;
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync()) //mientras reader tenga registos por retornar va a ejecutarse
                    {
                        paciente = new PacienteDTO();

                        if (reader["IdPaciente"] != DBNull.Value) // si IdExamen no es nulo entra en la funcion
                        {
                            paciente.IdPaciente = Convert.ToInt32(reader["IdPaciente"]); //lo que hacemos aca es asignarle a IdExamen el valor encontrado
                        }

                        if (reader["NumDoc"] != DBNull.Value)
                        {
                            paciente.NumDoc = Convert.ToString(reader["NumDoc"]);
                        }

                        if (reader["NombrePac"] != DBNull.Value)
                        {
                            paciente.NombrePac = Convert.ToString(reader["NombrePac"]);
                        }

                        if (reader["Apellido"] != DBNull.Value)
                        {
                            paciente.Apellido = Convert.ToString(reader["Apellido"]);
                        }
                        if (reader["FechaCump"] != DBNull.Value)
                        {
                            paciente.FechaCump = Convert.ToDateTime(reader["FechaCump"]);
                        }

                        if (reader["Edad"] != DBNull.Value)
                        {
                            paciente.Edad = Convert.ToInt32(reader["Edad"]);
                        }

                        if (reader["IdTdFk"] != DBNull.Value)
                        {
                            paciente.IdTdFk = Convert.ToInt32(reader["IdTdFk"]);
                        }

                        ListPacientes.Add(paciente);

                    }

                    await conn.CloseAsync(); //cerrar la lectura de la ejecucion

                    //validaciones

                    if (ListPacientes.Count == 0)
                    {
                        r.Message = "No hay registros";
                    }
                    else
                    {
                        r.Data = ListPacientes;
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




        #region servicio obtener paciente por numero de documento
        public async Task<ResponseService> GetPacienteByDocument(string NumDoc)//aqui declaro el metodo que trae todos los registros con parametro codigo
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString)) //instanciar el objeto de la clase que hace la conexion con la bd
            {
                try
                {
                    await conn.OpenAsync(); //abrimos conexion con la bd
                    string sentence = "SELECT * " +
                        "FROM paciente AS P " +
                        "JOIN tipodocumento AS TP ON P.IdTdFk = TP.IdTd " +
                        $"where NumDoc = '{NumDoc}';";



                    var cmd = new NpgsqlCommand(sentence, conn);// Crear objeto que ejecuta la sentencia en la base datos recibiendo como parametro la sentencia y la conexion de la base de datos
                    cmd.CommandType = CommandType.Text;
                    var reader = await cmd.ExecuteReaderAsync();
                    PacienteDTO paciente = null;

                    while (await reader.ReadAsync()) //mientras reader tenga registos por retornar va a ejecutarse
                    {
                        paciente = new PacienteDTO();

                        if (reader["IdPaciente"] != DBNull.Value) // si IdExamen no es nulo entra en la funcion
                        {
                            paciente.IdPaciente = Convert.ToInt32(reader["IdPaciente"]); //lo que hacemos aca es asignarle a pruebaId el valor encontrado
                        }

                        if (reader["NumDoc"] != DBNull.Value)
                        {
                            paciente.NumDoc = Convert.ToString(reader["NumDoc"]);
                        }

                        if (reader["NombrePac"] != DBNull.Value)
                        {
                            paciente.NombrePac = Convert.ToString(reader["NombrePac"]);
                        }

                        if (reader["Apellido"] != DBNull.Value)
                        {
                            paciente.Apellido = Convert.ToString(reader["Apellido"]);
                        }

                        if (reader["FechaCump"] != DBNull.Value)
                        {
                            paciente.FechaCump = Convert.ToDateTime(reader["FechaCump"]);
                        }

                        if (reader["Edad"] != DBNull.Value)
                        {
                            paciente.Edad = Convert.ToInt32(reader["Edad"]);
                        }
                        if (reader["IdTdFk"] != DBNull.Value)
                        {
                            paciente.IdTdFk = Convert.ToInt32(reader["IdTdFk"]);
                        }
                        if (reader["TipoDoc"] != DBNull.Value)
                        {
                            paciente.TipoDoc = Convert.ToString(reader["TipoDoc"]);
                        }

                    }

                    await conn.CloseAsync(); //cerrar la lectura de la ejecucion

                    //validaciones

                    if (paciente == null)
                    {
                        r.Status = 400;
                        r.Message = "No se encontro ningun dato con el codigo seleccionado";
                    }
                    else
                    {

                        r.Data = paciente;
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
    }
}
