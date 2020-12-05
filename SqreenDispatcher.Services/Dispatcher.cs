using SqreenDispatcher.Services.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqreenDispatcher.Services
{
    public class Dispatcher
    {
        private readonly IEnumerable<ITarget> _targets;

        public Dispatcher(IEnumerable<ITarget> targets) => _targets = targets;

        public Task Dispatch(SqreenMessage[] messages) => Task.Run(() => Parallel.ForEach(_targets, target =>
                                                                    {
                                                                        target.Notify(messages);
                                                                    }));

    }
}
