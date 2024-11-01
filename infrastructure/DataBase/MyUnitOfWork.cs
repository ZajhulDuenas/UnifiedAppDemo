using EFCore.BulkExtensions;
using interfaces.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.DataBase
{

    public class MyUnitOfWork(JdvExamenContext context) : BaseUnitOfWork<JdvExamenContext>(context), IMyUnitOfWork
    {
       
    }
}
