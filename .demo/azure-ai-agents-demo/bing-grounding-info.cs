
// See https://learn.microsoft.com/en-us/azure/ai-services/agents/how-to/tools/bing-code-samples?pivots=csharp#prerequisites for info on connection id
string bingSearchConnectionId = "/subscriptions/f10bc0c5-d71e-453b-a596-00c30c2bc48c/resourceGroups/rg-dadecook/providers/Microsoft.CognitiveServices/accounts/ai-hubdadecook825146559413/projects/ai-hubdadecook825146559-project/connections/bingsearch";

BingGroundingSearchConfiguration bingToolConfiguration = new(bingSearchConnectionId);
BingGroundingSearchToolParameters bingToolParameters = new([bingToolConfiguration]);
