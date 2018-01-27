using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Librg
{
    public enum Mode
    {
        Server,
        Client,
    };

    public unsafe class Context
    {
        private Native.LibrgContext* _ctx;
        private bool initialized = false;

        public bool IsSet
        {
            get { return _ctx != null; }
        }

        public Native.LibrgContext* NativeData
        {
            get { return _ctx; }
            set { _ctx = value; }
        }

        public Mode Mode
        {
            get { CheckCreated(); return (Mode)_ctx->mode; }
            set { CheckCreated(); _ctx->mode = (UInt16)value; }
        }        

        public UInt16 TickDelay
        {
            get { CheckCreated(); return _ctx->tick_delay; }
            set { CheckCreated(); _ctx->tick_delay = value; }
        }

        public UInt16 MaxConnections
        {
            get { CheckCreated(); return _ctx->max_connections; }
            set { CheckCreated(); _ctx->max_connections = value; }
        }

        public UInt32 MaxEntities
        {
            get { CheckCreated(); return _ctx->max_entities; }
            set { CheckCreated(); _ctx->max_entities = value; }
        }

        public float WorldX
        {
            get { CheckCreated(); return _ctx->world_size.x; }
            set { CheckCreated(); _ctx->world_size.x = value; }
        }

        public float WorldY
        {
            get { CheckCreated(); return _ctx->world_size.y; }
            set { CheckCreated(); _ctx->world_size.y = value; }
        }

        public float WorldZ
        {
            get { CheckCreated(); return _ctx->world_size.z; }
            set { CheckCreated(); _ctx->world_size.z = value; }
        }

        public float LastUpdate
        {
            get { CheckCreated(); return _ctx->last_update; }
            set { CheckCreated(); _ctx->last_update = value; }
        }

        public Context()
        {
            _ctx = Native.librg_allocate_ctx();
        }

        ~Context()
        {
            Dispose(false);
        }

        public void Init()
        {
            Native.librg_init(_ctx);
            initialized = true;
        }

        public void Free()
        {
            Native.librg_free(_ctx);
            initialized = false;
        }

        public void Tick()
        {
            Native.librg_tick(_ctx);
        }

        public bool IsConnected()
        {
            return Native.librg_is_connected(_ctx);
        }

        public bool IsServer()
        {
            return Native.librg_is_server(_ctx);
        }

        public bool IsClient()
        {
            return Native.librg_is_client(_ctx);
        }

        public void Start(String host, uint port)
        {
            Native.librg_network_start(_ctx, new Native.LibrgAddress {
                port = port,
                host = host,
            });
        }

        public void Start(uint port)
        {
            Native.librg_network_start(_ctx, new Native.LibrgAddress {
                port = port,
            });
        }

        public void Stop()
        {
            Native.librg_network_stop(_ctx);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_ctx != null)
            {
                if (initialized) {
                    Free();
                }

                Native.librg_release(_ctx);
                _ctx = null;
            }
        }

        private void CheckCreated()
        {
            if (_ctx == null)
            {
                throw new InvalidOperationException("Not created.");
            }
        }
    }
}
