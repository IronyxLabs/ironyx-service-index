using System.Reflection;
using Xunit.Sdk;

namespace Ironyx.ServiceIndex.Test.Unit.Attributes
{
    public class EmptyInlineDataAttribute : DataAttribute
    {
        private readonly IEnumerable<object[]> _data = [
            [null!],
            [string.Empty],
            ["       "]
        ];

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return _data;
        }
    }
}
