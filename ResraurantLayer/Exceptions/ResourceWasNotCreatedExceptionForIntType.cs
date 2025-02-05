namespace RestaurantLayer.Exceptions
{
    public class ResourceWasNotCreatedExceptionForIntType : Exception
    {
        public ResourceWasNotCreatedExceptionForIntType(
            int? recourceNumber,
            params object[] resourceParams
            )
            : base($"Resource {recourceNumber} was not created. Params: {string.Join(',', resourceParams)}")
        {

        }
    }
}
