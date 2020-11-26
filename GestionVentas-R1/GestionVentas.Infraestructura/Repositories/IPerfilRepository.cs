using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using System.Collections.Generic;

namespace GestionVentas.Infraestructura.Repositories
{
    public interface IPerfilRepository : IRepositoryBase<Perfil>
    {
        int AgregarPerfilConModulos(PerfilDTO p_perfilDTO);
    }
}