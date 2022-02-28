using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProgrammersBlog.Data.Abstract
{ 
    public interface IUnitOfWork : IAsyncDisposable
    {
        IArticleDal Articles { get; }
        IRoleDal Roles { get; }
        IUserDal Users { get; }
        ICommentDal Comments { get;}
        ICategoryDal Categories { get;}

        Task<int> SaveAsync();
    }
}
