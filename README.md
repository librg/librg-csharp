# librg-csharp

C# bindings for librg.

## State
Still under heavy contruction.

## Example

```csharp
var ctx = new Librg(Mode.Client, 32, Vector3.one * 5000.0f, 50000);

Debug.Log(ctx.IsClient());

ctx.EventAdd(124, (librg.Event z) => {
    Debug.Log(z.data);
});

ctx.EventTrigger(124, new librg.Event(228));

var foo = new Data();

foo.WriteUInt32(15);
foo.WriteFloat32(99.2424f);

ctx.NetworkStart("localhost", 7777);

while (true) {
    ctx.Tick();
}

```
