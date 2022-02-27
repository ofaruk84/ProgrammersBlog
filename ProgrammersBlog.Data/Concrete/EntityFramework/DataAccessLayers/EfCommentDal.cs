using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Concrete.EntityFramework;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.DataAccessLayers
{
    public class EfCommentDal : EfEntityRepositoryBase<Comment>,ICommentDal
    {
        public EfCommentDal(DbContext context) : base(context)
        {
        }
    }
}
