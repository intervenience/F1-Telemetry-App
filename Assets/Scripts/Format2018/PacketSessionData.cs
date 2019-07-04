using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2018 {
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct PacketSessionData {
        public PacketHeader m_header;                   // Header

        public byte weather;                // Weather - 0 = clear, 1 = light cloud, 2 = overcast
                                            // 3 = light rain, 4 = heavy rain, 5 = storm
        public sbyte trackTemperature;      // Track temp. in degrees celsius
        public sbyte airTemperature;        // Air temp. in degrees celsius
        public byte totalLaps;              // Total number of laps in this race
        public UInt16 trackLength;              // Track length in metres
        public byte sessionType;            // 0 = unknown, 1 = P1, 2 = P2, 3 = P3, 4 = Short P
                                            // 5 = Q1, 6 = Q2, 7 = Q3, 8 = Short Q, 9 = OSQ
                                            // 10 = R, 11 = R2, 12 = Time Trial
        public sbyte trackId;               // -1 for unknown, 0-21 for tracks, see appendix
        public byte era;                    // Era, 0 = modern, 1 = classic
        public UInt16 sessionTimeLeft;      // Time left in session in seconds
        public sbyte sessionDuration;       // Session duration in seconds
        public byte pitSpeedLimit;          // Pit speed limit in kilometres per hour
        public byte gamePaused;               // Whether the game is paused
        public byte isSpectating;           // Whether the player is spectating
        public byte spectatorCarIndex;      // Index of the car being spectated
        public byte sliProNativeSupport;    // SLI Pro support, 0 = inactive, 1 = active
        public byte numMarshalZones;            // Number of marshal zones to follow
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 21)]
        public MarshalZone [] marshalZones; //21        // List of marshal zones – max 21
        public byte safetyCarStatus;          // 0 = no safety car, 1 = full safety car
                                              // 2 = virtual safety car
        public byte networkGame;              // 0 = offline, 1 = online
    }
}