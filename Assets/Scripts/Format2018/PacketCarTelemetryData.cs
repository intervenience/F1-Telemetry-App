using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2018 {
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct PacketCarTelemetryData {
        public PacketHeader header;                // Header
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 20)]
        public CarTelemetryData [] carTelemetryData;   //20

        public UInt32 buttonStatus;         // Bit flags specifying which buttons are being
                                            // pressed currently - see appendices
    }
}