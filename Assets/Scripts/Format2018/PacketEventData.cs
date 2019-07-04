using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2018 {

    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct PacketEventData {
        public PacketHeader header;               // Header
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public byte [] eventStringCode;  //4 // Event string code, see above
    }
}