
using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace lpe_ai_academy_sk_demo.Plugins;

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

	[KernelFunction, Description("Tries to remove a light source from the scene by name. Returning true if successful, false otherwise.")]
	public bool TryRemoveLight(string name)
	{
		var lightToRemove = lights.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

		if (lightToRemove != null)
			lights.Remove(lightToRemove);

		return lightToRemove != null;
	}

	[KernelFunction, Description("Lists all light sources in the scene.")]
	public List<Light> ListLights()
	{
		return lights;
	}

	[KernelFunction, Description("Gets a light source by name.")]
	public Light? GetLight(string name)
	{
		return lights.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
	}

	[KernelFunction, Description("Updates the state of a light source by name.")]
	public bool UpdateLightState(string name, LightState state)
	{
		var light = lights.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

		if (light != null)
		{
			light.State = state;
			return true;
		}

		return false;
	}

	[KernelFunction, Description("Updates the color of a light source by name.")]
	public bool UpdateLightColor(string name, string color)
	{
		var light = lights.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

		if (light != null)
		{
			light.Color = color;
			return true;
		}

		return false;
	}

	[KernelFunction, Description("Updates the name of a light source.")]
	public bool UpdateLightName(string oldName, string newName)
	{
		var light = lights.FirstOrDefault(l => l.Name.Equals(oldName, StringComparison.OrdinalIgnoreCase));

		if (light != null)
		{
			light.Name = newName;
			return true;
		}

		return false;
	}

	[KernelFunction, Description("Clears all light sources from the scene.")]
	public void ClearLights()
	{
		lights.Clear();
	}
}