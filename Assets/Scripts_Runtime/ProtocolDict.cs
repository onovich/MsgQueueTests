using System;
using System.Collections.Generic;

public static class ProtocolDict {

    public static object GetMessage(byte id) {
        var has = idToTypeDict.TryGetValue(id, out Type type);
        if (!has) {
            throw new ArgumentException("No type found for the given ID.", id.ToString());
        }
        return Activator.CreateInstance(type);
    }

    public static byte GetID(IMessage msg) {
        var type = msg.GetType();
        var has = typeToIdDict.TryGetValue(type, out byte id);
        if (!has) {
            throw new ArgumentException("ID Not Found");
        }
        return id;
    }

    public static byte GetID<T>() where T : IMessage {
        var has = typeToIdDict.TryGetValue(typeof(T), out byte id);
        if (!has) {
            throw new ArgumentException("ID Not Found");
        }
        return id;
    }

    static readonly Dictionary<byte, Type> idToTypeDict = new Dictionary<byte, Type> {
            {101, typeof(Test01ReqMessage)},
            {103, typeof(Test02ReqMessage)},
    };

    static readonly Dictionary<Type, byte> typeToIdDict = new Dictionary<Type, byte> {
            {typeof(Test01ReqMessage), 101},
            {typeof(Test02ReqMessage), 103},
    };

}