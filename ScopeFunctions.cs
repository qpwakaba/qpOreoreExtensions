using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace qpwakaba.Extensions
{
    [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "not needed")]
    [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "not needed")]
    public static class ScopeFunctions
    {
        public static R Let<T, R>(this T t, Func<T, R> action) => action(t);
        public static Void Let<T>(this T t, Action<T> action)
        {
            action(t);
            return default;
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "need catch all exceptions")]
        public static PipeState<TOut> Pipe<TIn, TOut>(this TIn tin, Func<TIn, TOut> func)
        {
            try
            {
                return func(tin);
            }
            catch (Exception ex)
            {
                return PipeState<TOut>.FromException(ex);    
            }
        }
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "need catch all exceptions")]
        public static PipeState<TOut> Pipe<TIn, TOut>(this PipeState<TIn> tin, Func<TIn, TOut> func)
        {
            if (tin.HasException) return PipeState<TOut>.FromException(tin.Exception);

            try
            {
                return func(tin.Value);
            }
            catch (Exception ex)
            {
                return PipeState<TOut>.FromException(ex);    
            }
        }
        public static T Also<T, R>(this T t, Func<T, R> action)
        {
            action(t);
            return t;
        }
        public static T Also<T>(this T t, Action<T> action)
        {
            action(t);
            return t;
        }
        public static T Also<T>(this T t, Action action)
        {
            action();
            return t;
        }

        public static IfExpression<T> If<T>(this T t, Predicate<T> condition) => new IfExpression<T>(t, condition(t));
        public static IfExpression<T> If<T>(this T t, bool condition) => new IfExpression<T>(t, condition);

        public readonly ref struct PipeState<T>
        {
            private readonly T value;
            private readonly Exception exception;
            private readonly bool unavailable;

            public PipeState(T value, Exception exception, bool unavailable)
            {
                this.value = value;
                this.exception = exception;
                this.unavailable = unavailable;
            }
            public T Value
            {
                get
                {
                    if (this.exception != null) throw this.exception;
                    return value;
                }
            }
            public Exception Exception
            {
                get
                {
                    if (this.exception == null) throw new InvalidOperationException("No exception has been occured.");
                    return exception;
                }
            }
            public bool HasException => this.exception != null;
            public bool Available => this.HasException.Not() && this.unavailable.Not();

            public PipeState<T> Or(T value) => this.Available switch
                {
                    true => value,
                    false => this
                };
            public PipeState<T> On<TException>(Func<T> func) => this.exception switch
            {
                TException _ => func(),
                _ => this
            };
            public PipeState<T> On<TException>(Func<TException, T> func) => this.exception switch
            {
                TException ex => func(ex),
                _ => this
            };
            public PipeState<T> On<TException>(Action action)
            {
                if (/* this.HasException && */ this.exception is TException)
                {
                    action();
                    return new PipeState<T>(default, default, true);
                }
                return this;
            }
            public PipeState<T> On<TException>(Action<TException> action)
            {
                if (/* this.HasException && */ this.exception is TException ex)
                {
                    action(ex);
                    return new PipeState<T>(default, default, true);
                }
                return this;
            }
            public PipeState<T> OnAvailable(Action<T> action)
            {
                if (this.Available) action(this.value);
                return this;
            }

            public static implicit operator PipeState<T>(T value) => new PipeState<T>(value, null, false);
            public static implicit operator T(PipeState<T> value) => value.Value;
            public static PipeState<T> FromValue(T value) => new PipeState<T>(value, null, false);
            public static PipeState<T> FromException(Exception ex) => new PipeState<T>(default, ex, false);
        }

        public readonly ref struct IfExpression<T>
        {
            private readonly T t;
            private readonly bool condition;
            internal IfExpression(T t, bool cond)
            {
                this.t = t;
                this.condition = cond;
            }

            public IfExpression<T> Then(Action<T> action)
            {
                if (condition) action(this.t);
                return this;
            }

            public IfExpression<T> Otherwise(Action<T> action)
            {
                if (condition.Not()) action(this.t);
                return this;
            }

            public T Fi() => t;
        }

        public readonly struct Void { }

    }
}
