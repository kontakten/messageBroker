using System;

[Serializable]
public class Message
{
    public string Text { get; set; }
    public int ID { get; set; }
    public string Timestamp { get; set; }
}