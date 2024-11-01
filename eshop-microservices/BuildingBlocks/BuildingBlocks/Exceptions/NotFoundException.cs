namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        // passing the exception message
        public NotFoundException(string message) : base(message)
        {
            
        }

        // naming and keying for writing a custom exception message
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
            
        }
    }
}
