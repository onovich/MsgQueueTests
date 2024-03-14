using System;

public class ReceiveContext {

    public byte[] messagePreReceiveBuffer;

    public ReceiveContext() {
        messagePreReceiveBuffer = new byte[1024];
    }

    public void ClearBuffer() {
        Array.Clear(messagePreReceiveBuffer, 0, messagePreReceiveBuffer.Length);
    }

}