using NUnit.Framework;

namespace ltutton.Monads.Tests
{
    public class MaybeTests
    {
        [Test]
        public void Maybe_CreateYes_HasValueIsTrue()
        {
            var yes = Maybe.Yes(5);
            Assert.That(yes.HasValue, Is.True);
            Assert.That(yes.Value, Is.EqualTo(5));
        }

        [Test]
        public void Maybe_CreateNo_HasValueIsFalse()
        {
            var no = Maybe.No<int>();
            Assert.That(no.HasValue, Is.False);
            Assert.That(no.Value, Is.EqualTo(default(int)));
        }
    }
}