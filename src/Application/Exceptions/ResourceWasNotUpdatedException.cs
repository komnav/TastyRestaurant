namespace RestaurantLayer.Exceptions
{
    public class ResourceWasNotUpdatedException : Exception
    {
        public ResourceWasNotUpdatedException(
            string resourceName,
            params object[] resourceParams)
            : base($"Resource {resourceName} was not updated. Params: {string.Join(',', resourceParams)}")
        {
        }
    }
}