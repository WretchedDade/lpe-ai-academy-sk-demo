
// Enable tool calling
AzureOpenAIPromptExecutionSettings settings = new() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };

// Create agent definition for the LightingAgent
ChatCompletionAgent agent =
	new()
	{
		Name = "LightingAgent",
		Instructions = """
			You are an agent designed to manage the smart lighting system in a home. 
			can perform various tasks related to the lights, such as adding, removing, listing, and updating lights.
			can also retrieve the current state of the lights and their colors.

			When adding a light:
			- If a user does not specify a color, you should default to "white"
			- If a user does not specify a state, you should default to "off"
		""",
		Kernel = kernel,
		Arguments = new KernelArguments(settings)
	};
