using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Librg
{
    public static unsafe class Option
    {
        public enum Options
        {
            LIBRG_PLATFORM_ID,
            LIBRG_PLATFORM_PROTOCOL,
            LIBRG_PLATFORM_BUILD,

            LIBRG_DEFAULT_CLIENT_TYPE,
            LIBRG_DEFAULT_STREAM_RANGE,
            LIBRG_DEFAULT_DATA_SIZE,

            LIBRG_NETWORK_CAPACITY,
            LIBRG_NETWORK_CHANNELS,
            LIBRG_NETWORK_PRIMARY_CHANNEL,
            LIBRG_NETWORK_SECONDARY_CHANNEL,
            LIBRG_NETWORK_MESSAGE_CHANNEL,

            LIBRG_MAX_ENTITIES_PER_BRANCH,
            LIBRG_MAX_THREADS_PER_UPDATE,

            LIBRG_OPTIONS_SIZE,
        };

        public static UInt32 Get(Options option)
        {
            return Native.librg_option_get((uint)option);
        }

        public static void Set(Options option, UInt32 value)
        {
            Native.librg_option_set((uint)option, value);
        }
    }
}
