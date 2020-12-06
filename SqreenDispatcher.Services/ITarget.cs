using SqreenDispatcher.Services.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqreenDispatcher.Services
{
    public interface ITarget
    {
        public Task Notify(IEnumerable<SqreenMessage> messages);
    }
}
