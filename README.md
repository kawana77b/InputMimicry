# InputMimicry

## Description

Mini Library for keyboard and mouse emulation for Windows.

A simple library for emulating devices and acquiring display information.

**Note: This is reinventing the wheel. It is designed for simple purposes and can be useful for needs that need to be easily accomplished. It is generally preferable to look for applications such as PowerToys or other good libraries.**

## Env

Currently TargetPlatform is set as follows:

- .NET 8.0 (Windows Only)
- .NET Framework 4.8

## Install

Packages are distributed through **Github Packages**. Check the `packages` page for details.  
**Personal Access Token configuration is required to install the package**. Please check the following page:  
[Working with the NuGet registry](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry#authenticating-with-a-personal-access-token)

Use `dotnet nuget add source` or add feeds with `Nuget.Config`.  
Please add `https://nuget.pkg.github.com/kawana77b/index.json` to the package source and set your personal token.

Or clone the repository and include the project.

Currently I am not willing to host this project in NuGet.

## Usage

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

## Why did you create this?

Originally, I had a personal old tool program that I replaced with a keyboard operation because I found a certain mouse-click-requiring task tedious.
This library picks up such tool programs and re-edits them.

## Credits

- [dotnet](https://github.com/dotnet/runtime) MIT License - Copyright (c) .NET Foundation and Contributors
- [microsoft/CodeContracts](https://github.com/microsoft/CodeContracts/tree/master) MIT License - Copyright (c) Microsoft Corporation
