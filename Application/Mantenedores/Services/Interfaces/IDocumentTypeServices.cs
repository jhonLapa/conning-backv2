using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IDocumentTypeServices : ICrudCoreService<DocumentTypeDto, DocumentTypeSaveDto, int>
    {
    }
}
