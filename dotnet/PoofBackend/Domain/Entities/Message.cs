using System;

namespace Domain.Entities
{
    public class Message
    {
        public Message(string id, string kuldo, string tartalom, DateTime datum)
        {
            Id = id;
            Kuldo = kuldo;
            Tartalom = tartalom;
            Datum = datum;
        }

        public string Id { get; set; }
        public string Kuldo { get; set; }
        public string Tartalom { get; set; }
        public DateTime Datum { get; set; }
    }
}
