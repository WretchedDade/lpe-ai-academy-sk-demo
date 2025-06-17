using Azure.Identity;
using Microsoft.SemanticKernel;

string endpoint = "https://ai-hubdadecook825146559413.openai.azure.com/";

IKernelBuilder kernelBuilder = Kernel.CreateBuilder()
   .AddAzureOpenAIChatClient("gpt-4.1-mini", endpoint, new AzureCliCredential(), serviceId: "chat");

Kernel kernel = kernelBuilder.Build();
