namespace LLMPromptProcessor.PromptTemplates
{
    using LLMGenCoreLib.PromptTemplates;

    public class PromptTemplateV1 : PromptGroundingData
    {
        private const string filePath = "PromptV1.hbs";

        public PromptTemplateV1()
            : base(filePath)
        { }
    }
}
