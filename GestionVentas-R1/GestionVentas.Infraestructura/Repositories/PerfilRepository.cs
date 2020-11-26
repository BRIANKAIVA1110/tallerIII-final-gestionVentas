using GestionVentas.DataTransferObjects.EntityDTO;
using GestionVentas.Domain.Entities;
using GestionVentas.Infraestructura.DataAccess.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace GestionVentas.Infraestructura.Repositories
{
    public class PerfilRepository : RepositoryBase<Perfil>, IPerfilRepository
    {
        private readonly ApplicationContext _applicationContext;
        public PerfilRepository(ApplicationContext applicationContext, IDbConnection dbConnection) : base(applicationContext, dbConnection)
        {
            this._applicationContext = applicationContext;
        }


        public int AgregarPerfilConModulos(PerfilDTO p_perfilDTO)
        {
            //este no es la mejor manera de hacerlo, deberia ir en la capa de servicio la logica...
            //ver como implementar patron unitOfWork.
            int result = 0;
            using (IDbContextTransaction transaction = this._applicationContext.Database.BeginTransaction())
            {
                try
                {
                    Perfil entityPerfil = new Perfil();
                    entityPerfil.Descripcion = p_perfilDTO.Descripcion;

                    this._entity.Add(entityPerfil);
                    result = this._applicationContext.SaveChanges();

                    List<ModulosApplicacionDTO> listmodulosApplicacionDTO = this.ExecuteQuery(new ObtenerModulosApplicacion());

                    if (p_perfilDTO.IsCheckArticulos) {
                        int moduloId = listmodulosApplicacionDTO.Where(x=> x.Descripcion == "articulos").Select(x => x.Id ).FirstOrDefault();
                        if (moduloId == 0)
                            throw new Exception();


                        ModulosXPerfil mxp = new ModulosXPerfil();
                        mxp.ModuloId = moduloId;
                        mxp.PerfilId = entityPerfil.Id;
                        this._applicationContext.Set<ModulosXPerfil>().Add(mxp);
                        result = this._applicationContext.SaveChanges();
                    }
                    if (p_perfilDTO.IsCheckStock)
                    {
                        int moduloId = listmodulosApplicacionDTO.Where(x => x.Descripcion == "stock").Select(x => x.Id).FirstOrDefault();
                        if (moduloId == 0)
                            throw new Exception();


                        ModulosXPerfil mxp = new ModulosXPerfil();
                        mxp.ModuloId = moduloId;
                        mxp.PerfilId = entityPerfil.Id;
                        this._applicationContext.Set<ModulosXPerfil>().Add(mxp);
                        result = this._applicationContext.SaveChanges();
                    }
                    if (p_perfilDTO.IsCheckVentas)
                    {
                        int moduloId = listmodulosApplicacionDTO.Where(x => x.Descripcion == "ventas").Select(x => x.Id).FirstOrDefault();
                        if (moduloId == 0)
                            throw new Exception();


                        ModulosXPerfil mxp = new ModulosXPerfil();
                        mxp.ModuloId = moduloId;
                        mxp.PerfilId = entityPerfil.Id;
                        this._applicationContext.Set<ModulosXPerfil>().Add(mxp);
                        result = this._applicationContext.SaveChanges();
                    }
                    if (p_perfilDTO.IsCheckReportes)
                    {
                        int moduloId = listmodulosApplicacionDTO.Where(x => x.Descripcion == "reportes").Select(x => x.Id).FirstOrDefault();
                        if (moduloId == 0)
                            throw new Exception();


                        ModulosXPerfil mxp = new ModulosXPerfil();
                        mxp.ModuloId = moduloId;
                        mxp.PerfilId = entityPerfil.Id;
                        this._applicationContext.Set<ModulosXPerfil>().Add(mxp);
                        result = this._applicationContext.SaveChanges();
                    }
                    if (p_perfilDTO.IsCheckSeguridad)
                    {
                        int moduloId = listmodulosApplicacionDTO.Where(x => x.Descripcion == "seguridad").Select(x => x.Id).FirstOrDefault();
                        if (moduloId == 0)
                            throw new Exception();


                        ModulosXPerfil mxp = new ModulosXPerfil();
                        mxp.ModuloId = moduloId;
                        mxp.PerfilId = entityPerfil.Id;
                        this._applicationContext.Set<ModulosXPerfil>().Add(mxp);
                        result = this._applicationContext.SaveChanges();
                    }


                    transaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.ToString());
                }

            }


            return 0;
        }
        public int ModificarPerfilConModulos(PerfilDTO p_perfilDTO)
        {
            //este no es la mejor manera de hacerlo, deberia ir en la capa de servicio la logica...
            //ver como implementar patron unitOfWork.
            int result = 0;
            using (IDbContextTransaction transaction = this._applicationContext.Database.BeginTransaction())
            {
                try
                {
                    //actualizamos perfil
                    Perfil entityPerfil=  this._entity.FirstOrDefault(x=> x.Id == p_perfilDTO.Id);
                    entityPerfil.Descripcion = p_perfilDTO.Descripcion;
                    this._entity.Update(entityPerfil);

                    result = this._applicationContext.SaveChanges();
                    if (result == 0) {
                        throw new Exception("no se pudo modificar perfil");
                    }

                    //traemos ModulosXPerfil actuales y eliminamos todas las referencias con el perfil actual
                    var mxperfiles = this._applicationContext.Set<ModulosXPerfil>().Where(x => x.PerfilId == entityPerfil.Id).ToList();
                    this._applicationContext.Set<ModulosXPerfil>().RemoveRange(mxperfiles);

                    result = this._applicationContext.SaveChanges();
                    



                    //agregamos los nuevos
                    List<ModulosApplicacionDTO> listmodulosApplicacionDTO = this.ExecuteQuery(new ObtenerModulosApplicacion());

                    if (p_perfilDTO.IsCheckArticulos)
                    {
                        int moduloId = listmodulosApplicacionDTO.Where(x => x.Descripcion == "articulos").Select(x => x.Id).FirstOrDefault();
                        if (moduloId == 0)
                            throw new Exception("no se pudo modificar perfil");


                        ModulosXPerfil mxp = new ModulosXPerfil();
                        mxp.ModuloId = moduloId;
                        mxp.PerfilId = entityPerfil.Id;
                        this._applicationContext.Set<ModulosXPerfil>().Add(mxp);
                        result = this._applicationContext.SaveChanges();
                    }
                    if (p_perfilDTO.IsCheckStock)
                    {
                        int moduloId = listmodulosApplicacionDTO.Where(x => x.Descripcion == "stock").Select(x => x.Id).FirstOrDefault();
                        if (moduloId == 0)
                            throw new Exception("no se pudo modificar perfil");


                        ModulosXPerfil mxp = new ModulosXPerfil();
                        mxp.ModuloId = moduloId;
                        mxp.PerfilId = entityPerfil.Id;
                        this._applicationContext.Set<ModulosXPerfil>().Add(mxp);
                        result = this._applicationContext.SaveChanges();
                    }
                    if (p_perfilDTO.IsCheckVentas)
                    {
                        int moduloId = listmodulosApplicacionDTO.Where(x => x.Descripcion == "ventas").Select(x => x.Id).FirstOrDefault();
                        if (moduloId == 0)
                            throw new Exception("no se pudo modificar perfil");


                        ModulosXPerfil mxp = new ModulosXPerfil();
                        mxp.ModuloId = moduloId;
                        mxp.PerfilId = entityPerfil.Id;
                        this._applicationContext.Set<ModulosXPerfil>().Add(mxp);
                        result = this._applicationContext.SaveChanges();
                    }
                    if (p_perfilDTO.IsCheckReportes)
                    {
                        int moduloId = listmodulosApplicacionDTO.Where(x => x.Descripcion == "reportes").Select(x => x.Id).FirstOrDefault();
                        if (moduloId == 0)
                            throw new Exception("no se pudo modificar perfil");


                        ModulosXPerfil mxp = new ModulosXPerfil();
                        mxp.ModuloId = moduloId;
                        mxp.PerfilId = entityPerfil.Id;
                        this._applicationContext.Set<ModulosXPerfil>().Add(mxp);
                        result = this._applicationContext.SaveChanges();
                    }
                    if (p_perfilDTO.IsCheckSeguridad)
                    {
                        int moduloId = listmodulosApplicacionDTO.Where(x => x.Descripcion == "seguridad").Select(x => x.Id).FirstOrDefault();
                        if (moduloId == 0)
                            throw new Exception("no se pudo modificar perfil");


                        ModulosXPerfil mxp = new ModulosXPerfil();
                        mxp.ModuloId = moduloId;
                        mxp.PerfilId = entityPerfil.Id;
                        this._applicationContext.Set<ModulosXPerfil>().Add(mxp);
                        result = this._applicationContext.SaveChanges();
                    }


                    transaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.ToString());
                }

            }


            return 0;
        }


        public override int Delete(Perfil p_entity)
        {
            using (IDbContextTransaction transaction = this._applicationContext.Database.BeginTransaction())
            {

                int result = 0;
                try
                {
                    var listModulisxperfil = this._applicationContext.Set<ModulosXPerfil>().Where(x => x.PerfilId == p_entity.Id).ToList(); ;
                    if (listModulisxperfil.Any())
                    {

                        Perfil entityPerfil = this._applicationContext.Set<Perfil>().FirstOrDefault(x => x.Id == p_entity.Id);
                        this._applicationContext.Set<Perfil>().Remove(entityPerfil);
                        result = this._applicationContext.SaveChanges();
                        //se se elimino entonce
                        if (result != 0)
                        {
                            this._applicationContext.Set<ModulosXPerfil>().RemoveRange(listModulisxperfil);
                            result = this._applicationContext.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("no se pudo eliminar perfil");
                        }
                    }
                    else {
                        throw new Exception("no se pudo eliminar perfil");
                    }

                    transaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.ToString());
                }
            }
        }
    }
}
