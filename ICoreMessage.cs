namespace ProducerForRabbitMQ
{
    public interface ICoreMessage
    {
        void SendMessage(string message);
    }
}
