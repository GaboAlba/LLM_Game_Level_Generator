namespace LLMPromptProcessor.PromptTemplates
{
    public class OptimizerPromptTemplateV1 : OptimizerPromptTemplateBase
    {
        private const string filePath = "OptimizerPromptV1.hbs";

        public OptimizerPromptTemplateV1()
            : base(filePath)
        { }
    }
}
