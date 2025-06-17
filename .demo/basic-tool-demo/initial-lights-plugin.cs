
public class Light
{
	public required string Name { get; set; }
	public required string Color { get; set; }
	public LightState State { get; set; }
}

public enum LightState
{
	Off,
	On
}

public class LightsPlugin
{
	private List<Light> lights = [new() { Name = "Porch Light", Color = "Blue", State = LightState.On }];

	[KernelFunction, Description("Adds a light source to the scene.")]
	public void AddLight(string name, string color, LightState state)
	{
		lights.Add(new() { Name = name, Color = color, State = state });
	}
	
}