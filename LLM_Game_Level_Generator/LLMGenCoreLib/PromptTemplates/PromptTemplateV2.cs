namespace LLMPromptProcessor.PromptTemplates
{
    using LLMGenCoreLib.PromptTemplates;

    public class PromptTemplateV2 : PromptGroundingData
    {
        private const string filePath = "PromptV2.hbs";

        public PromptTemplateV2()
            : base(filePath)
        { }
    }
}
