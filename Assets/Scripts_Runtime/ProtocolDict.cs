using System;
using System.Collections.Generic;

public static class ProtocolDict {

    public static byte GetID(IMessage msg) {
        var type = msg.GetType();
        var has = typeToIdDict.TryGetValue(type, out byte id);
        if (!has) {
            throw new ArgumentException("ID Not Found");
        }
        return id;
    }

    static readonly Dictionary<Type, byte> typeToIdDict = new Dictionary<Type, byte> {
            {typeof(Test01ReqMessage), 101},
            {typeof(Test02ReqMessage), 103},
    };

}