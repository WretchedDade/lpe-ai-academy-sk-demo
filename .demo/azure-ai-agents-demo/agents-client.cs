
string aiAgentsEndpoint = "https://ai-hubdadecook825146559413.services.ai.azure.com/api/projects/ai-hubdadecook825146559-project";

// Get the client for retrieving AzureAI Agents
PersistentAgentsClient agentsClient = AzureAIAgent.CreateAgentsClient(aiAgentsEndpoint, new AzureCliCredential());
