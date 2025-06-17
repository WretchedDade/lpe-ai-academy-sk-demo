using System.ComponentModel;
using Azure.Identity;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Spectre.Console;

string endpoint = "https://ai-hubdadecook825146559413.openai.azure.com/";

IKernelBuilder kernelBuilder = Kernel.CreateBuilder()
   .AddAzureOpenAIChatCompletion("gpt-4.1-mini", endpoint, new AzureCliCredential(), serviceId: "chat");

Kernel kernel = kernelBuilder.Build();

// Get the chat service from the kernel's service provider
IChatCompletionService chatService = kernel.GetRequiredService<IChatCompletionService>("chat");

ChatMessageContent response;
ChatHistory history = [];

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
    history.AddUserMessage(userMessage);

	// Get the response from the chat client
	response = await chatService.GetChatMessageContentAsync(history);

	// Add the assistant's response to the chat history
    history.AddAssistantMessage(response.Content ?? "");

	// Display the assistant's response
	AnsiConsole.MarkupLineInterpolated($"[aqua]> {response.Content}[/]");
}
