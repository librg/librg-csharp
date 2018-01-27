using System;
using System.Runtime.InteropServices;

namespace Librg
{
    public static unsafe partial class Native
    {
        #region Nested type: ENetAddress

        [StructLayout(LayoutKind.Sequential)]
        public struct ENetAddress
        {
            public uint host;
            public ushort port;
        }

        #endregion

        #region Nested type: ENetCallbacks

        [StructLayout(LayoutKind.Sequential)]
        public struct ENetCallbacks
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate IntPtr malloc_cb(IntPtr size);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void free_cb(IntPtr memory);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void no_memory_cb();

            public IntPtr malloc, free, no_memory;
        }

        #endregion

        #region Nested type: ENetChannel

        [StructLayout(LayoutKind.Sequential)]
        public struct ENetChannel
        {
            public ushort outgoingReliableSequenceNumber;
            public ushort outgoingUnreliableSequenceNumber;
            public ushort usedReliableWindows;
            public ushort reliableWindows;
            public ushort incomingReliableSequenceNumber;
            public ushort incomingUnreliableSequenceNumber;
            public ENetList* incomingReliableCommands;
            public ENetList* incomingUnreliableCommands;
        }

        #endregion

        #region Nested type: ENetCompressor

        [StructLayout(LayoutKind.Sequential)]
        public struct ENetCompressor
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void compress_cb(IntPtr context, IntPtr inBuffers, IntPtr inBufferCount, IntPtr inLimit, IntPtr outData, IntPtr outLimit);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void decompress_cb(IntPtr context, IntPtr inData, IntPtr inLimit, IntPtr outData, IntPtr outLimit);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void destroy_cb(IntPtr context);

            public IntPtr context;
            public IntPtr compress, decompress, destroy;
        }

        #endregion

        #region Nested type: ENetEvent

        public enum EventType
        {
            None = 0,
            Connect = 1,
            Disconnect = 2,
            Receive = 3
        }

        [Flags]
        public enum PacketFlags
        {
            None = 0,
            Reliable = 1 << 0,
            Unsequenced = 1 << 1,
            NoAllocate = 1 << 2
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ENetEvent
        {
            public EventType type;
            public LibrgPeer* peer;
            public byte channelID;
            public uint data;
            public LibrgPacket* packet;
        }

        #endregion

        #region Nested type: ENetHost

        [StructLayout(LayoutKind.Sequential)]
        public struct LibrgHost
        {
        }

        #endregion

        #region Nested type: ENetList

        [StructLayout(LayoutKind.Sequential)]
        public struct ENetList
        {
            public ENetListNode* sentinel;
        }

        #endregion

        #region Nested type: ENetListNode

        [StructLayout(LayoutKind.Sequential)]
        public struct ENetListNode
        {
            public ENetListNode* next, previous;
        }

        #endregion

        #region Nested type: ENetPacket

        [StructLayout(LayoutKind.Sequential)]
        public struct LibrgPacket
        {
            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void freeCallback_cb(IntPtr packet);

            public IntPtr referenceCount;
            public PacketFlags flags;
            public void* data;
            public IntPtr dataLength;
            public IntPtr freeCallback;
        }

        #endregion

        #region Nested type: ENetPeer

        [StructLayout(LayoutKind.Sequential)]
        public struct LibrgPeer
        {
            public ENetListNode dispatchList;
            public LibrgHost* host;
            public ushort outgoingPeerID;
            public ushort incomingPeerID;
            public uint connectID;
            public byte outgoingSessionID;
            public byte incomingSessionID;
            public ENetAddress address;
            public IntPtr data;
            public PeerState state;
            public ENetChannel* channels;
            public IntPtr channelcount;
            public uint incomingBandwidth;
            public uint outgoingBandwidth;
            public uint incomingBandwidthThrottleEpoch;
            public uint outgoingBandwidthThrottleEpoch;
            public uint incomingDataTotal;
            public uint outgoingDataTotal;
            public uint lastSendTime;
            public uint lastReceiveTime;
            public uint nextTimeout;
            public uint earliestTimeout;
            public uint packetLossEpoch;
            public uint packetsSent;
            public uint packetsLost;
            public uint packetLoss;
            public uint packetLossVariance;
            public uint packetThrottle;
            public uint packetThrottleLimit;
            public uint packetThrottleCounter;
            public uint packetThrottleEpoch;
            public uint packetThrottleAcceleration;
            public uint packetThrottleDeceleration;
            public uint packetThrottleInterval;
            public uint pingInterval;
            public uint timeoutLimit;
            public uint timeoutMinimum;
            public uint timeoutMaximum;
            public uint lastRoundTripTime;
            public uint lowestRoundTripTime;
            public uint lastRoundTripTimeVariance;
            public uint highestRoundTripTimeVariance;
            public uint roundTripTime;
            public uint roundTripTimeVariance;
            public uint mtu;
            public uint windowSize;
            public uint reliableDataInTransit;
            public ushort outgoingReliableSequenceNumber;
            public ENetList* acknowledgements;
            public ENetList* sentReliableCommands;
            public ENetList* sentUnreliableCommands;
            public ENetList* outgoingReliableCommands;
            public ENetList* outgoingUnreliableCommands;
            public ENetList* dispatchedCommands;
            public int needsDispatch;
            public ushort incomingUnsequencedGroup;
            public ushort outgoingUnsequencedGroup;
            public uint unsequencedWindow;
            public uint eventData;
        }

        #endregion

        [StructLayout(LayoutKind.Sequential)]
        public struct vector3
        {
            public float x;
            public float y;
            public float z;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LibrgAddress
        {
            public UInt32 port;
            public String host;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LibrgContext
        {
            public UInt16 mode;
            public UInt16 tick_delay;

            public UInt16 max_connections;
            public UInt32 max_entities;

            public vector3 world_size;
            public vector3 min_branch_size;

            public float last_update;
            public void* user_data;

            [StructLayout(LayoutKind.Sequential)]
            public struct network {
                public LibrgPeer* peer;
                public LibrgPeer* host;
            };
        }
    }
}
