namespace blog_list_net_backend.Utils
{
    public static class ConfigValues
    {
        private static IConfigurationRoot MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        public readonly static string? JWT_SECRET = MyConfig.GetValue<string>("Jwt:Key");
    }
}
