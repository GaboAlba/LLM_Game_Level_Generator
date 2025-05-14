import LLMClient
import openai

class OpenAIClient(LLMClient):  
  def _init_(self, api_key : str, model : str):
    self.allowed_models = openai.models.list()
    super()._init_(api_key, model)
    self.client = openai.OpenAI(api_key= self.api_key, model = self.model)