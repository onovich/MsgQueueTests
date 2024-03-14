using System;
using MortiseFrame.LitIO;

public struct Test01ReqMessage : IMessage {

    public string txt1;
    public string txt2;

    public void WriteTo(byte[] dst, ref int offset) {
        ByteWriter.WriteUTF8String(dst, txt1, ref offset);
        ByteWriter.WriteUTF8String(dst, txt2, ref offset);
    }

    public void FromBytes(byte[] src, ref int offset) {
        txt1 = ByteReader.ReadUTF8String(src, ref offset);
        txt2 = ByteReader.ReadUTF8String(src, ref offset);
    }

    public int GetEvaluatedSize(out bool isCertain) {
        int count = ByteCounter.CountUTF8String(txt1) + ByteCounter.CountUTF8String(txt2);
        isCertain = false;
        return count;
    }

    public byte[] ToBytes() {
        int count = GetEvaluatedSize(out bool isCertain);
        int offset = 0;
        byte[] src = new byte[count];
        WriteTo(src, ref offset);
        if (isCertain) {
            return src;
        } else {
            byte[] dst = new byte[offset];
            Buffer.BlockCopy(src, 0, dst, 0, offset);
            return dst;
        }
    }

}