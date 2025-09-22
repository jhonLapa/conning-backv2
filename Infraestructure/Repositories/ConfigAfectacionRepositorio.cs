using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class ConfigAfectacionRespositorio
        : CrudCoreRespository<ConfigAfectacion, int>, IConfigAfectacionRepositorio
    {
        private readonly ApplicationDbContext _dbContext;

        public ConfigAfectacionRespositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<ConfigAfectacion?> FindByEmpresaAndAfectacionAsync(int idEmpresa, int idAfectacion)
        {
            return await _dbContext.Set<ConfigAfectacion>()
                .FirstOrDefaultAsync(c => c.IdEmpresa == idEmpresa && c.IdAfectacion == idAfectacion);
        }
    }
}
