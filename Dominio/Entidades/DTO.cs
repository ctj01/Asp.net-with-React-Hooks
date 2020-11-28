using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public abstract class DTO : IDisposable
    {
        bool _Isdiposable = true;
        public void Dispose()
        {
            if (!_Isdiposable)
            {
                _Isdiposable = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
