using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;

namespace Infraestructure.Repositories
{
    public class DocumentTypeRepositorio(ApplicationDbContext context) : CrudCoreRespository<DocumentType, int>(context) , IDocumentoRepositorio
    {
    }
}
