using System.Numerics.Tensors;
using Azure.Identity;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Spectre.Console;

string endpoint = "https://ai-hubdadecook825146559413.openai.azure.com/";

IKernelBuilder kernelBuilder = Kernel.CreateBuilder()
	.AddAzureOpenAIEmbeddingGenerator("text-embedding-3-large", endpoint, new AzureCliCredential(), serviceId: "embedding");

Kernel kernel = kernelBuilder.Build();

// Get the embedding generator from the kernel's service provider
var embeddingGenerator = kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();

Embedding<float> source = await embeddingGenerator.GenerateAsync("Semantic Kernel is a framework for building AI applications.");
Embedding<float> query1 = await embeddingGenerator.GenerateAsync("Semantic Kernel");
Embedding<float> query2 = await embeddingGenerator.GenerateAsync("Azure OpenAI");

// Calculate the cosine similarity between the source and queries
var similarity1 = TensorPrimitives.CosineSimilarity<float>(source.Vector.Span, query1.Vector.Span);
var similarity2 = TensorPrimitives.CosineSimilarity<float>(source.Vector.Span, query2.Vector.Span);

AnsiConsole.MarkupLineInterpolated($"[aqua]> The source is 'Semantic Kernel is a framework for building AI applications.'[/]");
AnsiConsole.MarkupLineInterpolated($"[aqua]> The similarity of 'Semantic Kernel' to the source is: {similarity1}[/]");
AnsiConsole.MarkupLineInterpolated($"[aqua]> The similarity of 'Azure OpenAI' to the source is: {similarity2}[/]");