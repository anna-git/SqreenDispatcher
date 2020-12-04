using SqreenDispatcher.Services.Model;
using System;

namespace SqreenDispatcher.Services.Targets
{
    public class DatabaseTarget : ITarget
    {
        public void Notify(SqreenMessage message) => throw new NotImplementedException();
    }
}
