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
await mouse.LeftClickAsync();

// Emulate keyboard
var keyboard = new Keyboard();
await keyboard.PushAsync(KeyCode.A);

// Receive keyboard input
using var receiver = new KeyboardReceiver();
receiver.KeyDown += (sender, e) =>
{
    Console.WriteLine(e.KeyCode);
};
```
