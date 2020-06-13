using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2019 {

    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct PacketEventData {
        public PacketHeader header;               // Header
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public byte [] eventStringCode;  //4 // Event string code, see above
		public EventDataDetails eventData;
    }
	[StructLayout (LayoutKind.Explicit)]
	public struct EventDataDetails {
		public struct FastestLap {
			public byte vehicleId;
			public float lapTime;
		}
		
		public struct Retirement {
			public byte vehicleId;
		}
		
		public struct TeamMateInPits {
			public byte vehicleId;
		}
		
		public struct RaceWinner {
			public byte vehicleId;
		}
	}
}