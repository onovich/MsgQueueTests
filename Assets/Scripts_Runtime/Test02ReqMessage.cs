using System;
using MortiseFrame.LitIO;

public struct Test02ReqMessage : IMessage {

    public string txt3;
    public string txt4;

    public void WriteTo(byte[] dst, ref int offset) {
        ByteWriter.WriteUTF8String(dst, txt3, ref offset);
        ByteWriter.WriteUTF8String(dst, txt4, ref offset);
    }

    public void FromBytes(byte[] src, ref int offset) {
        txt3 = ByteReader.ReadUTF8String(src, ref offset);
        txt4 = ByteReader.ReadUTF8String(src, ref offset);
    }

    public int GetEvaluatedSize(out bool isCertain) {
        int count = ByteCounter.CountUTF8String(txt3) + ByteCounter.CountUTF8String(txt4);
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