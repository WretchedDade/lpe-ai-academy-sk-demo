using lpe_ai_academy_sk_demo.Plugins;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;

namespace lpe_ai_academy_sk_demo.Agents;

internal static class LightingAgent
{
    public static ChatCompletionAgent Create(Kernel baseKernel)
    {
        // Create a clone to avoid modifying the original kernel
        Kernel kernel = baseKernel.Clone();

        // Import the LightsPlugin into the kernel
        kernel.ImportPluginFromType<LightsPlugin>();

        // Enable tool calling
        AzureOpenAIPromptExecutionSettings settings = new() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };

        // Create agent definition for the LightingAgent
        ChatCompletionAgent lightingAgent =
            new()
            {
                Name = "LightingAgent",
                Description = "Manages the smart lighting system in a home. Can add, remove, list, and update lights, as well as retrieve their current state and colors.",
                Instructions = """
                    You are an agent designed to manage the smart lighting system in a home. 
                    You can perform various tasks related to the lights, such as adding, removing, listing, and updating lights.
                    You can also retrieve the current state of the lights and their colors.
                    
                    When adding a light:
                    - If a user does not specify a color, you should default to "white"
                    - If a user does not specify a state, you should default to "off"
                """,
                Kernel = kernel,
                Arguments = new KernelArguments(settings)
            };

        return lightingAgent;
    }
}
