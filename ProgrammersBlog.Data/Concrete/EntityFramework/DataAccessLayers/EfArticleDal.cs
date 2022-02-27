using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Concrete.EntityFramework;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.DataAccessLayers
{
    public class EfArticleDal : EfEntityRepositoryBase<Article>,IArticleDal
    {
        public EfArticleDal(DbContext context) : base(context)
        {
        }
    }
}
