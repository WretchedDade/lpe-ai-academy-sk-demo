
// Create the definition for the search agent in Azure AI Agents
PersistentAgent searchAgentDefinition = await agentsClient.Administration.CreateAgentAsync(
    model: "gpt-4.1-mini",
    name: "SearchAgent",
    instructions: "You are an agent who tries to answer questions for users. You should always ground your searches with bing.",
    description: "Answers general questions by searching for relevant information with Bing.",
    tools: [new BingGroundingToolDefinition(bingToolParameters)]
);

// Build a Semantic Kernel agent from the definition and the agents client
AzureAIAgent searchAgent = new(searchAgentDefinition, agentsClient);
