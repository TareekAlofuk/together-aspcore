namespace together_aspcore.Shared.Response
{
    public class SuccessfulStatusResponse
    {
        public bool Success { get; set; }
        public string message { get; set; }
        public object extra { get; set; }
    }
}