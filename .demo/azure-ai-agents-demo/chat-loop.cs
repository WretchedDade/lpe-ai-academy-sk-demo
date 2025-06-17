
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