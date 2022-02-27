using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Abstract
{
    public abstract class EntityBase
    {
        public virtual  int Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual  bool IsActive { get; set; }
        public virtual string CreatedByName { get; set; } = "Admin";
        public virtual string ModifiedByName { get; set; } = "Admin";
        public string Note { get; set; }
    }
}
