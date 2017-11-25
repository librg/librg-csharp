using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using librg;
using System.Runtime.InteropServices;

public class test : MonoBehaviour {
    internal Librg ctx;
    int foo = 0;

    // Use this for initialization
    void Start()
    {
        ctx = new Librg(Mode.Client, 32, Vector3.one * 5000.0f, 50000);

        Debug.Log(ctx.IsClient());

        ctx.EventAdd(124, (librg.Event z) => {
            Debug.Log(z.data);
        });

        ctx.EventTrigger(124, new librg.Event(228));

        var foo = new Data();

        foo.WriteUInt32(15);
        foo.WriteFloat32(99.2424f);

        ctx.NetworkStart("localhost", 7777);

        ctx.EventAdd(ActionType.CONNECTION_ACCEPT, (librg.Event evt) => {
            new Message(ctx, 42, (Data data) => { data.WriteUInt32(1515); }).SendToAll();
        });

        //librg_internal.librg_message_send_all(ref ctx.ctx, 42, foo)
    }

    void Update()
    {
        ctx.Tick();
    }

        /*
        Librg.OptionSet(Option.LIBRG_PLATFORM_BUILD, 15);


        ctx = new librg_ctx_t
        {
            mode = Librg.MODE_SERVER,
            max_connections = 15,
            max_entities = 50000,
            tick_delay = 32,
        };

        librg_internal.librg_init(ref ctx);

        librg_internal.librg_event_add(ref ctx, (ulong)librg.Event.LIBRG_CONNECTION_REQUEST, (ref librg_event_t evt) => {
            Debug.Log("LIBRG_CONNECTION_REQUEST");
        });

        librg_internal.librg_event_add(ref ctx, (ulong)librg.Event.LIBRG_CONNECTION_ACCEPT, (ref librg_event_t evt) => {
            Debug.Log("LIBRG_CONNECTION_ACCEPT");
        });

        librg_internal.librg_event_add(ref ctx, (ulong)librg.Event.LIBRG_CONNECTION_REFUSE, (ref librg_event_t evt) => {
            Debug.Log("LIBRG_CONNECTION_REFUSE");
        });

        librg_internal.librg_event_add(ref ctx, 42, (ref librg_event_t evt) => {
            Debug.Log("CUSTOM EVENT");
            unsafe { Debug.Log(*(int*)evt.user_data); }
        });

        librg_internal.librg_event_add(ref ctx, (ulong)librg.Event.LIBRG_ENTITY_CREATE, (ref librg_event_t evt) => {
            Debug.Log("LIBRG_ENTITY_CREATE | id: " + evt.entity);
        });

        librg_internal.librg_event_add(ref ctx, (ulong)librg.Event.LIBRG_ENTITY_UPDATE, (ref librg_event_t evt) => {
            float a = 0;
            unsafe
            {
                librg_internal.librg_data_rptr(ref *evt.data, &a, (UIntPtr)sizeof(float));
            }
        });

        librg_internal.librg_network_add(ref ctx, 42, (ref librg_message_t msg) => {
            Debug.Log("GOT MESSAGE 42");
        });

        if (true)
        librg_internal.librg_network_start(ref ctx, new librg_address_t
        {
            port = 7777,
            host = "localhost",
        });

        Debug.Log("connecting");

        var data = new librg_data_t();

        librg_internal.librg_data_init(ref data);

        unsafe {
            float foo = 32.0f;
            float bar = 699.0f;
            librg_internal.librg_data_wptr(ref data, &foo, (UIntPtr)sizeof(float));
            librg_internal.librg_data_wptr(ref data, &bar, (UIntPtr)sizeof(float));

            float zoo;
            float zar;

            librg_internal.librg_data_rptr(ref data, &zoo, (UIntPtr)sizeof(float));
            librg_internal.librg_data_rptr(ref data, &zar, (UIntPtr)sizeof(float));

            Debug.Log(zoo);
            Debug.Log(zar);
        }

        librg_internal.librg_data_free(ref data);

        unsafe
        {
            for (var i = 0; i < 15000; i++)
            {
                librg_internal.librg_entity_create(ref ctx, 0);
                librg_entity_blob_t* blob = librg_internal.librg_entity_blob(ref ctx, (uint)i);
                blob->position = new Vector3();
                blob->position.x = i;
                blob->position.y = i;
            }

            var ent = librg_internal.librg_entity_create(ref ctx, 0);
            librg_entity_blob_t* ablob = librg_internal.librg_entity_blob(ref ctx, (uint)ent);
            ablob->position = new Vector3();
            ablob->position.x = 5;
            ablob->position.y = 5;

            Debug.Log("created entity " + ent);
            Debug.Log("entity valid: " + librg_internal.librg_entity_valid(ref ctx, ent));
            //librg_internal.librg_entity_visibility_set(ref ctx, ent, false);
            Debug.Log("visibility for: " + librg_internal.librg_entity_visibility_get(ref ctx, ent));

            librg_internal.librg_tick(ref ctx);

            uint *result;
            uint size = (uint)librg_internal.librg_entity_query(ref ctx, ent, &result);

            Debug.Log(ablob->flags);
            Debug.Log("found: " + size);
        }
    }

	// Update is called once per frame
	void Update () {
        librg_internal.librg_tick(ref ctx);

        if (++foo > 500) {
            foo = 0; Debug.Log("triggereing event");
            var evt = new librg_event_t();
            int a = 15;
            unsafe { evt.user_data = &a; }
            librg_internal.librg_event_trigger(ref ctx, 42, ref evt);
        }

        if (Input.GetKey("escape")) {
            librg_internal.librg_network_stop(ref ctx);
            librg_internal.librg_free(ref ctx);
            Application.Quit();
        }
    }

    void OnDestroy() {
    }*/
}
