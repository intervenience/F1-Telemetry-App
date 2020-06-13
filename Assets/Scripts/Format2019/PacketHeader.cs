using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2019 {
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct PacketHeader {
        public UInt16 packetFormat;       // 2019
		public byte gameMajorVersion;	  // X.00
		public byte gameMinorVersion;	  // 1.XX
        public byte packetVersion;        // Version of this packet type, all start from 1
        public byte packetId;             // Identifier for the packet type, see below
        public UInt64 sessionUID;         // Unique identifier for the session
        public float sessionTime;         // Session timestamp
        public uint frameIdentifier;      // Identifier for the frame the data was retrieved on
        public byte playerCarIndex;       // Index of player's car in the array
    }
}