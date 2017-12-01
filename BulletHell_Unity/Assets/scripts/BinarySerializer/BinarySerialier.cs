using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class BinarySerialier <T, Y>
{
    public static void SaveData(T t, Y y)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/" + y + ".puq", FileMode.Create);
        bf.Serialize(stream, t);
        stream.Close();
    }

    public static T LoadData(Y y)
    {      
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/" + y + ".puq", FileMode.Open);
            T t = (T)bf.Deserialize(stream);
            stream.Close();
            return t;    
    }

}
