using ServiceStack.Caching;

static class SessionUtils
{
    public static T GetOrAdd<T>(this ISession session, string key, T defaultValue = default)
    {
        var value = session.Get<T>(key);
        if (value != null) return value;

        session[key] = defaultValue;

        return defaultValue;
    }
}