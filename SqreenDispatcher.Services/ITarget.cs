using SqreenDispatcher.Services.Model;
using System.Collections.Generic;

namespace SqreenDispatcher.Services
{
    public interface ITarget
    {
        public void Notify(IEnumerable<SqreenMessage> message);
    }
}
