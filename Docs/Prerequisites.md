# Prerequisites

## Azure Resources

An Azure subscription is required for these demos. I will be using an IC subscription in the FDPO tenant. Instructions for creating one of these can be found in the [WWL Engineering Hub](https://github.com/mcaps-microsoft/wwl-engineering-hub/blob/main/onboarding/step-3-setup-azure-ic-subscription.md).

### Azure AI Foundry Project (Azure Open AI)

In your Azure subscription, you will need an Azure AI Foundry Hub and Project (formerly Azure Open AI) with a couple of models deployed to it. Create the following deployments:

- gpt-4.1-mini
- text-embedding-3-large

### Bing Grounding

One of the demos makes use of an Azure AI Agent with a connected [Grounding with Bing Search resource](https://learn.microsoft.com/en-us/azure/ai-foundry/agents/how-to/tools/bing-code-samples?pivots=portal). 

You will also need to find the resources connection id to be able to reference it in the code. Here is an example of what mine looks like:

```
/subscriptions/f10bc0c5-d71e-453b-a596-00c30c2bc48c/resourceGroups/rg-dadecook/providers/Microsoft.CognitiveServices/accounts/ai-hubdadecook825146559413/projects/ai-hubdadecook825146559-project/connections/bingsearch
```

## Coding

To be able to run this code locally, you will need the [.Net 9 SDK](https://dotnet.microsoft.com/en-us/download) and a way to authenticate your Azure account for getting tokens. I use the Azure CLI because it easily allows me to switch subscriptions/tenants.

### Setting up the Azure CLI

To use the Azure CLI for authentication, make sure you have it installed by opening a terminal and running `winget install --exact --id Microsoft.AzureCLI`. This will run the installer using [Windows Package Manager](https://learn.microsoft.com/en-us/windows/package-manager/winget/). Alternatively, you follow instructions to install the Azure CLI [here](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli-windows?view=azure-cli-latest&pivots=winget).

With the CLI install, you can run the `az login` command in a terminal. A popup will appear to ask you to pick an account to log in as. Once you are logged in, a list of subscriptions will appear in the terminal. Enter the number corresponding to the subscription you wish to use and your auth will be set up!

### Running the code

With the .Net 9 SDK downloaded, you can run the code in the `Program.cs` file by opening a terminal at the root of the project and running the `dotnet run` command. This will build the project and run it.

## Running the Demos (Optional)

If you want to be able to step through the demos as I did, you can do so using the [Demo Time](https://demotime.show/) VS Code Extension. Just open this project in a VS Code instance with the extension installed and execute a demo.