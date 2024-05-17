namespace BlazorApp.Extensions.ViewModels
{
    public class JwtViewModel
    {
        public string? id { get; set; }
        public string? userName { get; set; }
        public bool isEmailConfirmed { get; set; }
        public bool requiresTwoFactor { get; set; }
        public string? token { get; set; }
    }
}
