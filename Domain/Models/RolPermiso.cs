using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class RolPermiso
    {

        #region MIEMBROS PRIVADOS
        int _rolid;
        int _permisoid;
        #endregion

        #region CONSTRUCTORES
        public RolPermiso(int rolid, int permisoid)
        {
            RolId = rolid;
            PermisoId = permisoid;
        }
        #endregion

        #region PROPIEDADES PUBLICAS
        public int RolId
        {
            get { return _rolid; }
            set { _rolid = value; }
        }

        public int PermisoId
        {
            get { return _permisoid; }
            set { _permisoid = value; }
        }
        #endregion

    }
}
