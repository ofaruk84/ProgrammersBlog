using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Concrete.EntityFramework;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.DataAccessLayers
{
    public class EfRoleDal : EfEntityRepositoryBase<Role>,IRoleDal
    {
        public EfRoleDal(DbContext context) : base(context)
        {
        }
    }
}
