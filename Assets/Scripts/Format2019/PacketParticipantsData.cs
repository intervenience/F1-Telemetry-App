using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2019 {
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct PacketParticipantsData {
        public PacketHeader header;            // Header

        public byte numCars;           // Number of cars in the data
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 20)]
        public ParticipantData [] participants; //20
    }
}