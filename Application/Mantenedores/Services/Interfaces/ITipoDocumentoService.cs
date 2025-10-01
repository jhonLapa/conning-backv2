using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.TiposDocumento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface ITipoDocumentoService : ICrudCoreService<TipoDocumentoDto, TipoDocumentoSaveDto, int>
    {
    }
}
