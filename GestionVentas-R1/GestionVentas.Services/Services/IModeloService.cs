using GestionVentas.DataTransferObjects.EntityDTO;
using System.Collections.Generic;

namespace GestionVentas.Services.Services
{
    public interface IModeloService
    {
        int AgregarModelo(ModeloDTO p_modeloDTO);
        int ModificarModelo(ModeloDTO p_modeloDTO);
        int EliminarModelo(int p_id);
        ModeloDTO getModelo(int p_id);
        IEnumerable<ModeloDTO> getModelos();
    }
}