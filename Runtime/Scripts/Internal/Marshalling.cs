using System;
using System.Runtime.InteropServices;

namespace Unity.WebRTC
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct OptionalInt
    {
        [MarshalAs(UnmanagedType.U1)]
        public bool hasValue;
        public int value;

        public static implicit operator int?(OptionalInt a)
        {
            return a.hasValue ? a.value : (int?)null;
        }
        public static implicit operator OptionalInt(int? a)
        {
            return new OptionalInt { hasValue = a.HasValue, value = a.GetValueOrDefault() };
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct OptionalUlong
    {
        [MarshalAs(UnmanagedType.U1)]
        public bool hasValue;
        public ulong value;

        public static implicit operator ulong?(OptionalUlong a)
        {
            return a.hasValue ? a.value : (ulong?)null;
        }
        public static implicit operator OptionalUlong(ulong? a)
        {
            return new OptionalUlong { hasValue = a.HasValue, value = a.GetValueOrDefault() };
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct OptionalUint
    {
        [MarshalAs(UnmanagedType.U1)]
        public bool hasValue;
        public uint value;

        public static implicit operator uint?(OptionalUint a)
        {
            return a.hasValue ? a.value : (uint?)null;
        }
        public static implicit operator OptionalUint(uint? a)
        {
            return new OptionalUint { hasValue = a.HasValue, value = a.GetValueOrDefault() };
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct OptionalDouble
    {
        [MarshalAs(UnmanagedType.U1)]
        public bool hasValue;
        public double value;

        public static implicit operator double?(OptionalDouble a)
        {
            return a.hasValue ? a.value : (double?)null;
        }
        public static implicit operator OptionalDouble(double? a)
        {
            return new OptionalDouble { hasValue = a.HasValue, value = a.GetValueOrDefault() };
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MarshallingArray<T> where T : struct
    {
        public int length;
        public IntPtr ptr;

        public T[] ToArray()
        {
            var array = ptr.AsArray<T>(length);
            ptr = IntPtr.Zero;
            return array;
        }

        public void Set(T[] array)
        {
            length = array.Length;
            ptr = IntPtrExtension.ToPtr(array);
        }

        public void Dispose()
        {
            if (ptr != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(ptr);
                ptr = IntPtr.Zero;
            }
            length = 0;
        }
    }
}
