
// Calculate the cosine similarity between the source and queries
var similarity1 = TensorPrimitives.CosineSimilarity<float>(source.Vector.Span, query1.Vector.Span);
var similarity2 = TensorPrimitives.CosineSimilarity<float>(source.Vector.Span, query2.Vector.Span);

AnsiConsole.MarkupLineInterpolated($"[aqua]> The source is 'Semantic Kernel is a framework for building AI applications.'[/]");
AnsiConsole.MarkupLineInterpolated($"[aqua]> The similarity of 'Semantic Kernel' to the source is: {similarity1}[/]");
AnsiConsole.MarkupLineInterpolated($"[aqua]> The similarity of 'Azure OpenAI' to the source is: {similarity2}[/]");