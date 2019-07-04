using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using UnityEngine;
using UnityEngine.UI;

using F1Telemetry.Models.Raw.F12018;
using F1Telemetry.Events;
using F1Telemetry.Manager;
using Format2018;

//https://forums.codemasters.com/topic/38920-f1-2019-udp-specification/
public class Telemetry : MonoBehaviour
{
    [Header ("Set up")]
    public Text helpText;
    public GameObject initialScreen, telemetryScreen;

    [Header ("Box and DRS")]
    public GameObject box;
    public GameObject drs;
    public Image drsAvailable;
    [Header ("Fuel and ERS")]
    public GameObject fuelMode, deployment;
    [Header ("Tyres")]
    public GameObject [] temps, wear;
    [Header ("Flag")]
    public GameObject flag;
    public Image flagStatus;

    public GameObject vsc, sc, pit;

    [Header ("Text")]
    public Text gear, revs, speed, bb, diff, delta, deploy, flTT, frTT, rlTT, rrTT, flTW, frTW, rlTW, rrTW, fuelMargin, trackPos, tyre, currentLap, fuelModeText;
    [Header ("Battery")]
    public Slider battery, available, harvested;

    F1Telemetry.Manager.TelemetryRecorder tr;
    F1Telemetry.Manager.TelemetryManager tm;
    F1Telemetry.Manager.F1Manager f1Manager;
    UdpListener udpListener;

    Format2018.CarStatusData c;
    Format2018.PacketCarTelemetryData ctd;
    Format2018.PacketLapData l;
    Format2018.PacketSessionData psd;
    Format2018.PacketCarSetupData cs;
    //F1Telemetry.Models.Raw.F12018.CarStatusData c;
    //F1Telemetry.Models.Raw.F12018.CarTelemetryData ctd;
    //F1Telemetry.Models.Raw.F12018.LapData l;
    //F1Telemetry.Models.Raw.F12018.PacketSessionData psd;
    //F1Telemetry.Models.Raw.F12018.CarSetupData cs;

    float maxERS = 0;
    float maxHarvest = 0;
    float currentTotalBattery = 0;

    bool ready = false;

    void Start () {
#if (!UNITY_WEBGL)
        helpText.text = string.Format ("Your local IP address is {0}\n" +
            "In F1 2018, ensure you've disabled UDP broadcast, and set the target IP address to the IP above.\n" +
            "The higher the frequency of data transmission, the more accurate the values will be on this screen.\n" +
            "This will still be limited by the refresh rate of your device.", GetLocalIP ());
#endif
        
        initialScreen.SetActive (true);
        telemetryScreen.SetActive (false);

        //not much point running this higher than 60, information updates only come
#if (UNITY_STANDALONE || UNITY_EDITOR)
            Application.targetFrameRate = 60;
            Application.runInBackground = true;
#endif

        //lower frame rate on mobile devices, it's unnecessary to run at higher fps.
#if (UNITY_ANDROID || UNITY_IOS)
            Application.targetFrameRate = 45;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif

        box.SetActive (false);
        drs.SetActive (false);
        flag.SetActive (false);
        flagStatus.color = Color.green;
        vsc.SetActive (false);
        sc.SetActive (false);
        pit.SetActive (false);
    }

    void OnDisable () {
        /*f1Manager.SessionPacketReceived -= F1Manager_SessionPacketReceived;
        f1Manager.LapPacketReceived -= F1Manager_LapPacketReceived;
        f1Manager.CarTelemetryReceived -= F1Manager_CarTelemetryReceived;
        f1Manager.CarStatusReceived -= F1Manager_CarStatusReceived;
        tm.CarSetupPacketReceived -= Tm_CarSetupPacketReceived;*/
        Debug.Log ("On disable");
        udpListener.SessionPacketReceived -= UdpListener_SessionPacketReceived;
        udpListener.StatusPacketReceived -= UdpListener_StatusPacketReceived;
    }

    public void StartTelemetryButtonPress () {
        Debug.Log ("Started");
        initialScreen.SetActive (false);
        telemetryScreen.SetActive (true);

        /*tr = new TelemetryRecorder ();
        tm = new TelemetryManager (tr);
        f1Manager = new F1Manager (tm);
        Debug.Log ("Update interval " + f1Manager.UpdateInterval);
        f1Manager.CarStatusReceived += F1Manager_CarStatusReceived;
        Debug.Log ("Car status set up");
        f1Manager.SessionPacketReceived += F1Manager_SessionPacketReceived;
        f1Manager.LapPacketReceived += F1Manager_LapPacketReceived;
        f1Manager.CarTelemetryReceived += F1Manager_CarTelemetryReceived;
        
        tm.CarSetupPacketReceived += Tm_CarSetupPacketReceived;*/
        udpListener = new UdpListener ();
        udpListener.SessionPacketReceived += UdpListener_SessionPacketReceived;
        udpListener.StatusPacketReceived += UdpListener_StatusPacketReceived;
        ready = true;
    }

    private void UdpListener_StatusPacketReceived (object sender, StatusPacketReceivedEventArgs e) {
        Debug.Log ("Status packet received");
        c = e.PacketCarStatusData.carStatusData [e.PacketCarStatusData.header.playerCarIndex];
    }

    private void UdpListener_SessionPacketReceived (object sender, SessionPacketReceivedEventArgs e) {
        //Debug.Log ("Session packet received");
    }

    //ideally we wouldn't put all these calls in update, but unity doesn't like updates to the UI from other threads so we have to do this in an update function
    void Update () {
        try {
            available.value = available.maxValue - c.ersDeployedThisLap;
            harvested.value = c.ersHarvestedThisLapMGUH + c.ersHarvestedThisLapMGUK;
            //battery.value = c.ERSStoreEnergy;
            deploy.text = c.ersDeployMode.ToString ();
            //not working
            if (maxERS == 0) {
                maxERS = c.ersStoreEnergy * 4;
                available.maxValue = c.ersStoreEnergy;
                battery.maxValue = maxERS;
                currentTotalBattery = maxERS;
            }
            if (maxHarvest == 0) {
                harvested.maxValue = c.ersStoreEnergy;
            }

            //not working
            currentTotalBattery = (currentTotalBattery - c.ersDeployedThisLap) + c.ersHarvestedThisLapMGUH + c.ersHarvestedThisLapMGUK;
            battery.value = currentTotalBattery;

            if (c.pitLimiterStatus == 1) {
                pit.SetActive (true);
            } else {
                pit.SetActive (false);
            }

            //not working
            uint bias = c.frontBrakeBias;
            bb.text = bias.ToString () + "%";

            //not working - changed
            fuelMargin.text = c.fuelInTank.ToString ();
            //not working
            fuelModeText.text = c.fuelMix.ToString ();

            //not working - changed
            if (wear [0].activeSelf && c.tyresWear != null) {
                flTW.text = c.tyresWear [2].ToString ();
                frTW.text = c.tyresWear [3].ToString ();
                rlTW.text = c.tyresWear [0].ToString ();
                rrTW.text = c.tyresWear [1].ToString ();
            }
        } catch (Exception exception) {
            //Debug.LogError (exception.Source);
            throw (exception);
        }

        /*if (ready) {
            //working
            switch (c.DRSAllowed) {
                case 0:
                default:
                    if (ctd.DRS == 1) {
                        drs.SetActive (true);
                    } else {
                        drs.SetActive (false);
                    }
                    break;
                case 1:
                    drs.SetActive (true);
                    break;
            }
          
            //works
            speed.text = ctd.Speed.ToString ();
            
            //works
            try {
                if (temps [0].activeSelf && ctd.TyresSurfaceTemperature != null) {
                    flTT.text = ctd.TyresSurfaceTemperature [2].ToString ();
                    frTT.text = ctd.TyresSurfaceTemperature [3].ToString ();
                    rlTT.text = ctd.TyresSurfaceTemperature [0].ToString ();
                    rrTT.text = ctd.TyresSurfaceTemperature [1].ToString ();
                }
            } catch (Exception exception) {
                Debug.LogError ("Couldn't get tyre temperatures: " + exception.Message);
            }
            
            //working
            try {
                short drsOn = ctd.DRS;
                switch (drsOn) {
                    case 0:
                        drsAvailable.color = Color.red;
                        break;
                    case 1:
                        drsAvailable.color = Color.green;
                        break;
                }
            } catch (Exception exception) {
                Debug.LogError (exception.Source);
            }
            
            //working
            revs.text = ctd.EngineRpm.ToString ();

            //working
            gear.text = ctd.Gear.ToString ().Substring (ctd.Gear.ToString ().Length - 1);            

            delta.text = l.CurrentLapTime.ToString ();
            //working
            uint pos = l.CarPosition;
            trackPos.text = pos.ToString ();
            //working
            currentLap.text = l.CurrentLapNum.ToString ();

            switch (psd.SafetyCarStatus) {
                case SafetyCarStatus.NoSafetyCar:
                default:
                    vsc.SetActive (false);
                    sc.SetActive (false);
                    break;
                case SafetyCarStatus.FullSafetyCar:
                    vsc.SetActive (false);
                    sc.SetActive (true);
                    break;
                case SafetyCarStatus.VirtualSafetyCar:
                    vsc.SetActive (true);
                    sc.SetActive (false);
                    break;
            }
            
            //differential-on is the value that players change in game, so we show that.
            byte diffON = cs.OnThrottle;
            diff.text = diffON.ToString () + "%";

            try {
                available.value = available.maxValue - c.ERSDeployedThisLap;
                harvested.value = c.ERSHarvestedThisLapMGUH + c.ERSHarvestedThisLapMGUK;
                //battery.value = c.ERSStoreEnergy;
                deploy.text = c.ERSDeployMode.ToString ();
                //not working
                if (maxERS == 0) {
                    maxERS = c.ERSStoreEnergy * 4;
                    available.maxValue = c.ERSStoreEnergy;
                    battery.maxValue = maxERS;
                    currentTotalBattery = maxERS;
                }
                if (maxHarvest == 0) {
                    harvested.maxValue = c.ERSStoreEnergy;
                }

                //not working
                currentTotalBattery = (currentTotalBattery - c.ERSDeployedThisLap) + c.ERSHarvestedThisLapMGUH + c.ERSHarvestedThisLapMGUK;
                battery.value = currentTotalBattery;

                if (c.PitLimiterStatus == 1) {
                    pit.SetActive (true);
                } else {
                    pit.SetActive (false);
                }

                //not working
                switch (c.VehicleFiaFlags) {
                    case Flag.Blue:
                        flagStatus.color = Color.blue;
                        flag.SetActive (true);
                        break;
                    case Flag.Yellow:
                        flagStatus.color = Color.yellow;
                        flag.SetActive (true);
                        break;
                    case Flag.Red:
                        flagStatus.color = Color.red;
                        flag.SetActive (true);
                        break;
                    case Flag.None:
                    case Flag.Green:
                    case Flag.InvalidOrUnknown:
                        flag.SetActive (false);
                        break;
                }

                //not working
                uint bias = c.FrontBrakeBias;
                bb.text = bias.ToString () + "%";

                //not working - changed
                fuelMargin.text = c.FuelInTank.ToString ();
                //not working
                fuelModeText.text = c.FuelMix.ToString ();

                //not working - changed
                if (wear [0].activeSelf && c.TyresWear != null) {
                    flTW.text = c.TyresWear [2].ToString ();
                    frTW.text = c.TyresWear [3].ToString ();
                    rlTW.text = c.TyresWear [0].ToString ();
                    rrTW.text = c.TyresWear [1].ToString ();
                }

                switch (c.TyreCompound) {
                    default:
                        tyre.text = "";
                        break;
                    case TyreCompound.HyperSoft:
                        tyre.text = "HS";
                        break;
                    case TyreCompound.UltraSoft:
                        tyre.text = "US";
                        break;
                    case TyreCompound.SuperSoft:
                        tyre.text = "Ss";
                        break;
                    case TyreCompound.Soft:
                        tyre.text = "S";
                        break;
                    case TyreCompound.Medium:
                        tyre.text = "M";
                        break;
                    case TyreCompound.Hard:
                        tyre.text = "H";
                        break;
                    case TyreCompound.SuperHard:
                        tyre.text = "SH";
                        break;
                    case TyreCompound.Intermediate:
                        tyre.text = "I";
                        break;
                    case TyreCompound.Wet:
                        tyre.text = "W";
                        break;
                }
            } catch (Exception exception) {
                //Debug.LogError (exception.Source);
                throw (exception);
            }
        }*/
    }

    public string GetLocalIP () {
        IPHostEntry host = Dns.GetHostEntry (Dns.GetHostName ());
        foreach (var ip in host.AddressList) {
            if (ip.AddressFamily == AddressFamily.InterNetwork) {
                return ip.ToString ();
            }
        }
        throw new Exception ("No network adapters with an IPv4 address");
    }

    /*void Tm_CarSetupPacketReceived (object sender, PacketReceivedEventArgs<PacketCarSetupData> e) {
        cs = e.Packet.CarSetups [e.Packet.Header.PlayerCarIndex];
        Debug.Log ("setup packet");
    }

    void F1Manager_CarStatusReceived (object sender, PacketReceivedEventArgs<PacketCarStatusData> e) {
        c = e.Packet.GetPlayerLapData ();

        Debug.Log ("status packet done");
    }

    void F1Manager_CarTelemetryReceived (object sender, PacketReceivedEventArgs<PacketCarTelemetryData> e) {
        ctd = e.Packet.GetPlayerLapData ();
        Debug.Log ("telemetry packet");
    }

    void F1Manager_LapPacketReceived (object sender, PacketReceivedEventArgs<PacketLapData> e) {
        l = e.Packet.GetPlayerLapData ();
        Debug.Log ("lap packet");
    }

    void F1Manager_SessionPacketReceived (object sender, PacketReceivedEventArgs<PacketSessionData> e) {
        psd = e.Packet;
        Debug.Log ("session packet");
    }*/

    public void ToggleTyres () {
        foreach (GameObject g in temps) {
            g.SetActive (!g.activeSelf);
        }
        foreach (GameObject g in wear) {
            g.SetActive (!g.activeSelf);
        }
    }

}
