namespace UnitTests
{
    using ExternalServices.Contract.LLM_Response;

    public class LLMErrorTests
    {
        [Fact]
        public void Properties_WhenNotSet_DefaultToNull()
        {
            var error = new LLMError();

            Assert.Null(error.Code);
            Assert.Null(error.Message);
        }

        [Fact]
        public void Properties_WhenSet_ReturnCorrectValues()
        {
            var error = new LLMError
            {
                Code = "rate_limit_exceeded",
                Message = "You have exceeded the rate limit.",
            };

            Assert.Equal("rate_limit_exceeded", error.Code);
            Assert.Equal("You have exceeded the rate limit.", error.Message);
        }
    }
}
