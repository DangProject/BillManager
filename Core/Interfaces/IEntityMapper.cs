using System;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IEntityMapper
    {
        void Configure<TFrom, TTo>();
        TTo Map<TFrom, TTo>(TFrom source, TTo destination)
            where TFrom : class, new()
            where TTo : class, new();
        TTo Map<TFrom, TTo>(TFrom source)
            where TFrom : class, new()
            where TTo : class;
    }
}
