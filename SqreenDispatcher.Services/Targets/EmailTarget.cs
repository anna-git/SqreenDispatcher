using SqreenDispatcher.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqreenDispatcher.Services.Targets
{
    public class EmailTarget : ITarget
    {
        public void Notify(IEnumerable<SqreenMessage> messages) => throw new NotImplementedException();
    }
}
