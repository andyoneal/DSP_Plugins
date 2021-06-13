﻿using System;

namespace GSFullSerializer.Internal {
    /// <summary>
    /// Simple option type. This is akin to nullable types.
    /// </summary>
    public struct fsOption<T> {
        private readonly bool _hasValue;
        private readonly T _value;

        public bool HasValue => _hasValue;
        public bool IsEmpty => _hasValue == false;
        public T Value {
            get {
                if (IsEmpty) {
                    throw new InvalidOperationException("fsOption is empty");
                }

                return _value;
            }
        }

        public fsOption(T value) {
            _hasValue = true;
            _value = value;
        }

        public static fsOption<T> Empty;
    }

    public static class fsOption {
        public static fsOption<T> Just<T>(T value) => new fsOption<T>(value);
    }
}