using System;
using System.Runtime.InteropServices;

namespace Librg
{
    public static unsafe partial class Native
    {
#if win32
#if DEBUG
        public const string DLL_NAME = "librg_win32_d";
#else
        public const string DLL_NAME = "librg_win32_r";
#endif
#else
#if DEBUG
        public const string DLL_NAME = "librg_win64_d";
#else
        public const string DLL_NAME = "librg_win64_r";
#endif
#endif

        public const int ENET_PEER_PACKET_THROTTLE_SCALE = 32;
        public const int ENET_PEER_PACKET_THROTTLE_ACCELERATION = 2;
        public const int ENET_PEER_PACKET_THROTTLE_DECELERATION = 2;
        public const int ENET_PEER_PACKET_THROTTLE_INTERVAL = 5000;
        public const int ENET_PROTOCOL_MINIMUM_CHANNEL_COUNT = 0x01;
        public const int ENET_PROTOCOL_MAXIMUM_CHANNEL_COUNT = 0xff;
        public const int ENET_PROTOCOL_MAXIMUM_PEER_ID = 0xfff;
        public const uint ENET_VERSION = (1 << 16) | (4 << 8) | (6);
        public const uint ENET_HOST_ANY = 0;
        public const uint ENET_HOST_BROADCAST = 0xffffffff;


        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 librg_option_get(UInt32 option);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_option_set(UInt32 option, UInt32 value);


        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern LibrgContext *librg_allocate_ctx();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_release(LibrgContext *ctx);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_init(LibrgContext *ctx);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_free(LibrgContext *ctx);
   
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_tick(LibrgContext *ctx);
        
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool librg_is_server(LibrgContext* ctx);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool librg_is_client(LibrgContext* ctx);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool librg_is_connected(LibrgContext* ctx);
 
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_network_start(LibrgContext* ctx, LibrgAddress address);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_network_stop(LibrgContext* ctx);
               /*

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_network_add(ref librg_ctx_t ctx, UInt16 id, librg_message_cb callback);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_network_remove(ref librg_ctx_t ctx, UInt16 id);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_message_send_all(ref librg_ctx_t ctx, UInt16 id, void* data, UIntPtr size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_message_send_to(ref librg_ctx_t ctx, UInt16 id, ref librg_peer_t peer, void* data, UIntPtr size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_message_send_except(ref librg_ctx_t ctx, UInt16 id, ref librg_peer_t peer, void* data, UIntPtr size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_message_send_instream(ref librg_ctx_t ctx, UInt16 id, UInt32 entity, void* data, UIntPtr size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_message_send_instream_except(ref librg_ctx_t ctx, UInt16 id, UInt32 entity, ref librg_peer_t peer, void* data, UIntPtr size);


        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt64 librg_event_add(ref librg_ctx_t ctx, UInt64 id, librg_event_cb callback);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_event_trigger(ref librg_ctx_t ctx, UInt64 id, ref librg_event_t evt);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_event_remove(ref librg_ctx_t ctx, UInt64 id, UInt64 index);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_event_reject(ref librg_event_t evt);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool librg_event_succeeded(ref librg_event_t evt);


        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_data_init(ref librg_data_t data);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_data_init_size(ref librg_data_t data, UIntPtr size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_data_free(ref librg_data_t data);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_data_reset(ref librg_data_t data);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_data_grow(ref librg_data_t data, UIntPtr min_size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr librg_data_capacity(ref librg_data_t data);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr librg_data_get_rpos(ref librg_data_t data);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr librg_data_get_wpos(ref librg_data_t data);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_data_set_rpos(ref librg_data_t data, UIntPtr position);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_data_set_wpos(ref librg_data_t data, UIntPtr position);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_data_rptr(ref librg_data_t data, void* ptr, UIntPtr size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_data_wptr(ref librg_data_t data, void* ptr, UIntPtr size);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_data_rptr_at(ref librg_data_t data, void* ptr, UIntPtr size, IntPtr position);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_data_wptr_at(ref librg_data_t data, void* ptr, UIntPtr size, IntPtr position);


        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 librg_entity_create(ref librg_ctx_t ctx, UInt32 type);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool librg_entity_valid(ref librg_ctx_t ctx, UInt32 entity);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 librg_entity_type(ref librg_ctx_t ctx, UInt32 entity);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern librg_entity_blob_t* librg_entity_blob(ref librg_ctx_t ctx, UInt32 entity);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_entity_destroy(ref librg_ctx_t ctx, UInt32 entity);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UIntPtr librg_entity_query(ref librg_ctx_t ctx, UInt32 entity, UInt32** result);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern UInt32 librg_entity_find(ref librg_ctx_t ctx, ref librg_peer_t peer);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_entity_visibility_set(ref librg_ctx_t ctx, UInt32 entity, bool state);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_entity_visibility_set_for(ref librg_ctx_t ctx, UInt32 entity, UInt32 target, bool state);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool librg_entity_visibility_get(ref librg_ctx_t ctx, UInt32 entity);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool librg_entity_visibility_get_for(ref librg_ctx_t ctx, UInt32 entity, UInt32 target);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_entity_control_set(ref librg_ctx_t ctx, UInt32 entity, ref librg_peer_t peer);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern librg_peer_t* librg_entity_control_get(ref librg_ctx_t ctx, UInt32 entity);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_entity_control_remove(ref librg_ctx_t ctx, UInt32 entity);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void librg_entity_iterate(ref librg_ctx_t ctx, UInt64 flags, librg_entity_cb callback);*/

    }
}
