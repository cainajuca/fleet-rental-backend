namespace Fleet.Worker;
public static class Env
{
    public static class Keys
    {
        public const string ApiBaseUrl = "API_BASE_URL";
        public const string RabbitMqHost = "RABBITMQ_HOST";
    }

    public static string Get(string key, string fallback) =>
        Environment.GetEnvironmentVariable(key) ?? fallback;
}
