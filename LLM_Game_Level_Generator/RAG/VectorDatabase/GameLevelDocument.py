import json
from dataclasses import dataclass

@dataclass
class GameLevelDocument:

  embedding : list[float]
  path : str
  input : str = None
  output : str = None
  content : dict[str, str] = None

  # Methods
  def read_content(self) -> str:
    with open(self.path, 'r', encoding='utf-8') as file:
      fullContent = file.read()
      self.content = json.loads(fullContent)
      self.input = self.content.get("input", "")
      self.output = self.content.get("output", "")
      return self.input + "\n" + self.output
