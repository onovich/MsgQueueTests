using System.Collections.Generic;

public class SendContext {

    Queue<IMessage> messagePreSendQueue;

    public SendContext() {
        messagePreSendQueue = new Queue<IMessage>();
    }

    public void Enqueue(IMessage message) {
        messagePreSendQueue.Enqueue(message);
    }

    public bool TryDequeue(out IMessage message) {
        return messagePreSendQueue.TryDequeue(out message);
    }

    public int GetCount() {
        return messagePreSendQueue.Count;
    }

}