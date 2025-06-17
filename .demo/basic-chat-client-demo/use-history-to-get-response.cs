	// Add user's message to the chat history
	messages.Add(new(ChatRole.User, userMessage));

	// Get the response from the chat client
	response = await chatClient.GetResponseAsync(messages);

	// Add the assistant's response to the chat history
	messages.Add(new(ChatRole.Assistant, response.Text));