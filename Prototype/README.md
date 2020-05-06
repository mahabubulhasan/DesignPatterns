## ICloneable Is Bad 
The .NET Framework comes with an interface called `ICloneable`. This interface has a single method, `Clone()` , but this method is ill-specified: the documentation does not suggest whether this should be a shallow copy or a deep copy. Also, the name of the method, `Clone`, does not really help here because we don’t know exactly what cloning does. The typical implementation of `ICloneable` for a type (say, `Person`) is something like this:

```C#
public class Person : ICloneable
{
	// members as before     
	public Person Clone()
	{
		return (Person)MemberwiseClone();
	}
}
```
The method `Object.MemberwiseClone()` is a protected method of Object, so it is automatically inherited by every single reference type. It creates a shallow copy of the `object`. In other words, if you were to implement it on `Address` and `Person` in our example, you would run into the following problem:

```C#
var john = new Person(" John Smith", new Address(" London Road", 123)); 

var jane = john.Clone(); 
jane.Name = "Jane Smith"; // John's name DID NOT change (good!) 
jane.Address.HouseNumber = 321; // John's address changed :(
```

This helped, but not a lot. Even though the `name` is now assigned correctly, `john` and `jane` now share an `Address` reference— it was simply copied over, so they both point to the same `Address`. Shallow copy is therefore not for us: we want deep copying, recursive copying of all of an object’s members and the construction of shiny new counterpart objects, each initialized with identical data.

## Binary Serialization Caveats
Binary serialization requires every class to be marked with `[Serializable]`, otherwise the serializer simply throws an exception (not a good thing). So, if we wanted to use this approach on an existing set of classes, we might go with a different approach that doesn’t require the aforementioned attribute. For example, you could use Extensible Markup Language (XML) serialization instead:

```C#
public static T DeepCopyXml<T>(this T self)
{
	using (var ms = new MemoryStream())
	{
		XmlSerializer s = new XmlSerializer(typeof(T));

		s.Serialize(ms, self);
		ms.Position = 0;
		return (T)s.Deserialize(ms);
	}
}
```
