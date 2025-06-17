
Embedding<float> source = await embeddingGenerator.GenerateAsync("Semantic Kernel is a framework for building AI applications.");
Embedding<float> query1 = await embeddingGenerator.GenerateAsync("Semantic Kernel");
Embedding<float> query2 = await embeddingGenerator.GenerateAsync("Azure OpenAI");
