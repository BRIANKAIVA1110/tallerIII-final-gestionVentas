using GestionVentas.DataTransferObjects.EntityDTO;
using System.Collections.Generic;

namespace GestionVentas.Services.Services
{
    public interface IModeloService
    {
        int AgregarModelo(ModeloDTO p_modeloDTO);
        ModeloDTO getModelo(int p_id);
        IEnumerable<ModeloDTO> getModelos();
    }
}