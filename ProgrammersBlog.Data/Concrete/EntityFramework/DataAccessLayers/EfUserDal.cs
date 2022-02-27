using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Concrete.EntityFramework;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.DataAccessLayers
{
    public class EfUserDal : EfEntityRepositoryBase<User>,IUserDal
    {
        public EfUserDal(DbContext context) : base(context)
        {
        }
    }
}
