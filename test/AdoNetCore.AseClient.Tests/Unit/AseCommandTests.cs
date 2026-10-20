using NUnit.Framework;

namespace AdoNetCore.AseClient.Tests.Unit
{
    public class AseCommandTests
    {
        [Test]
        public void RepeatedDisposal_DoesNotThrow()
        {
            var command = new AseCommand();
            command.Dispose();
            command.Dispose();
        }

        // IDbCommand.CommandType: "The default is Text."
        [Test]
        public void CommandType_DefaultsToText()
        {
            Assert.AreEqual(System.Data.CommandType.Text, new AseCommand().CommandType);
        }
    }
}
