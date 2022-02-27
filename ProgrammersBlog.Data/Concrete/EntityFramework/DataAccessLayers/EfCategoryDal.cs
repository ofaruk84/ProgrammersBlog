using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Concrete.EntityFramework;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.DataAccessLayers
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category>,ICategoryDal
    {
        public EfCategoryDal(DbContext context) : base(context)
        {
        }
    }
}
