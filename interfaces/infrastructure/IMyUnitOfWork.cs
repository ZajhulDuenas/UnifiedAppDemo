using interfaces.infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interfaces.DataBase
{
  
    public interface IMyUnitOfWork : EntityFrameworkCore.UnitOfWork.Interfaces.IUnitOfWork, IRepositoryBulk
    {
    }
}
