using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2019 {
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct MarshalZone {
        public float zoneStart;   // Fraction (0..1) of way through the lap the marshal zone starts
        public sbyte zoneFlag;    // -1 = invalid/unknown, 0 = none, 1 = green, 2 = blue, 3 = yellow, 4 = red

    }
}