using Application.Core.Services.Interfaces;
using Application.Mantenedores.Dtos.Bancks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mantenedores.Services.Interfaces
{
    public interface IBanckService : ICrudCoreService<BanckDto, BanckSaveDto , int>
    {
    }
}
