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

        // IDbCommand.Prepare, Exceptions: InvalidOperationException, "The Connection is not set. -or- The Connection is not Open()."
        [Test]
        public void Prepare_WithoutAnOpenConnection_ThrowsInvalidOperation()
        {
            Assert.Throws<System.InvalidOperationException>(() => new AseCommand("select 1").Prepare());

            var closed = new AseCommand("select 1", new AseConnection("Data Source=localhost;Port=5000;Uid=sa;Pwd=;"));
            Assert.Throws<System.InvalidOperationException>(() => closed.Prepare());
        }

        // IDbCommand.CommandType: "The default is Text."
        [Test]
        public void CommandType_DefaultsToText()
        {
            Assert.AreEqual(System.Data.CommandType.Text, new AseCommand().CommandType);
        }
    }
}
