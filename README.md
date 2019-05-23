# NConsole.Options

*NConsole.Options* is a command line arguments parser for C#. See [ndesk.org](http://www.ndesk.org/Options) for background reading informing this project.

## Overview

Regardless of which language features you decide to support, your option callbacks are invoked when the parser encounters those pattern matches.

Let's assume you have the following application bits you intend to support:

```C#
var verbose = 0;
var showHelp = false;
var debugMode = false;
var names = new List<string>();
var pairs = new Dictionary<Guid, int>();
```

*NConsole.Options* makes it possible to leverage [C# version 3.0](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-30), such as [initializer lists](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers) as well as [lambda expressions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions) in order to furnish short, concise option specifications.

```C#
var p = new OptionSet {
    {"v|verbose",  v => verbose += (v == null ? 0 : 1)
    , {"h|?|help", v => showHelp = v != null}
    , {"n|name=",  v => names.Add(v)},
};
```

[C# version 2.0](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-20) is also fully supported:

```C#
var p = new OptionSet()
    .Add("v|verbose", delegate (string v) { verbose += (v == null ? 0 : 1); })
    .Add("h|?|help",  delegate (string v) { showHelp = v != null; })
    .Add("n|name=",   delegate (string v) { names.Add(v); })
    ;
```

## Additional Features

For brevity, from now on, we will illustrate the initializer list examples.

### Valueless Option Specifications

In addition to value oriented option patterns, we also provide support for *valueless* matches. In this case, you simply provide a parameterless callback.

```C#
var p = new OptionSet {{"d|debug", () => debugMode = true}};
```

### Key/Value Pair Option Specifications

Last but not least, we provide support for *key/value pair* matches. In this case, you provide the *key* and *value* oriented callback.

```C#
var p = new OptionSet {{"p|pair", (Guid key, int value) => pairs[key] = value}};
```


## Invoking the Parser

Assuming you have ``string[] args``

To invoke the parser, simply:

```C#
IEnumerable<string> unprocessedArgs = p.Parse(args);
```

Parsing will incrementally invoke your option specifications as they are encountered. It will also yield a collection of *unprocessed arguments* which failed to match either the specified option prototypes or be consumed as option parameters.

## Value and Key/Value Paired Arguments

You may specify value and key/value paired arguments in several ways.

```
--myarg:value
--myarg=value
--myarg value
--myarg:key,value
--myarg=key,value
--myarg:key value
--myarg=key value
--myarg key value
```

## Boolean Flag Considerations

Boolean flags may be specially described in your arguments in several ways.

```
--myflag=true
--myflag=false
--myflag:true
--myflag:false
--myflag true
--myflag false
```

Additionally, you may specify a *Boolean* shorthand.

```
--myflag+
--myflag-
```

Use the ``+`` marker to indicate ``true``, and the ``-`` marker to indicate ``false``.

### Boolean Flags in Key Value Pairs

Shorthand may also be used to inform key/value paired *Boolean* values. We make the assumption that the shorthand always informs the *value* side of the pair.

```
--myflag=key+
--myflag=key-
--myflag:key+
--myflag:key-
--myflag+ key
--myflag- key
```

The last couple of arguments may look a little strange, but we are assuming that the shorthand marker informs the *value*.
