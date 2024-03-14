using Laboratorio.Core.Interfaces.IExamen;
using Laboratorio.Core.Interfaces.IPaciente;
using Laboratorio.Infrastucture.Repositories.PacienteRepositories;
using Proyecto.Infrastucture.Repositories.ExamenRepositories;

namespace APIProyectoPrueba.Extensions
{
    public static class StartupTrasient
    {
        public static void ConfigureTrasient(this IServiceCollection service)
        {
            service.AddTransient<IExamen, ExamenRepository>();
            service.AddTransient<IPaciente, PacienteRepository>();


        }

    }
}
