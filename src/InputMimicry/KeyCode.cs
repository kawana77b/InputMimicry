﻿#pragma warning disable CS1591

// The source of this file is referenced from the following URL:
// https://github.com/microsoft/CodeContracts/blob/master/Microsoft.Research/Contracts/System.Windows.Forms/System.Windows.Forms.Keys.cs

// ------- Original Source License Indication -----------
// CodeContracts
//
// Copyright (c) Microsoft Corporation
//
// All rights reserved.
//
// MIT License
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// -------------------------------------------------------

namespace InputMimicry
{
    /// <summary>
    /// Indicates the key code. The value of this enumerator constant is the same as in System.Windows.Forms.Keys
    /// </summary>
    /// <remarks>https://github.com/microsoft/CodeContracts/blob/master/Microsoft.Research/Contracts/System.Windows.Forms/System.Windows.Forms.Keys.cs</remarks>
    public enum KeyCode
    {
        /// <summary>
        /// No key code
        /// </summary>
        None = 0,

        Backspace = 8,
        Tab = 9,
        LineFeed = 10,
        Clear = 12,
        Enter = 13,
        Return = 13,
        Shift = 16,
        Ctrl = 17,
        Alt = 18,
        Pause = 19,
        CapsLock = 20,

        KanaMode = 21,
        HanguelMode = 21,
        HangulMode = 21,
        JunjaMode = 23,
        FinalMode = 24,
        KanjiMode = 25,
        HanjaMode = 25,

        Escape = 27,

        IMEConvert = 28,
        IMENonconvert = 29,
        IMEAceept = 30,
        IMEModeChange = 31,

        Space = 32,

        PageUp = 33,
        PageDown = 34,
        End = 35,
        Home = 36,

        Left = 37,
        Up = 38,
        Right = 39,
        Down = 40,

        Select = 41,
        Print = 42,
        Execute = 43,
        PrintScreen = 44,
        Insert = 45,
        Delete = 46,
        Help = 47,

        D0 = 48,
        D1 = 49,
        D2 = 50,
        D3 = 51,
        D4 = 52,
        D5 = 53,
        D6 = 54,
        D7 = 55,
        D8 = 56,
        D9 = 57,

        A = 65,
        B = 66,
        C = 67,
        D = 68,
        E = 69,
        F = 70,
        G = 71,
        H = 72,
        I = 73,
        J = 74,
        K = 75,
        L = 76,
        M = 77,
        N = 78,
        O = 79,
        P = 80,
        Q = 81,
        R = 82,
        S = 83,
        T = 84,
        U = 85,
        V = 86,
        W = 87,
        X = 88,
        Y = 89,
        Z = 90,

        LWin = 91,
        RWin = 92,
        Apps = 93,
        Sleep = 95,

        NumPad0 = 96,
        NumPad1 = 97,
        NumPad2 = 98,
        NumPad3 = 99,
        NumPad4 = 100,
        NumPad5 = 101,
        NumPad6 = 102,
        NumPad7 = 103,
        NumPad8 = 104,
        NumPad9 = 105,

        Multiply = 106,
        Add = 107,
        Separator = 108,
        Subtract = 109,
        Decimal = 110,
        Divide = 111,

        F1 = 112,
        F2 = 113,
        F3 = 114,
        F4 = 115,
        F5 = 116,
        F6 = 117,
        F7 = 118,
        F8 = 119,
        F9 = 120,
        F10 = 121,
        F11 = 122,
        F12 = 123,
        F13 = 124,
        F14 = 125,
        F15 = 126,
        F16 = 127,
        F17 = 128,
        F18 = 129,
        F19 = 130,
        F20 = 131,
        F21 = 132,
        F22 = 133,
        F23 = 134,
        F24 = 135,

        NumLock = 144,

        Scroll = 145,
        LShift = 160,
        RShift = 161,
        LCtrl = 162,
        RCtrl = 163,
        LAlt = 164,
        RAlt = 165,

        BrowserBack = 166,
        BrowserForward = 167,
        BrowserRefresh = 168,
        BrowserStop = 169,
        BrowserSearch = 170,
        BrowserFavorites = 171,
        BrowserHome = 172,

        VolumeMute = 173,
        VolumeDown = 174,
        VolumeUp = 175,

        MediaNextTrack = 176,
        MediaPreviousTrack = 177,
        MediaStop = 178,
        MediaPlayPause = 179,

        LaunchMail = 180,
        SelectMedia = 181,
        LaunchApplication1 = 182,
        LaunchApplication2 = 183,

        Oem1 = 186,
        OemSemicolon = 186,
        Oemplus = 187,
        Oemcomma = 188,
        OemMinus = 189,
        OemPeriod = 190,
        OemQuestion = 191,
        Oem2 = 191,
        Oemtilde = 192,
        Oem3 = 192,
        Oem4 = 219,
        OemOpenBrackets = 219,
        OemPipe = 220,
        Oem5 = 220,
        Oem6 = 221,
        OemCloseBrackets = 221,
        Oem7 = 222,
        OemQuotes = 222,
        Oem8 = 223,
        Oem102 = 226,
        OemBackslash = 226,
        ProcessKey = 229,
        Packet = 231,

        Attn = 246,
        Crsel = 247,
        Exsel = 248,
        EraseEof = 249,
        Play = 250,
        Zoom = 251,
        NoName = 252,
        Pa1 = 253,
        OemClear = 254,
    }
}

#pragma warning restore CS1591