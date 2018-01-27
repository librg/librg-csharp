#region

using System;

#endregion

namespace Librg
{
    public unsafe struct Peer
    {
        private Native.LibrgPeer* _peer;

        public Peer(Native.LibrgPeer* peer)
        {
            _peer = peer;
        }

        public bool IsSet
        {
            get { return _peer != null; }
        }

        public uint RoundTripTime
        {
            get { return _peer->lastRoundTripTime; }
        }

        public Native.LibrgPeer* NativeData
        {
            get { return _peer; }
            set { _peer = value; }
        }

        public PeerState State
        {
            get { return IsSet ? _peer->state : PeerState.Uninitialized; }
        }

        public IntPtr UserData
        {
            get
            {
                CheckCreated();
                return _peer->data;
            }
            set
            {
                CheckCreated();
                _peer->data = value;
            }
        }

        private void CheckCreated()
        {
            if (_peer == null)
            {
                throw new InvalidOperationException("No native peer.");
            }
        }
    }
}