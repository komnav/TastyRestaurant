namespace RestaurantLayer.Exceptions
{
    public class ResourceWasNotCreatedException : Exception
    {
        public ResourceWasNotCreatedException(
            string resourceName, 
            params object[] resourceParams) 
            : base($"Resource {resourceName} was not created. Params: {string.Join(',', resourceParams)}")
        {

        }
    }
}
