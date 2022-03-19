using Project.DTO.DTOs;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.GenericRepository.IntRep
{
    public interface IRepository<T> where T : BaseEntity
    {
        //List Commands
        List<T> GetAll();
        List<T> GetActives();
        List<T> GetPassives();
        List<T> GetModifieds();

        //Modification Commands
        void Add(T item);
        void AddRange(List<T> list);
        void Update(T item);
        void UpdateRange(List<T> list);
        void Delete(T item);
        void DeleteRange(List<T> list);
        void Destroy(T item);
        void DestroyRange(List<T> list);

        //Expression Commands
        List<T> Where(Expression<Func<T, bool>> exp);
        bool Any(Expression<Func<T, bool>> exp);
        object Select(Expression<Func<T, object>> exp);
        T FirstOrDefault(Expression<Func<T, bool>> exp);

        IQueryable<X> Select<X>(Expression<Func<T, X>> exp);
        IQueryable<BaseEntityDTO> SelectByDTO(Expression<Func<T, BaseEntityDTO>> exp);

        //Find Command
        T Find(int id);

        //Get Last / First Data
        T GetLastData();
        T GetFirstData();
    }
}
