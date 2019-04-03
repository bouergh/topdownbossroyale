﻿using System.Net;
using UnityEngine;

using Unity.Networking.Transport;
using Unity.Collections;
using NetworkConnection = Unity.Networking.Transport.NetworkConnection; 
using UdpCNetworkDriver = Unity.Networking.Transport.BasicNetworkDriver<Unity.Networking.Transport.IPv4UDPSocket>;
using Unity.Jobs;
using System;

struct ClientUpdateJob: IJob
{
	public UdpCNetworkDriver driver;
	public NativeArray<NetworkConnection> connection;
	public NativeArray<byte> done;
	
	public void Execute(){

        if (!connection[0].IsCreated)
        {
            if (done[0] != 1)
                Debug.Log("Something went wrong during connect");
            return;
        }
        DataStreamReader stream;
        NetworkEvent.Type cmd;
        while ((cmd = connection[0].PopEvent(driver, out stream)) != 
            NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect)
            {
                Debug.Log("We are now connected to the server");
                
                var value = 1;
                using (var writer = new DataStreamWriter(4, Allocator.Temp))
                {
                    writer.Write(value);
                    connection[0].Send(driver, writer);
                }
            }
            else if (cmd == NetworkEvent.Type.Data)
            {
                var readerCtx = default(DataStreamReader.Context);
                uint value = stream.ReadUInt(ref readerCtx);
                Debug.Log("Got the value = " + value + " back from the server");
                done[0] = 1;
                connection[0].Disconnect(driver);
                connection[0] = default(NetworkConnection);
            }
            else if (cmd == NetworkEvent.Type.Disconnect)
            {
                Debug.Log("Client got disconnected from server");
                connection[0] = default(NetworkConnection);
            }
        }

    
    }

}


public class JobifiedClientBehaviour : MonoBehaviour {
    public UdpCNetworkDriver m_Driver;
    public NativeArray<NetworkConnection> m_Connection;
    public NativeArray<byte> m_Done;
    public JobHandle ClientJobHandle;

    

    [SerializeField]
    String ip_String = "127.0.0.1"; //142.44.247.201
    
    
    public void OnDestroy(){
         
        ClientJobHandle.Complete();
    
        m_Connection.Dispose();
        m_Driver.Dispose();
        m_Done.Dispose();
        
        
    }
    public void Start(){
        m_Driver = new UdpCNetworkDriver(new INetworkParameter[0]);
        m_Connection = new NativeArray<NetworkConnection>(1, Allocator.Persistent);
        m_Done = new NativeArray<byte>(1, Allocator.Persistent);
    
        IPAddress serverIp = IPAddress.Parse(ip_String);
        //serverIp = IPAddress.Loopback;


        var endpoint = new IPEndPoint(serverIp, 9000);
        m_Connection[0] = m_Driver.Connect(endpoint);
    }
    public void Update(){

        ClientJobHandle.Complete();
        var job = new ClientUpdateJob
        {
            driver = m_Driver,
            connection = m_Connection,
            done = m_Done
        };
        ClientJobHandle = m_Driver.ScheduleUpdate();
        ClientJobHandle = job.Schedule(ClientJobHandle);
    
    }
}
