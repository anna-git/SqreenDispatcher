using GeekLearning.Domain;
using Moq;
using SqreenDispatcher.Services;
using SqreenDispatcher.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SqreenDispatcher.Tests
{
    public class DispatcherTests
    {
        [Fact]
        public void TestDispatch()
        {
            var mock1 = new Mock<ITarget>();
            var mock2 = new Mock<ITarget>();
            var mock3 = new Mock<ITarget>();
            var message = new SqreenMessage
            {
                ApiVersion = "3.1.2",
                Message = new Message { EventId="3ud"}
            };
            var dispatcher = new Dispatcher(new List<ITarget> { mock1.Object, mock2.Object, mock3.Object });
            IEnumerable<SqreenMessage> messages = message.Yield();
            dispatcher.Dispatch(messages);
            mock1.Verify(m => m.Notify(messages), Times.Once);
            mock2.Verify(m => m.Notify(messages), Times.Once);
            mock3.Verify(m => m.Notify(messages), Times.Once);
        }
    }
}
