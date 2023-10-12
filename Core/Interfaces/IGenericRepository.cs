using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T>GetByIdAsync(int id);
        Task<IEnumerable<T>>GetByIdAsync();
        IEnumerable<T>Find(Expression<Func<T, bool>> expression); // Metodo que permite realizar busquedas con LinQ
        
        //Este metodo se define para incrementar la paginación de consultas, Segmenta el tamaño de las consultas y define un parametro de busqueda 
        Task<(int totalRegistros, IEnumerable<T>registros)>GetAllAsync(int pageIndex, int pageSize, String search);
        void Add(T entity);
        void AddRange(IEnumerable<T> entites);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}