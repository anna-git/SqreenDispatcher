using SqreenDispatcher.Services.Model;
using System.Collections.Generic;

namespace SqreenDispatcher.Services
{
    public class Dispatcher
    {
        private readonly IEnumerable<ITarget> _targets;

        public Dispatcher(IEnumerable<ITarget> targets) => _targets = targets;

        public void Dispatch(Message message)
        {
            foreach (var target in _targets)
                target.Notify(message);
        }
    }
}
