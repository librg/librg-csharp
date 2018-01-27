using System.Runtime.InteropServices;
using System;

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

    public class Data
    {
        protected librg_data_t ptr;

        public Data()
        {
            ptr = new librg_data_t();
            native.librg_data_init(ref ptr);
        }

        ~Data()
        {
            native.librg_data_free(ref ptr);
        }

        public void Reset()
        {
            native.librg_data_reset(ref ptr);
        }

        public ulong Capacity
        {
            get { return (ulong)native.librg_data_capacity(ref ptr); }
        }

        public ulong ReadPosition
        {
            get { return (ulong)native.librg_data_get_rpos(ref ptr); }
            set { native.librg_data_set_rpos(ref ptr, (UIntPtr)value); }
        }

        public ulong WritePosition
        {
            get { return (ulong)native.librg_data_get_wpos(ref ptr); }
            set { native.librg_data_set_wpos(ref ptr, (UIntPtr)value); }
        }

        public unsafe void WriteUInt8(Byte value, long position) { native.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(Byte), (IntPtr)position); }
        public unsafe void WriteUInt16(UInt16 value, long position) { native.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt16), (IntPtr)position); }
        public unsafe void WriteUInt32(UInt32 value, long position) { native.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt32), (IntPtr)position); }
        public unsafe void WriteUInt64(UInt64 value, long position) { native.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt64), (IntPtr)position); }
        public unsafe void WriteInt8(SByte value, long position) { native.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(SByte), (IntPtr)position); }
        public unsafe void WriteInt16(Int16 value, long position) { native.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(Int16), (IntPtr)position); }
        public unsafe void WriteInt32(Int32 value, long position) { native.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(Int32), (IntPtr)position); }
        public unsafe void WriteInt64(Int64 value, long position) { native.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(Int64), (IntPtr)position); }
        public unsafe void WriteFloat32(float value, long position) { native.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(float), (IntPtr)position); }
        public unsafe void WriteFloat64(double value, long position) { native.librg_data_wptr_at(ref ptr, &value, (UIntPtr)sizeof(double), (IntPtr)position); }

        public unsafe Byte ReadUInt8(long position) { Byte value; native.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(Byte), (IntPtr)position); return value; }
        public unsafe UInt16 ReadUInt16(long position) { UInt16 value; native.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt16), (IntPtr)position); return value; }
        public unsafe UInt32 ReadUInt32(long position) { UInt32 value; native.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt32), (IntPtr)position); return value; }
        public unsafe UInt64 ReadUInt64(long position) { UInt64 value; native.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt64), (IntPtr)position); return value; }
        public unsafe SByte ReadInt8(long position) { SByte value; native.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(SByte), (IntPtr)position); return value; }
        public unsafe UInt16 ReadInt16(long position) { UInt16 value; native.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt16), (IntPtr)position); return value; }
        public unsafe UInt32 ReadInt32(long position) { UInt32 value; native.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt32), (IntPtr)position); return value; }
        public unsafe UInt64 ReadInt64(long position) { UInt64 value; native.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(UInt64), (IntPtr)position); return value; }
        public unsafe float ReadFloat32(long position) { float value; native.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(float), (IntPtr)position); return value; }
        public unsafe double ReadFloat64(long position) { double value; native.librg_data_rptr_at(ref ptr, &value, (UIntPtr)sizeof(double), (IntPtr)position); return value; }

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
            native.librg_message_send_to(
                ref ctx.ctx, id, ref peer.peer,
                data.ptr.rawptr, (UIntPtr)data.WritePosition
            );
        }

        public unsafe void SendToAll()
        {
            native.librg_message_send_all(
                ref ctx.ctx, id, data.ptr.rawptr,
                (UIntPtr)data.WritePosition
            );
        }

        public unsafe void SendExcept(Peer peer)
        {
            native.librg_message_send_except(
                ref ctx.ctx, id, ref peer.peer,
                data.ptr.rawptr, (UIntPtr)data.WritePosition
            );
        }

        public unsafe void SendInStream(UInt32 entity)
        {
            native.librg_message_send_instream(
                ref ctx.ctx, id, entity,
                data.ptr.rawptr, (UIntPtr)data.WritePosition
            );
        }

        public unsafe void SendInStreamExcept(UInt32 entity, Peer peer)
        {
            native.librg_message_send_instream_except(
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
            native.librg_event_reject(ref evt);
        }

        public bool Succeeded()
        {
            return native.librg_event_succeeded(ref evt);
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

            native.librg_init(ref ctx);
        }

        ~Librg()
        {
            native.librg_free(ref ctx);
        }

        public static int OptionGet(Option option)
        {
            return native.librg_option_get(option);
        }

        public static void OptionSet(Option option, int value)
        {
            native.librg_option_set(option, value);
        }

        public void Tick()
        {
            native.librg_tick(ref ctx);
        }

        public bool IsServer()
        {
            return native.librg_is_server(ref ctx);
        }

        public bool IsClient()
        {
            return native.librg_is_client(ref ctx);
        }

        public bool IsConnected()
        {
            return native.librg_is_connected(ref ctx);
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

            return native.librg_event_add(ref ctx, id, (ref librg_event_t evt) => {
                doo(ref evt);
            });
        }

        public ulong EventAdd(ActionType id, Action<Event> callback)
        {
            return EventAdd((ulong)id, callback);
        }

        public void EventRemove(ulong id, ulong index)
        {
            native.librg_event_remove(ref ctx, id, index);
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

            native.librg_event_trigger(ref ctx, id, ref local);
        }

        public void EventTrigger(ActionType id, Event evt)
        {
            EventTrigger((ulong)id, evt);
        }

        public void NetworkStart(String host, uint port)
        {
            native.librg_network_start(ref ctx, new librg_address_t {
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
            native.librg_network_stop(ref ctx);
        }

        public void NetworkAdd(ushort id, Action<Message> callback)
        {
            native.librg_network_add(ref ctx, id, (ref librg_message_t msg) => {
                callback(new Message(msg));
            });
        }

        public void NetworkAdd(ActionType id, Action<Message> callback)
        {
            NetworkAdd(id, callback);
        }

        public void NetworkRemove(ushort id)
        {
            native.librg_network_remove(ref ctx, id);
        }

        public void NetworkRemove(ActionType id)
        {
            NetworkRemove(id);
        }

    }
}
