using System;
using MortiseFrame.LitIO;
using UnityEngine;

public static class ReceiveCore {

    public static void PreReceiveBuffer(ReceiveContext receiveContext, byte[] bytes) {
        if (bytes.Length == 0) {
            return;
        }
        Buffer.BlockCopy(bytes, 0, receiveContext.messagePreReceiveBuffer, 0, bytes.Length);
        Debug.Log("PreReceive : Bytes: " + BitConverter.ToString(bytes).Replace("-", " ") + " Length: " + bytes.Length);
    }

    public static void TickReceiveBuffer(ReceiveContext receiveContext) {

        if (receiveContext.messagePreReceiveBuffer == null) {
            return;
        }

        byte[] bytes = receiveContext.messagePreReceiveBuffer;
        int offset = 0;
        int msgCount = ByteReader.Read<int>(bytes, ref offset);

        for (int i = 0; i < msgCount; i++) {
            int length = ByteReader.Read<int>(bytes, ref offset);
            if (length == 0) {
                break;
            }
            byte id = ByteReader.Read<byte>(bytes, ref offset);
            ReadMessage(receiveContext, id, bytes, ref offset);
        }

        receiveContext.ClearBuffer();

    }

    public static void DirectReadMessage(ReceiveContext receiveContext, byte[] bytes) {
        int offset = 0;
        int length = ByteReader.Read<int>(bytes, ref offset);
        byte id = ByteReader.Read<byte>(bytes, ref offset);
        ReadMessage(receiveContext, id, bytes, ref offset);
    }

    public static void ReadMessage(ReceiveContext receiveContext, int id, byte[] bytes, ref int offset) {
        switch (id) {
            case 101:
                ReadReq1(receiveContext, bytes, ref offset);
                break;
            case 103:
                ReadReq2(receiveContext, bytes, ref offset);
                break;
        }
    }

    public static void ReadReq1(ReceiveContext receiveContext, byte[] bytes, ref int offset) {
        Test01ReqMessage message = new Test01ReqMessage();
        message.FromBytes(bytes, ref offset);
        string txt1 = message.txt1;
        string txt2 = message.txt2;
        Debug.Log("Read Message : Txt1: " + txt1 + " Txt2: " + txt2 + " Type: " + message.GetType().Name + " Bytes: " + BitConverter.ToString(bytes).Replace("-", " "));
    }

    public static void ReadReq2(ReceiveContext receiveContext, byte[] bytes, ref int offset) {
        Test02ReqMessage message = new Test02ReqMessage();
        message.FromBytes(bytes, ref offset);
        string txt3 = message.txt3;
        string txt4 = message.txt4;
        Debug.Log("Read Message : Txt3: " + txt3 + " Txt4: " + txt4 + " Type: " + message.GetType().Name + " Bytes: " + BitConverter.ToString(bytes).Replace("-", " "));
    }

}