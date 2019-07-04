using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2018 {
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct LapData {
        public float lastLapTime;           // Last lap time in seconds
        public float currentLapTime;        // Current time around the lap in seconds
        public float bestLapTime;           // Best lap time of the session in seconds
        public float sector1Time;           // Sector 1 time in seconds
        public float sector2Time;           // Sector 2 time in seconds
        public float lapDistance;           // Distance vehicle is around current lap in metres – could
                                            // be negative if line hasn’t been crossed yet
        public float totalDistance;         // Total distance travelled in session in metres – could
                                            // be negative if line hasn’t been crossed yet
        public float safetyCarDelta;        // Delta in seconds for safety car
        public byte carPosition;           // Car race position
        public byte currentLapNum;         // Current lap number
        public byte pitStatus;             // 0 = none, 1 = pitting, 2 = in pit area
        public byte sector;                // 0 = sector1, 1 = sector2, 2 = sector3
        public byte currentLapInvalid;     // Current lap invalid - 0 = valid, 1 = invalid
        public byte penalties;             // Accumulated time penalties in seconds to be added
        public byte gridPosition;          // Grid position the vehicle started the race in
        public byte driverStatus;          // Status of driver - 0 = in garage, 1 = flying lap
                                           // 2 = in lap, 3 = out lap, 4 = on track
        public byte resultStatus;          // Result status - 0 = invalid, 1 = inactive, 2 = active
                                           // 3 = finished, 4 = disqualified, 5 = not classified
                                           // 6 = retired
    }
}