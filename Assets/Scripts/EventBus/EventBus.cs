using System.Collections.Generic;
using System.Reflection;
using System;

public static class EventBus<T> where T : IEvent
{
	static readonly HashSet<IEventBinding<T>> bindings = new();

	public static void Register(EventBinding<T> binding) => bindings.Add(binding);
    public static void Deregister(EventBinding<T> binding) => bindings.Remove(binding);

	public static void Raise(T @event)
	{
		foreach (var binding in bindings)
		{
			binding.OnEvent.Invoke(@event);
			binding.OnEventNoArgs.Invoke();
		}
	}
}

public static class PredefinedAssemblyUtil
{
	enum AssemblyType
	{
		AssemblyCSharp,
		AssemblyCSharpEditor,
        AssemblyCSharpFirstPass,
        AssemblyCSharpEditorFirstPass,
    }

	static AssemblyType? GetAssemblyType(string assemblyName)
	{
		return assemblyName switch
		{
			"Assembly-CSharp" => AssemblyType.AssemblyCSharp,
            "Assembly-CSharp-Editor" => AssemblyType.AssemblyCSharpEditor,
            "Assembly-CSharp-firstpass" => AssemblyType.AssemblyCSharpFirstPass,
            "Assembly-CSharp-Editor-firstpass" => AssemblyType.AssemblyCSharpEditorFirstPass,
			_ => null
        };
	}

	private static void AddTypesFromAssembly(Type[] assembly, ICollection<Type> types, Type interfaceType)
	{
		if (assembly == null)
			return;

		for (int i = 0; i < assembly.Length; i++)
		{
			Type type = assembly[i];
			if (type != interfaceType && interfaceType.IsAssignableFrom(type)) 
			{ 
				types.Add(type); 
			}
		}
	}

	public static List<Type> GetTypes(Type interfaceTypes)
	{
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

		Dictionary<AssemblyType, Type[]> assemblyTypes = new();
		List<Type> types = new();

		for (int i = 0; i < assemblies.Length; i++)
		{
			AssemblyType? assemblyType = GetAssemblyType(assemblies[i].GetName().Name);

			if (assemblyType != null)
			{
				assemblyTypes.Add((AssemblyType) assemblyType, assemblies[i].GetTypes());
			}
		}

		AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharp], types, interfaceTypes);
        AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharpFirstPass], types, interfaceTypes);

        return types;
	}
}