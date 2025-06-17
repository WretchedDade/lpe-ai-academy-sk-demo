# LPE AI Academy SK Demo

This is project was put together to demonstrate how to use Semantic Kernel to develop AI solutions. It covers various topics like chat completions, embedding generation, tool calling, and agents. To keep things simple, most demos only modify or rely on the `Program.cs` file. We can do this because of the [top level statements](https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/top-level-statements) feature introduced in .Net 6.

To be able run any of the code, please ensure that you have gone through the [prerequisites](Docs/Prerequisites.md) documentation. You will need some Azure resources, the .Net CLI, and some way to authenticate with Azure locally using the `DefaultAzureCredential()`.

## Demos

You can learn more about each demo and see the final code snippet in their corresponding doc pages:

- [Basic Chat Client Demo (Start Here)](Docs/BasicChatClientDemo.md)
- [Basic Tool Demo](Docs/BasicToolDemo.md)
- [Basic Agent Demo](Docs/BasicAgentDemo.md)
- [Azure AI Agents Demo](Docs/AzureAIAgentsDemo.md)
- [Basic Embedding Demo](Docs/BasicEmbeddingDemo.md)