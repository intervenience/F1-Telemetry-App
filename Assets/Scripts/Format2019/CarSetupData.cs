using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2019 {
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct CarSetupData {
        public byte frontWing;                // Front wing aero
        public byte rearWing;                 // Rear wing aero
        public byte onThrottle;               // Differential adjustment on throttle (percentage)
        public byte offThrottle;              // Differential adjustment off throttle (percentage)
        public float frontCamber;              // Front camber angle (suspension geometry)
        public float rearCamber;               // Rear camber angle (suspension geometry)
        public float frontToe;                 // Front toe angle (suspension geometry)
        public float rearToe;                  // Rear toe angle (suspension geometry)
        public byte frontSuspension;          // Front suspension
        public byte rearSuspension;           // Rear suspension
        public byte frontAntiRollBar;         // Front anti-roll bar
        public byte rearAntiRollBar;          // Front anti-roll bar
        public byte frontSuspensionHeight;    // Front ride height
        public byte rearSuspensionHeight;     // Rear ride height
        public byte brakePressure;            // Brake pressure (percentage)
        public byte brakeBias;                // Brake bias (percentage)
        public float frontTyrePressure;        // Front tyre pressure (PSI)
        public float rearTyrePressure;         // Rear tyre pressure (PSI)
        public byte ballast;                  // Ballast
        public float fuelLoad;                 // Fuel load
    }
}