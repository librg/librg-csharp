using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Librg
{
    public unsafe class Data
    {
        protected Native.LibrgData* _ptr;

        public Data()
        {
            _ptr = Native.librg_data_init_new();
        }

        ~Data()
        {
            Native.librg_data_free(_ptr);
            Native.librg_release(_ptr);
        }

        public void Reset()
        {
            Native.librg_data_reset(_ptr);
        }

        public ulong Capacity
        {
            get { return (ulong)Native.librg_data_capacity(_ptr); }
        }

        public ulong ReadPosition
        {
            get { return (ulong)Native.librg_data_get_rpos(_ptr); }
            set { Native.librg_data_set_rpos(_ptr, (UIntPtr)value); }
        }

        public ulong WritePosition
        {
            get { return (ulong)Native.librg_data_get_wpos(_ptr); }
            set { Native.librg_data_set_wpos(_ptr, (UIntPtr)value); }
        }

        private void CheckCreated()
        {
            if (_ptr == null)
            {
                throw new InvalidOperationException("Not created.");
            }
        }

        public void WriteUInt8At(Byte value, long position)
        {
            CheckCreated();
            Native.librg_data_wptr_at(_ptr, &value, (UIntPtr)sizeof(Byte), (IntPtr)position);
        }

        public void WriteUInt16At(UInt16 value, long position)
        {
            CheckCreated();
            Native.librg_data_wptr_at(_ptr, &value, (UIntPtr)sizeof(UInt16), (IntPtr)position);
        }

        public void WriteUInt32At(UInt32 value, long position)
        {
            CheckCreated();
            Native.librg_data_wptr_at(_ptr, &value, (UIntPtr)sizeof(UInt32), (IntPtr)position);
        }

        public void WriteUInt64At(UInt64 value, long position)
        {
            CheckCreated();
            Native.librg_data_wptr_at(_ptr, &value, (UIntPtr)sizeof(UInt64), (IntPtr)position);
        }

        public void WriteInt8At(SByte value, long position)
        {
            CheckCreated();
            Native.librg_data_wptr_at(_ptr, &value, (UIntPtr)sizeof(SByte), (IntPtr)position);
        }

        public void WriteInt16At(Int16 value, long position)
        {
            CheckCreated();
            Native.librg_data_wptr_at(_ptr, &value, (UIntPtr)sizeof(Int16), (IntPtr)position);
        }

        public void WriteInt32At(Int32 value, long position)
        {
            CheckCreated();
            Native.librg_data_wptr_at(_ptr, &value, (UIntPtr)sizeof(Int32), (IntPtr)position);
        }

        public void WriteInt64At(Int64 value, long position)
        {
            CheckCreated();
            Native.librg_data_wptr_at(_ptr, &value, (UIntPtr)sizeof(Int64), (IntPtr)position);
        }

        public void WriteFloat32At(float value, long position)
        {
            CheckCreated();
            Native.librg_data_wptr_at(_ptr, &value, (UIntPtr)sizeof(float), (IntPtr)position);
        }

        public void WriteFloat64At(double value, long position)
        {
            CheckCreated();
            Native.librg_data_wptr_at(_ptr, &value, (UIntPtr)sizeof(double), (IntPtr)position);
        }

        public Byte ReadUInt8At(long position)
        {
            CheckCreated();
            Byte value;
            Native.librg_data_rptr_at(_ptr, &value, (UIntPtr)sizeof(Byte), (IntPtr)position);
            return value;
        }

        public UInt16 ReadUInt16At(long position)
        {
            CheckCreated();
            UInt16 value;
            Native.librg_data_rptr_at(_ptr, &value, (UIntPtr)sizeof(UInt16), (IntPtr)position);
            return value;
        }

        public UInt32 ReadUInt32At(long position)
        {
            CheckCreated();
            UInt32 value;
            Native.librg_data_rptr_at(_ptr, &value, (UIntPtr)sizeof(UInt32), (IntPtr)position);
            return value;
        }

        public UInt64 ReadUInt64At(long position)
        {
            CheckCreated();
            UInt64 value;
            Native.librg_data_rptr_at(_ptr, &value, (UIntPtr)sizeof(UInt64), (IntPtr)position);
            return value;
        }

        public SByte ReadInt8At(long position)
        {
            CheckCreated();
            SByte value;
            Native.librg_data_rptr_at(_ptr, &value, (UIntPtr)sizeof(SByte), (IntPtr)position);
            return value;
        }

        public UInt16 ReadInt16At(long position)
        {
            CheckCreated();
            UInt16 value;
            Native.librg_data_rptr_at(_ptr, &value, (UIntPtr)sizeof(UInt16), (IntPtr)position);
            return value;
        }

        public UInt32 ReadInt32At(long position)
        {
            CheckCreated();
            UInt32 value;
            Native.librg_data_rptr_at(_ptr, &value, (UIntPtr)sizeof(UInt32), (IntPtr)position);
            return value;
        }

        public UInt64 ReadInt64At(long position)
        {
            CheckCreated();
            UInt64 value;
            Native.librg_data_rptr_at(_ptr, &value, (UIntPtr)sizeof(UInt64), (IntPtr)position);
            return value;
        }

        public float ReadFloat32At(long position)
        {
            CheckCreated();
            float value;
            Native.librg_data_rptr_at(_ptr, &value, (UIntPtr)sizeof(float), (IntPtr)position);
            return value;
        }

        public double ReadFloat64At(long position)
        {
            CheckCreated();
            double value;
            Native.librg_data_rptr_at(_ptr, &value, (UIntPtr)sizeof(double), (IntPtr)position);
            return value;
        }

        public void WriteUInt8(Byte value)
        {
            CheckCreated();
            Native.librg_data_wptr(_ptr, &value, (UIntPtr)sizeof(Byte));
        }

        public void WriteUInt16(UInt16 value)
        {
            CheckCreated();
            Native.librg_data_wptr(_ptr, &value, (UIntPtr)sizeof(UInt16));
        }

        public void WriteUInt32(UInt32 value)
        {
            CheckCreated();
            Native.librg_data_wptr(_ptr, &value, (UIntPtr)sizeof(UInt32));
        }

        public void WriteUInt64(UInt64 value)
        {
            CheckCreated();
            Native.librg_data_wptr(_ptr, &value, (UIntPtr)sizeof(UInt64));
        }

        public void WriteInt8(SByte value)
        {
            CheckCreated();
            Native.librg_data_wptr(_ptr, &value, (UIntPtr)sizeof(SByte));
        }

        public void WriteInt16(Int16 value)
        {
            CheckCreated();
            Native.librg_data_wptr(_ptr, &value, (UIntPtr)sizeof(Int16));
        }

        public void WriteInt32(Int32 value)
        {
            CheckCreated();
            Native.librg_data_wptr(_ptr, &value, (UIntPtr)sizeof(Int32));
        }

        public void WriteInt64(Int64 value)
        {
            CheckCreated();
            Native.librg_data_wptr(_ptr, &value, (UIntPtr)sizeof(Int64));
        }

        public void WriteFloat32(float value)
        {
            CheckCreated();
            Native.librg_data_wptr(_ptr, &value, (UIntPtr)sizeof(float));
        }

        public void WriteFloat64(double value)
        {
            CheckCreated();
            Native.librg_data_wptr(_ptr, &value, (UIntPtr)sizeof(double));
        }

        public Byte ReadUInt8()
        {
            CheckCreated();
            Byte value;
            Native.librg_data_rptr(_ptr, &value, (UIntPtr)sizeof(Byte));
            return value;
        }

        public UInt16 ReadUInt16()
        {
            CheckCreated();
            UInt16 value;
            Native.librg_data_rptr(_ptr, &value, (UIntPtr)sizeof(UInt16));
            return value;
        }

        public UInt32 ReadUInt32()
        {
            CheckCreated();
            UInt32 value;
            Native.librg_data_rptr(_ptr, &value, (UIntPtr)sizeof(UInt32));
            return value;
        }

        public UInt64 ReadUInt64()
        {
            CheckCreated();
            UInt64 value;
            Native.librg_data_rptr(_ptr, &value, (UIntPtr)sizeof(UInt64));
            return value;
        }

        public SByte ReadInt8()
        {
            CheckCreated();
            SByte value;
            Native.librg_data_rptr(_ptr, &value, (UIntPtr)sizeof(SByte));
            return value;
        }

        public UInt16 ReadInt16()
        {
            CheckCreated();
            UInt16 value;
            Native.librg_data_rptr(_ptr, &value, (UIntPtr)sizeof(UInt16));
            return value;
        }

        public UInt32 ReadInt32()
        {
            CheckCreated();
            UInt32 value;
            Native.librg_data_rptr(_ptr, &value, (UIntPtr)sizeof(UInt32));
            return value;
        }

        public UInt64 ReadInt64()
        {
            CheckCreated();
            UInt64 value;
            Native.librg_data_rptr(_ptr, &value, (UIntPtr)sizeof(UInt64));
            return value;
        }

        public float ReadFloat32()
        {
            CheckCreated();
            float value;
            Native.librg_data_rptr(_ptr, &value, (UIntPtr)sizeof(float));
            return value;
        }

        public double ReadFloat64()
        {
            CheckCreated();
            double value;
            Native.librg_data_rptr(_ptr, &value, (UIntPtr)sizeof(double));
            return value;
        }
    }
}
