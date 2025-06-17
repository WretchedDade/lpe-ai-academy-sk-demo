# Basic Chat Client Demo

## Semantic Kernel

Semantic Kernel is a lightweight, open-source development kit that lets you easily build AI agents and integrate the latest AI models into your C#, Python, or Java codebase. It serves as an efficient middleware that enables rapid delivery of enterprise-grade solutions.

![Diagram showing how semantic kernel is the nucleus that brings models, plugins, and custom code together.](<Semantic Kernel.png>)

## Demo Overview

This demo serves as an introduction to semantic kernel and demonstrates a basic use case of having a conversation with an LLM.

## Final Code Snippet 

Replace the code in `Program.cs` with the snippet below to try this demo!

```csharp
using Azure.Identity;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Spectre.Console;

string endpoint = "https://ai-hubdadecook825146559413.openai.azure.com/";

IKernelBuilder kernelBuilder = Kernel.CreateBuilder()
   .AddAzureOpenAIChatClient("gpt-4.1-mini", endpoint, new AzureCliCredential(), serviceId: "chat");

Kernel kernel = kernelBuilder.Build();

// Get the chat client from the kernel's service provider
IChatClient chatClient = kernel.GetRequiredService<IChatClient>("chat");

ChatResponse response;
List<ChatMessage> messages = [];

while (true)
{
	// Get input from the user
	var userMessage = AnsiConsole.Ask<string>("> ");

	// Check if the user wants to exit
	if (userMessage.Equals("exit", StringComparison.OrdinalIgnoreCase))
	{
		break;
	}

	// Add user's message to the chat history
	messages.Add(new(ChatRole.User, userMessage));

	// Get the response from the chat client
	response = await chatClient.GetResponseAsync(messages);

	// Add the assistant's response to the chat history
	messages.Add(new(ChatRole.Assistant, response.Text));

	// Display the assistant's response
	AnsiConsole.MarkupLineInterpolated($"[aqua]> {response.Text}[/]");
}
```