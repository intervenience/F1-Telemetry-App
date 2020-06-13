using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Format2019 {
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct PacketMotionData {
        [MarshalAs (UnmanagedType.Struct)]
        public PacketHeader m_header;               // Header

        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 20)]
        public CarMotionData [] carMotionData; //20    // Data for all cars on track

        // Extra player car ONLY data
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public float [] suspensionPosition; //4       // Note: All wheel arrays have the following order:
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public float [] suspensionVelocity;  //4      // RL, RR, FL, FR
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public float [] suspensionAcceleration; //4   // RL, RR, FL, FR
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public float [] wheelSpeed;  //4              // Speed of each wheel
        [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
        public float [] wheelSlip; //4               // Slip ratio for each wheel
        public float localVelocityX;              // Velocity in local space
        public float localVelocityY;              // Velocity in local space
        public float localVelocityZ;              // Velocity in local space
        public float angularVelocityX;            // Angular velocity x-component
        public float angularVelocityY;            // Angular velocity y-component
        public float angularVelocityZ;            // Angular velocity z-component
        public float angularAccelerationX;        // Angular velocity x-component
        public float angularAccelerationY;        // Angular velocity y-component
        public float angularAccelerationZ;        // Angular velocity z-component
        public float frontWheelsAngle;            // Current front wheels angle in radians
    }

}