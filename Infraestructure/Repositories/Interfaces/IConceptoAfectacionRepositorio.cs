using Domain;
using Infraestructure.Core.Repositories.Interfaces;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IConceptoAfectacionRepositorio : ICrudCoreRespository<ConceptoAfectacion, int>
    {
        Task<ConceptoAfectacion?> FindByConceptoAndAfectacionAsync(int IdConcepto, int idAfectacion);

    }
}

