


using System;
using System.Runtime.Serialization;

[Serializable()]
public class SaveDTO : ISerializable
{
    public bool Caso1Resuelto = false;
    public bool Caso2Resuelto = false;
    public bool Caso3Resuelto = false;

    public SaveDTO() { }

    public SaveDTO(SerializationInfo info, StreamingContext ctxt)
    {
        //Get the values from info and assign them to the appropriate properties
        Caso1Resuelto = (bool)info.GetValue("Caso1Resuelto", typeof(bool));
        Caso2Resuelto = (bool)info.GetValue("Caso2Resuelto", typeof(bool));
        Caso3Resuelto = (bool)info.GetValue("Caso3Resuelto", typeof(bool));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Caso1Resuelto",Caso1Resuelto);
        info.AddValue("Caso2Resuelto", Caso1Resuelto);
        info.AddValue("Caso3Resuelto", Caso1Resuelto);
    }
}
