	// Enable tool calling
	AzureOpenAIPromptExecutionSettings promptExecutionSettings = new() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };

	// Get the response from the chat client
	response = await chatService.GetChatMessageContentAsync(history, promptExecutionSettings, kernel);