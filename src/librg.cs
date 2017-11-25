using System.Runtime.InteropServices;
using System;
using UnityEngine;

namespace librg
{
    public enum Option
    {
        PLATFORM_ID,
        PLATFORM_PROTOCOL,
        PLATFORM_BUILD,

        DEFAULT_CLIENT_TYPE,
        DEFAULT_DATA_SIZE,

        NETWORK_CAPACITY,
        NETWORK_CHANNELS,
        NETWORK_PRIMARY_CHANNEL,
        NETWORK_SECONDARY_CHANNEL,
        NETWORK_MESSAGE_CHANNEL,

        MAX_ENTITIES_PER_BRANCH,
        MAX_THREADS_PER_UPDATE,

        OPTIONS_SIZE,
    };

    public enum Mode
    {
        Server,
        Client,
    };

    public enum ActionType
    {
        CONNECTION_INIT,
        CONNECTION_REQUEST,
        CONNECTION_REFUSE,
        CONNECTION_ACCEPT,
        CONNECTION_DISCONNECT,

        ENTITY_CREATE,
        ENTITY_UPDATE,
        ENTITY_REMOVE,
        CLIENT_STREAMER_ADD,
        CLIENT_STREAMER_REMOVE,
        CLIENT_STREAMER_UPDATE,

        EVENT_LAST,
    };

    [StructLayout(LayoutKind.Sequential, Size = 48)]
    unsafe struct librg_data_t
    {
        UIntPtr capacity;
        UIntPtr read_pos;
        UIntPtr write_pos;

        public void* rawptr;
    };

    [StructLayout(LayoutKind.Sequential, Size = 12)]
    unsafe struct librg_address_t
    {
        public Int32 port;
        public String host;
    };

    [StructLayout(LayoutKind.Sequential, Size = 40)]
    unsafe struct librg_message_t
    {
        public librg_ctx_t *ctx;

        public librg_data_t *data;
        public librg_peer_t *peer;
        public librg_packet_t *packet;

        public void* user_data; /* optional: user information */
    };

    [StructLayout(LayoutKind.Sequential, Size = 40)]
    unsafe struct librg_event_t
    {
        public librg_ctx_t *ctx;   /* librg context where event has been called */

        public librg_data_t *data;  /* optional: data is used for built-in events */
        public librg_peer_t *peer;  /* optional: peer is used for built-in events */
        public UInt32       entity; /* optional: peer is used for built-in events */

        public UInt64 flags;  /* flags for that event */
        public void* user_data; /* optional: user information */
    };

    unsafe delegate void librg_entity_cb(ref librg_ctx_t ctx, UInt32 entity);
    unsafe delegate void librg_message_cb(ref librg_message_t msg);
    unsafe delegate void librg_event_cb(ref librg_event_t evt);

    enum librg_entity_flag
    {
        LIBRG_ENTITY_NONE = 0,
        LIBRG_ENTITY_ALIVE = (1 << 0),

        LIBRG_ENTITY_CLIENT = (1 << 4),
        LIBRG_ENTITY_IGNORING = (1 << 5),
        LIBRG_ENTITY_QUERIED = (1 << 6),

        LIBRG_ENTITY_CONTROLLED = (1 << 10),
    };

    [StructLayout(LayoutKind.Sequential, Size = 104)]
    unsafe struct librg_entity_blob_t
    {
        public UInt32 id;
        public UInt32 type;
        public UInt64 flags;

        public Vector3 position;
        public float stream_range;

        public void* user_data;
    };

    [StructLayout(LayoutKind.Sequential, Size = 48)]
    struct librg_packet_t { };

    // TODO: add mapping for peer
    [StructLayout(LayoutKind.Sequential, Size = 472)]
    struct librg_peer_t { };

    [StructLayout(LayoutKind.Sequential, Size = 480)]
    unsafe struct librg_ctx_t
    {
        // core
        public UInt16 mode;
        public UInt16 tick_delay;

        public UInt16 max_connections;
        public UInt32 max_entities;

        public Vector3 world_size;
        public Vector3 min_branch_size;

        public float last_update;
        public void* user_data;
    };

    unsafe class librg_internal
    {
        [DllImport("librg_shared")] public static extern Int32 librg_option_get(Option option);
        [DllImport("librg_shared")] public static extern void librg_option_set(Option option, Int32 value);
        [DllImport("librg_shared")] public static extern void librg_init(ref librg_ctx_t ctx);
        [DllImport("librg_shared")] public static extern void librg_free(ref librg_ctx_t ctx);
        [DllImport("librg_shared")] public static extern void librg_tick(ref librg_ctx_t ctx);
        [DllImport("librg_shared")] public static extern bool librg_is_server(ref librg_ctx_t ctx);
        [DllImport("librg_shared")] public static extern bool librg_is_client(ref librg_ctx_t ctx);
        [DllImport("librg_shared")] public static extern bool librg_is_connected(ref librg_ctx_t ctx);
        [DllImport("librg_shared")] public static extern void librg_network_start(ref librg_ctx_t ctx, librg_address_t address);
        [DllImport("librg_shared")] public static extern void librg_network_stop(ref librg_ctx_t ctx);

        [DllImport("librg_shared")] public static extern void librg_network_add(ref librg_ctx_t ctx, UInt16 id, librg_message_cb callback);
        [DllImport("librg_shared")] public static extern void librg_network_remove(ref librg_ctx_t ctx, UInt16 id);
        [DllImport("librg_shared")] public static extern void librg_message_send_all(ref librg_ctx_t ctx, UInt16 id, void* data, UIntPtr size);
        [DllImport("librg_shared")] public static extern void librg_message_send_to(ref librg_ctx_t ctx, UInt16 id, ref librg_peer_t peer, void* data, UIntPtr size);
        [DllImport("librg_shared")] public static extern void librg_message_send_except(ref librg_ctx_t ctx, UInt16 id, ref librg_peer_t peer, void* data, UIntPtr size);
        [DllImport("librg_shared")] public static extern void librg_message_send_instream(ref librg_ctx_t ctx, UInt16 id, UInt32 entity, void* data, UIntPtr size);
        [DllImport("librg_shared")] public static extern void librg_message_send_instream_except(ref librg_ctx_t ctx, UInt16 id, UInt32 entity, ref librg_peer_t peer, void* data, UIntPtr size);

        [DllImport("librg_shared")] public static extern UInt64 librg_event_add(ref librg_ctx_t ctx, UInt64 id, librg_event_cb callback);
        [DllImport("librg_shared")] public static extern void librg_event_trigger(ref librg_ctx_t ctx, UInt64 id, ref librg_event_t evt);
        [DllImport("librg_shared")] public static extern void librg_event_remove(ref librg_ctx_t ctx, UInt64 id, UInt64 index);
        [DllImport("librg_shared")] public static extern void librg_event_reject(ref librg_event_t evt);
        [DllImport("librg_shared")] public static extern bool librg_event_succeeded(ref librg_event_t evt);

        [DllImport("librg_shared")] public static extern void librg_data_init(ref librg_data_t data);
        [DllImport("librg_shared")] public static extern void librg_data_init_size(ref librg_data_t data, UIntPtr size);
        [DllImport("librg_shared")] public static extern void librg_data_free(ref librg_data_t data);
        [DllImport("librg_shared")] public static extern void librg_data_reset(ref librg_data_t data);
        [DllImport("librg_shared")] public static extern void librg_data_grow(ref librg_data_t data, UIntPtr min_size);
        [DllImport("librg_shared")] public static extern UIntPtr librg_data_capacity(ref librg_data_t data);
        [DllImport("librg_shared")] public static extern UIntPtr librg_data_get_rpos(ref librg_data_t data);
        [DllImport("librg_shared")] public static extern UIntPtr librg_data_get_wpos(ref librg_data_t data);
        [DllImport("librg_shared")] public static extern void librg_data_set_rpos(ref librg_data_t data, UIntPtr position);
        [DllImport("librg_shared")] public static extern void librg_data_set_wpos(ref librg_data_t data, UIntPtr position);
        [DllImport("librg_shared")] public static extern void librg_data_rptr(ref librg_data_t data, void* ptr, UIntPtr size);
        [DllImport("librg_shared")] public static extern void librg_data_wptr(ref librg_data_t data, void* ptr, UIntPtr size);
        [DllImport("librg_shared")] public static extern void librg_data_rptr_at(ref librg_data_t data, void* ptr, UIntPtr size, IntPtr position);
        [DllImport("librg_shared")] public static extern void librg_data_wptr_at(ref librg_data_t data, void* ptr, UIntPtr size, IntPtr position);

        [DllImport("librg_shared")] public static extern UInt32 librg_entity_create(ref librg_ctx_t ctx, UInt32 type);
        [DllImport("librg_shared")] public static extern bool librg_entity_valid(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport("librg_shared")] public static extern UInt32 librg_entity_type(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport("librg_shared")] public static extern librg_entity_blob_t* librg_entity_blob(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport("librg_shared")] public static extern void librg_entity_destroy(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport("librg_shared")] public static extern UIntPtr librg_entity_query(ref librg_ctx_t ctx, UInt32 entity, UInt32** result);
        [DllImport("librg_shared")] public static extern UInt32 librg_entity_find(ref librg_ctx_t ctx, ref librg_peer_t peer);
        [DllImport("librg_shared")] public static extern void librg_entity_visibility_set(ref librg_ctx_t ctx, UInt32 entity, bool state);
        [DllImport("librg_shared")] public static extern void librg_entity_visibility_set_for(ref librg_ctx_t ctx, UInt32 entity, UInt32 target, bool state);
        [DllImport("librg_shared")] public static extern bool librg_entity_visibility_get(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport("librg_shared")] public static extern bool librg_entity_visibility_get_for(ref librg_ctx_t ctx, UInt32 entity, UInt32 target);
        [DllImport("librg_shared")] public static extern void librg_entity_control_set(ref librg_ctx_t ctx, UInt32 entity, ref librg_peer_t peer);
        [DllImport("librg_shared")] public static extern librg_peer_t* librg_entity_control_get(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport("librg_shared")] public static extern void librg_entity_control_remove(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport("librg_shared")] public static extern void librg_entity_iterate(ref librg_ctx_t ctx, UInt64 flags, librg_entity_cb callback);

    }

    public class Data
    {
        internal librg_data_t ptr;

        public Data()
        {
            ptr = new librg_data_t();
            librg_internal.librg_data_init(ref ptr);
        }

        ~Data()
        {
            librg_internal.librg_data_free(ref ptr);
        }

        public void Reset()
        {
            librg_internal.librg_data_reset(ref ptr);
        }

        public ulong Capacity
        {
            get { return (ulong)librg_internal.librg_data_capacity(ref ptr); }
        }

        public ulong ReadPosition
        {
            get { return (ulong)librg_internal.librg_data_get_rpos(ref ptr); }
            set { librg_internal.librg_data_set_rpos(ref ptr, (UIntPtr)value); }
        }

        public ulong WritePosition
        {
            get { return (ulong)librg_internal.librg_data_get_wpos(ref ptr); }
            set { librg_internal.librg_data_set_wpos(ref ptr, (UIntPtr)value); }
        }

        public unsafe void WriteUInt8(Byte value, long position) { librg_internal.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(Byte), (IntPtr)position); }
        public unsafe void WriteUInt16(UInt16 value, long position) { librg_internal.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt16), (IntPtr)position); }
        public unsafe void WriteUInt32(UInt32 value, long position) { librg_internal.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt32), (IntPtr)position); }
        public unsafe void WriteUInt64(UInt64 value, long position) { librg_internal.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt64), (IntPtr)position); }
        public unsafe void WriteInt8(SByte value, long position) { librg_internal.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(SByte), (IntPtr)position); }
        public unsafe void WriteInt16(Int16 value, long position) { librg_internal.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(Int16), (IntPtr)position); }
        public unsafe void WriteInt32(Int32 value, long position) { librg_internal.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(Int32), (IntPtr)position); }
        public unsafe void WriteInt64(Int64 value, long position) { librg_internal.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(Int64), (IntPtr)position); }
        public unsafe void WriteFloat32(float value, long position) { librg_internal.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(float), (IntPtr)position); }
        public unsafe void WriteFloat64(double value, long position) { librg_internal.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(double), (IntPtr)position); }

        public unsafe Byte ReadUInt8(long position) { Byte value; librg_internal.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(Byte), (IntPtr)position); return value; }
        public unsafe UInt16 ReadUInt16(long position) { UInt16 value; librg_internal.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt16), (IntPtr)position); return value; }
        public unsafe UInt32 ReadUInt32(long position) { UInt32 value; librg_internal.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt32), (IntPtr)position); return value; }
        public unsafe UInt64 ReadUInt64(long position) { UInt64 value; librg_internal.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt64), (IntPtr)position); return value; }
        public unsafe SByte ReadInt8(long position) { SByte value; librg_internal.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(SByte), (IntPtr)position); return value; }
        public unsafe UInt16 ReadInt16(long position) { UInt16 value; librg_internal.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt16), (IntPtr)position); return value; }
        public unsafe UInt32 ReadInt32(long position) { UInt32 value; librg_internal.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt32), (IntPtr)position); return value; }
        public unsafe UInt64 ReadInt64(long position) { UInt64 value; librg_internal.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt64), (IntPtr)position); return value; }
        public unsafe float ReadFloat32(long position) { float value; librg_internal.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(float), (IntPtr)position); return value; }
        public unsafe double ReadFloat64(long position) { double value; librg_internal.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(double), (IntPtr)position); return value; }

        public void WriteUInt8(Byte value) { WriteUInt8(value, (long)WritePosition); }
        public void WriteUInt16(UInt16 value) { WriteUInt16(value, (long)WritePosition); }
        public void WriteUInt32(UInt32 value) { WriteUInt32(value, (long)WritePosition); }
        public void WriteUInt64(UInt64 value) { WriteUInt64(value, (long)WritePosition); }
        public void WriteInt8(SByte value) { WriteInt8(value, (long)WritePosition); }
        public void WriteInt16(Int16 value) { WriteInt16(value, (long)WritePosition); }
        public void WriteInt32(Int32 value) { WriteInt32(value, (long)WritePosition); }
        public void WriteInt64(Int64 value) { WriteInt64(value, (long)WritePosition); }
        public void WriteFloat32(float value) { WriteFloat32(value, (long)WritePosition); }
        public void WriteFloat64(double value) { WriteFloat64(value, (long)WritePosition); }

        public Byte ReadUInt8() { return ReadUInt8((long)ReadPosition); }
        public UInt16 ReadUInt16() { return ReadUInt16((long)ReadPosition); }
        public UInt32 ReadUInt32() { return ReadUInt32((long)ReadPosition); }
        public UInt64 ReadUInt64() { return ReadUInt64((long)ReadPosition); }
        public SByte ReadInt8() { return ReadInt8((long)ReadPosition); }
        public UInt16 ReadInt16() { return ReadInt16((long)ReadPosition); }
        public UInt32 ReadInt32() { return ReadInt32((long)ReadPosition); }
        public UInt64 ReadInt64() { return ReadInt64((long)ReadPosition); }
        public float ReadFloat32() { return ReadFloat32((long)ReadPosition); }
        public double ReadFloat64() { return ReadFloat64((long)ReadPosition); }

    }

    public class Peer
    {
        internal librg_peer_t peer;
    }

    public class Message
    {
        private Librg ctx;
        private Data data;
        private librg_message_t msg;
        private ushort id;

        internal Message(librg_message_t msgarg)
        {
            msg = msgarg;
        }

        public Message(Librg ctx, ushort id, Action<Data> callback)
        {
            this.id = id;
            this.ctx = ctx;
            this.data = new Data();

            callback(data);
        }


        public Message(Librg ctx, ushort id, Data data)
        {
            this.id = id;
            this.ctx = ctx;
            this.data = data;
        }

        public unsafe void SendTo(Peer peer)
        {
            librg_internal.librg_message_send_to(
                ref ctx.ctx, id, ref peer.peer,
                data.ptr.rawptr, (UIntPtr)data.WritePosition
            );
        }

        public unsafe void SendToAll()
        {
            librg_internal.librg_message_send_all(
                ref ctx.ctx, id, data.ptr.rawptr,
                (UIntPtr)data.WritePosition
            );
        }

        public unsafe void SendExcept(Peer peer)
        {
            librg_internal.librg_message_send_except(
                ref ctx.ctx, id, ref peer.peer,
                data.ptr.rawptr, (UIntPtr)data.WritePosition
            );
        }

        public unsafe void SendInStream(UInt32 entity)
        {
            librg_internal.librg_message_send_instream(
                ref ctx.ctx, id, entity,
                data.ptr.rawptr, (UIntPtr)data.WritePosition
            );
        }

        public unsafe void SendInStreamExcept(UInt32 entity, Peer peer)
        {
            librg_internal.librg_message_send_instream_except(
                ref ctx.ctx, id, entity, ref peer.peer,
                data.ptr.rawptr, (UIntPtr)data.WritePosition
            );
        }
    }

    public class Event
    {
        private librg_event_t evt;
        public int data;

        internal Event(librg_event_t evtarg)
        {
            evt = evtarg;
        }

        public Event(int val)
        {
            data = val;
        }

        public void Reject()
        {
            librg_internal.librg_event_reject(ref evt);
        }

        public bool Succeeded()
        {
            return librg_internal.librg_event_succeeded(ref evt);
        }
    }

    public class Librg
    {
        internal librg_ctx_t ctx;

        public Librg(Mode mode, ushort tickDelay, Vector3 worldSize, uint maxEntities)
        {
            ctx = new librg_ctx_t();

            ctx.mode = (UInt16)mode;
            ctx.max_entities = maxEntities;
            ctx.tick_delay = tickDelay;
            ctx.world_size = worldSize;

            librg_internal.librg_init(ref ctx);
        }

        ~Librg()
        {
            librg_internal.librg_free(ref ctx);
        }

        public static int OptionGet(Option option)
        {
            return librg_internal.librg_option_get(option);
        }

        public static void OptionSet(Option option, int value)
        {
            librg_internal.librg_option_set(option, value);
        }

        public void Tick()
        {
            librg_internal.librg_tick(ref ctx);
        }

        public bool IsServer()
        {
            return librg_internal.librg_is_server(ref ctx);
        }

        public bool IsClient()
        {
            return librg_internal.librg_is_client(ref ctx);
        }

        public bool IsConnected()
        {
            return librg_internal.librg_is_connected(ref ctx);
        }

        private librg_event_cb fooo(Action<Event> callback)
        {
            return (ref librg_event_t evt) =>
            {
                unsafe
                {
                    if (evt.user_data != null)
                    {
                        var tr = *(TypedReference*)evt.user_data;
                        var obj = __refvalue(tr, Event);
                        callback(obj);
                        return;
                    }
                };

                var foo = new Event(evt);
                callback(foo);
            };
        }

        public ulong EventAdd(ulong id, Action<Event> callback)
        {
            var doo = fooo(callback);

            return librg_internal.librg_event_add(ref ctx, id, (ref librg_event_t evt) => {
                doo(ref evt);
            });
        }

        public ulong EventAdd(ActionType id, Action<Event> callback)
        {
            return EventAdd((ulong)id, callback);
        }

        public void EventRemove(ulong id, ulong index)
        {
            librg_internal.librg_event_remove(ref ctx, id, index);
        }

        public void EventRemove(ActionType id, ulong index)
        {
            EventRemove((ulong)id, index);
        }

        public void EventTrigger(ulong id, Event evt)
        {
            var local = new librg_event_t();

            unsafe
            {
                TypedReference tr = __makeref(evt);
                local.user_data = (void *)(&tr);
            }

            librg_internal.librg_event_trigger(ref ctx, id, ref local);
        }

        public void EventTrigger(ActionType id, Event evt)
        {
            EventTrigger((ulong)id, evt);
        }

        public void NetworkStart(String host, uint port)
        {
            librg_internal.librg_network_start(ref ctx, new librg_address_t {
                port = 7777,
                host = host,
            });
        }

        public void NetworkStart(uint port)
        {
            NetworkStart("localhost", port);
        }

        public void NetworkStop()
        {
            librg_internal.librg_network_stop(ref ctx);
        }

        public void NetworkAdd(ushort id, Action<Message> callback)
        {
            librg_internal.librg_network_add(ref ctx, id, (ref librg_message_t msg) => {
                callback(new Message(msg));
            });
        }

        public void NetworkAdd(ActionType id, Action<Message> callback)
        {
            NetworkAdd(id, callback);
        }

        public void NetworkRemove(ushort id)
        {
            librg_internal.librg_network_remove(ref ctx, id);
        }

        public void NetworkRemove(ActionType id)
        {
            NetworkRemove(id);
        }

    }
}
