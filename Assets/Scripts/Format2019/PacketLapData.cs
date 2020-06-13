using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2019 {

    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct PacketLapData {
        public PacketHeader header;              // Header
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 20)]
        public LapData [] lapData; //20        // Lap data for all cars on track
    }
}