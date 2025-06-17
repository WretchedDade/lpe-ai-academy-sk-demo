# Basic Tool Demo

Tools serve as the backbone to a lot of the experiences users expect when conversing with an LLM today. They are used to enable RAG scenarios and give LLMs the ability to take actions. In this demo, we highlight the value of tools by creating a plugin for managing the lights in a smart home. This plugin lets the user create, read, update, and delete lights in their home and lets the LLM keep track and manage it for the user based on their natural language requests.

## Final Code Snippet 

Replace the code in `Program.cs` with the snippet below to try this demo!

```csharp
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

// Import the LightsPlugin into the kernel
kernel.ImportPluginFromType<LightsPlugin>();

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

	// Enable tool calling
	AzureOpenAIPromptExecutionSettings promptExecutionSettings = new() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };

	// Get the response from the chat client
	response = await chatService.GetChatMessageContentAsync(history, promptExecutionSettings, kernel);

	// Add the assistant's response to the chat history
    history.AddAssistantMessage(response.Content ?? "");

	// Display the assistant's response
	AnsiConsole.MarkupLineInterpolated($"[aqua]> {response.Content}[/]");
}

public class Light
{
	public required string Name { get; set; }
	public required string Color { get; set; }
	public LightState State { get; set; }
}

public enum LightState
{
	Off,
	On
}

public class LightsPlugin
{
	private List<Light> lights = [new() { Name = "Porch Light", Color = "Blue", State = LightState.On }];

	[KernelFunction, Description("Adds a light source to the scene.")]
	public void AddLight(string name, string color, LightState state)
	{
		lights.Add(new() { Name = name, Color = color, State = state });
	}

	[KernelFunction, Description("Tries to remove a light source from the scene by name. Returning true if successful, false otherwise.")]
	public bool TryRemoveLight(string name)
	{
		var lightToRemove = lights.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

		if (lightToRemove != null)
		{
			lights.Remove(lightToRemove);
		}

		return lightToRemove != null;
	}

	[KernelFunction, Description("Lists all light sources in the scene.")]
	public List<Light> ListLights()
	{
		return lights;
	}

	[KernelFunction, Description("Gets a light source by name.")]
	public Light? GetLight(string name)
	{
		return lights.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
	}

	[KernelFunction, Description("Updates the state of a light source by name.")]
	public bool UpdateLightState(string name, LightState state)
	{
		var light = lights.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

		if (light != null)
		{
			light.State = state;
			return true;
		}

		return false;
	}

	[KernelFunction, Description("Updates the color of a light source by name.")]
	public bool UpdateLightColor(string name, string color)
	{
		var light = lights.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

		if (light != null)
		{
			light.Color = color;
			return true;
		}

		return false;
	}

	[KernelFunction, Description("Updates the name of a light source.")]
	public bool UpdateLightName(string oldName, string newName)
	{
		var light = lights.FirstOrDefault(l => l.Name.Equals(oldName, StringComparison.OrdinalIgnoreCase));

		if (light != null)
		{
			light.Name = newName;
			return true;
		}

		return false;
	}

	[KernelFunction, Description("Clears all light sources from the scene.")]
	public void ClearLights()
	{
		lights.Clear();
	}
}
```