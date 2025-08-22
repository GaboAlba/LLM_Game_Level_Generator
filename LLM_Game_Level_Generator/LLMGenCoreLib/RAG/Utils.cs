namespace LLM_Game_Level_Generator.RAG
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Utils
    {
        public static List<string> ChunkText(string text, int chunkSize)
        {
            var chunks = new List<string>();
            var index = 0;
            var length = text.Length;
            while (index < length)
            {
                var chunk = text.Substring(index, chunkSize);
                chunks.Append(chunk);
                index += chunkSize;
            }

            return chunks;
        }
    }
}
