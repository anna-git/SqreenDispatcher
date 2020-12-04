using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqreenDispatcher.Services.Targets
{
    public class TargetsConsts
    {
        public static IDictionary<string, Type> targetTypes = new Dictionary<string, Type>
        {
            {"email",  typeof(EmailTarget)},
            {"log",  typeof(LogTarget)},
            {"database",  typeof(DatabaseTarget)},
        };
    }
}
