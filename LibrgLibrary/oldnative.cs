using System;
using System.Runtime.InteropServices;

namespace librg
{
    struct vector3 {
        float x;
        float y;
        float z;
    }

    [StructLayout(LayoutKind.Sequential, Size = 48)]
    protected unsafe struct librg_data_t
    {
        UIntPtr capacity;
        UIntPtr read_pos;
        UIntPtr write_pos;

        public void* rawptr;
    }

    [StructLayout(LayoutKind.Sequential, Size = 12)]
    protected unsafe struct librg_address_t
    {
        public Int32 port;
        public String host;
    }

    [StructLayout(LayoutKind.Sequential, Size = 40)]
    protected unsafe struct librg_message_t
    {
        public librg_ctx_t* ctx;

        public librg_data_t* data;
        public librg_peer_t* peer;
        public librg_packet_t* packet;

        public void* user_data; /* optional: user information */
    }

    [StructLayout(LayoutKind.Sequential, Size = 40)]
    protected unsafe struct librg_event_t
    {
        public librg_ctx_t* ctx;   /* librg context where event has been called */

        public librg_data_t* data;  /* optional: data is used for built-in events */
        public librg_peer_t* peer;  /* optional: peer is used for built-in events */
        public UInt32 entity; /* optional: peer is used for built-in events */

        public UInt64 flags;  /* flags for that event */
        public void* user_data; /* optional: user information */
    }

    protected unsafe delegate void librg_entity_cb(ref librg_ctx_t ctx, UInt32 entity);
    protected unsafe delegate void librg_message_cb(ref librg_message_t msg);
    protected unsafe delegate void librg_event_cb(ref librg_event_t evt);

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
    protected unsafe struct librg_entity_blob_t
    {
        public UInt32 id;
        public UInt32 type;
        public UInt64 flags;

        public vector3 position;
        public float stream_range;

        public void* user_data;
    }

    [StructLayout(LayoutKind.Sequential, Size = 48)]
    protected struct librg_packet_t {}

    // TODO: add mapping for peer
    [StructLayout(LayoutKind.Sequential, Size = 472)]
    protected struct librg_peer_t {}

    [StructLayout(LayoutKind.Sequential, Size = 480)]
    protected unsafe struct librg_ctx_t
    {
        // core
        public UInt16 mode;
        public UInt16 tick_delay;

        public UInt16 max_connections;
        public UInt32 max_entities;

        public vector3 world_size;
        public vector3 min_branch_size;

        public float last_update;
        public void* user_data;
    }

    protected unsafe class native
    {
#if DEBUG
        const string DLL_PATH = "librg_win64_d";
#else
        const string DLL_PATH = "librg_win64_r";
#endif

        [DllImport(DLL_PATH)] public static extern Int32 librg_option_get(Option option);
        [DllImport(DLL_PATH)] public static extern void librg_option_set(Option option, Int32 value);
        [DllImport(DLL_PATH)] public static extern void librg_init(ref librg_ctx_t ctx);
        [DllImport(DLL_PATH)] public static extern void librg_free(ref librg_ctx_t ctx);
        [DllImport(DLL_PATH)] public static extern void librg_tick(ref librg_ctx_t ctx);
        [DllImport(DLL_PATH)] public static extern bool librg_is_server(ref librg_ctx_t ctx);
        [DllImport(DLL_PATH)] public static extern bool librg_is_client(ref librg_ctx_t ctx);
        [DllImport(DLL_PATH)] public static extern bool librg_is_connected(ref librg_ctx_t ctx);
        [DllImport(DLL_PATH)] public static extern void librg_network_start(ref librg_ctx_t ctx, librg_address_t address);
        [DllImport(DLL_PATH)] public static extern void librg_network_stop(ref librg_ctx_t ctx);

        [DllImport(DLL_PATH)] public static extern void librg_network_add(ref librg_ctx_t ctx, UInt16 id, librg_message_cb callback);
        [DllImport(DLL_PATH)] public static extern void librg_network_remove(ref librg_ctx_t ctx, UInt16 id);
        [DllImport(DLL_PATH)] public static extern void librg_message_send_all(ref librg_ctx_t ctx, UInt16 id, void* data, UIntPtr size);
        [DllImport(DLL_PATH)] public static extern void librg_message_send_to(ref librg_ctx_t ctx, UInt16 id, ref librg_peer_t peer, void* data, UIntPtr size);
        [DllImport(DLL_PATH)] public static extern void librg_message_send_except(ref librg_ctx_t ctx, UInt16 id, ref librg_peer_t peer, void* data, UIntPtr size);
        [DllImport(DLL_PATH)] public static extern void librg_message_send_instream(ref librg_ctx_t ctx, UInt16 id, UInt32 entity, void* data, UIntPtr size);
        [DllImport(DLL_PATH)] public static extern void librg_message_send_instream_except(ref librg_ctx_t ctx, UInt16 id, UInt32 entity, ref librg_peer_t peer, void* data, UIntPtr size);

        [DllImport(DLL_PATH)] public static extern UInt64 librg_event_add(ref librg_ctx_t ctx, UInt64 id, librg_event_cb callback);
        [DllImport(DLL_PATH)] public static extern void librg_event_trigger(ref librg_ctx_t ctx, UInt64 id, ref librg_event_t evt);
        [DllImport(DLL_PATH)] public static extern void librg_event_remove(ref librg_ctx_t ctx, UInt64 id, UInt64 index);
        [DllImport(DLL_PATH)] public static extern void librg_event_reject(ref librg_event_t evt);
        [DllImport(DLL_PATH)] public static extern bool librg_event_succeeded(ref librg_event_t evt);

        [DllImport(DLL_PATH)] public static extern void librg_data_init(ref librg_data_t data);
        [DllImport(DLL_PATH)] public static extern void librg_data_init_size(ref librg_data_t data, UIntPtr size);
        [DllImport(DLL_PATH)] public static extern void librg_data_free(ref librg_data_t data);
        [DllImport(DLL_PATH)] public static extern void librg_data_reset(ref librg_data_t data);
        [DllImport(DLL_PATH)] public static extern void librg_data_grow(ref librg_data_t data, UIntPtr min_size);
        [DllImport(DLL_PATH)] public static extern UIntPtr librg_data_capacity(ref librg_data_t data);
        [DllImport(DLL_PATH)] public static extern UIntPtr librg_data_get_rpos(ref librg_data_t data);
        [DllImport(DLL_PATH)] public static extern UIntPtr librg_data_get_wpos(ref librg_data_t data);
        [DllImport(DLL_PATH)] public static extern void librg_data_set_rpos(ref librg_data_t data, UIntPtr position);
        [DllImport(DLL_PATH)] public static extern void librg_data_set_wpos(ref librg_data_t data, UIntPtr position);
        [DllImport(DLL_PATH)] public static extern void librg_data_rptr(ref librg_data_t data, void* ptr, UIntPtr size);
        [DllImport(DLL_PATH)] public static extern void librg_data_wptr(ref librg_data_t data, void* ptr, UIntPtr size);
        [DllImport(DLL_PATH)] public static extern void librg_data_rptr_at(ref librg_data_t data, void* ptr, UIntPtr size, IntPtr position);
        [DllImport(DLL_PATH)] public static extern void librg_data_wptr_at(ref librg_data_t data, void* ptr, UIntPtr size, IntPtr position);

        [DllImport(DLL_PATH)] public static extern UInt32 librg_entity_create(ref librg_ctx_t ctx, UInt32 type);
        [DllImport(DLL_PATH)] public static extern bool librg_entity_valid(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport(DLL_PATH)] public static extern UInt32 librg_entity_type(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport(DLL_PATH)] public static extern librg_entity_blob_t* librg_entity_blob(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport(DLL_PATH)] public static extern void librg_entity_destroy(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport(DLL_PATH)] public static extern UIntPtr librg_entity_query(ref librg_ctx_t ctx, UInt32 entity, UInt32** result);
        [DllImport(DLL_PATH)] public static extern UInt32 librg_entity_find(ref librg_ctx_t ctx, ref librg_peer_t peer);
        [DllImport(DLL_PATH)] public static extern void librg_entity_visibility_set(ref librg_ctx_t ctx, UInt32 entity, bool state);
        [DllImport(DLL_PATH)] public static extern void librg_entity_visibility_set_for(ref librg_ctx_t ctx, UInt32 entity, UInt32 target, bool state);
        [DllImport(DLL_PATH)] public static extern bool librg_entity_visibility_get(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport(DLL_PATH)] public static extern bool librg_entity_visibility_get_for(ref librg_ctx_t ctx, UInt32 entity, UInt32 target);
        [DllImport(DLL_PATH)] public static extern void librg_entity_control_set(ref librg_ctx_t ctx, UInt32 entity, ref librg_peer_t peer);
        [DllImport(DLL_PATH)] public static extern librg_peer_t* librg_entity_control_get(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport(DLL_PATH)] public static extern void librg_entity_control_remove(ref librg_ctx_t ctx, UInt32 entity);
        [DllImport(DLL_PATH)] public static extern void librg_entity_iterate(ref librg_ctx_t ctx, UInt64 flags, librg_entity_cb callback);
    }
}
