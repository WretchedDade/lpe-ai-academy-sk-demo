	await foreach (ChatMessageContent response in agent.InvokeAsync(userMessage, agentThread))
	{
		// Display the agents's response
		AnsiConsole.MarkupLineInterpolated($"[aqua]> {response.Content}[/]");
	}