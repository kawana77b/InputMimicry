# Usage

```csharp
using InputMimicry;

var display = new Display();
// Get display size
Console.WriteLine(display.Width);
Console.WriteLine(display.Height);
// Get screenshot and pixel color
using var bmp = display.GetBitmap();
bmp.Save("test.png");
var color = display.GetColor(0, 0);

// Emulate mouse
var mouse = new Mouse();
await mouse.SetPositionAsync(0, 0);
await mouse.MoveAsync(0, 100);
await mouse.LeftClickAsync();

// Async operations accept an optional CancellationToken
using var cts = new CancellationTokenSource();
await mouse.LeftDoubleClickAsync(cts.Token);

// Emulate keyboard
var keyboard = new Keyboard();
await keyboard.PushAsync(KeyCode.A);

// Receive keyboard input
// The creating thread must run a message loop (e.g. a WinForms/WPF UI thread).
using var receiver = new KeyboardReceiver();
receiver.KeyDown += (sender, e) =>
{
    // Keep handlers short. Keys sent by Keyboard are ignored by default (IgnoreInjected).
    Console.WriteLine(e.KeyCode);
};
```

`Mouse` and `Keyboard` throw `Win32Exception` when the input cannot be sent.
