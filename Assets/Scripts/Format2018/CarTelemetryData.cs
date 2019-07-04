using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2018 {
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct CarTelemetryData {
        public UInt16 speed;                      // Speed of car in kilometres per hour
        public byte throttle;                   // Amount of throttle applied (0 to 100)
        public sbyte steer;                      // Steering (-100 (full lock left) to 100 (full lock right))
        public byte brake;                      // Amount of brake applied (0 to 100)
        public byte clutch;                     // Amount of clutch applied (0 to 100)
        public sbyte gear;                       // Gear selected (1-8, N=0, R=-1)
        public UInt16 engineRPM;                  // Engine RPM
        public byte drs;                        // 0 = off, 1 = on
        public byte revLightsPercent;           // Rev lights indicator (percentage)
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public UInt16 [] brakesTemperature;    //4   // Brakes temperature (celsius)
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public UInt16 [] tyresSurfaceTemperature;//4 // Tyres surface temperature (celsius)
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public UInt16 [] tyresInnerTemperature; //4  // Tyres inner temperature (celsius)
        public UInt16 engineTemperature;          // Engine temperature (celsius)
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public float [] tyresPressure;   //4        // Tyres pressure (PSI)
    }
}