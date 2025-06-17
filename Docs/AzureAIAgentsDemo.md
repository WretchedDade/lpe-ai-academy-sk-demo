# Azure AI Agents

Azure AI Agent Service is a comprehensive platform designed to help developers build, deploy, and manage autonomous AI agents that can automate complex business processes. This service integrates various models, tools, and technologies from Microsoft, OpenAI, and other industry-leading partners to provide a robust solution for creating AI agents.

This demo centers around creating an agent from code that uses a Bing Grounding tool to enable the agent to supplement it's knowledge with results from the web. Using the Semantic Kernel libraries allow us to use a familiar agent pattern and would allow us to orchestrate this agent with other agents in the future.

## Final Code Snippet 

Replace the code in `Program.cs` with the snippet below to try this demo!

```csharp
using Azure.AI.Agents.Persistent;
using Azure.Identity;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.AzureAI;
using Microsoft.SemanticKernel.ChatCompletion;
using Spectre.Console;

string aiAgentsEndpoint = "https://ai-hubdadecook825146559413.services.ai.azure.com/api/projects/ai-hubdadecook825146559-project";

// Get the client for retrieving AzureAI Agents
PersistentAgentsClient agentsClient = AzureAIAgent.CreateAgentsClient(aiAgentsEndpoint, new AzureCliCredential());

// See https://learn.microsoft.com/en-us/azure/ai-services/agents/how-to/tools/bing-code-samples?pivots=csharp#prerequisites for info on connection id
string bingSearchConnectionId = "/subscriptions/f10bc0c5-d71e-453b-a596-00c30c2bc48c/resourceGroups/rg-dadecook/providers/Microsoft.CognitiveServices/accounts/ai-hubdadecook825146559413/projects/ai-hubdadecook825146559-project/connections/bingsearch";

BingGroundingSearchConfiguration bingToolConfiguration = new(bingSearchConnectionId);
BingGroundingSearchToolParameters bingToolParameters = new([bingToolConfiguration]);

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

AzureAIAgentThread agentThread = new(agentsClient);

try
{
    while (true)
    {
        // Get input from the user
        var userMessage = AnsiConsole.Ask<string>("> ");

        // Check if the user wants to exit
        if (userMessage.Equals("exit", StringComparison.OrdinalIgnoreCase))
        {
            break;
        }

        ChatMessageContent message = new(AuthorRole.User, userMessage);
        await foreach (ChatMessageContent response in searchAgent.InvokeAsync(message, agentThread))
        {
            // Display the agents's response
            AnsiConsole.MarkupLineInterpolated($"[aqua]> {response.Content}[/]");

            var annotations = response.Items.OfType<AnnotationContent>().ToList();

            if (annotations.Count != 0)
            {
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[yellow]Annotations:[/]");

                foreach (AnnotationContent annotation in annotations)
                {
                    AnsiConsole.MarkupLineInterpolated($" [green]{annotation.Label} {annotation.Title}[/]");
                }

                AnsiConsole.WriteLine();
            }
        }
    }
}
finally
{
    await agentThread.DeleteAsync();
    await agentsClient.Administration.DeleteAgentAsync(searchAgent.Id);
}
```