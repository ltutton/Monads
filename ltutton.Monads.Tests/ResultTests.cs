using System.Linq;
using NUnit.Framework;

namespace ltutton.Monads.Tests
{
    public class ResultTests
    {
        [Test]
        public void Result_NonGeneric_Ok_SuccessIsReturned()
        {
            var success = Result.Ok();
            Assert.That(success.Success, Is.True);
            Assert.That(success.Message, Is.Null.Or.Empty);
        }

        [Test]
        public void Result_NonGeneric_Fail_FailIsReturned()
        {
            const string failMessage = "Test failure";

            var fail = Result.Fail(failMessage);
            Assert.That(fail.Success, Is.False);
            Assert.That(fail.Message, Is.EqualTo(failMessage));
        }

        [Test]
        public void Result_Generic_Ok_SuccessAndResultIsReturned()
        {
            var success = Result.Ok(5);

            Assert.That(success.Success, Is.True);
            Assert.That(success.Value, Is.EqualTo(5));
            Assert.That(success.Message, Is.Null.Or.Empty);
        }

        [Test]
        public void Result_Generic_Fail_FailIsReturned()
        {
            const string failMessage = "Test failure";

            var fail = Result.Fail<int>(failMessage);
            Assert.That(fail.Success, Is.False);
            Assert.That(fail.Message, Is.EqualTo(failMessage));
            Assert.That(fail.Value, Is.EqualTo(default(int)));
        }

        [Test]
        public void Result_Generic_FailWithValue_FailIsReturned()
        {
            const string failMessage = "Test failure";

            var fail = Result.Fail<int>(failMessage, 45);
            Assert.That(fail.Success, Is.False);
            Assert.That(fail.Message, Is.EqualTo(failMessage));
            Assert.That(fail.Value, Is.EqualTo(45));
        }

        [Test]
        public void Result_Generic_CastToNonGeneric_StillSuccess()
        {            
            var genericSuccess = Result.Ok(5);
            var success = (Result) genericSuccess;

            Assert.That(success.Success, Is.True);
            Assert.That(success.Message, Is.Null.Or.Empty);
        }
        
        [Test]
        public void Result_Generic_CastToNonGeneric_StillFailure()
        {
            const string failMessage = "Test failure";

            var genericFail = Result.Fail<int>(failMessage);
            var fail = (Result) genericFail;
            Assert.That(fail.Success, Is.False);
            Assert.That(fail.Message, Is.EqualTo(failMessage));
        }

        [Test]
        public void Result_NonGeneric_FailIfAnyNoFailures_SuccessesReturned()
        {
            var input = new[]
            {
                Result.Ok(),
                Result.Ok(),
                Result.Ok()
            };

            var result = Result.FailIfAny(input).ToArray();
            Assert.That(result, Is.EquivalentTo(input));
        }

        [Test]
        public void Result_NonGeneric_FailIfAnyHasFailure_FailuresReturned()
        {
            const string failMessage = "Test failure";
            var input = new[]
            {
                Result.Ok(),
                Result.Fail(failMessage),
                Result.Ok()
            };

            var expected = new[]
            {
                Result.Fail(failMessage)
            };

            var result = Result.FailIfAny(input).ToArray();
            Assert.That(result, Is.EquivalentTo(expected));
        }

        
        [Test]
        public void Result_Generic_FailIfAnyNoFailures_SuccessesReturned()
        {
            var input = new[]
            {
                Result.Ok(5),
                Result.Ok(4),
                Result.Ok(3)
            };

            var result = Result.FailIfAny(input).ToArray();
            Assert.That(result, Is.EquivalentTo(input));
        }

        [Test]
        public void Result_Generic_FailIfAnyHasFailure_FailuresReturned()
        {
            const string failMessage = "Test failure";
            var input = new[]
            {
                Result.Ok(5),
                Result.Fail<int>(failMessage),
                Result.Ok(3)
            };

            var expected = new[]
            {
                Result.Fail<int>(failMessage)
            };

            var result = Result.FailIfAny(input).ToArray();
            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}