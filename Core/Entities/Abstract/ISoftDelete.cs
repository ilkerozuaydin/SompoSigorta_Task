using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Abstract
{
   public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
