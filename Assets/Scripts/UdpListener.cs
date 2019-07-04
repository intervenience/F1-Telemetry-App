using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using Format2018;


public enum PacketID {
    Motion = 0,
    Session = 1,
    LapData = 2,
    Event = 3,
    Participants = 4,
    CarSetups = 5,
    CarTelemetry = 6,
    CarStatus = 7
}

public class UdpListener
{
    int port = 20777;
    UdpClient listener;

    public UdpListener () {
        StartListenerAsync ();
    }

    public async void StartListenerAsync () {
        listener = new UdpClient ();
        IPEndPoint groupEP = new IPEndPoint (IPAddress.Any, port);
        listener.Client.SetSocketOption (SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        listener.ExclusiveAddressUse = false;
        listener.Client.Bind (groupEP);

        while (true) {
            try {
                listener.Client.ReceiveTimeout = 10000;
                UdpReceiveResult udpReceiveResult = await listener.ReceiveAsync ();
                byte [] bytes = udpReceiveResult.Buffer;

                PacketHeader header = new PacketHeader ();
                header = FromArrayToStruct<PacketHeader> (bytes);
                PacketID id = (PacketID) header.packetId;
                Debug.Log (id);
                switch (id) {
                    case PacketID.Motion:
                        var motionPacket = FromArrayToStruct<CarMotionData> (bytes);
                        OnMotionPacketReceived (new MotionPacketReceivedEventArgs(motionPacket));
                        break;
                    case PacketID.Session:
                        var sessionPacket = FromArrayToStruct<PacketSessionData> (bytes);
                        OnSessionPacketReceived (new SessionPacketReceivedEventArgs (sessionPacket));
                        break;
                    case PacketID.LapData:
                        var lapPacket = FromArrayToStruct<PacketLapData> (bytes);
                        OnLapPacketReceived (new LapPacketReceivedEventArgs (lapPacket));
                        break;
                    case PacketID.Event:
                        var eventPacket = FromArrayToStruct<PacketEventData> (bytes);
                        OnEventPacketReceived (new EventPacketReceivedEventArgs (eventPacket));
                        break;
                    case PacketID.Participants:
                        var participantPacket = FromArrayToStruct<PacketParticipantsData> (bytes);
                        OnParticipantPacketReceived (new ParticipantPacketReceivedEventArgs (participantPacket));
                        break;
                    case PacketID.CarSetups:
                        var carSetupsPacket = FromArrayToStruct<PacketCarSetupData> (bytes);
                        OnSetupPacketReceived (new SetupPacketReceivedEventArgs (carSetupsPacket));
                        break;
                    case PacketID.CarTelemetry:
                        var telemetryPacket = FromArrayToStruct<PacketCarTelemetryData> (bytes);
                        OnTelemetryPacketReceived (new TelemetryPacketReceivedEventArgs (telemetryPacket));
                        break;
                    case PacketID.CarStatus:
                        var statusPacket = FromArrayToStruct<PacketCarStatusData> (bytes);
                        OnStatusPacketReceived (new StatusPacketReceivedEventArgs (statusPacket));
                        break;
                }
            } catch (Exception e) {
                throw (e);
            } finally {

            }
        }
    }

    void OnDisable () {
        listener.Dispose ();
        listener.Close ();
    }

    static T FromArrayToStruct<T> (byte[] bytes) {
        GCHandle handle = GCHandle.Alloc (bytes, GCHandleType.Pinned);
        T structure = (T) Marshal.PtrToStructure (handle.AddrOfPinnedObject (), typeof (T));
        handle.Free ();

        return structure;
    }

    #region Events
    public event EventHandler<MotionPacketReceivedEventArgs> MotionPacketReceived;
    public event EventHandler<SessionPacketReceivedEventArgs> SessionPacketReceived;
    public event EventHandler<LapPacketReceivedEventArgs> LapPacketReceived;
    public event EventHandler<EventPacketReceivedEventArgs> EventPacketReceived;
    public event EventHandler<ParticipantPacketReceivedEventArgs> ParticipantPacketReceived;
    public event EventHandler<SetupPacketReceivedEventArgs> SetupPacketReceived;
    public event EventHandler<TelemetryPacketReceivedEventArgs> TelemetryPacketReceived;
    public event EventHandler<StatusPacketReceivedEventArgs> StatusPacketReceived;

    protected virtual void OnMotionPacketReceived (MotionPacketReceivedEventArgs e) {
        MotionPacketReceived?.Invoke (this, e);
    }

    protected virtual void OnSessionPacketReceived (SessionPacketReceivedEventArgs e) {
        SessionPacketReceived?.Invoke (this, e);
    }

    protected virtual void OnLapPacketReceived (LapPacketReceivedEventArgs e) {
        LapPacketReceived?.Invoke (this, e);
    }

    protected virtual void OnEventPacketReceived (EventPacketReceivedEventArgs e) {
        EventPacketReceived?.Invoke (this, e);
    }

    protected virtual void OnParticipantPacketReceived (ParticipantPacketReceivedEventArgs e) {
        ParticipantPacketReceived?.Invoke (this, e);
    }

    protected virtual void OnSetupPacketReceived (SetupPacketReceivedEventArgs e) {
        SetupPacketReceived?.Invoke (this, e);
    }

    protected virtual void OnTelemetryPacketReceived (TelemetryPacketReceivedEventArgs e) {
        TelemetryPacketReceived?.Invoke (this, e);
    }

    protected virtual void OnStatusPacketReceived (StatusPacketReceivedEventArgs e) {
        StatusPacketReceived?.Invoke (this, e);
    }
    #endregion
}

#region EventArgs
public class MotionPacketReceivedEventArgs : EventArgs {
    public MotionPacketReceivedEventArgs (CarMotionData carMotionData) {
        CarMotionData = carMotionData;
    }

    public CarMotionData CarMotionData { get; set; }
}
public class SessionPacketReceivedEventArgs : EventArgs {
    public SessionPacketReceivedEventArgs (PacketSessionData packetSessionData) {
        PacketSessionData = packetSessionData;
    }

    public PacketSessionData PacketSessionData { get; set; }
}
public class LapPacketReceivedEventArgs : EventArgs {
    public LapPacketReceivedEventArgs (PacketLapData packetLapData) {
        PacketLapData = packetLapData;
    }

    public PacketLapData PacketLapData { get; set; }
}
public class EventPacketReceivedEventArgs : EventArgs {
    public EventPacketReceivedEventArgs (PacketEventData packetEventData) {
        PacketEventData = packetEventData;
    }

    public PacketEventData PacketEventData { get; set; }
}
public class ParticipantPacketReceivedEventArgs : EventArgs {
    public ParticipantPacketReceivedEventArgs (PacketParticipantsData packetParticipantsData) {
        PacketParticipantsData = packetParticipantsData;
    }

    public PacketParticipantsData PacketParticipantsData { get; set; }
}
public class SetupPacketReceivedEventArgs : EventArgs {
    public SetupPacketReceivedEventArgs (PacketCarSetupData packetCarSetupData) {
        PacketCarSetupData = packetCarSetupData;
    }

    public PacketCarSetupData PacketCarSetupData { get; set; }
}
public class TelemetryPacketReceivedEventArgs : EventArgs {
    public TelemetryPacketReceivedEventArgs (PacketCarTelemetryData packetCarTelemetryData) {
        PacketCarTelemetryData = packetCarTelemetryData;
    }

    public PacketCarTelemetryData PacketCarTelemetryData { get; set; }
}
public class StatusPacketReceivedEventArgs : EventArgs {
    public StatusPacketReceivedEventArgs (PacketCarStatusData packetCarStatusData) {
        PacketCarStatusData = packetCarStatusData;
    }

    public PacketCarStatusData PacketCarStatusData { get; set; }
}
#endregion
