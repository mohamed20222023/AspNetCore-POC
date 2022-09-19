using Demo.BL.Models;
using Demo.DAL.Entity;

namespace Demo.BL.Interface
{
    public interface IDepartmentRep
    {

        Task<IEnumerable<Department>> GetAsync() ;
        Task<Department> GetByIdAsync( int id) ;
        Task CreateAsync(Department obj) ;
        Task UpdateAsync(Department obj) ;
        Task RemoveAsync( int id) ;

    }
}
