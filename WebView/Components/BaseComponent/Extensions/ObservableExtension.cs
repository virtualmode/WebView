using System.Reactive.Linq;

namespace WebView
{
    public static class ObservableExtensions
    {
        /// <summary>
        /// Debounce events.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static IObservable<T> SampleFirst<T>(
            this IObservable<T> source,
            TimeSpan timeout)
        {
            return source.Window(() => { return Observable.Interval(timeout); })
                        .SelectMany(x => x.Take(1));
        }
    }
}
