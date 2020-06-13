using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2019 {
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct CarStatusData {
        public byte tractionControl;          // 0 (off) - 2 (high)
        public byte antiLockBrakes;           // 0 (off) - 1 (on)
        public byte fuelMix;                  // Fuel mix - 0 = lean, 1 = standard, 2 = rich, 3 = max
        public byte frontBrakeBias;           // Front brake bias (percentage)
        public byte pitLimiterStatus;         // Pit limiter status - 0 = off, 1 = on
        public float fuelInTank;               // Current fuel mass
        public float fuelCapacity;             // Fuel capacity
		public float fuelRemainingLaps;		   // Fuel remaining in terms of laps (value on MFD)
        public UInt16 maxRPM;                   // Cars max RPM, point of rev limiter
        public UInt16 idleRPM;                  // Cars idle RPM
        public byte maxGears;                 // Maximum number of gears
        public byte drsAllowed;               // 0 = not allowed, 1 = allowed, -1 = unknown
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public byte [] tyresWear;     //4        // Tyre wear percentage
        public byte tyreCompound;             // F1 Modern - 16 = C5, 17 = C4, 18 = C3, 19 = C2, 20 = C1
												// 7 = inter, 8 = wet
												// F1 Classic - 9 = dry, 10 = wet
												// F2 – 11 = super soft, 12 = soft, 13 = medium, 14 = hard
												// 15 = wet
		public byte tyreVisualCompound;			// F1 visual (can be different from actual compound)
												// 16 = soft, 17 = medium, 18 = hard, 7 = inter, 8 = wet
												// F1 Classic – same as above
												// F2 – same as above
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public byte [] tyresDamage; //4          // Tyre damage (percentage)
        public byte frontLeftWingDamage;      // Front left wing damage (percentage)
        public byte frontRightWingDamage;     // Front right wing damage (percentage)
        public byte rearWingDamage;           // Rear wing damage (percentage)
        public byte engineDamage;             // Engine damage (percentage)
        public byte gearBoxDamage;            // Gear box damage (percentage)
        public sbyte vehicleFiaFlags;          // -1 = invalid/unknown, 0 = none, 1 = green
                                               // 2 = blue, 3 = yellow, 4 = red
        public float ersStoreEnergy;           // ERS energy store in Joules
        public byte ersDeployMode;            // ERS deployment mode, 0 = none, 1 = low, 2 = medium
                                              // 3 = high, 4 = overtake, 5 = hotlap
        public float ersHarvestedThisLapMGUK;  // ERS energy harvested this lap by MGU-K
        public float ersHarvestedThisLapMGUH;  // ERS energy harvested this lap by MGU-H
        public float ersDeployedThisLap;       // ERS energy deployed this lap
    }
}