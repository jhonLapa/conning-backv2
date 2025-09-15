using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class DocumentoRepositorio(ApplicationDbContext context) : CrudCoreRespository<Documento, int>(context) , IDocumentoRepositorio
    {
    }
}
