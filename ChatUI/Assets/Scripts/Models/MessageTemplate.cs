namespace Assets.Scripts.Models
{
    using System;

    public class MessageTemplate
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime SentOn { get; set; }

        public int IsMyMessage { get; set; }
    }
}
