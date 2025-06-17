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
