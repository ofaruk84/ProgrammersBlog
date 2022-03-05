using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete.EntityFramework.Context;
using ProgrammersBlog.Data.Concrete.EntityFramework.DataAccessLayers;

namespace ProgrammersBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProgrammersBlogContext _context;
        private readonly EfRoleDal _roleDal;
        private readonly EfCommentDal _commentDal;
        private readonly EfUserDal _userDal;
        private readonly EfCategoryDal _categoryDal;
        private readonly EfArticleDal _articleDal;

        public IArticleDal Articles => _articleDal ?? new EfArticleDal(_context);
        public IRoleDal Roles => _roleDal ?? new EfRoleDal(_context);
        public IUserDal Users => _userDal ?? new EfUserDal(_context);
        public ICommentDal Comments => _commentDal ?? new EfCommentDal(_context);
        public ICategoryDal Categories => _categoryDal ?? new EfCategoryDal(_context);

        public UnitOfWork(ProgrammersBlogContext context)
        {
            _context = context;
        }



        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
             await _context.DisposeAsync();
        }

    }
}
