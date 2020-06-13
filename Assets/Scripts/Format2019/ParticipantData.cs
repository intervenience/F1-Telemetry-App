using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2019 {
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct ParticipantData {
        public byte aiControlled;           // Whether the vehicle is AI (1) or Human (0) controlled
        public byte driverId;               // Driver id - see appendix
        public byte teamId;                 // Team id - see appendix
        public byte raceNumber;             // Race number of the car
        public byte nationality;            // Nationality of the driver
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 48)]
        public char [] name;   //48            // Name of participant in UTF-8 format – null terminated
                               // Will be truncated with … (U+2026) if too long
		public byte telemetrySettings;	// 0 = restricted, 1 = public
    }
}