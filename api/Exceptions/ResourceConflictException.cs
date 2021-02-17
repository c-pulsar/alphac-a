namespace AlphacA.Exceptions
{
  [System.Serializable]
  public class ResourceConflictException : System.Exception
  {
    public ResourceConflictException() { }
    public ResourceConflictException(string message) : base(message) { }
    public ResourceConflictException(string message, System.Exception inner) : base(message, inner) { }
    protected ResourceConflictException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
}