
ChatResponse response;

while (true)
{
	// Get input from the user
	var userMessage = AnsiConsole.Ask<string>("> ");

	// Check if the user wants to exit
	if (userMessage.Equals("exit", StringComparison.OrdinalIgnoreCase))
	{
		break;
	}

	// Get the response from the chat client
	response = await chatClient.GetResponseAsync(userMessage);

	// Display the assistant's response
	AnsiConsole.MarkupLineInterpolated($"[aqua]> {response.Text}[/]");
}
