namespace ExternalServices.Contract.LLM_Response.OutputItems
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class OutputMessage : ILLMOutputItem
    {
        private const OutputItemTypes itemType = OutputItemTypes.Message;

        public string Type
        {
            get => field = itemType.ToString().ToLower();
            set
            {
                if (value != itemType.ToString().ToLower())
                {
                    throw new Exception("Type does not match Output Message type");
                }

                field = value;
            }
        }

        public required string Id { get; set; }
    }
}
