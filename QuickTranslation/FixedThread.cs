using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading.Tasks;

using Himesyo;

namespace QuickTranslation
{
    /// <summary>
    /// 实现 <see cref="IComponent"/> 接口的 <see cref="FixedThread{ShowItem}"/>
    /// </summary>
    public class FixedThreadShowItem : FixedThreadFlag<ShowItem>, IComponent, IDisposable
    {
        public Guid Guid { get; } = Guid.NewGuid();
        /// <inheritdoc/>
        public ISite Site { get; set; }
    }

    /// <summary>
    /// 跨线程异步传递消息。如果多个位置同时接受消息将阻塞。
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class FixedThread<TResult> : IDisposable
    {
        private readonly object sync = new object();
        private TaskCompletionSource<TResult> source;
        private readonly ConcurrentQueue<TResult> results = new ConcurrentQueue<TResult>();

        /// <summary>
        /// 是否已调用 <see cref="Dispose"/> 方法。
        /// </summary>
        [Browsable(false)]
        public bool IsDispose { get; private set; } = false;

        /// <summary>
        /// 释放完成后触发。
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// 设置一个结果，如果已等待则通知。
        /// </summary>
        /// <param name="result"></param>
        public void SetResult(TResult result)
        {
            if (IsDispose)
            {
                return;
            }
            lock (sync)
            {
                if (source == null)
                {
                    results.Enqueue(result);
                }
                else
                {
                    var temp = source;
                    source = null;
                    temp.SetResult(result);
                }
            }
        }

        /// <summary>
        /// 获取一个消息。如果 <see cref="IsDispose"/> 为 <see langword="true"/>，则直接返回 <see langword="default"/>(<typeparamref name="TResult"/>) 。
        /// </summary>
        /// <returns></returns>
        public Task<TResult> WaitMessage()
        {
            if (IsDispose)
            {
                return Task.FromResult<TResult>(default);
            }
            lock (sync)
            {
                if (results.TryDequeue(out TResult result))
                {
                    source = null;
                    return Task.FromResult(result);
                }
                else
                {
                    source = new TaskCompletionSource<TResult>();
                    return source.Task;
                }
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            IsDispose = true;
            source?.TrySetResult(default);
            source = null;
            Disposed?.Invoke(this, EventArgs.Empty);
        }

    }

    /// <summary>
    /// 跨线程异步传递位域。如果多个位置同时接受消息将阻塞。
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class FixedThreadFlag<TResult> : IDisposable
        where TResult : Enum, new()
    {
        private readonly object sync = new object();
        private TaskCompletionSource<TResult> source;
        private TResult cacheResult = default;

        /// <summary>
        /// 是否已调用 <see cref="Dispose"/> 方法。
        /// </summary>
        [Browsable(false)]
        public bool IsDispose { get; private set; } = false;

        /// <summary>
        /// 释放完成后触发。
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// 设置一个位域，如果已经存在缓存的位域则合并。如果已等待则通知。
        /// </summary>
        /// <param name="result"></param>
        public void SetResult(TResult result)
        {
            if (IsDispose)
            {
                return;
            }
            lock (sync)
            {
                if (source == null)
                {
                    if (Equals(cacheResult, default(TResult)))
                    {
                        cacheResult = result;
                    }
                    else
                    {
                        long cache = ((IConvertible)cacheResult).ToInt64(null);
                        long flag = ((IConvertible)result).ToInt64(null);
                        cacheResult = (TResult)Enum.ToObject(typeof(TResult), cache | flag);
                    }
                }
                else
                {
                    var temp = source;
                    source = null;
                    temp.SetResult(result);
                }
            }
        }

        /// <summary>
        /// 获取一个消息。如果 <see cref="IsDispose"/> 为 <see langword="true"/>，则直接返回 <see langword="default"/>(<typeparamref name="TResult"/>) 。
        /// </summary>
        /// <returns></returns>
        public Task<TResult> WaitMessage()
        {
            if (IsDispose)
            {
                return Task.FromResult<TResult>(default);
            }
            lock (sync)
            {
                if (Equals(cacheResult, default(TResult)))
                {
                    source = new TaskCompletionSource<TResult>();
                    return source.Task;
                }
                else
                {
                    TResult result = cacheResult;
                    source = null;
                    cacheResult = default;
                    return Task.FromResult(result);
                }
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            IsDispose = true;
            source?.TrySetResult(default);
            source = null;
            Disposed?.Invoke(this, EventArgs.Empty);
        }

    }
}
