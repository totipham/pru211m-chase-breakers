 using System;
 
 [Serializable]
 public class SerializableVector
 {
     public float x;
     public float y;
     public float z;
 
     public SerializableVector(float x, float y, float z)
     {
         this.x = x;
         this.y = y;
         this.z = z;
     }
 
     // Convert from UnityEngine.Vector3 to SerializableVector3
     public static implicit operator SerializableVector(UnityEngine.Vector3 vector)
     {
         return new SerializableVector(vector.x, vector.y, vector.z);
     }
 
     // Convert from SerializableVector3 to UnityEngine.Vector3
     public static implicit operator UnityEngine.Vector3(SerializableVector vector)
     {
         return new UnityEngine.Vector3(vector.x, vector.y, vector.z);
     }
 }
