using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2018 {
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct CarMotionData {

        public float worldPositionX;           // World space X position
        public float worldPositionY;           // World space Y position
        public float worldPositionZ;           // World space Z position
        public float worldVelocityX;           // Velocity in world space X
        public float worldVelocityY;           // Velocity in world space Y
        public float worldVelocityZ;           // Velocity in world space Z
        public UInt16 worldForwardDirX;         // World space forward X direction (normalised)
        public UInt16 worldForwardDirY;         // World space forward Y direction (normalised)
        public UInt16 worldForwardDirZ;         // World space forward Z direction (normalised)
        public UInt16 worldRightDirX;           // World space right X direction (normalised)
        public UInt16 worldRightDirY;           // World space right Y direction (normalised)
        public UInt16 worldRightDirZ;           // World space right Z direction (normalised)
        public float gForceLateral;            // Lateral G-Force component
        public float gForceLongitudinal;       // Longitudinal G-Force component
        public float gForceVertical;           // Vertical G-Force component
        public float yaw;                      // Yaw angle in radians
        public float pitch;                    // Pitch angle in radians
        public float roll;                     // Roll angle in radians
    }
}
