using System;
using MortiseFrame.LitIO;
using UnityEngine;

public static class SendCore {

    public static void EnqueuReq1(SendContext context, string txt1, string txt2) {
        Test01ReqMessage message = new Test01ReqMessage();
        message.txt1 = txt1;
        message.txt2 = txt2;
        context.Enqueue(message);
    }

    public static void EnqueueReq2(SendContext context, string txt3, string txt4) {
        Test02ReqMessage message = new Test02ReqMessage();
        message.txt3 = txt3;
        message.txt4 = txt4;
        context.Enqueue(message);
    }

    public static void DirectSendReq1(SendContext context, string txt1, string txt2, Action<byte[]> Send) {
        Test01ReqMessage message = new Test01ReqMessage();
        message.txt1 = txt1;
        message.txt2 = txt2;

        var dst = new byte[1024];
        var src = message.ToBytes();

        byte id = ProtocolDict.GetID(message);
        int length = src.Length;

        int offset = 0;

        ByteWriter.Write<int>(dst, length, ref offset);
        ByteWriter.Write<byte>(dst, id, ref offset);
        ByteWriter.WriteArray<byte>(dst, src, ref offset);

        Send.Invoke(dst);
    }

    public static void DirectSendReq2(SendContext context, string txt3, string txt4, Action<byte[]> Send) {
        Test02ReqMessage message = new Test02ReqMessage();
        message.txt3 = txt3;
        message.txt4 = txt4;

        var dst = new byte[1024];
        var src = message.ToBytes();

        byte id = ProtocolDict.GetID(message);
        int length = src.Length;

        int offset = 0;

        ByteWriter.Write<int>(dst, length, ref offset);
        ByteWriter.Write<byte>(dst, id, ref offset);
        ByteWriter.WriteArray<byte>(dst, src, ref offset);

        Send.Invoke(dst);
    }

    public static void TickQueueSend(SendContext context, Action<byte[]> Send) {
        byte[] dst = new byte[1024];
        int offset = 0;
        int msgCount = context.GetCount();
        if (msgCount == 0) {
            return;
        }
        ByteWriter.Write<int>(dst, msgCount, ref offset);
        while (context.TryDequeue(out IMessage message)) {
            var src = message.ToBytes();
            var length = src.Length;
            byte id = ProtocolDict.GetID(message);

            ByteWriter.Write<int>(dst, length, ref offset);
            ByteWriter.Write<byte>(dst, id, ref offset);
            ByteWriter.WriteArray<byte>(dst, src, ref offset);

            Debug.Log("Send Message : ID: " + id + " Length: " + length + " Type: " + message.GetType().Name);
        }
        if (offset == 0) {
            return;
        }
        Send.Invoke(dst);
    }

}