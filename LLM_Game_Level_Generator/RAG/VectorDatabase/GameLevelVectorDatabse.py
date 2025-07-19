import json
import os
from dataclasses import dataclass
from VectorDatabase.GameLevelDocument import GameLevelDocument
import RAG.Utils as utils
import logging
import uuid

@dataclass
class GameLevelVectorDatabase:

  VectorList: list[dict[uuid.UUID, GameLevelDocument]] = None
  DatabaseRootPath: str = "LLM_Game_Level_Generator\RAG\VectorDatabase"
  DatabaseFileName: str = "GameLevelVectorDatabase.json"
  FilesPath: str = "LLM_Game_Level_Generator\RAG\GameLevelExemplars"

  def fetch_vector_list(self) -> list[dict[int, GameLevelDocument]]:
    if not os.path.exists(self.DatabaseRootPath):
      os.makedirs(self.DatabaseRootPath)

    database_file_path = os.path.join(self.DatabaseRootPath, self.DatabaseFileName)
    if os.path.exists(database_file_path):
      with open(database_file_path, 'r', encoding='utf-8') as file:
        self.VectorList = json.load(file)
    else:
      self.create_vector_list()
      
    return self.VectorList

  def create_vector_list(self):
    self.VectorList = []
    database_file_path = os.path.join(self.DatabaseRootPath, self.DatabaseFileName)
    
    # In the case of video game levels, we can assume that
    # every file will be it's own chunk, as the complete context is necessary
    # for cohesion an understanding by the LLM.
    for file in os.listdir(self.FilesPath):
      if not file.endswith(".json"):
        logging.warning(f"Skipping non-JSON file: {file}")
      else:
        document = GameLevelDocument(embedding=[], path=os.path.join(self.FilesPath, file))
        _ = document.read_content()
        
        # Only the input will be used for the embedding to capture only the user intent instead 
        # of the LLM output, which is not relevant for the vector database.
        if not document.input:
          logging.error(f"Skipping file with no input content: {file}")
          continue
        embedding = utils.get_embeddings(utils.get_embedding_model(), document.input)
        document.embedding = embedding

        # Add the document to the vector list with its id
        self.VectorList.append({uuid.uuid4() : document})
    # Save the vector list to the database file
    with open(database_file_path, 'w', encoding='utf-8') as file:
      json.dump(self.VectorList, file, indent=2)

  def get_similarity_scores(self, query: str, number_of_exemplars_to_extract: int = 10) -> list[tuple[uuid.UUID, float]]:
    if not self.VectorList:
      self.fetch_vector_list()

    query_embedding = utils.get_embeddings(utils.get_embedding_model(), query)
    similarity_scores = []

    for vector in self.VectorList:
      for doc_id, document in vector.items():
        score = utils.cosine_similarity(query_embedding, document.embedding)
        similarity_scores.append((doc_id, score))

    # Sort by score in descending order
    similarity_scores.sort(key=lambda x: x[1], reverse=True)
    return similarity_scores[:number_of_exemplars_to_extract]
  
  def get_exemplars(self, list_of_scores: list[tuple[uuid.UUID, float]]) -> str:
    exemplars = []
    for doc_id, score in list_of_scores:
      for vector in self.VectorList:
        if doc_id in vector:
          document = vector[doc_id]
          exemplars.append(f"Input: {document.input}\nOutput: {document.output}\n")
          break
    return "\n".join(exemplars)
  
  def get_exemplars_list(self, list_of_scores: list[tuple[uuid.UUID, float]]) -> list[GameLevelDocument]:
    exemplars = []
    for doc_id, score in list_of_scores:
      for vector in self.VectorList:
        if doc_id in vector:
          document = vector[doc_id]
          exemplars.append(document)
          break
    return exemplars
  
  def get_exemplars(self, query: str, number_of_exemplars_to_extract: int = 10) -> str:
    if not self.VectorList:
      self.fetch_vector_list()

    list_of_scores = self.get_similarity_scores(query, number_of_exemplars_to_extract)
    if not list_of_scores:
      return "No relevant exemplars found."

    exemplar = self.get_exemplars(list_of_scores)
    return exemplar
  
  def get_exemplars_list(self, query: str, number_of_exemplars_to_extract: int = 10) -> list[GameLevelDocument]:
    if not self.VectorList:
      self.fetch_vector_list()

    list_of_scores = self.get_similarity_scores(query, number_of_exemplars_to_extract)
    if not list_of_scores:
      return []

    exemplars = self.get_exemplars_list(list_of_scores)
    return exemplars


  


    
