## ICloneable Is Bad 
The .NET Framework comes with an interface called `ICloneable`. This interface has a single method, `Clone()` , but this method is ill-specified: the documentation does not suggest whether this should be a shallow copy or a deep copy. Also, the name of the method, `Clone`, does not really help here because we don’t know exactly what cloning does. The typical implementation of `ICloneable` for a type (say, `Person`) is something like this:

```C#
public class Person : ICloneable {    
	// members as before     
	public Person Clone()    
	{         
		return (Person) MemberwiseClone();
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

