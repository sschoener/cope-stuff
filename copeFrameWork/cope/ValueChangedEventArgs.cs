﻿#region

using System;

#endregion

namespace cope
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public ValueChangedEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public T OldValue { get; private set; }

        public T NewValue { get; private set; }
    }
}