#region

using System;

#endregion

namespace Librg
{
    public unsafe class Host
    {
        private Native.LibrgHost* _host;

        public bool IsSet
        {
            get { return _host != null; }
        }

        public Native.LibrgHost* NativeData
        {
            get { return _host; }
            set { _host = value; }
        }

        private void CheckChannelLimit(int channelLimit)
        {
            if (channelLimit < 0 || channelLimit > Native.ENET_PROTOCOL_MAXIMUM_CHANNEL_COUNT)
            {
                throw new ArgumentOutOfRangeException("channelLimit");
            }
        }

        private void CheckCreated()
        {
            if (_host == null)
            {
                throw new InvalidOperationException("Not created.");
            }
        }
    }
}