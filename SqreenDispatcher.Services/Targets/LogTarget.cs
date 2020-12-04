using SqreenDispatcher.Services.Model;
using System;

namespace SqreenDispatcher.Services.Targets
{
    public class LogTarget : ITarget
    {
        public void Notify(Message message) => throw new NotImplementedException();
    }
}
