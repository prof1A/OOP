using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

interface ISerializable<T>
{
    string path { get; set; }
    T obj { get; set; }
    bool Serialize();
    T Deserialize();

}
public class XmlSerialization<T> : ISerializable<T>
{
    public string path { get; set; }
    public T obj { get; set; }
    readonly XmlSerializer formatter;
    public XmlSerialization(string path, T obj)
    {
        this.path = path;
        this.obj = obj;

        formatter = new XmlSerializer(typeof(T));

    }


    public bool Serialize()
    {
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, obj);

            return true;
        }

    }
    public T Deserialize()
    {
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            T newObject = (T)formatter.Deserialize(fs);


            return newObject;
        }
    }
}
public class JsonSerialization<T> : ISerializable<T>
{
    public string path { get; set; }
    public T obj { get; set; }
    readonly DataContractJsonSerializer jsonFormatter;
    public JsonSerialization(string path, T obj)
    {
        this.path = path;
        this.obj = obj;

        jsonFormatter = new DataContractJsonSerializer(typeof(T));
    }
    public bool Serialize()
    {
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            jsonFormatter.WriteObject(fs, obj);
            return true;
        }
    }
    public T Deserialize()
    {
        using (FileStream fs = new FileStream(@"D:\BaguetStorage\StorageSerialized.json", FileMode.OpenOrCreate))
        {
            T newObject = (T)jsonFormatter.ReadObject(fs);
            return newObject;
        }
    }
}
