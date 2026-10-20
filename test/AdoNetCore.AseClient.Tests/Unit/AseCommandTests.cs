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

        // IDbCommand.ExecuteNonQuery, Exceptions: InvalidOperationException, "The connection does not exist. -or- The connection is not open."
        [Test]
        public void Execute_WithoutAnOpenConnection_ThrowsInvalidOperation()
        {
            var noConnection = new AseCommand { CommandText = "select 1" };
            Assert.Throws<System.InvalidOperationException>(() => noConnection.ExecuteNonQuery());
            Assert.Throws<System.InvalidOperationException>(() => noConnection.ExecuteReader());
            Assert.Throws<System.InvalidOperationException>(() => noConnection.ExecuteScalar());

            var closed = new AseCommand("select 1", new AseConnection("Data Source=localhost;Port=5000;Uid=sa;Pwd=;"));
            Assert.Throws<System.InvalidOperationException>(() => closed.ExecuteNonQuery());
        }

        // IDbCommand.CommandType: "The default is Text."
        [Test]
        public void CommandType_DefaultsToText()
        {
            Assert.AreEqual(System.Data.CommandType.Text, new AseCommand().CommandType);
        }
    }
}
