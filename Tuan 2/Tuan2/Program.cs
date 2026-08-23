namespace Tuan2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            app.MapGet("/", () => "hello Quang Minh");
            app.Run();
        }
    }
}
